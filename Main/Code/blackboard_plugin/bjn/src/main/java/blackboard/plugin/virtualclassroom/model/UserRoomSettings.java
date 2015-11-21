package blackboard.plugin.virtualclassroom.model;

import java.util.HashMap;
import java.util.Map;
import javax.annotation.Generated;
import com.fasterxml.jackson.annotation.JsonAnyGetter;
import com.fasterxml.jackson.annotation.JsonAnySetter;
import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonInclude;
import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.annotation.JsonPropertyOrder;

@JsonInclude(JsonInclude.Include.NON_NULL)
@Generated("org.jsonschema2pojo")
@JsonPropertyOrder({ "id", "name", "numericId", "personalMeetingId", "personalMeetingUUId", "moderatorPasscode",
		"participantPasscode", "backgroundImage", "defaultLayout", "welcomeMessage", "originPopId", "allow720p",
		"playAudioAlerts", "showVideoAnimations", "publishMeeting", "encryptionType", "videoBestFit",
		"showAllParticipantsInIcs", "isModeratorLess", "idleTimeout", "disallowChat", "muteParticipantsOnEntry",
		"isLargeMeeting" })
public class UserRoomSettings {

	@JsonProperty("id")
	private Integer id;
	@JsonProperty("name")
	private String name;
	@JsonProperty("numericId")
	private String numericId;
	@JsonProperty("personalMeetingId")
	private Integer personalMeetingId;
	@JsonProperty("personalMeetingUUId")
	private Object personalMeetingUUId;
	@JsonProperty("moderatorPasscode")
	private String moderatorPasscode;
	@JsonProperty("participantPasscode")
	private String participantPasscode;
	@JsonProperty("backgroundImage")
	private String backgroundImage;
	@JsonProperty("defaultLayout")
	private String defaultLayout;
	@JsonProperty("welcomeMessage")
	private String welcomeMessage;
	@JsonProperty("originPopId")
	private Integer originPopId;
	@JsonProperty("allow720p")
	private Object allow720p;
	@JsonProperty("playAudioAlerts")
	private Boolean playAudioAlerts;
	@JsonProperty("showVideoAnimations")
	private Boolean showVideoAnimations;
	@JsonProperty("publishMeeting")
	private Boolean publishMeeting;
	@JsonProperty("encryptionType")
	private String encryptionType;
	@JsonProperty("videoBestFit")
	private Boolean videoBestFit;
	@JsonProperty("showAllParticipantsInIcs")
	private Boolean showAllParticipantsInIcs;
	@JsonProperty("isModeratorLess")
	private Boolean isModeratorLess;
	@JsonProperty("idleTimeout")
	private Integer idleTimeout;
	@JsonProperty("disallowChat")
	private Boolean disallowChat;
	@JsonProperty("muteParticipantsOnEntry")
	private Boolean muteParticipantsOnEntry;
	@JsonProperty("isLargeMeeting")
	private Boolean isLargeMeeting;
	@JsonIgnore
	private Map<String, Object> additionalProperties = new HashMap<String, Object>();

	/**
	 * 
	 * @return The id
	 */
	@JsonProperty("id")
	public Integer getId() {
		return id;
	}

	/**
	 * 
	 * @param id
	 *            The id
	 */
	@JsonProperty("id")
	public void setId(Integer id) {
		this.id = id;
	}

	/**
	 * 
	 * @return The name
	 */
	@JsonProperty("name")
	public String getName() {
		return name;
	}

	/**
	 * 
	 * @param name
	 *            The name
	 */
	@JsonProperty("name")
	public void setName(String name) {
		this.name = name;
	}

	/**
	 * 
	 * @return The numericId
	 */
	@JsonProperty("numericId")
	public String getNumericId() {
		return numericId;
	}

	/**
	 * 
	 * @param numericId
	 *            The numericId
	 */
	@JsonProperty("numericId")
	public void setNumericId(String numericId) {
		this.numericId = numericId;
	}

	/**
	 * 
	 * @return The personalMeetingId
	 */
	@JsonProperty("personalMeetingId")
	public Integer getPersonalMeetingId() {
		return personalMeetingId;
	}

	/**
	 * 
	 * @param personalMeetingId
	 *            The personalMeetingId
	 */
	@JsonProperty("personalMeetingId")
	public void setPersonalMeetingId(Integer personalMeetingId) {
		this.personalMeetingId = personalMeetingId;
	}

