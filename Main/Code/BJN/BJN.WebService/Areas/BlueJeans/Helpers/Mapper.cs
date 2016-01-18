using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BJN.Domain.Entities;
using BJN.WebService.Areas.BlueJeans.Models;

namespace BJN.WebService.Areas.BlueJeans.Helpers
{
    public class Mapper
    {
        public static Meeting MapLtiMeetingViewModelToMeeting(LtiMeetingViewModel ltiMeetingViewModel)
        {
            var attendeeList = new List<Attendee>();
            if (!String.IsNullOrEmpty(ltiMeetingViewModel.attendeesString))
            {
                var attendeeArray = Array.ConvertAll(ltiMeetingViewModel.attendeesString.Split(','), a => a.Trim());
                attendeeList = attendeeArray.Select(a => new Attendee() { email = a }).ToList();
            }


            var meeting = new Meeting()
            {
                title = ltiMeetingViewModel.title,
                description = ltiMeetingViewModel.description,
                start = ltiMeetingViewModel.start,
                end = ltiMeetingViewModel.end,
                timezone = "America/Toronto",
                advancedMeetingOptions = new AdvancedMeetingOptions()
                {
                    allowStream = ltiMeetingViewModel.advancedMeetingOptions.allowStream,
                    autoRecord = ltiMeetingViewModel.advancedMeetingOptions.autoRecord,
                    disallowChat = ltiMeetingViewModel.advancedMeetingOptions.disallowChat,
                    encryptionType = /*ltiMeetingViewModel.advancedMeetingOptions.encryptionType*/"NO_ENCRYPTION",
                    moderatorLess = ltiMeetingViewModel.advancedMeetingOptions.moderatorLess,
                    muteParticipantsOnEntry = ltiMeetingViewModel.advancedMeetingOptions.muteParticipantsOnEntry,
                    publishMeeting = ltiMeetingViewModel.advancedMeetingOptions.publishMeeting,
                    showAllAttendeesInMeetingInvite =
                        ltiMeetingViewModel.advancedMeetingOptions.showAllAttendeesInMeetingInvite,
                    videoBestFit = ltiMeetingViewModel.advancedMeetingOptions.videoBestFit
                },
                addAttendeePasscode = ltiMeetingViewModel.addAttendeePasscode,
                deleted = false,
                allow720p = ltiMeetingViewModel.allow720p,
                sequenceNumber = 0,
                endPointType = "LTI",
                endPointVersion = "1.2",
                attendees = attendeeList,
                recurrencePattern = ltiMeetingViewModel.recurrencePattern.recurrenceType == null ? null : new RecurrencePattern()
                {
                    dayOfMonth = ltiMeetingViewModel.recurrencePattern.dayOfMonth,
                    daysOfWeekMask = ltiMeetingViewModel.recurrencePattern.daysOfWeekMask,
                    endDate = ltiMeetingViewModel.recurrencePattern.endDate,
                    frequency = ltiMeetingViewModel.recurrencePattern.frequency,
                    monthOfYear = ltiMeetingViewModel.recurrencePattern.monthOfYear,
                    recurrenceCount = ltiMeetingViewModel.recurrencePattern.recurrenceCount,
                    recurrenceType = ltiMeetingViewModel.recurrencePattern.recurrenceType,
                    weekOfMonth = ltiMeetingViewModel.recurrencePattern.weekOfMonth
                }
            };

            return meeting;
        }
    }
}