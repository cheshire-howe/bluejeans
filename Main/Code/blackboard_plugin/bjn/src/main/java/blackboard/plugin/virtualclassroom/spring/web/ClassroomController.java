package blackboard.plugin.virtualclassroom.spring.web;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.servlet.ModelAndView;

import blackboard.data.course.Course;
import blackboard.platform.spring.beans.annotations.ContextValue;
import blackboard.plugin.virtualclassroom.dao.MeetingLoader;
import blackboard.plugin.virtualclassroom.dao.MeetingPersister;
import blackboard.plugin.virtualclassroom.model.Meeting;
import blackboard.plugin.virtualclassroom.model.UserRoomSettings;
import blackboard.plugin.virtualclassroom.spring.lib.Connector;
import blackboard.plugin.virtualclassroom.spring.lib.Utils;

@Controller
public class ClassroomController {
	
	private Connector _connector;
	private MeetingLoader _meetingLoader;
	private MeetingPersister _meetingPersister;
	
	public ClassroomController() {
		_connector = new Connector();
		_meetingLoader = new MeetingLoader();
		_meetingPersister = new MeetingPersister();
	}
	
	// GET: /
	@RequestMapping(value = {"", "/index"}, method = RequestMethod.GET)
	public ModelAndView getClasses(@RequestParam("course_id") String bbCourseId) {

		List<Meeting> meetings = _meetingLoader.loadMeetings(bbCourseId);
		List<Meeting> ms = new ArrayList<Meeting>();
		
		for (Meeting meeting : meetings) {
			Meeting m = _connector.getMeeting(meeting.getId().toString());
			ms.add(m);
		}

		ModelAndView mView = new ModelAndView("index");
		mView.addObject("meetings", ms);
		mView.addObject("courseId", bbCourseId);

		return mView;
	}

	// GET: /create
	@RequestMapping(value = "/create", method = RequestMethod.GET)
	public ModelAndView getCreate(@RequestParam("course_id") String bbCourseId, @ContextValue Course course) {
		
		ModelAndView mView = new ModelAndView("create", "command", new Meeting());
		mView.addObject("courseId", bbCourseId);

		return mView;
	}

	// POST: /create
	@RequestMapping(value = "/create", method = RequestMethod.POST)
	public ModelAndView postClass(@RequestParam("course_id") String bbCourseId, @ModelAttribute Meeting meeting) {

		meeting = Utils.convertMeeting(meeting);

		Meeting m = _connector.createMeeting(meeting);
		m.setEmail(meeting.getEmail());

		// save meeting_id and other info to db
		_meetingPersister.saveMeeting(m, bbCourseId);

		ModelAndView mView = new ModelAndView("redirect:/details?course_id=" + bbCourseId + "&meeting_id=" + m.getId());

		return mView;
	}

	// GET: /update?course_id=:course_id&meeting_id=:meeting_id
	@RequestMapping(value = "/update", method = RequestMethod.GET)
	public ModelAndView getUpdate(@RequestParam("course_id") String bbCourseId,
			@RequestParam("meeting_id") String meetingId, @ContextValue Course course) {

		Meeting meeting = _connector.getMeeting(meetingId);

		// Attendees
		String attendees = Utils.serializeAttendees(meeting.getAttendees());

		// Calendar objects for start time, end time
		Map<String, Calendar> times = new HashMap<String, Calendar>();
		Calendar start = Utils.unixToCalendar(meeting.getStart());
		Calendar end = Utils.unixToCalendar(meeting.getEnd());
		times.put("start", start);
		times.put("end", end);

		ModelAndView mView = new ModelAndView("update");

		mView.addObject("meeting", meeting);
		mView.addObject("attendees", attendees);
		mView.addObject("times", times);

		return mView;
	}