	/**
	 * 
	 * @return The personalMeetingUUId
	 */
	@JsonProperty("personalMeetingUUId")
	public Object getPersonalMeetingUUId() {
		return personalMeetingUUId;
	}

	/**
	 * 
	 * @param personalMeetingUUId
	 *            The personalMeetingUUId
	 */
	@JsonProperty("personalMeetingUUId")
	public void setPersonalMeetingUUId(Object personalMeetingUUId) {
		this.personalMeetingUUId = personalMeetingUUId;
	}

	/**
	 * 
	 * @return The moderatorPasscode
	 */
	@JsonProperty("moderatorPasscode")
	public String getModeratorPasscode() {
		return moderatorPasscode;
	}

	/**
	 * 
	 * @param moderatorPasscode
	 *            The moderatorPasscode
	 */
	@JsonProperty("moderatorPasscode")
	public void setModeratorPasscode(String moderatorPasscode) {
		this.moderatorPasscode = moderatorPasscode;
	}

	/**
	 * 
	 * @return The participantPasscode
	 */
	@JsonProperty("participantPasscode")
	public String getParticipantPasscode() {
		return participantPasscode;
	}

	/**
	 * 
	 * @param participantPasscode
	 *            The participantPasscode
	 */
	@JsonProperty("participantPasscode")
	public void setParticipantPasscode(String participantPasscode) {
		this.participantPasscode = participantPasscode;
	}

	/**
	 * 
	 * @return The backgroundImage
	 */
	@JsonProperty("backgroundImage")
	public String getBackgroundImage() {
		return backgroundImage;
	}

	/**
	 * 
	 * @param backgroundImage
	 *            The backgroundImage
	 */
	@JsonProperty("backgroundImage")
	public void setBackgroundImage(String backgroundImage) {
		this.backgroundImage = backgroundImage;
	}

	/**
	 * 
	 * @return The defaultLayout
	 */
	@JsonProperty("defaultLayout")
	public String getDefaultLayout() {
		return defaultLayout;
	}

	/**
	 * 
	 * @param defaultLayout
	 *            The defaultLayout
	 */
	@JsonProperty("defaultLayout")
	public void setDefaultLayout(String defaultLayout) {
		this.defaultLayout = defaultLayout;
	}

	/**
	 * 
	 * @return The welcomeMessage
	 */
	@JsonProperty("welcomeMessage")
	public String getWelcomeMessage() {
		return welcomeMessage;
	}

	/**
	 * 
	 * @param welcomeMessage
	 *            The welcomeMessage
	 */
	@JsonProperty("welcomeMessage")
	public void setWelcomeMessage(String welcomeMessage) {
		this.welcomeMessage = welcomeMessage;
	}

	/**
	 * 
	 * @return The originPopId
	 */
	@JsonProperty("originPopId")
	public Integer getOriginPopId() {
		return originPopId;
	}

	/**
	 * 
	 * @param originPopId
	 *            The originPopId
	 */
	@JsonProperty("originPopId")
	public void setOriginPopId(Integer originPopId) {
		this.originPopId = originPopId;
	}

	/**
	 * 
	 * @return The allow720p
	 */
	@JsonProperty("allow720p")
	public Object getAllow720p() {
		return allow720p;
	}

	/**
	 * 
	 * @param allow720p
	 *            The allow720p
	 */
	@JsonProperty("allow720p")
	public void setAllow720p(Object allow720p) {
		this.allow720p = allow720p;
	}

	/**
	 * 
	 * @return The playAudioAlerts
	 */
	@JsonProperty("playAudioAlerts")
	public Boolean getPlayAudioAlerts() {
		return playAudioAlerts;
	}

	/**
	 * 
	 * @param playAudioAlerts
	 *            The playAudioAlerts
	 */
	@JsonProperty("playAudioAlerts")
	public void setPlayAudioAlerts(Boolean playAudioAlerts) {
		this.playAudioAlerts = playAudioAlerts;
	}

	/**
	 * 
	 * @return The showVideoAnimations
	 */
	@JsonProperty("showVideoAnimations")
	public Boolean getShowVideoAnimations() {
		return showVideoAnimations;
	}

	/**
	 * 
	 * @param showVideoAnimations
	 *            The showVideoAnimations
	 */
	@JsonProperty("showVideoAnimations")
	public void setShowVideoAnimations(Boolean showVideoAnimations) {
		this.showVideoAnimations = showVideoAnimations;
	}

