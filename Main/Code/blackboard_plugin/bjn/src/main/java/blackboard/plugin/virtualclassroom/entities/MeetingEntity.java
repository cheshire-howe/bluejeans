package blackboard.plugin.virtualclassroom.entities;

import blackboard.data.AbstractIdentifiable;
import blackboard.persist.DataType;
import blackboard.persist.Id;
import blackboard.persist.impl.mapping.annotation.Column;
import blackboard.persist.impl.mapping.annotation.Table;

@Table("tp_meeting_table")
public class MeetingEntity extends AbstractIdentifiable
{
	public static final DataType DATA_TYPE = new DataType(MeetingEntity.class);
	
	@Column({"title"})
	private String title;
	
	@Column({"meeting_id"})
	private Integer meeting_id;
	
	@Column({"course_id"})
	private String course_id;
	
	@Column({"email"})
	private String email;

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public Integer getMeeting_id() {
		return meeting_id;
	}

	public void setMeeting_id(Integer meeting_id) {
		this.meeting_id = meeting_id;
	}

	public String getCourse_id() {
		return course_id;
	}

	public void setCourse_id(String course_id) {
		this.course_id = course_id;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}
}
