package blackboard.plugin.virtualclassroom.model;

import com.fasterxml.jackson.annotation.JsonInclude;

@JsonInclude(JsonInclude.Include.NON_NULL)
public class Attendee {
	private Integer id;
	private MeetingAttending meeting;
	private String email;
	private Object followupEmailSentDate;

	/**
	 * 
	 * @return The id
	 */
	public Integer getId() {
		return id;
	}

	/**
	 * 
	 * @param id
	 *            The id
	 */
	public void setId(Integer id) {
		this.id = id;
	}

	/**
	 * 
	 * @return The meeting
	 */
	public MeetingAttending getMeeting() {
		return meeting;
	}

	/**
	 * 
	 * @param meeting
	 *            The meeting
	 */
	public void setMeeting(MeetingAttending meeting) {
		this.meeting = meeting;
	}

	/**
	 * 
	 * @return The email
	 */
	public String getEmail() {
		return email;
	}

	/**
	 * 
	 * @param email
	 *            The email
	 */
	public void setEmail(String email) {
		this.email = email;
	}

	/**
	 * 
	 * @return The followupEmailSentDate
	 */
	public Object getFollowupEmailSentDate() {
		return followupEmailSentDate;
	}

	/**
	 * 
	 * @param followupEmailSentDate
	 *            The followupEmailSentDate
	 */
	public void setFollowupEmailSentDate(Object followupEmailSentDate) {
		this.followupEmailSentDate = followupEmailSentDate;
	}
}
