package blackboard.plugin.virtualclassroom.dao;

import java.util.ArrayList;
import java.util.List;

import blackboard.plugin.virtualclassroom.entities.MeetingDao;
import blackboard.plugin.virtualclassroom.entities.MeetingEntity;
import blackboard.plugin.virtualclassroom.model.Meeting;

public class MeetingLoader
{
	private MeetingDao _meetingDao;
	
	public MeetingLoader() {
		_meetingDao = new MeetingDao();
	}
	
	public List<Meeting> loadMeetings(String bbCourseId) {
		
		List<MeetingEntity> meetingEntities = _meetingDao.loadMeetingEntitiesByCourseId(bbCourseId);
		List<Meeting> meetings = new ArrayList<Meeting>();
		
		for (MeetingEntity meetingEntity : meetingEntities) {
			
			Meeting m = new Meeting();
			m.setTitle(meetingEntity.getTitle());
			m.setId(meetingEntity.getMeeting_id());
			
			meetings.add(m);
		}
		
		return meetings;
	}
	
	public Meeting loadMeeting(int meetingId) {
		MeetingEntity meetingEntity = _meetingDao.loadMeetingEntityByMeetingId(meetingId);
		Meeting m = new Meeting();
		
		m.setTitle(meetingEntity.getTitle());
		m.setId(meetingEntity.getMeeting_id());
		m.setEmail(meetingEntity.getEmail());
		
		return m;
	}
}