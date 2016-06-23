<%@page import="blackboard.portal.external.*, com.spvsoftwareproducts.blackboard.utils.B2Context" errorPage="/error.jsp"%>
<%@ taglib uri="/bbData" prefix="bbData"%>
<%@ taglib uri="/bbNG" prefix="bbNG"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>

<%
	B2Context b2context = new B2Context(request);
	pageContext.setAttribute("webapp", b2context.getPath());
%>

<c:url var="gotoipeer" value="/module/gotoipeer?redirect=/home/index" context="${webapp}"/>

<p>${gotoipeer}</p>