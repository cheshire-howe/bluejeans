<%@ page language="java" contentType="text/html" charset="utf-8"
	pageEncoding="utf-8"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01/EN" "http://www.w3.org/TR/html4/strict.dtd">

<%@ taglib uri="/bbNG" prefix="bbNG"%>

<bbNG:includedPage ctxId="ctx">

	<p>Hello Module Type World</p>

	<%
		/** you could get the user from the Context like this
			
			blackboard.data.user.User user = ctx.getUser():
			
			// using the ctxId entry in the tagLib to get the Context for free :-)
			
			**/
	%>

</bbNG:includedPage>