using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using BJN.Domain.Entities;
using BJN.Domain.Extensions;
using BJN.WebService.Areas.BlueJeans.Models;

namespace BJN.WebService.Areas.BlueJeans.Helpers
{
    public class Utils
    {
        public static Dictionary<string, string> GetRecurrencePatternValues(LtiRecurrencePatternViewModel recurrencePattern)
        {
            var recurrenceOptions = new Dictionary<string, string>();

            if (recurrencePattern.recurrenceType == "DAILY")
            {
                recurrenceOptions.Add("when", "Every Day");
            }
            else if (recurrencePattern.recurrenceType == "WEEKLY")
            {
                if (recurrencePattern.daysOfWeekMask == 62)
                {
                    recurrenceOptions.Add("when", "Weekdays");
                }
                else
                {
                    if (recurrencePattern.frequency == 1)
                    {
                        recurrenceOptions.Add("when", "Every " + DecodeDayMask(recurrencePattern.daysOfWeekMask));
                    }
                    else
                    {
                        recurrenceOptions.Add("when", "Every "
                            + NumberToWords(recurrencePattern.frequency) + " "
                            + DecodeDayMask(recurrencePattern.daysOfWeekMask));
                    }
                }
            }
            else if (recurrencePattern.recurrenceType == "MONTHLY")
            {
                if (recurrencePattern.frequency == 1 && recurrencePattern.daysOfWeekMask != 0)
                {
                    recurrenceOptions.Add("when", "Every "
                        + CapsToWords(recurrencePattern.weekOfMonth) + " "
                        + DecodeDayMask(recurrencePattern.daysOfWeekMask)
                        + " of the month");
                }
                else if (recurrencePattern.frequency != 1)
                {
                    recurrenceOptions.Add("when", "Every "
                                                  + CapsToWords(recurrencePattern.weekOfMonth) + " "
                                                  + DecodeDayMask(recurrencePattern.daysOfWeekMask)
                                                  + " of every "
                                                  + NumberToWords(recurrencePattern.frequency));
                }
                else
                {
                    recurrenceOptions.Add("when", "Every "
                        + NumberToWords(recurrencePattern.dayOfMonth) + " "
                        + " of the month");
                }
            }

            return recurrenceOptions;
        }

        public static string GetClassDurationString(long start, long end)
        {
            var durationString = new StringBuilder();
            var startTime = start.UnixTimestampToDateTime();

            var duration = (end - start)/60/1000;

            durationString.Append(startTime.ToLongDateString());
            durationString.Append(", ");
            durationString.Append(startTime.ToShortTimeString());
            durationString.Append(" / ");
            durationString.Append(duration);
            durationString.Append(" mins");

            return durationString.ToString();
        }

        private static string DecodeDayMask(int mask)
        {
            var result = new StringBuilder();
            var dayMap = new Dictionary<int, string>
            {
                {0, "Saturday"},
                {1, "Friday"},
                {2, "Thursday"},
                {3, "Wednesday"},
                {4, "Tuesday"},
                {5, "Monday"},
                {6, "Sunday"}
            };

            var bin = Convert.ToString(mask, 2).PadLeft(7, '0');
            // var binDigits = Regex.Split(bin, string.Empty);
            var binDigits = bin.Select(x => x.ToString()).ToArray();

            for (int i = 6; i >= 0; i--)
            {
                if (binDigits[i] == "1")
                {
                    result.Append(dayMap[i] + ", ");
                }
            }

            return result.ToString().Substring(0, result.Length - 2);
        }

        private static string NumberToWords(int num)
        {
            switch (num)
            {
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

        private static string CapsToWords(string caps)
        {
            switch (caps)
            {
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
    }
}