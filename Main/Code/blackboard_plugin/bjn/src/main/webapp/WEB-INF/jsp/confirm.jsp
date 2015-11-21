<!doctype>
<%@page import="
        blackboard.data.navigation.NavigationItem,
        blackboard.persist.navigation.NavigationItemDbLoader,
        blackboard.platform.plugin.PlugInUtil,
        java.util.Map,
        blackboard.data.course.Course"
%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@taglib prefix="fn" uri="http://java.sun.com/jsp/jstl/functions" %>
<%@taglib uri="/bbNG" prefix="bbNG" %>
<%@taglib uri="/bbData" prefix="bbData" %>

<bbNG:genericPage>
	<bbNG:cssFile href="resources/vendor/bootstrap/dist/css/grid12.css"/>
	<bbNG:cssFile href="resources/css/custom.css"/>
	
	<bbNG:jsFile href="resources/vendor/jquery/dist/jquery.min.js" />
	
	<bbNG:pageHeader instructions="Here are the details of the scheduled class">
        <bbNG:breadcrumbBar environment="COURSE" navItem="course_plugin_manage">
        	<bbNG:breadcrumb title="Virtual Classroom - Details" href="details?course_id=${ ctx.courseId.externalString }&meeting_id=${ meeting.id }" />
        	<bbNG:breadcrumb title="Virtual Classroom - Home" href="index?course_id=${ ctx.courseId.externalString }" />
            <bbNG:breadcrumb>Virtual Classroom - Details</bbNG:breadcrumb>
        </bbNG:breadcrumbBar>
        <bbNG:pageTitleBar iconUrl="/images/ci/icons/tools_u.gif" showTitleBar="true" title="Virtual Classroom - Details"/>
    </bbNG:pageHeader>
    
	<h1>${ meeting.title }</h1>
	
	<a href="cancel?course_id=${ courseId }&meeting_id=${ meetingId }">
		<button class="btn-go">Are you sure you want to cancel this class?</button>
	</a>
	
</bbNG:genericPage>