	/**
	 * 
	 * @return The publishMeeting
	 */
	@JsonProperty("publishMeeting")
	public Boolean getPublishMeeting() {
		return publishMeeting;
	}

	/**
	 * 
	 * @param publishMeeting
	 *            The publishMeeting
	 */
	@JsonProperty("publishMeeting")
	public void setPublishMeeting(Boolean publishMeeting) {
		this.publishMeeting = publishMeeting;
	}

	/**
	 * 
	 * @return The encryptionType
	 */
	@JsonProperty("encryptionType")
	public String getEncryptionType() {
		return encryptionType;
	}

	/**
	 * 
	 * @param encryptionType
	 *            The encryptionType
	 */
	@JsonProperty("encryptionType")
	public void setEncryptionType(String encryptionType) {
		this.encryptionType = encryptionType;
	}

	/**
	 * 
	 * @return The videoBestFit
	 */
	@JsonProperty("videoBestFit")
	public Boolean getVideoBestFit() {
		return videoBestFit;
	}

	/**
	 * 
	 * @param videoBestFit
	 *            The videoBestFit
	 */
	@JsonProperty("videoBestFit")
	public void setVideoBestFit(Boolean videoBestFit) {
		this.videoBestFit = videoBestFit;
	}

	/**
	 * 
	 * @return The showAllParticipantsInIcs
	 */
	@JsonProperty("showAllParticipantsInIcs")
	public Boolean getShowAllParticipantsInIcs() {
		return showAllParticipantsInIcs;
	}

	/**
	 * 
	 * @param showAllParticipantsInIcs
	 *            The showAllParticipantsInIcs
	 */
	@JsonProperty("showAllParticipantsInIcs")
	public void setShowAllParticipantsInIcs(Boolean showAllParticipantsInIcs) {
		this.showAllParticipantsInIcs = showAllParticipantsInIcs;
	}

	/**
	 * 
	 * @return The isModeratorLess
	 */
	@JsonProperty("isModeratorLess")
	public Boolean getIsModeratorLess() {
		return isModeratorLess;
	}

	/**
	 * 
	 * @param isModeratorLess
	 *            The isModeratorLess
	 */
	@JsonProperty("isModeratorLess")
	public void setIsModeratorLess(Boolean isModeratorLess) {
		this.isModeratorLess = isModeratorLess;
	}

	/**
	 * 
	 * @return The idleTimeout
	 */
	@JsonProperty("idleTimeout")
	public Integer getIdleTimeout() {
		return idleTimeout;
	}

	/**
	 * 
	 * @param idleTimeout
	 *            The idleTimeout
	 */
	@JsonProperty("idleTimeout")
	public void setIdleTimeout(Integer idleTimeout) {
		this.idleTimeout = idleTimeout;
	}

	/**
	 * 
	 * @return The disallowChat
	 */
	@JsonProperty("disallowChat")
	public Boolean getDisallowChat() {
		return disallowChat;
	}

	/**
	 * 
	 * @param disallowChat
	 *            The disallowChat
	 */
	@JsonProperty("disallowChat")
	public void setDisallowChat(Boolean disallowChat) {
		this.disallowChat = disallowChat;
	}

	/**
	 * 
	 * @return The muteParticipantsOnEntry
	 */
	@JsonProperty("muteParticipantsOnEntry")
	public Boolean getMuteParticipantsOnEntry() {
		return muteParticipantsOnEntry;
	}

	/**
	 * 
	 * @param muteParticipantsOnEntry
	 *            The muteParticipantsOnEntry
	 */
	@JsonProperty("muteParticipantsOnEntry")
	public void setMuteParticipantsOnEntry(Boolean muteParticipantsOnEntry) {
		this.muteParticipantsOnEntry = muteParticipantsOnEntry;
	}

	/**
	 * 
	 * @return The isLargeMeeting
	 */
	@JsonProperty("isLargeMeeting")
	public Boolean getIsLargeMeeting() {
		return isLargeMeeting;
	}

	/**
	 * 
	 * @param isLargeMeeting
	 *            The isLargeMeeting
	 */
	@JsonProperty("isLargeMeeting")
	public void setIsLargeMeeting(Boolean isLargeMeeting) {
		this.isLargeMeeting = isLargeMeeting;
	}

	@JsonAnyGetter
	public Map<String, Object> getAdditionalProperties() {
		return this.additionalProperties;
	}

	@JsonAnySetter
	public void setAdditionalProperty(String name, Object value) {
		this.additionalProperties.put(name, value);
	}

}