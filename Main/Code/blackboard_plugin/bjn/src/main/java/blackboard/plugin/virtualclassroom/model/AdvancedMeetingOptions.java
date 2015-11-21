package blackboard.plugin.virtualclassroom.model;

import com.fasterxml.jackson.annotation.JsonInclude;

@JsonInclude(JsonInclude.Include.NON_NULL)
public class AdvancedMeetingOptions {
	private Boolean videoBestFit;
	private Boolean publishMeeting;
	private String encryptionType;
	private Boolean moderatorLess;
	private Boolean allowStream;
	private Boolean autoRecord;
	private Boolean disallowChat;
	private Boolean muteParticipantsOnEntry;
	private Boolean showAllAttendeesInMeetingInvite;

	/**
	 * 
	 * @return The videoBestFit
	 */
	public Boolean getVideoBestFit() {
		return videoBestFit;
	}

	/**
	 * 
	 * @param videoBestFit
	 *            The videoBestFit
	 */
	public void setVideoBestFit(Boolean videoBestFit) {
		this.videoBestFit = videoBestFit;
	}

	/**
	 * 
	 * @return The publishMeeting
	 */
	public Boolean getPublishMeeting() {
		return publishMeeting;
	}

	/**
	 * 
	 * @param publishMeeting
	 *            The publishMeeting
	 */
	public void setPublishMeeting(Boolean publishMeeting) {
		this.publishMeeting = publishMeeting;
	}

	/**
	 * 
	 * @return The encryptionType
	 */
	public String getEncryptionType() {
		return encryptionType;
	}

	/**
	 * 
	 * @param encryptionType
	 *            The encryptionType
	 */
	public void setEncryptionType(String encryptionType) {
		this.encryptionType = encryptionType;
	}

	/**
	 * 
	 * @return The moderatorLess
	 */
	public Boolean getModeratorLess() {
		return moderatorLess;
	}

	/**
	 * 
	 * @param moderatorLess
	 *            The moderatorLess
	 */
	public void setModeratorLess(Boolean moderatorLess) {
		this.moderatorLess = moderatorLess;
	}

	/**
	 * 
	 * @return The allowStream
	 */
	public Boolean getAllowStream() {
		return allowStream;
	}

	/**
	 * 
	 * @param allowStream
	 *            The allowStream
	 */
	public void setAllowStream(Boolean allowStream) {
		this.allowStream = allowStream;
	}

	/**
	 * 
	 * @return The autoRecord
	 */
	public Boolean getAutoRecord() {
		return autoRecord;
	}

	/**
	 * 
	 * @param autoRecord
	 *            The autoRecord
	 */
	public void setAutoRecord(Boolean autoRecord) {
		this.autoRecord = autoRecord;
	}

	/**
	 * 
	 * @return The disallowChat
	 */
	public Boolean getDisallowChat() {
		return disallowChat;
	}

	/**
	 * 
	 * @param disallowChat
	 *            The disallowChat
	 */
	public void setDisallowChat(Boolean disallowChat) {
		this.disallowChat = disallowChat;
	}

	/**
	 * 
	 * @return The muteParticipantsOnEntry
	 */
	public Boolean getMuteParticipantsOnEntry() {
		return muteParticipantsOnEntry;
	}

	/**
	 * 
	 * @param muteParticipantsOnEntry
	 *            The muteParticipantsOnEntry
	 */
	public void setMuteParticipantsOnEntry(Boolean muteParticipantsOnEntry) {
		this.muteParticipantsOnEntry = muteParticipantsOnEntry;
	}

	/**
	 * 
	 * @return The showAllAttendeesInMeetingInvite
	 */
	public Boolean getShowAllAttendeesInMeetingInvite() {
		return showAllAttendeesInMeetingInvite;
	}

	/**
	 * 
	 * @param showAllAttendeesInMeetingInvite
	 *            The showAllAttendeesInMeetingInvite
	 */
	public void setShowAllAttendeesInMeetingInvite(Boolean showAllAttendeesInMeetingInvite) {
		this.showAllAttendeesInMeetingInvite = showAllAttendeesInMeetingInvite;
	}
}
