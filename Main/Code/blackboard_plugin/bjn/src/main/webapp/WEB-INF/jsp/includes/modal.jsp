<%@page import="
        blackboard.data.navigation.NavigationItem,
        blackboard.persist.navigation.NavigationItemDbLoader,
        blackboard.platform.plugin.PlugInUtil,
        java.util.Map,
        blackboard.base.BbList,
        blackboard.data.user.User,
        blackboard.persist.user.UserDbLoader,
        blackboard.data.course.Course"
%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@taglib prefix="fn" uri="http://java.sun.com/jsp/jstl/functions" %>
<%@taglib uri="/bbNG" prefix="bbNG" %>
<%@taglib uri="/bbData" prefix="bbData" %>

<!-- Modal -->
<div class="modal fade" id="attendeesModal" tabindex="-1" role="dialog" aria-labelledby="attendeesModal" aria-hidden="true" style="display: none;">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalLabel">Invite Students to this Class</h4>
            </div>
            <div id="userlists" class="modal-body">
                <div class="container-fluid">
                    <div class="row-fluid">
                        <div class="col-sm-5">
                            <h4>Invited</h4>
                            <ul style="min-height: 50px;" id="going"  class="sortable-list bjn-well">
	                            
                            </ul>
                        </div>
                        <div class="col-sm-2 all-btns">
                            <div class="row">
                                <div id="deselect-all" class="button-1">&gt;&gt;</div>
                            </div>
                            <div class="row">
                                <div id="select-all" class="button-1">&lt;&lt;</div>
                            </div>
                        </div>
                        <div class="col-sm-5">
                            <h4>Not Invited</h4>
                            <ul style="min-height: 50px;" id="notgoing" class="sortable-list bjn-well"></ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button id="save" type="button" class="button-1">Ok</button>
                <button type="button" class="button-2" data-dismiss="modal">Cancel</button>
            </div>
        </div>
    </div>
</div>