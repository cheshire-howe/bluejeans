<!doctype>
<%@page import="
        blackboard.data.navigation.NavigationItem,
        blackboard.persist.navigation.NavigationItemDbLoader,
        blackboard.platform.plugin.PlugInUtil,
        java.util.Map,
        blackboard.data.course.Course,
        blackboard.data.user.User"
%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@taglib prefix="fn" uri="http://java.sun.com/jsp/jstl/functions" %>
<%@taglib uri="/bbNG" prefix="bbNG" %>
<%@taglib uri="/bbData" prefix="bbData" %>

<bbData:context id="ctx">
	<%
		// Get course
		Course course = ctx.getCourse();
	
		// Get current user and role
		User user = ctx.getUser();
		User.SystemRole role = user.getSystemRole();
		boolean isAdmin = false;
		isAdmin = role == User.SystemRole.SYSTEM_ADMIN;
		
		pageContext.setAttribute("course", course);
		pageContext.setAttribute("role", role);
		pageContext.setAttribute("isAdmin", isAdmin);
	%>
</bbData:context>

<bbNG:genericPage>
	<bbNG:cssFile href="resources/vendor/bootstrap/dist/css/grid12.css"/>
	<bbNG:cssFile href="resources/css/custom.css"/>
	
	<bbNG:jsFile href="resources/vendor/jquery/dist/jquery.min.js" />
	
	<bbNG:jsFile href="resources/js/build/details.js" />
	
	<bbNG:jsBlock>
		<script type="text/javascript">
			app.details.init();
		</script>
	</bbNG:jsBlock>
	
	<bbNG:pageHeader instructions="Here are the details of the scheduled class">
        <bbNG:breadcrumbBar environment="COURSE" navItem="course_plugin_manage">
        	<bbNG:breadcrumb title="Virtual Classroom - Home" href="index?course_id=${ courseId }" />
            <bbNG:breadcrumb>Virtual Classroom - Details</bbNG:breadcrumb>
        </bbNG:breadcrumbBar>
        <bbNG:pageTitleBar iconUrl="/images/ci/icons/tools_u.gif" showTitleBar="true" title="Virtual Classroom - Details"/>
    </bbNG:pageHeader>
    
    <h1>${ meeting.title }</h1>
    
    <img src="resources/images/BJN_logowtext@2x.png" class="bjn-branding bjn-logo-header">
    
    <c:if test="${ isAdmin }">
		<div>
			<a href="update?course_id=${ courseId }&meeting_id=${meeting.id}">Edit</a>
			|
			<a href="cancelConfirm?course_id=${ courseId }&meeting_id=${meeting.id}">Delete</a>
		</div>
    </c:if>
	
	<hr>
	
	<c:if test="${ meeting.description!=null }">
		<h4>${ meeting.description }</h4>
	</c:if>
	
	<h2>Time of Class</h2>
	<c:if test="${ meeting.recurrencePattern!=null }">
		<p><em>${ rp.get('when') }</em></p>
	</c:if>
	<p>${ viewValues.get("start") } - ${ viewValues.get("end") }</p>
	
	<h2>Invitees</h2>
	<ul>
	<c:forEach var="attendee" items="${ meeting.attendees }">
		<li>${ attendee.email }</li>
	</c:forEach>
	</ul>
	
	<h2>Location</h2>
	<h3>BlueJeans Video Conference</h3>
	
	<c:choose>
		<c:when test="${ isAdmin }">
			<a href="https://bluejeans.com/${ meeting.numericMeetingId }/${ viewValues.moderatorPasscode }" target="_blank">
				<button class="btn-go">Join Meeting as Moderator</button>
			</a>
		</c:when>
		<c:otherwise>
			<c:choose>
				<c:when test="${ meeting.addAttendeePasscode }"> <!-- TODO: change to meeting passcode check -->
					<a href="https://bluejeans.com/${ meeting.numericMeetingId }/${ meeting.attendeePasscode }" target="_blank">
						<button class="btn-go">Join Meeting as Participant</button>
					</a>
				</c:when>
				<c:otherwise>
					<a href="https://bluejeans.com/${ meeting.numericMeetingId }" target="_blank">
						<button class="btn-go">Join Meeting as Participant</button>
					</a>
				</c:otherwise>
			</c:choose>
		</c:otherwise>
	</c:choose>
	
	<h2>Recordings</h2>
	<a href="videos?course_id=${ courseId }&meeting_id=${ meeting.id }&numeric_meeting_id=${ meeting.numericMeetingId }">
		<button class="btn-go">View recordings from this class</button>
	</a>
	
	<h2 id="meetingInfoTrigger">Meeting Information</h2>
	<div style="display:none;" id="meetingInfo">
		<dl class="meeting-info">
			<dt>Meeting URL</dt>
			<dd>https://bluejeans.com/${ meeting.numericMeetingId }</dd>
			<dt></dt>
			<dd></dd>
		</dl>
	</div>
</bbNG:genericPage>
