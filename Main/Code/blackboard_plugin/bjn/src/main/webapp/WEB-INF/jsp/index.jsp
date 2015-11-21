<!doctype>
<%@page import="
        blackboard.data.navigation.NavigationItem,
        blackboard.persist.navigation.NavigationItemDbLoader,
        blackboard.platform.plugin.PlugInUtil,
        java.util.Map,
        java.util.Date,
        blackboard.base.BbList,
        blackboard.data.user.User,
        blackboard.persist.user.UserDbLoader,
        blackboard.data.course.Course"
%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@taglib prefix="fn" uri="http://java.sun.com/jsp/jstl/functions" %>
<%@taglib uri="/bbNG" prefix="bbNG" %>
<%@taglib uri="/bbData" prefix="bbData" %>

<bbNG:genericPage>
	<bbNG:cssFile href="resources/css/custom.css"/>
	
	<bbNG:jsFile href="resources/vendor/moment/min/moment.min.js" />
	
	<bbNG:pageHeader instructions="Here you can view your scheduled video conferences.">
        <bbNG:breadcrumbBar environment="COURSE" navItem="course_plugin_manage">
            <bbNG:breadcrumb>Virtual Classroom - Home</bbNG:breadcrumb>
        </bbNG:breadcrumbBar>
        <bbNG:pageTitleBar iconUrl="/images/ci/icons/tools_u.gif" showTitleBar="true" title="Virtual Classroom - Home"/>
    </bbNG:pageHeader>
	
	<div>
		<c:forEach var="meeting" items="${ meetings }">
			<h1 class="meeting-title"><a href="details?course_id=${ courseId }&meeting_id=${ meeting.getId().toString() }">${ meeting.getTitle() }</a></h1>
			<p><em>Next Class: <script>document.write(moment.unix(${ meeting.getNextStart() } / 1000).format("dddd, MMMM Do YYYY, h:mm a"));</script></em></p>
		</c:forEach>
	</div>
	
	<div>
		<h3><a href='create?course_id=${ courseId }'>Schedule Class</a></h3>
	</div>
    
    <img src="resources/images/BJN_logowtext@2x.png">
	
</bbNG:genericPage>