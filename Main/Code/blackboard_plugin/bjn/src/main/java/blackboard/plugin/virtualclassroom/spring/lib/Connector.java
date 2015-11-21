package blackboard.plugin.virtualclassroom.spring.lib;

import java.util.HashMap;
import java.util.Map;

import org.springframework.web.client.RestTemplate;

import blackboard.plugin.virtualclassroom.model.Meeting;
import blackboard.plugin.virtualclassroom.model.UserRoomSettings;

public class Connector {
	
	private RestTemplate _restTemplate;
	
	public Connector() {
		_restTemplate = new RestTemplate();
	}
	
	// Meetings
	public Meeting createMeeting(Meeting meeting) {
		
		return _restTemplate.postForObject("http://dev.threepointturn.com:8178/api/Meeting", meeting,
				Meeting.class);
	}
	
	public Meeting getMeeting(String meetingId) {
		
		Meeting meeting = _restTemplate.getForObject("http://dev.threepointturn.com:8178/api/Meeting/" + meetingId,
				Meeting.class);
		
		return meeting;
	}
	
	public void updateMeeting(Meeting meeting, String meetingId) {

		final String uri = "http://dev.threepointturn.com:8178/api/Meeting/{id}";

		Map<String, String> params = new HashMap<String, String>();
		params.put("id", meetingId);

		_restTemplate.put(uri, meeting, params);
	}
	
	public void deleteMeeting(String meetingId) {
		
		final String uri = "http://dev.threepointturn.com:8178/api/Meeting/{id}";
	     
	    Map<String, String> params = new HashMap<String, String>();
	    params.put("id", meetingId);
	     
	    _restTemplate.delete (uri, params);
	}
	
	// UserRoomSettings
	public UserRoomSettings getUserRoomSettings() {
		
		return _restTemplate.getForObject("http://dev.threepointturn.com:8178/api/User/RoomSettings", UserRoomSettings.class);
	}
	
	// Video Links
	public String[] getDownloadUrls(String numericMeetingId) {
		
		String[] response = _restTemplate.getForObject("http://dev.threepointturn.com:8178/api/Meeting/" + numericMeetingId + "/GetDownloadUrls", String[].class);
		return response;
	}
}