package blackboard.plugin.virtualclassroom.spring.lib;

import java.util.HashMap;
import java.util.Map;

import org.springframework.web.client.RestTemplate;

import blackboard.platform.context.Context;
import blackboard.platform.context.ContextManager;
import blackboard.platform.context.ContextManagerFactory;
import blackboard.plugin.virtualclassroom.model.Meeting;
import blackboard.plugin.virtualclassroom.model.UserRoomSettings;

public class Connector {
	
	private RestTemplate _restTemplate;
	
	public Connector() {
		_restTemplate = new RestTemplate();
	}
	
	// Meetings
	public Meeting createMeeting(Meeting meeting) {
		
		return _restTemplate.postForObject("http://dev.threepointturn.com:8179/api/Meeting?email=" + meeting.getEmail(), meeting,
				Meeting.class);
	}
	
	public Meeting getMeeting(String meetingId) {
		
		ContextManager contextManager = ContextManagerFactory.getInstance();
		Context ctx = contextManager.getContext();
		String currentUserEmail = ctx.getUser().getEmailAddress();
		
		Meeting meeting = _restTemplate.getForObject("http://dev.threepointturn.com:8179/api/Meeting/" + meetingId + "?email=" + currentUserEmail,
				Meeting.class);
		
		return meeting;
	}
	
	public void updateMeeting(Meeting meeting, String meetingId) {

		final String uri = "http://dev.threepointturn.com:8179/api/Meeting/{id}?email=" + meeting.getEmail();

		Map<String, String> params = new HashMap<String, String>();
		params.put("id", meetingId);

		_restTemplate.put(uri, meeting, params);
	}
	
	public void deleteMeeting(String meetingId) {
		
		ContextManager contextManager = ContextManagerFactory.getInstance();
		Context ctx = contextManager.getContext();
		String currentUserEmail = ctx.getUser().getEmailAddress();
		
		final String uri = "http://dev.threepointturn.com:8179/api/Meeting/{id}?email=" + currentUserEmail;
	     
	    Map<String, String> params = new HashMap<String, String>();
	    params.put("id", meetingId);
	     
	    _restTemplate.delete (uri, params);
	}
	
	// UserRoomSettings
	public UserRoomSettings getUserRoomSettings() {
		
		ContextManager contextManager = ContextManagerFactory.getInstance();
		Context ctx = contextManager.getContext();
		String currentUserEmail = ctx.getUser().getEmailAddress();
		
		return _restTemplate.getForObject("http://dev.threepointturn.com:8179/api/User/RoomSettings?email=" + currentUserEmail, UserRoomSettings.class);
	}
	
	// Video Links
	public String[] getDownloadUrls(String numericMeetingId) {
		
		ContextManager contextManager = ContextManagerFactory.getInstance();
		Context ctx = contextManager.getContext();
		String currentUserEmail = ctx.getUser().getEmailAddress();
		
		String[] response = _restTemplate.getForObject("http://dev.threepointturn.com:8179/api/Meeting/" + numericMeetingId + "/GetDownloadUrls?email=" + currentUserEmail, String[].class);
		return response;
	}
}