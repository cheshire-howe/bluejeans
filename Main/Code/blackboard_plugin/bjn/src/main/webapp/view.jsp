<%@page import="blackboard.data.course.CourseMembership.Role"%>
<%@page import="blackboard.data.course.CourseMembership"%>
<%@page import="blackboard.data.course.Course,
				blackboard.portal.external.*,
				blackboard.plugin.virtualclassroom.spring.lib.Connector,
				blackboard.plugin.virtualclassroom.dao.MeetingLoader,
				blackboard.plugin.virtualclassroom.model.Meeting,
				java.util.List,
				java.util.ArrayList,
				com.spvsoftwareproducts.blackboard.utils.B2Context"%>
<%@ taglib uri="/bbData" prefix="bbData"%>
<%@ taglib uri="/bbNG" prefix="bbNG"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>

<bbData:context id="ctx">
	<%
		B2Context b2Context = new B2Context(request);
	
		String courseId = b2Context.getRequestParameter("course_id", "");
	
		MeetingLoader meetingLoader = new MeetingLoader();
	
		List<Meeting> meetings = meetingLoader.loadMeetings(courseId);
		
		// Get course
		Course course = ctx.getCourse();
		
		CourseMembership cm = ctx.getCourseMembership();
		Role cRole = cm.getRole();
		boolean isInstructor = cRole == CourseMembership.Role.INSTRUCTOR;
	
		pageContext.setAttribute("course", course);
		pageContext.setAttribute("isInstructor", isInstructor);
	
		pageContext.setAttribute("webapp", b2Context.getPath());
		pageContext.setAttribute("meetings", meetings);
	%>
</bbData:context>

<bbNG:includedPage>
	<div>
		<h4>Classes for this Course</h4>
	</div>

	<div>
		<c:forEach var="meeting" items="${ meetings }">
			<h1 class="meeting-title"><a href="${webapp}details?course_id=${ ctx.courseId.externalString }&meeting_id=${ meeting.getId().toString() }">${ meeting.getTitle() }</a></h1>
			<!-- <p><em>Next Class: <script>document.write(moment.unix(${ meeting.getNextStart() } / 1000).format("dddd, MMMM Do YYYY, h:mm a"));</script></em></p> -->
		</c:forEach>
	</div>
	
	<c:choose>
		<c:when test="${ isInstructor }">
			<a href="${webapp}create?course_id=${ctx.courseId.externalString}">Schedule Class</a>
		</c:when>
	</c:choose>
	
</bbNG:includedPage>
