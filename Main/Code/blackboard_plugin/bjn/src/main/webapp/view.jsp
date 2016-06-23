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
	//Connector connector = new Connector();

	List<Meeting> meetings = meetingLoader.loadMeetings(courseId);
	/* List<Meeting> ms = new ArrayList<Meeting>();
	
	for (Meeting meeting : meetings) {
		Meeting m = connector.getMeeting(meeting.getId().toString());
		ms.add(m);
	} */

	pageContext.setAttribute("webapp", b2Context.getPath());
	pageContext.setAttribute("meetings", meetings);
%>
</bbData:context>

<bbNG:includedPage>

	<%-- <bbNG:jsFile href="resources/vendor/moment/min/moment.min.js" /> --%>
	<div>
		<c:forEach var="meeting" items="${ meetings }">
			<h1 class="meeting-title"><a href="${webapp}details?course_id=${ ctx.courseId.externalString }&meeting_id=${ meeting.getId().toString() }">${ meeting.getTitle() }</a></h1>
			<!-- <p><em>Next Class: <script>document.write(moment.unix(${ meeting.getNextStart() } / 1000).format("dddd, MMMM Do YYYY, h:mm a"));</script></em></p> -->
		</c:forEach>
	</div>
	
	<a href="${webapp}create?course_id=${ctx.courseId.externalString}">Schedule Class</a>
	
</bbNG:includedPage>
