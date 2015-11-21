package blackboard.plugin.virtualclassroom.spring.lib;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang3.StringUtils;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

import blackboard.plugin.virtualclassroom.model.AdvancedMeetingOptions;
import blackboard.plugin.virtualclassroom.model.Attendee;
import blackboard.plugin.virtualclassroom.model.Meeting;
import blackboard.plugin.virtualclassroom.model.RecurrencePattern;

public class Utils {
	
	public static Meeting convertMeeting(Meeting meeting) {
		meeting.setEndPointType("BlackBoard");
		meeting.setEndPointVersion("9.1");
		meeting.setTimezone("America/Toronto");
		
		if (meeting.getAddAttendeePasscode() == null) {
			meeting.setAddAttendeePasscode(false);
		}
		
		if (meeting.getAllow720p() == null) {
			meeting.setAllow720p(false);
		}
		
		if (meeting.getIsLargeMeeting() == null) {
			meeting.setIsLargeMeeting(false);
		}
		
		// Advanced Meeting Options
		AdvancedMeetingOptions advancedMeetingOptions = new AdvancedMeetingOptions();
		
		if (meeting.getAdvancedMeetingOptions() != null) {
			advancedMeetingOptions = meeting.getAdvancedMeetingOptions();
		}
		
		if (advancedMeetingOptions.getAllowStream() == null) {
			advancedMeetingOptions.setAllowStream(false);
		}
		
		if (advancedMeetingOptions.getAutoRecord() == null) {
			advancedMeetingOptions.setAutoRecord(false);
		}
		
		if (advancedMeetingOptions.getDisallowChat() == null) {
			advancedMeetingOptions.setDisallowChat(false);
		}
		
		if (advancedMeetingOptions.getModeratorLess() == null) {
			advancedMeetingOptions.setModeratorLess(false);
		}
		
		if (advancedMeetingOptions.getMuteParticipantsOnEntry() == null) {
			advancedMeetingOptions.setMuteParticipantsOnEntry(false);
		}
		
		if (advancedMeetingOptions.getPublishMeeting() == null) {
			advancedMeetingOptions.setPublishMeeting(false);
		}
		
		if (advancedMeetingOptions.getShowAllAttendeesInMeetingInvite() == null) {
			advancedMeetingOptions.setShowAllAttendeesInMeetingInvite(false);
		}
		
		if (advancedMeetingOptions.getVideoBestFit() == null) {
			advancedMeetingOptions.setVideoBestFit(false);
		}
		
		if (advancedMeetingOptions.getEncryptionType() == null) {
			advancedMeetingOptions.setEncryptionType("NO_ENCRYPTION");
		}
		
		meeting.setAdvancedMeetingOptions(advancedMeetingOptions);
		
		// Recurrence Pattern
		RecurrencePattern recurrencePattern = new RecurrencePattern();
		recurrencePattern = meeting.getRecurrencePattern();

		if (recurrencePattern.getRecurrenceType() == "") {
			recurrencePattern.setRecurrenceType("NONE");
			recurrencePattern.setEndDate(null);
			recurrencePattern.setRecurrenceCount(0);
			recurrencePattern.setFrequency(0);
			recurrencePattern.setDaysOfWeekMask(0);
			recurrencePattern.setDayOfMonth(0);
			recurrencePattern.setWeekOfMonth(null);
			recurrencePattern.setMonthOfYear(null);
		}
		
		meeting.setRecurrencePattern(recurrencePattern);
		
		return meeting;
	}
	
