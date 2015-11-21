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
	<bbNG:cssFile href="resources/vendor/jquery-ui/themes/smoothness/jquery-ui.min.css"/>
	
	<bbNG:jsFile href="resources/vendor/jquery/dist/jquery.min.js" />
	<bbNG:jsFile href="resources/vendor/jquery-ui/jquery-ui.min.js" />
	
	<bbNG:jsFile href="resources/js/build/video.js" />
	
	<bbNG:pageHeader instructions="Here you can view your recordings.">
        <bbNG:breadcrumbBar environment="COURSE" navItem="course_plugin_manage">
        	<bbNG:breadcrumb title="Virtual Classroom - Home" href="index?course_id=${ bbCourseId }" />
        	<bbNG:breadcrumb title="Virtual Classroom - Details" href="details?course_id=${ bbCourseId }&meeting_id=${ meetingId }" />
            <bbNG:breadcrumb>Virtual Classroom - Recordings</bbNG:breadcrumb>
        </bbNG:breadcrumbBar>
        <bbNG:pageTitleBar iconUrl="/images/ci/icons/tools_u.gif" showTitleBar="true" title="Virtual Classroom - Recordings"/>
    </bbNG:pageHeader>
	
	<div id="accordion">
		<c:forEach var="videoUrl" items="${ videoUrls }">
			<h3>Video</h3>
			<div>
				<p>
					<video controls="controls" width="640" height="360">
						<source src="${ videoUrl }" type="video/mp4"></source>
					</video>
					<p><strong>Download video:</strong> <a href="${ videoUrl }">MP4 format</a></p>
				</p>
			</div>
		</c:forEach>
	</div>
	
</bbNG:genericPage>