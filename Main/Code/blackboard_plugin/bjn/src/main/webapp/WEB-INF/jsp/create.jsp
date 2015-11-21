<!doctype>
<%@page
	import="
        blackboard.data.navigation.NavigationItem,
        blackboard.persist.navigation.NavigationItemDbLoader,
        blackboard.platform.plugin.PlugInUtil,
        java.util.Map,
        java.util.HashMap,
        java.util.ArrayList,
        java.util.Date,
        java.util.Calendar,
        java.util.TimeZone,
        com.fasterxml.jackson.databind.ObjectMapper,
        blackboard.base.BbList,
        blackboard.data.user.User,
        blackboard.persist.user.UserDbLoader,
        blackboard.data.course.Course"
%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%@taglib prefix="fn" uri="http://java.sun.com/jsp/jstl/functions"%>
<%@taglib uri="http://www.springframework.org/tags/form" prefix="form"%>
<%@taglib uri="/bbNG" prefix="bbNG"%>
<%@taglib uri="/bbData" prefix="bbData" %>

<bbData:context id="ctx">
	<%
		// End time one hour after start time by default
		Date now = new Date();
		Date nowPlusOneHour = new Date(now.getTime() + (1000 * 60 * 60));
		Calendar c = Calendar.getInstance();
		c.setTime(nowPlusOneHour);

		// Get Course
		Course course = ctx.getCourse();
		
		// Get all users
		UserDbLoader userLoader = UserDbLoader.Default.getInstance();
		BbList<User> users = userLoader.loadByCourseId(ctx.getCourseId());
		
		// Get user data to fill out form
		HashMap<Integer, HashMap<String, String>> usertable = new HashMap<Integer, HashMap<String, String>>();
		
		int i = 0;
		for (User user : users) {
			HashMap<String, String> info = new HashMap<String, String>();
			
			info.put("name", user.getGivenName() + " " + user.getFamilyName());
			info.put("email", user.getEmailAddress());
			
			usertable.put(i, info);
			i++;
		}
		
		ObjectMapper objectMapper = new ObjectMapper();
		
		String userinfo = objectMapper.writeValueAsString(usertable);

		pageContext.setAttribute("c", c);
		pageContext.setAttribute("users", userinfo);
	%>
</bbData:context>

