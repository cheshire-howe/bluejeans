package blackboard.plugin.virtualclassroom.dao;

import java.sql.Connection;
import java.sql.PreparedStatement;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import blackboard.db.BbDatabase;
import blackboard.db.ConnectionManager;
import blackboard.db.ConnectionNotAvailableException;
import blackboard.persist.impl.SimpleSelectQuery;
import blackboard.plugin.virtualclassroom.entities.MeetingDao;
import blackboard.plugin.virtualclassroom.entities.MeetingEntity;
import blackboard.plugin.virtualclassroom.model.Meeting;

public class MeetingPersister {
	
	//private static final Logger logger = LogManager.getLogger();
	private MeetingDao _meetingDao;
	
	public MeetingPersister() {
		_meetingDao = new MeetingDao();
	}
	
	public boolean saveMeeting(Meeting meeting, String bbCourseId) {
		
		MeetingEntity meetingEntity = new MeetingEntity();
		
		meetingEntity.setTitle(meeting.getTitle());
		meetingEntity.setMeeting_id(meeting.getId());
		meetingEntity.setCourse_id(bbCourseId);
		meetingEntity.setEmail(meeting.getEmail());
		
		try {

			_meetingDao.persist(meetingEntity);
			return true;
			
		} catch (Exception e) {
			return false;
		}
	}
	
	public void deleteMeeting(String meetingId) {
		
		MeetingEntity meetingEntity = _meetingDao.loadMeetingEntityByMeetingId(meetingId);
		
		try {

			_meetingDao.deleteById(meetingEntity.getId());
			
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}