	public static Map<String, String> getRecurrencePatternValues(RecurrencePattern recurrencePattern) {
		
		Map<String, String> recurrenceOptions = new HashMap<String, String>();
		
		if (recurrencePattern.getRecurrenceType().equals("DAILY")) {
			recurrenceOptions.put("when", "Every Day");
			
		} else if (recurrencePattern.getRecurrenceType().equals("WEEKLY")) {
			if (recurrencePattern.getDaysOfWeekMask() == 62) {
				recurrenceOptions.put("when", "Weekdays");
			} else {
				if (recurrencePattern.getFrequency() == 1) {
					recurrenceOptions.put("when", "Every " + decodeDayMask(recurrencePattern.getDaysOfWeekMask()));
				} else {
					recurrenceOptions.put("when", "Every "
							+ numberToWords(recurrencePattern.getFrequency()) + " "
							+ decodeDayMask(recurrencePattern.getDaysOfWeekMask()));
				}
			}
		} else if (recurrencePattern.getRecurrenceType().equals("MONTHLY")) {
            if (recurrencePattern.getFrequency() == 1 && recurrencePattern.getDaysOfWeekMask() != 0) {
                recurrenceOptions.put("when", "Every "
                		+ capsToWords(recurrencePattern.getWeekOfMonth()) + " "
                		+ decodeDayMask(recurrencePattern.getDaysOfWeekMask())
                		+ " of the month");
            } else if (recurrencePattern.getFrequency() != 1) {
            	recurrenceOptions.put("when", "Every "
            			+ capsToWords(recurrencePattern.getWeekOfMonth()) + " "
            			+ decodeDayMask(recurrencePattern.getDaysOfWeekMask())
            			+ " of every "
            			+ numberToWords(recurrencePattern.getFrequency()));
            
            } else {
            	recurrenceOptions.put("when", "Every "
                + numberToWords(recurrencePattern.getDayOfMonth()) + " "
                + " of the month");
            }
		}
		
		return recurrenceOptions;
	}
	
	public static String serializeAttendees(List<Attendee> attendees) {
		
		List<String> attending = new ArrayList<String>();

		for (Attendee attendee : attendees) {
			attending.add(attendee.getEmail());
		}

		ObjectMapper objectMapper = new ObjectMapper();

		try {
			return objectMapper.writeValueAsString(attending);
		} catch (JsonProcessingException e) {
			e.printStackTrace();
			return "";
		}
	}
	
	private static String decodeDayMask(Integer mask) {
		StringBuilder result = new StringBuilder();
		Map<Integer, String> dayMap = new HashMap<Integer, String>();
		dayMap.put(0, "Saturday");
		dayMap.put(1, "Friday");
		dayMap.put(2, "Thursday");
		dayMap.put(3, "Wednesday");
		dayMap.put(4, "Tuesday");
		dayMap.put(5, "Monday");
		dayMap.put(6, "Sunday");
		
		String bin = Integer.toBinaryString(mask);// mask = 30 then bin = 0011110
		String padded = StringUtils.leftPad(bin, 7, "0");
		String[] binDigits = padded.split("(?!^)");
		
		for (int i = 6; i >= 0; i--) {
			if (binDigits[i].equals("1")) {
				//result.insert(0, dayMap.get(i) + ", ");
				result.append(dayMap.get(i) + ", ");
			}
		}
		
		return result.substring(0, result.length() - 2);
	}
	
	private static String numberToWords(Integer num) {
		switch (num) {
			case 1:
				return "1st";
			case 2:
				return "2nd";
			case 3:
				return "3rd";
			case 4:
				return "4th";
			case 5:
				return "5th";
			default:
				return "Unknown";
		}
	}
	
	private static String capsToWords(String caps) {
	    switch (caps) {
	        case "FIRST":
	            return "1st";
	        case "SECOND":
	            return "2nd";
	        case "THIRD":
	            return "3rd";
	        case "FOURTH":
	            return "4th";
	        case "FIFTH":
	            return "5th";
            default:
            	return "Unknown";
	    }
	}
	
	/**
	 * Converts Unix time to Calendar instance.
	 */
	public static Calendar unixToCalendar(long unixTime){
	    Calendar calendar = Calendar.getInstance();
	    calendar.setTimeInMillis(unixTime);
	    return calendar;
	}
}