	// POST: /update?course_id=:course_id&meeting_id=:meeting_id
	@RequestMapping(value = "/update", method = RequestMethod.POST)
	public ModelAndView postUpdatedClass(@RequestParam("course_id") String bbCourseId,
			@RequestParam("meeting_id") String meetingId, @ModelAttribute Meeting meeting) {

		meeting = Utils.convertMeeting(meeting);
		
		_connector.updateMeeting(meeting, meetingId);

		ModelAndView mView = new ModelAndView("redirect:/details?course_id=" + bbCourseId + "&meeting_id=" + meetingId);

		return mView;
	}
	
	@RequestMapping(value = "/cancelConfirm", method = RequestMethod.GET)
	public ModelAndView deleteClassConfirm(@RequestParam("course_id") String bbCourseId,
										   @RequestParam("meeting_id") String meetingId) {
		
		ModelAndView mView = new ModelAndView("confirm");
		mView.addObject("courseId", bbCourseId);
		mView.addObject("meetingId", meetingId);
		
		return mView;
	}
	
	@RequestMapping(value = "/cancel", method = RequestMethod.GET)
	public ModelAndView deleteClass(@RequestParam("course_id") String bbCourseId,
										   @RequestParam("meeting_id") String meetingId) {
		
		_connector.deleteMeeting(meetingId);
		_meetingPersister.deleteMeeting(meetingId);
		
		ModelAndView mView = new ModelAndView("redirect:/index?course_id=" + bbCourseId);
		
		return mView;
	}

	// GET: /details?course_id=:course_id&meeting_id=:meeting_id
	@RequestMapping(value = "/details", method = RequestMethod.GET)
	public ModelAndView getDetails(@RequestParam("course_id") String bbCourseId,
								   @RequestParam("meeting_id") String meetingId) {
		
		Meeting meeting = _connector.getMeeting(meetingId);

		Map<String, String> recurrenceOptions = new HashMap<String, String>();
		if (meeting.getRecurrencePattern() != null) {
			recurrenceOptions = Utils.getRecurrencePatternValues(meeting.getRecurrencePattern());
		} else {
			recurrenceOptions = null;
		}

		Date nextStart = new Date((long) meeting.getNextStart());
		Date nextEnd = new Date((long) meeting.getNextEnd());
		
		SimpleDateFormat startFormat = new SimpleDateFormat("EEEE MMMM dd yyyy, hh:mm");
		SimpleDateFormat endFormat = new SimpleDateFormat("hh:mm a zz");
		
		Map<String, String> viewValues = new HashMap<String, String>();
		viewValues.put("start", startFormat.format(nextStart));
		viewValues.put("end", endFormat.format(nextEnd));
		
		UserRoomSettings usr = _connector.getUserRoomSettings();
		viewValues.put("moderatorPasscode", usr.getModeratorPasscode());

		ModelAndView mView = new ModelAndView("details");

		mView.addObject("meeting", meeting);
		mView.addObject("rp", recurrenceOptions);
		mView.addObject("viewValues", viewValues);
		mView.addObject("courseId", bbCourseId);

		return mView;
	}
	
	// GET: /videos?course_id=:bbCourseId&meeting_id=:meetingId
	@RequestMapping(value = "/videos", method = RequestMethod.GET)
	public ModelAndView getVideos(@RequestParam("course_id") String bbCourseId,
								  @RequestParam("meeting_id") String meetingId,
								  @RequestParam("numeric_meeting_id") String numericMeetingId) {
		
		String[] videoUrls = _connector.getDownloadUrls(numericMeetingId);
		
		ModelAndView mView = new ModelAndView("videos");
		mView.addObject("videoUrls", videoUrls);
		mView.addObject("bbCourseId", bbCourseId);
		mView.addObject("meetingId", meetingId);
		
		return mView;
	}
	
	// GET: /view
	@RequestMapping(value = "/view", method= RequestMethod.GET)
	public ModelAndView getView() {
		
		ModelAndView mView = new ModelAndView("view");
		
		return mView;
		
	}
}
