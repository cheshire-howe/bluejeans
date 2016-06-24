package blackboard.plugin.virtualclassroom.dao;

import java.sql.Connection;
import java.sql.PreparedStatement;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import blackboard.db.BbDatabase;
import blackboard.db.ConnectionManager;
import blackboard.db.ConnectionNotAvailableException;
import blackboard.plugin.virtualclassroom.model.Meeting;

public class MeetingPersister {
	
	//private static final Logger logger = LogManager.getLogger();
	
	public boolean saveMeeting(Meeting meeting, String bbCourseId) {
		boolean saveResult = true;
		StringBuffer queryString = new StringBuffer("");
		ConnectionManager cManager = null;
		Connection conn = null;

		try {
			cManager = BbDatabase.getDefaultInstance().getConnectionManager();
			conn = cManager.getConnection();

			queryString.append("INSERT INTO tp_meeting_table ");
			queryString.append("(title, meeting_id, course_id, email ) ");

			queryString.append(" VALUES ?,?,?,?) ");

			PreparedStatement insertQuery = conn.prepareStatement(queryString.toString());
			insertQuery.setString(1, meeting.getTitle());
			insertQuery.setInt(2, meeting.getId());
			insertQuery.setString(3, bbCourseId);
			insertQuery.setString(4, meeting.getEmail());
			
			int insertResult = insertQuery.executeUpdate();

			if (insertResult != 1) {

				saveResult = false;

			}
			insertQuery.close();

		} catch (java.sql.SQLException sE) {

			saveResult = false;
			//logger.error( sE.getMessage() ) ;

		} catch (ConnectionNotAvailableException cE) {

			saveResult = false;
			//logger.error( cE.getMessage() ) ;

		} finally {
			if (conn != null) {
				cManager.releaseConnection(conn);
			}
		}
		
		return saveResult;
	}
	
	public void deleteMeeting(String meetingId) {
		
		StringBuffer queryString = new StringBuffer("");
        ConnectionManager cManager = null;
        Connection conn = null;

        try {
            cManager = BbDatabase.getDefaultInstance().getConnectionManager();
            conn = cManager.getConnection();

            queryString.append("delete from tp_meeting_table ");
            queryString.append("where meeting_id = ?");

            PreparedStatement deleteQuery = conn.prepareStatement(queryString.toString());
            
            deleteQuery.setInt(1, Integer.parseInt(meetingId));
            
            deleteQuery.executeUpdate();

            deleteQuery.close();

        } catch (java.sql.SQLException sE){
        	
        	//LOGGER.error( sE.getMessage() ) ;

        } catch (ConnectionNotAvailableException cE){
        	
        	//LOGGER.error( cE.getMessage() ) ;
           
        } finally {
            if(conn != null){
                cManager.releaseConnection(conn);
            }
        }
	}
}