<bbNG:genericPage>
	<bbNG:cssFile href="resources/vendor/bootstrap/dist/css/bootstrap.min.css"/>
	<bbNG:cssFile href="resources/vendor/bootstrap/dist/css/grid12.css"/>
	<bbNG:cssFile href="resources/css/custom.css"/>
	<bbNG:jsFile href="resources/vendor/jquery/dist/jquery.min.js" />
	<bbNG:jsFile href="resources/vendor/jquery-ui/jquery-ui.min.js" />
	<bbNG:jsFile href="resources/vendor/bootstrap/dist/js/bootstrap.min.js" />
	<bbNG:jsFile href="resources/vendor/moment/min/moment.min.js" />
	
	<bbNG:jsFile href="resources/js/recurring.js" />
	<bbNG:jsFile href="resources/js/build/modal.js" />
	
	<bbNG:jsBlock>
		<script type="text/javascript">
			app.recurring.init();
		    app.modal.init(${ users });
		</script>
	</bbNG:jsBlock>
	
	<bbNG:pageHeader
		instructions="Here you can schedule a video conference with students.">
		<bbNG:breadcrumbBar environment="COURSE" navItem="course_plugin_manage">
        	<bbNG:breadcrumb title="Virtual Classroom - Home" href="index?course_id=${ courseId }" />
			<bbNG:breadcrumb>Virtual Classroom - Schedule a Class</bbNG:breadcrumb>
		</bbNG:breadcrumbBar>
		<bbNG:pageTitleBar iconUrl="/images/ci/icons/tools_u.gif"
			showTitleBar="true" title="Virtual Classroom - Schedule a Class" />
	</bbNG:pageHeader>
	
	<%@include file="includes/modal.jsp" %>
    
    <img src="resources/images/BJN_logowtext@2x.png" class="bjn-branding bjn-logo-header">

	<bbNG:form action="" method="post" nonceId="/create" id="bjnform">
		<bbNG:dataCollection>
			<bbNG:step title="Step One">
				<bbNG:dataElement label="Class Title" isRequired="true" labelFor="title">
					<bbNG:textElement name="title" value="" isRequired="true" title="Title" />
				</bbNG:dataElement>

				<!-- <li>
					<div class="label">
						<label for="custom">Custom</label>
					</div>
					<div class="field">
						<input id="custom" type="text" name="custom"> <span
							class="fieldErrorText"></span>
					</div>
				</li> -->
			</bbNG:step>
			<bbNG:step title="Step Two">
				<bbNG:dataElement label="Start Time" isRequired="true" labelFor="start">
					<bbNG:datePicker baseFieldName="start" isRequired="true" showTime="true" />
				</bbNG:dataElement>
				<bbNG:dataElement label="End Time" isRequired="true" labelFor="end">
					<bbNG:datePicker baseFieldName="end" isRequired="true" showTime="true" dateTimeValue="${ c }" />
				</bbNG:dataElement>
				<input type="hidden" name="start" />
				<input type="hidden" name="end" />
				<bbNG:dataElement label="Repeat" labelFor="repeat" id="use_repeat_meeting">
					<bbNG:checkboxElement name="repeat" value="true" />
				</bbNG:dataElement>
				<!-- Recurring Meeting Options -->
				<div id="reccurrence_options" class="recurrence_options_container"
					style="display: none;">
					<hr />
					<div class="meetingUseRepeatMeeting_error"></div>
					<div class="fieldWrapper" id="div_repeat_meeting_type">
						<div class="repeatMeetingType_l">
							<label for="id_repeat_meeting">Repeats</label>:
						</div>
						<div id="repeatMeetingType">
							<select name="repeat_meeting" id="id_repeat_meeting">
								<option value="DAILY" selected="selected">Daily</option>
								<option value="WEEKLY">Weekly</option>
								<option value="MONTHLY">Monthly</option>
							</select> <span id="frequency_monthly_weekly" style="display: none;">
								every &nbsp;<input name="repeat_meeting_frequency" value="1"
								maxlength="2" type="text" id="id_repeat_meeting_frequency"
								size="2" aria-label="Weekly Repeat Frequency">&nbsp; <span
								id="frequency_unit"></span>

							</span>
						</div>
					</div>
					<div class="clear"></div>

					<div class="fieldWrapper" id="div_daily_options"
						style="display: block;">
						<div class="dailyOptions_l">&nbsp;</div>
						<div id="dailyOptions">
							<input type="radio"
								id="id_repeat_meeting_sub_options_daily_weekdays"
								name="repeat_meeting_sub_options_daily_weekdays" value="True">
							<label for="id_repeat_meeting_sub_options_daily_weekdays">Weekdays</label>
							<input type="radio"
								id="id_repeat_meeting_sub_options_daily_weekdays_1"
								name="repeat_meeting_sub_options_daily_weekdays"
								checked="checked" value="False"> <label
								for="id_repeat_meeting_sub_options_daily_weekdays_1">every</label>
							<input name="repeat_meeting_frequency_daily" value="1"
								maxlength="3" type="text" id="id_repeat_meeting_frequency_daily"
								size="3" aria-label="Daily Repeat Frequency">&nbsp;days

						</div>
					</div>
					<div class="clear"></div>

					<div class="fieldWrapper" id="div_weekly_options"
						style="display: none;">
						<div class="weeklyOptions_1">
							<label for="id_repeat_meeting_day_of_week_weekly_0">Repeats
								On</label>:
						</div>
						<div id="weeklyOptions">
							<ul>
								<li><label for="id_repeat_meeting_day_of_week_weekly_0"><input
										type="checkbox" name="repeat_meeting_day_of_week_weekly"
										value="2" id="id_repeat_meeting_day_of_week_weekly_0">
										Monday</label></li>
								<li><label for="id_repeat_meeting_day_of_week_weekly_1"><input
										type="checkbox" name="repeat_meeting_day_of_week_weekly"
										value="4" id="id_repeat_meeting_day_of_week_weekly_1">
										Tuesday</label></li>
								<li><label for="id_repeat_meeting_day_of_week_weekly_2"><input
										type="checkbox" name="repeat_meeting_day_of_week_weekly"
										value="8" id="id_repeat_meeting_day_of_week_weekly_2">
										Wednesday</label></li>
								<li><label for="id_repeat_meeting_day_of_week_weekly_3"><input
										type="checkbox" name="repeat_meeting_day_of_week_weekly"
										value="16" id="id_repeat_meeting_day_of_week_weekly_3">
										Thursday</label></li>
								<li><label for="id_repeat_meeting_day_of_week_weekly_4"><input
										type="checkbox" name="repeat_meeting_day_of_week_weekly"
										value="32" id="id_repeat_meeting_day_of_week_weekly_4">
										Friday</label></li>
								<li><label for="id_repeat_meeting_day_of_week_weekly_5"><input
										type="checkbox" name="repeat_meeting_day_of_week_weekly"
										value="64" id="id_repeat_meeting_day_of_week_weekly_5">
										Saturday</label></li>
								<li><label for="id_repeat_meeting_day_of_week_weekly_6"><input
										type="checkbox" name="repeat_meeting_day_of_week_weekly"
										value="1" id="id_repeat_meeting_day_of_week_weekly_6">
										Sunday</label></li>
							</ul>
						</div>
					</div>
					<div class="clear"></div>

					<div class="fieldWrapper" id="div_monthly_options"
						style="display: none;">
						<div class="monthlyOptions_1">
							<label for="id_repeat_meeting_week_of_month">On</label>:
						</div>
						<div id="monthlyOptions">
							<select name="repeat_meeting_week_of_month"
								id="id_repeat_meeting_week_of_month">
								<option value="0">Day</option>
								<option value="1">First</option>
								<option value="2">Second</option>
								<option value="3">Third</option>
								<option value="4">Fourth</option>
								<option value="5">Last</option>
							</select> <input type="text" name="repeat_meeting_day_of_month" value="27"
								id="id_repeat_meeting_day_of_month" aria-label=""
								style="display: none;"><span id="day_of_month">27</span>
							<select name="repeat_meeting_day_of_week_monthly"
								id="id_repeat_meeting_day_of_week_monthly"
								aria-label="Repeat on Day of Week" style="display: none;">
								<option value="1">Sunday</option>
								<option value="2">Monday</option>
								<option value="4">Tuesday</option>
								<option value="8">Wednesday</option>
								<option value="16">Thursday</option>
								<option value="32">Friday</option>
								<option value="64">Saturday</option>
							</select>
						</div>
					</div>
					<div class="clear"></div>
					<hr />
					<div id="div_recurrence_ending">
						<div class="fieldWrapper" id="div_recurrence_ending_1">
							<div class="recurrenceEnding_1">
								<label for="id_recurssion_ending_0">Ends</label>:
							</div>
							<div id="recurrenceEnding_1">
								<input id="id_recurssion_ending_0" type="radio"
									name="recurssion_ending" value="NEVER" checked="checked">
								<label for="id_recurssion_ending_0">Never</label>
							</div>
						</div>
						<div class="clear"></div>
	
						<div class="fieldWrapper" id="div_recurrence_ending_2">
							<div class="recurrenceEnding_2">&nbsp;</div>
							<div id="recurrenceEnding_2">
								<input id="id_recurssion_ending_1" type="radio"
									name="recurssion_ending" value="OCCURRENCES"> <label
									for="id_recurssion_ending_1">After</label> <input
									name="recurssion_ending_sub_options_occurrences" value="2"
									maxlength="3" type="text"
									id="id_recurssion_ending_sub_options_occurrences" size="3"
									aria-label="Number of Ocurrences" disabled="">
								occurrences
	
							</div>
						</div>
						<div class="clear"></div>
	
						<div class="fieldWrapper" id="div_recurrence_ending_3">
							<div class="recurrenceEnding_3">&nbsp;</div>
							<div id="recurrenceEnding_3">
								<input id="id_recurssion_ending_2" type="radio"
									name="recurssion_ending" value="ON"> <label
									for="id_recurssion_ending_2">On</label>
								<bbNG:dataElement>
									<bbNG:datePicker baseFieldName="endDate" isRequired="false" showTime="false" />
								</bbNG:dataElement>
								<input type="hidden" name="endDate" />
								<!-- <img class="ui-datepicker-trigger" src="/media/images/calendar.gif"
									alt="..." title="..." style="opacity: 0.5; cursor: default;"> -->
	
							</div>
						</div>
					</div>
					<div class="clear"></div>
				</div>
				<input type="hidden" name="recurrencePattern.recurrenceType"/>
				<input type="hidden" name="recurrencePattern.endDate"/>
				<input type="hidden" name="recurrencePattern.recurrenceCount"/>
				<input type="hidden" name="recurrencePattern.frequency"/>
				<input type="hidden" name="recurrencePattern.daysOfWeekMask"/>
				<input type="hidden" name="recurrencePattern.dayOfMonth"/>
				<input type="hidden" name="recurrencePattern.weekOfMonth"/>
				<input type="hidden" name="recurrencePattern.monthOfYear"/>
			</bbNG:step>
			<bbNG:step title="Step Three">
				<bbNG:dataElement label="Add Participant Passcode"
					labelFor="addAttendeePasscode">
					<bbNG:checkboxElement name="addAttendeePasscode" value="true" />
				</bbNG:dataElement>
			</bbNG:step>
			<bbNG:step title="Step Four">
				<!-- Button trigger modal -->
				<div class="bjn button-2" id="modal-toggle">
					Invite Participants
				</div>
				<div id="attendees"></div>
			</bbNG:step>
			<bbNG:step title="Step Five">
				<bbNG:dataElement label="Message" isRequired="true"
					labelFor="description">
					<bbNG:textbox name="description" />
				</bbNG:dataElement>
				<input type="hidden" name="description" />
				<bbNG:dataElement label="Moderator-less Class"
					labelFor="moderatorLess">
					<bbNG:checkboxElement name="advancedMeetingOptions.moderatorLess"
						value="true" />
				</bbNG:dataElement>
				<bbNG:dataElement label="Enable Auto-Recording"
					labelFor="autoRecord">
					<bbNG:checkboxElement name="advancedMeetingOptions.autoRecord"
						value="true" />
				</bbNG:dataElement>
				<bbNG:dataElement label="Disable Chat Messaging"
					labelFor="disallowChat">
					<bbNG:checkboxElement name="advancedMeetingOptions.disallowChat"
						value="true" />
				</bbNG:dataElement>
				<bbNG:dataElement label="Silent Participant Entry Mode"
					labelFor="muteParticipantsOnEntry">
					<bbNG:checkboxElement
						name="advancedMeetingOptions.muteParticipantsOnEntry" value="true" />
				</bbNG:dataElement>
			</bbNG:step>
			<bbNG:stepSubmit />
		</bbNG:dataCollection>
	</bbNG:form>
</bbNG:genericPage>
