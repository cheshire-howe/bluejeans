package blackboard.plugin.virtualclassroom.model;

import java.util.ArrayList;
import java.util.List;

import com.fasterxml.jackson.annotation.JsonInclude;

@JsonInclude(JsonInclude.Include.NON_NULL)
public class Meeting
{
	private Integer id;
	private String title;
	private String description;
	private Long start;
	private Long end;
	private String timezone;
	private RecurrencePattern recurrencePattern;
	private AdvancedMeetingOptions advancedMeetingOptions;
	private Moderator moderator;
	private String numericMeetingId;
	private String attendeePasscode;
	private Boolean addAttendeePasscode;
	private Boolean deleted;
	private Boolean allow720p;
	private Object status;
	private Boolean locked;
	private Integer sequenceNumber;
	private String icsUid;
	private String endPointType;
	private String endPointVersion;
	private List<Attendee> attendees = new ArrayList<Attendee>();
	private Boolean isLargeMeeting;
	private Long created;
	private Long lastModified;
	private Boolean isExpired;
	private First first;
	private Next next;
	private Long nextStart;
	private Long nextEnd;
	private Boolean isPersonalMeeting;
	private String email;

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
	 * @return The title
	 */
	public String getTitle() {
		return title;
	}

	/**
	 * 
	 * @param title
	 *            The title
	 */
	public void setTitle(String title) {
		this.title = title;
	}

	/**
	 * 
	 * @return The description
	 */
	public String getDescription() {
		return description;
	}

	/**
	 * 
	 * @param description
	 *            The description
	 */
	public void setDescription(String description) {
		this.description = description;
	}

	/**
	 * 
	 * @return The start
	 */
	public Long getStart() {
		return start;
	}

	/**
	 * 
	 * @param start
	 *            The start
	 */
	public void setStart(Long start) {
		this.start = start;
	}

	/**
	 * 
	 * @return The end
	 */
	public Long getEnd() {
		return end;
	}

	/**
	 * 
	 * @param end
	 *            The end
	 */
	public void setEnd(Long end) {
		this.end = end;
	}

	/**
	 * 
	 * @return The timezone
	 */
	public String getTimezone() {
		return timezone;
	}

	/**
	 * 
	 * @param timezone
	 *            The timezone
	 */
	public void setTimezone(String timezone) {
		this.timezone = timezone;
	}

	/**
	 * 
	 * @return The recurrencePattern
	 */
	public RecurrencePattern getRecurrencePattern() {
		return recurrencePattern;
	}

	/**
	 * 
	 * @param recurrencePattern
	 *            The recurrencePattern
	 */
	public void setRecurrencePattern(RecurrencePattern recurrencePattern) {
		this.recurrencePattern = recurrencePattern;
	}

	/**
	 * 
	 * @return The advancedMeetingOptions
	 */
	public AdvancedMeetingOptions getAdvancedMeetingOptions() {
		return advancedMeetingOptions;
	}

	/**
	 * 
	 * @param advancedMeetingOptions
	 *            The advancedMeetingOptions
	 */
	public void setAdvancedMeetingOptions(AdvancedMeetingOptions advancedMeetingOptions) {
		this.advancedMeetingOptions = advancedMeetingOptions;
	}

	/**
	 * 
	 * @return The moderator
	 */
	public Moderator getModerator() {
		return moderator;
	}

	/**
	 * 
	 * @param moderator
	 *            The moderator
	 */
	public void setModerator(Moderator moderator) {
		this.moderator = moderator;
	}

	/**
	 * 
	 * @return The numericMeetingId
	 */
	public String getNumericMeetingId() {
		return numericMeetingId;
	}

	/**
	 * 
	 * @param numericMeetingId
	 *            The numericMeetingId
	 */
	public void setNumericMeetingId(String numericMeetingId) {
		this.numericMeetingId = numericMeetingId;
	}

	/**
	 * 
	 * @return The attendeePasscode
	 */
	public String getAttendeePasscode() {
		return attendeePasscode;
	}

	/**
	 * 
	 * @param attendeePasscode
	 *            The attendeePasscode
	 */
	public void setAttendeePasscode(String attendeePasscode) {
		this.attendeePasscode = attendeePasscode;
	}

	/**
	 * 
	 * @return The addAttendeePasscode
	 */
	public Boolean getAddAttendeePasscode() {
		return addAttendeePasscode;
	}

	/**
	 * 
	 * @param addAttendeePasscode
	 *            The addAttendeePasscode
	 */
	public void setAddAttendeePasscode(Boolean addAttendeePasscode) {
		this.addAttendeePasscode = addAttendeePasscode;
	}

	/**
	 * 
	 * @return The deleted
	 */
	public Boolean getDeleted() {
		return deleted;
	}

	/**
	 * 
	 * @param deleted
	 *            The deleted
	 */
	public void setDeleted(Boolean deleted) {
		this.deleted = deleted;
	}

	/**
	 * 
	 * @return The allow720p
	 */
	public Boolean getAllow720p() {
		return allow720p;
	}

