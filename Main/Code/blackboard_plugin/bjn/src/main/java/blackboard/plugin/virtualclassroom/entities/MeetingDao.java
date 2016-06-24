package blackboard.plugin.virtualclassroom.entities;

import java.util.List;

import blackboard.persist.KeyNotFoundException;
import blackboard.persist.PersistenceRuntimeException;
import blackboard.persist.dao.impl.SimpleDAO;
import blackboard.persist.impl.SimpleSelectQuery;
import blackboard.persist.impl.mapping.DbObjectMap;
import blackboard.persist.impl.mapping.annotation.AnnotationMappingFactory;
import blackboard.platform.query.Criteria;

public class MeetingDao extends SimpleDAO<MeetingEntity>
{
	private static final DbObjectMap MEETING_EXT_MAP = AnnotationMappingFactory.getMap(MeetingEntity.class);
	
	public MeetingDao() {
		super(MEETING_EXT_MAP);
	}
	
	public List<MeetingEntity> loadMeetingEntitiesByCourseId(String courseId) {
		SimpleSelectQuery query = new SimpleSelectQuery(this.getDAOSupport().getMap());
		Criteria criteria = query.getCriteria();
		criteria.add(criteria.equal("course_id", courseId));
		
		try {

			return getDAOSupport().loadList(query);
			
		} catch (PersistenceRuntimeException e) {
			// TODO: handle exception
			return null;
		}
	}
	
	public MeetingEntity loadMeetingEntityByMeetingId(String meetingId) {
		return loadMeetingEntityByMeetingId(Integer.parseInt(meetingId));
	}
	
	public MeetingEntity loadMeetingEntityByMeetingId(Integer meetingId) {
		SimpleSelectQuery query = new SimpleSelectQuery(this.getDAOSupport().getMap());
		Criteria criteria = query.getCriteria();
		criteria.add(criteria.equal("meeting_id", meetingId));
		
		try {
			
			return getDAOSupport().load(query);
			
		} catch (KeyNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return null;
		} catch (PersistenceRuntimeException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return null;
		}
	}
}
