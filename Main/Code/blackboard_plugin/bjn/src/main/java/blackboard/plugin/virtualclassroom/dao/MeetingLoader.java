package blackboard.plugin.virtualclassroom.dao;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.List;

import blackboard.db.BbDatabase;
import blackboard.db.ConnectionManager;
import blackboard.db.ConnectionNotAvailableException;
import blackboard.plugin.virtualclassroom.model.Meeting;

public class MeetingLoader
{
	public List<Meeting> loadMeetings(String bbCourseId) {
		
		List<Meeting> meetings = new ArrayList<Meeting>();

		ConnectionManager cManager = null;
		Connection conn = null;
		StringBuffer queryString = new StringBuffer("");
		PreparedStatement selectQuery = null;

		try {

			cManager = BbDatabase.getDefaultInstance().getConnectionManager();
			conn = cManager.getConnection();

			queryString.append("SELECT title, meeting_id, course_id ");
			queryString.append("FROM ");
			queryString.append("tp_meeting_table ");
			queryString.append("WHERE course_id = ?");
			selectQuery = conn.prepareStatement(queryString.toString(), ResultSet.TYPE_FORWARD_ONLY,
					ResultSet.CONCUR_READ_ONLY);
			selectQuery.setString(1, bbCourseId);
			ResultSet rSet = selectQuery.executeQuery();

			while (rSet.next()) {
				Meeting m = new Meeting();
				m.setTitle(rSet.getString(1));
				m.setId(rSet.getInt(2));
				meetings.add(m);
			}

			rSet.close();

			selectQuery.close();

		} catch (java.sql.SQLException sE) {

			// LOGGER.error( sE.getMessage() ) ;

		} catch (ConnectionNotAvailableException cE) {

			// LOGGER.error( cE.getMessage() ) ;

		} finally {

			if (conn != null) {
				cManager.releaseConnection(conn);
			}

		}
		
		return meetings;
	}
}