	/**
	 * 
	 * @param allow720p
	 *            The allow720p
	 */
	public void setAllow720p(Boolean allow720p) {
		this.allow720p = allow720p;
	}

	/**
	 * 
	 * @return The status
	 */
	public Object getStatus() {
		return status;
	}

	/**
	 * 
	 * @param status
	 *            The status
	 */
	public void setStatus(Object status) {
		this.status = status;
	}

	/**
	 * 
	 * @return The locked
	 */
	public Boolean getLocked() {
		return locked;
	}

	/**
	 * 
	 * @param locked
	 *            The locked
	 */
	public void setLocked(Boolean locked) {
		this.locked = locked;
	}

	/**
	 * 
	 * @return The sequenceNumber
	 */
	public Integer getSequenceNumber() {
		return sequenceNumber;
	}

	/**
	 * 
	 * @param sequenceNumber
	 *            The sequenceNumber
	 */
	public void setSequenceNumber(Integer sequenceNumber) {
		this.sequenceNumber = sequenceNumber;
	}

	/**
	 * 
	 * @return The icsUid
	 */
	public String getIcsUid() {
		return icsUid;
	}

	/**
	 * 
	 * @param icsUid
	 *            The icsUid
	 */
	public void setIcsUid(String icsUid) {
		this.icsUid = icsUid;
	}

	/**
	 * 
	 * @return The endPointType
	 */
	public String getEndPointType() {
		return endPointType;
	}

	/**
	 * 
	 * @param endPointType
	 *            The endPointType
	 */
	public void setEndPointType(String endPointType) {
		this.endPointType = endPointType;
	}

	/**
	 * 
	 * @return The endPointVersion
	 */
	public String getEndPointVersion() {
		return endPointVersion;
	}

	/**
	 * 
	 * @param endPointVersion
	 *            The endPointVersion
	 */
	public void setEndPointVersion(String endPointVersion) {
		this.endPointVersion = endPointVersion;
	}

	/**
	 * 
	 * @return The attendees
	 */
	public List<Attendee> getAttendees() {
		return attendees;
	}

	/**
	 * 
	 * @param attendees
	 *            The attendees
	 */
	public void setAttendees(List<Attendee> attendees) {
		this.attendees = attendees;
	}

	/**
	 * 
	 * @return The isLargeMeeting
	 */
	public Boolean getIsLargeMeeting() {
		return isLargeMeeting;
	}

	/**
	 * 
	 * @param isLargeMeeting
	 *            The isLargeMeeting
	 */
	public void setIsLargeMeeting(Boolean isLargeMeeting) {
		this.isLargeMeeting = isLargeMeeting;
	}

	/**
	 * 
	 * @return The created
	 */
	public Long getCreated() {
		return created;
	}

	/**
	 * 
	 * @param created
	 *            The created
	 */
	public void setCreated(Long created) {
		this.created = created;
	}

	/**
	 * 
	 * @return The lastModified
	 */
	public Long getLastModified() {
		return lastModified;
	}

	/**
	 * 
	 * @param lastModified
	 *            The lastModified
	 */
	public void setLastModified(Long lastModified) {
		this.lastModified = lastModified;
	}

	/**
	 * 
	 * @return The isExpired
	 */
	public Boolean getIsExpired() {
		return isExpired;
	}

	/**
	 * 
	 * @param isExpired
	 *            The isExpired
	 */
	public void setIsExpired(Boolean isExpired) {
		this.isExpired = isExpired;
	}

	/**
	 * 
	 * @return The first
	 */
	public First getFirst() {
		return first;
	}

	/**
	 * 
	 * @param first
	 *            The first
	 */
	public void setFirst(First first) {
		this.first = first;
	}

	/**
	 * 
	 * @return The next
	 */
	public Next getNext() {
		return next;
	}

	/**
	 * 
	 * @param next
	 *            The next
	 */
	public void setNext(Next next) {
		this.next = next;
	}

	/**
	 * 
	 * @return The nextStart
	 */
	public Long getNextStart() {
		return nextStart;
	}

	/**
	 * 
	 * @param nextStart
	 *            The nextStart
	 */
	public void setNextStart(Long nextStart) {
		this.nextStart = nextStart;
	}

	/**
	 * 
	 * @return The nextEnd
	 */
	public Long getNextEnd() {
		return nextEnd;
	}

	/**
	 * 
	 * @param nextEnd
	 *            The nextEnd
	 */
	public void setNextEnd(Long nextEnd) {
		this.nextEnd = nextEnd;
	}

	/**
	 * 
	 * @return The isPersonalMeeting
	 */
	public Boolean getIsPersonalMeeting() {
		return isPersonalMeeting;
	}

	/**
	 * 
	 * @param isPersonalMeeting
	 *            The isPersonalMeeting
	 */
	public void setIsPersonalMeeting(Boolean isPersonalMeeting) {
		this.isPersonalMeeting = isPersonalMeeting;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}
}