

$(document).ready(function () {
    $("#DiscussionText").jqte();
    $("#DiscussionEditText").jqte();
});
var DiscussionManager = {
    ResetForm: function () {
        $('#meetingDiscussionForm')[0].reset();
       
    },

    SaveDiscussion: function () {
       
        if (!$('#DiscussionText').val()) {
            Message.Warning("Please add your discussion first!");
        }
        //else if (!$('#EmployeeId').val()) {
        //    Message.Warning("Please select your name!");
        //}
        else {
            if (Message.Prompt()) {
                //var employeeId = $('#DiscussionEmployeeId').val();
                var meetingId = $('#_MeetingId').val();
                var jsonParam = {
                    "DiscussionText": $('#DiscussionText').val(),
                    //"EmployeeId": employeeId,
                    "MeetingId": meetingId
                }
             
               // var jsonParam = $('#meetingDiscussionForm').serialize() ;
                var serviceURL = "/Discussion/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
              
            }

            function onSuccess(JsonData) {
                debugger;
                if (JsonData == "0") {
                    Message.Error("Saved");

                } else {
                    Message.Success("save");
                  $(".discussionDiv").empty();
                    var styles =
                        "border-top-left-radius: 5% !important; border-top-right-radius: 5% !important; background-color: lavender !important; ";
                    var margin1 = "margin-left: 1%";
                    var margin4 = "margin-left: 4%";
                    var divText = "";
                  
                    $.each(JsonData,
                        function (key, item) {
                            divText += ' <div  style="' + styles + '"> <div  style="' + margin1 + '">Discussion Added By:' + item.EmployeeName + '</div >' + ' <div  style="' + margin4 + '">' + item.DiscussionText + '</div >' +
                                '<button  type="submit" id="DiscussionId" value="' + item.DiscussionId + '">Edit</button> &nbsp;' +
                                '<button  type="submit" id="DeleteDiscussionId" value="' + item.DiscussionId + '">Delete</button>' +
                                '</div >';
                        });
                   
                    $(".discussionDiv").append(divText);
                    $("#DiscussionText").jqteVal('');
                }
            }

            function onFailed(xhr, status, err) {
                Message.Exception(xhr);
            }
        }
    },

    GetDiscussion: function (id) {
       if (Message.Prompt()) {
                //event.preventDefault(); // <------------------ stop default behaviour of button
                //var element = this;
                $.ajax({
                    url: "/Discussion/GetDiscussions/",
                    type: "GET",
                    data: { discussionId: id},
                    dataType: "json",
                    //traditional: true,
                    //contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var discussionText = data.DiscussionText;
                        var discussionId = data.DiscussionId;
                        $('#myDiscussionEditModal').modal('show');
                        $('#EditDiscussionId').val(discussionId);
                        $("#DiscussionEditText").jqteVal(discussionText) ;
                    },
                    error: function () {
                        alert("An error has occured!!!");
                    }
                });
            }
    },
    EditDiscussion: function () {
        debugger;
        if (!$('#DiscussionEditText').val()) {
            Message.Warning("Please add your discussion first!");
        } else {
            if (Message.Prompt()) {
                var data = {
                    "DiscussionText": $('#DiscussionEditText').val(),
                    "DiscussionId": $('#EditDiscussionId').val(),
                    "MeetingId": $("input[name=MeetingId]").val()

                }
                $.ajax({
                    url: "/Discussion/Edit/",
                    type: "GET",
                    data: data,
                    dataType: "json",
                    success: function (data) {
                        debugger;
                        $('#myDiscussionEditModal').modal('hide');
                        Message.Success("update");
                        $(".discussionDiv").empty();
                        var styles =
                            "border-top-left-radius: 5% !important; border-top-right-radius: 5% !important; background-color: lavender !important; ";
                        var margin1 = "margin-left: 1%";
                        var margin4 = "margin-left: 4%";
                        var divText = "";

                        $.each(data,
                            function (key, item) {
                                divText += ' <div  style="' + styles + '"> <div  style="' + margin1 + '">Discussion By:' + item.EmployeeName + '</div >' + ' <div  style="' + margin4 + '">' + item.DiscussionText + '</div >' +
                                    '<button  type="submit" id="DiscussionId" value="' + item.DiscussionId + '">Edit</button> &nbsp;' +
                                    '<button  type="submit" id="DeleteDiscussionId" value="' + item.DiscussionId + '">Delete</button>' +
                                    '</div >';
                            });

                        $(".discussionDiv").append(divText);
                        $("#DiscussionEditText").jqteVal('');
                    },
                    error: function (message) {
                        alert(message);
                    }
                });
            }

        }
    },
    DeleteDiscussion: function (id) {
        if (Message.Prompt()) {
            $.ajax({
                url: "/Discussion/Delete/",
                type: "GET",
                data: { discussionId: id },
                dataType: "json",
                success: function (data) {
                    $(".discussionDiv").empty();
                    var styles =
                        "border-top-left-radius: 5% !important; border-top-right-radius: 5% !important; background-color: lavender !important; ";
                    var margin1 = "margin-left: 1%";
                    var margin4 = "margin-left: 4%";
                    var divText = "";

                    $.each(data,
                        function (key, item) {
                            divText += ' <div  style="' + styles + '"> <div  style="' + margin1 + '">Discussion Added By:' + item.EmployeeName + '</div >' + ' <div  style="' + margin4 + '">' + item.DiscussionText + '</div >' +
                                '<button  type="submit" id="DiscussionId" value="' + item.DiscussionId + '">Edit</button> &nbsp;' +
                                '<button  type="submit" id="DeleteDiscussionId" value="' + item.DiscussionId + '">Delete</button>' +
                                '</div >';

                        });

                    $(".discussionDiv").append(divText);
                },
                error: function () {
                    alert("An error has occured!!!");
                }
            });
        }
    }
  

}

$(document).on('click', '#saveDiscussionButton', function () {
    DiscussionManager.SaveDiscussion();
});
$(document).on('click', '#DiscussionId', function () {
    debugger;
    var id = $(this).val();;
    DiscussionManager.GetDiscussion(id);
});

$(document).on('click', '#clearDiscussionEditButton', function () {
    $("#DiscussionEditText").jqteVal('');
});
$(document).on('click', '#editDiscussionButton', function () {
   
    DiscussionManager.EditDiscussion();
});
$(document).on('click', '#DeleteDiscussionId', function () {
    debugger;
    var id = $(this).val();;
    DiscussionManager.DeleteDiscussion(id);
});