var _id = null;
var _presentVenueId = null;
var _presentMeetingCallerId = null;
var dTable = null;
var n;
var activeTab = null;
var isVenueAvailable = false;
var isMeetingCallerAvailable = false;
$(document).ready(function () {
    //
    $('#allmeetings').addClass('active');
    activeTab = '#allmeetings';
    $('input[data-type=date]').datepicker({
        dateFormat: "dd-M-yy",
        changeMonth: true,
        changeYear: true,
        //yearRange: "-2:+0",
        minDate: 0,
        maxDate: "+12M +10D"
    });

    Manager.GetDataForTable(0);

    //Manager.LoadEmployeeDDL();

});
var Manager = {
    GetParameterByName : function (name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    },
    ResetForm: function () {
        $('#meetingForm')[0].reset();
        $('#presentVenueName').text("");
        $("div.presentVenueClass").css("display", "none");

        $('#presentMeetingCallerName').text("");
        $("div.presentMeetingCallerClass").css("display", "none");
       
       $('#myModalLabel').text('Create Meeting');
        $('#loadVenueDropDownButton').text('Load Venue');
        $("#loadVenueDropDownButton").css("margin-right", "5px");
        $("#loadVenueDropDownButton").css("width", "100px");

        $('#VenueId').empty();
        $("div.abc").css("display", "none");
        //$('#editButton').prop('id', 'saveButton');
        //$('#saveButton').text('Update');
        //$('#saveButton').addClass('btn-success');
        //$('#saveButton').removeClass('btn-warning');
        $('#loadMeetingCallerDropDownButton').text('Load Meeting Caller');
        $("#loadMeetingCallerDropDownButton").css("margin-right", "5px");
        $("#loadMeetingCallerDropDownButton").css("width", "200px");

        $('#EmployeeId').empty();
        $("div.MeetingCaller").css("display", "none");
    },

    SaveMeeting: function () {
        debugger;
        if (!$('#meetingNameTxt').val()) {
            Message.Warning("Meeting name is required");
        }
        else if (!$('#beginningdateTxt').val()) {
            Message.Warning("Beginning Date is required!");
        }
        else if (!$('#beginningTimeTxt').val()) {
            Message.Warning("Beginning Time is required!");
        }

        else if (!$('#enddateTxt').val()) {
            Message.Warning("End Date is required");
        }
        else if (!$('#endTimeTxt').val()) {
            Message.Warning("End Time is required");
        }
        else if ($('#EmployeeId').val()<1) {
            Message.Warning("Meeting Caller is required");
        }
        else {
            var startDate = moment($("#beginningdateTxt").val());
            var endDate = moment($("#enddateTxt").val());
            if (startDate > endDate) {
                Message.Warning("End Date Should Be bigger than Start Date");
            }
            else {
                if (Message.Prompt()) {
                    var jsonParam = $('#meetingForm').serialize();
                    var serviceURL = "/Meeting/Create/ ";
                    AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
                }
            }

            function onSuccess(JsonData) {
                if (JsonData == "0") {
                    Message.Error("Saved");

                } else {
                    Manager.ResetForm();
                    $('#myModal').modal('hide');
                    Message.Success("save");
                    n = 1;
                    Manager.GetDataForTable(1);

                }
            }

            function onFailed(xhr, status, err) {
               Message.Warning(xhr.statusText);
                //Message.Error("Rejectd");
            }
        }
    },
    CheckVenueAvailablity: function (meetingId,presentVenueId,beginningTime, endTime) {
        debugger;
        $.ajax({
            url: '/Meeting/CheckVenueAvailable/',
            type: "GET",
            datatype: "Json",
            cache: false,
            async: false, 
            data: { meetingId, presentVenueId, beginningTime, endTime },
            success: function (jsonData) {
                //return jsonData;
                isVenueAvailable = jsonData;
            },
            error: function () {

            }
        });
    },
    
    CheckMeetingCallerAvailablity: function (meetingId,presentMeetingCallerId, beginningTime, endTime) {
        debugger;
        $.ajax({
            url: '/Meeting/CheckMeetingCallerAvailablity/',
            type: "GET",
            datatype: "Json",
            cache: false,
            async: false,
            data: { meetingId,presentMeetingCallerId, beginningTime, endTime },
            success: function (jsonData) {
                //return jsonData;
                isMeetingCallerAvailable = jsonData;
            },
            error: function () {

            }
        });
    },
   

    EditMeeting: function (id, presentVenueId, presentMeetingCallerId) {
        debugger;
        if (!$('#beginningdateTxt').val()) {
            Message.Warning("Beginning Date is required!");
        }
        else if (!$('#beginningTimeTxt').val()) {
            Message.Warning("Beginning Time is required!");
        }

        else if (!$('#enddateTxt').val()) {
            Message.Warning("End Date is required");
        }
        else if (!$('#endTimeTxt').val()) {
            Message.Warning("End Time is required");
        }
        else {

            var startDate = moment($("#beginningdateTxt").val());
            var endDate = moment($("#enddateTxt").val());

            beginningTime = new Date($('#beginningdateTxt').val() + ' ' + $('#beginningTimeTxt').val());
            endTime = new Date($('#enddateTxt').val() + ' ' + $('#endTimeTxt').val());

            if (startDate > endDate) {
                Message.Warning("End Date Should Be bigger than Start Date");
            }
            else {
                debugger;
                var bt1 = new Date(da._d);
                var bt2 = beginningTime;
                var et1 = new Date(dae._d);
                var et2 = endTime;
                var meetingId = id;
                if (bt1.getTime() !== bt2.getTime() && et1.getTime() !== et2.getTime() )
                {

                    Manager.CheckVenueAvailablity(meetingId,presentVenueId, beginningTime.toUTCString(), endTime.toUTCString());
                    Manager.CheckMeetingCallerAvailablity(meetingId,presentMeetingCallerId, beginningTime.toUTCString(), endTime.toUTCString());
                    var venueId = $("#VenueId").val();
                    var meetingCallerId = $("#EmployeeId").val();

                    if (venueId < 1 && meetingCallerId < 1) {
                        if (isVenueAvailable == false && isMeetingCallerAvailable == false) {
                            Message.Warning("Please change venue & meeting caller. ");
                        }
                        else if (isVenueAvailable == true && isMeetingCallerAvailable == true) {
                            if (Message.Prompt()) {
                                PostUpdate();
                            }
                        }
                    }
                    else if (venueId < 1 && isVenueAvailable == false) {
                       
                            Message.Warning("Present venue is not available. Please change venue.");
                        
                    }
                    else if (meetingCallerId < 1 && isMeetingCallerAvailable == false) {
                       
                            Message.Warning("Please change meeting caller.");
                        
                    }
                    else if (Message.Prompt()) {
                        PostUpdate();
                    }
                }
                else if (Message.Prompt()) {
                    PostUpdate();
                }
                
               
            }
            function PostUpdate() {
                debugger;
                var jsonParam = $('#meetingForm').serialize() + '&MeetingId=' + id + '&presentVenueId=' + presentVenueId + '&presentMeetingCallerId=' + presentMeetingCallerId;
                var serviceURL = "/Meeting/Edit/ ";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }
            function onSuccess(JsonData) {
                debugger;
                if (JsonData == "0") {
                    Message.Error("update");
                } else {
                    Manager.ResetForm();
                    $('#myModal').modal('hide');
                    Message.Success("update");
                    $('#allmeetings').addClass('active');
                    $('#nextWeekMeetings').removeClass('active');
                    $('#thisWeekMeetings').removeClass('active');
                    n = 1;
                    if (activeTab == '#allmeetings') {
                        Manager.GetDataForTable(1);
                    }
                    if (activeTab == '#thisWeekMeeting') {
                        Manager.MeetingOfThisWeek();
                    }
                   
                    if (activeTab == '#nextWeekMeetings') {
                        Manager.MeetingOfNextWeek();
                    }
                }
            }

            function onFailed(xhr, status, err) {
                Message.Warning(xhr.statusText);
                //Message.Exception(xhr);
            }
        }

    },

    DeleteMeeting: function (id) {
        if (Message.Prompt()) {
            var jsonParam = '';
            var serviceURL = "/Meeting/Delete/" + id;
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }

        function onSuccess(JsonData) {
            if (JsonData == 0) {
                Message.Error("delete");
            } else {
                Message.Success("delete");
                n = 1;
                Manager.GetDataForTable(1);
            }
        }

        function onFailed(xhr, status, err) {
            Message.Warning(xhr.statusText);
           // Message.Exception(xhr);
        }
    },

    LoadEmployeeDDL: function (beginningTime, endTime) {
        debugger;
        $.ajax({
            url: '/Meeting/GetAllEmployees/',
            type: "GET",
            datatype: "Json",
            cache: false,
            data: { beginningTime, endTime },
            success: function (jsonData) {
                $('#EmployeeId').empty();
                $('#EmployeeId').append('<option value="0" selected ="Selected">Select Employee </option>');
                $.each(jsonData,
                    function (key, value) {
                        $('#EmployeeId').append(
                            '<option value="' + value.EmployeeId + '">' + value.EmployeeName + '</option>'
                        );
                    });

            },
            error: function () {

            }
        });
    },

    LoadVenueDDL: function (beginningTime, endTime) {
        $.ajax({
            url: '/Meeting/GetVenues/',
            type: "GET",
            datatype: "Json",
            cache: false,
            data: { beginningTime, endTime },
            success: function (jsonData) {
                $('#VenueId').empty();
                $('#VenueId').append('<option value="0" selected ="Selected">Select Venue </option>');
                $.each(jsonData,
                    function (key, value) {
                        $('#VenueId').append(
                            '<option value="' + value.VenueId + '">' + value.VenueName + '</option>'
                        );
                    });

            },
            error: function () {

            }
        });
    },


    LoadMembers: function (meetingId) {
        window.location.href = '/MeetingMembers/AddMembers/?meetingId=' + meetingId;

    },
    MeetingDetails: function (meetingId) {
        window.location.href = '/Meeting/MeetingDetails/?meetingId=' + meetingId;

    },

    MeetingOfThisWeek: function () {
        n = 1;
        var jsonParam = '';
        var serviceURL = "/Meeting/CurrentWeekMeeting/";
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            console.log('OK');

            $('#allmeetings').removeClass('active');
            $('#nextWeekMeetings').removeClass('active');
            $('#thisWeekMeetings').addClass('active');
            Manager.LoadDataTable(jsonData, 1);
        }

        function onFailed(xhr, status, err) {
            //Message.Exception(xhr);
            Message.Warning(xhr.statusText);
        }
    },

    MeetingOfNextWeek: function () {
        n = 1;
        var jsonParam = '';
        var serviceURL = "/Meeting/NextWeekMeeting/";
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            console.log('OK');
            $('#nextWeekMeetings').addClass('active');
            $('#thisWeekMeetings').removeClass('active');
            $('#allmeetings').removeClass('active');
            Manager.LoadDataTable(jsonData, 1);
        }

        function onFailed(xhr, status, err) {
            //Message.Exception(xhr);
            Message.Warning(xhr.statusText);
        }
    },

    AllMeeting: function () {
         n = 1;
        var jsonParam = '';
        var serviceURL = "/Meeting/Index/";
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            console.log('OK');
            Manager.LoadDataTable(jsonData, 1);
        }

        function onFailed(xhr, status, err) {
            //Message.Exception(xhr);
            Message.Warning(xhr.statusText);
        }
    },

    GetDataForTable: function (refresh) {
        n = 1;
        var jsonParam = '';
        var serviceURL = "/Meeting/Index/";
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            console.log('OK');
            Manager.LoadDataTable(jsonData, refresh);
        }
        function onFailed(xhr, status, err) {
            //Message.Exception(xhr);
            Message.Warning(xhr.statusText);
        }
    },
    LoadDataTable: function (data, refresh) {
        
        if (refresh == "0") {
            dTable = $('#tableElement').DataTable({
                dom: 'lB<"toolbar">ftrip',
                buttons: [
                    {
                        extend: 'csvHtml5',
                        exportOptions: {
                            columns: [0, 1]
                        },
                        title: 'Meetings'
                    }, 'print', 'pdfHtml5'
                ],
                initComplete: function() {
                    if (Manager.GetParameterByName('activeName') != '') {
                        //$('#' + Manager.GetParameterByName('activeName')).addClass('active');
                        setTimeout(function() {
                                $('#' + Manager.GetParameterByName('activeName')).trigger('click');
                            },
                        100);
                    }
                },
                scrollY: "300px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[5, 10, 15, 40], [5, 10, 15, 40, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { className: "dt-left", targets: [0, 1, 2] }
                ],
                columns: [
                    {
                        //data: 'SL',
                        name: 'SL',
                        title: '# SL',
                        width: 10,
                        render: function() {
                            return n++;
                        }

                    },
                    {
                        data: 'MeetingName',
                        name: 'MeetingName',
                        title: 'Meeting Name',
                        width: 100,

                    },

                    {
                        data: 'BeginningTime',
                        name: 'BeginningTime',
                        title: 'Beginning Date & Time',
                        width: 100,
                        render: function (data, type, row) {
                            var date = moment(data);
                            return date.format("LLL");
                        }
                    },

                    {
                        data: 'EndTime',
                        name: 'EndTime',
                        title: 'End Date & Time',
                        width: 100,
                        render: function (data, type, row) {
                            var date = moment(data);
                            //return date.format("DD-MMM-YYYY hh:mm a");
                            return date.format("LLL");
                        }
                    },

                    {
                        data: 'EmployeeName',
                        name: 'EmployeeName',
                        title: 'Meeting Caller',
                        width: 100,
                    },

                    {
                        data: 'VenueName',
                        name: 'VenueName',
                        title: 'Venue',
                        width: 100,
                    },
                    {
                        data: 'MeetingDescription',
                        name: 'MeetingDescription',
                        title: 'Meeting Description',
                        width: 100,
                    },

                    {
                        title: 'Details',
                        width: 50,

                        render: function (data, type, row) {
                            var meetingDetailsBtn = '';
                            meetingDetailsBtn = ' <span class="glyphicon glyphicon-list-alt spnMeetingDetails" id="meetingDetailsBtn" title="Meeting Details"></span>';

                            return meetingDetailsBtn;
                        }

                    },
                    {
                        name: 'Option',
                        title: 'Option',
                        width: 50,

                        render: function (data, type, row) {
                            var deleteBtn = '';
                            deleteBtn = '<span class="glyphicon glyphicon-trash spnDataTableDelete" id="deleteBtn" title="Click to delete"></span>';
                            return '<span class="glyphicon glyphicon-edit spnDataTableEdit id="editButton" title="Edit"></span>' + deleteBtn;
                        }
                    }

                ],
                data: data,
                order:[]

            });
        } else {
            if(dTable != null)
            dTable.clear().rows.add(data).draw();
        }
    }
}

function GetPresentVenueName(venueId) {
    $.ajax({
        url: '/Meeting/GetVenueName/',
        type: "Get",
        data: { venueId: venueId} ,
        datatype: "Json",
        success: function (jsonData) {
            debugger;
            var name = jsonData.VenueName;
            return name.toString();
        },
        error: function () {

        }
    });
}

var beginningdatetime;
var enddatetime;
var da;
var dae;
$(document).on('click', '.spnDataTableEdit', function () {
    debugger;
    var rowData = dTable.row($(this).parent()).data();
    $('#meetingNameTxt').val(rowData.MeetingName);

     da = moment(rowData.BeginningTime);
     dae = moment(rowData.EndTime);
     beginningdatetime = da.format('YYYY-MM-DD HH:mm');
     enddatetime = dae.format('YYYY-MM-DD HH:mm');
    var d2 = enddatetime.toString().split(' ');
    var d = beginningdatetime.toString().split(' ');
    $('#beginningdateTxt').val(d[0]);
    $('#beginningTimeTxt').val(d[1]);
    $('#enddateTxt').val(d2[0]);

    $('#endTimeTxt').val(d2[1]);
    //$('#EmployeeId').val(rowData.EmployeeId);
    $('#presentMeetingCallerName').text(rowData.EmployeeName);
    $("div.presentMeetingCallerClass").css("display", "block");
    $('#presentVenueName').text(rowData.VenueName);
    $("div.presentVenueClass").css("display", "block");
    $('#meetingDescriptionTxt').val(rowData.MeetingDescription);

    _id = rowData.MeetingId;
    _presentVenueId = rowData.VenueId;
    _presentMeetingCallerId = rowData.EmployeeId;

    $('#saveButton').text('Update');
    $('#myModalLabel').text('Update Meeting');
    $('#loadVenueDropDownButton').text('Change Venue');
    $("#loadVenueDropDownButton").css("margin-right", "5px");
    $("#loadVenueDropDownButton").css("width", "150px");
    $('#loadMeetingCallerDropDownButton').text('Change Meeting Caller');
    $("#loadMeetingCallerDropDownButton").css("margin-right", "5px");
    $("#loadMeetingCallerDropDownButton").css("width", "200px");
    $('#saveButton').removeClass('btn-success');
    $('#saveButton').addClass('btn-warning');
    $('#saveButton').prop('id', 'editButton');
    $('#myModal').modal('show');
});

$("#myModal").on('hidden.bs.modal', function () {
    $('#editButton').text('Save');
    $('#editButton').removeClass('btn-warning');
    $('#editButton').addClass('btn-success');
    $('#editButton').prop('id', 'saveButton');
    Manager.ResetForm();
});

$(document).on('click', '#editButton', function () {
    Manager.EditMeeting(_id, _presentVenueId, _presentMeetingCallerId);
});

$(document).on('click', '#saveButton', function () {
    Manager.SaveMeeting();
    $('#myModal').modal('hide');

});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().MeetingId;
    Manager.DeleteMeeting(_id);
});

$(document).on('click', '.thisWeekMeeting', function () {

    activeTab = '#thisWeekMeeting';
    Manager.MeetingOfThisWeek();

});

$(document).on('click', '#nextWeekMeetings', function () {

    
    activeTab = $(".tab btn.active");
    //alert($(activeTab).data("value"));
    //alert($(activeTab.activeElement).data("id"));
    activeTab = '#nextWeekMeetings';
    Manager.MeetingOfNextWeek();
});

$(document).on('click', '#allmeetings', function () {

    $('#allmeetings').addClass('active');
    $('#nextWeekMeetings').removeClass('active');
    $('#thisWeekMeetings').removeClass('active');
    Manager.AllMeeting();
});
var beginningTime;
var endTime;
$(document).on('click', '#loadVenueDropDownButton', function () {

    $('#VenueId').empty();
    //var beginningTime;
    //var endTime;
    if (!$('#beginningdateTxt').val()) {
        Message.Warning("Beginning Date is required!");
    }
    else if (!$('#beginningTimeTxt').val()) {
        Message.Warning("Beginning Time is required!");
    }

    else if (!$('#enddateTxt').val()) {
        Message.Warning("End Date is required");
    }
    else if (!$('#endTimeTxt').val()) {
        Message.Warning("End Time is required");
    }
    else {
        beginningTime = new Date($('#beginningdateTxt').val() + ' ' + $('#beginningTimeTxt').val());
        endTime = new Date($('#enddateTxt').val() + ' ' + $('#endTimeTxt').val());
    }
    Manager.LoadVenueDDL(beginningTime.toUTCString(), endTime.toUTCString());

    $("div.abc").css("display", "block");

});

$(document).on('click', '#loadMeetingCallerDropDownButton', function () {
    debugger;
    $('#EmployeeId').empty();
    var beginningTime;
    var endTime;
    if (!$('#beginningdateTxt').val()) {
        Message.Warning("Beginning Date is required!");
    }
    else if (!$('#beginningTimeTxt').val()) {
        Message.Warning("Beginning Time is required!");
    }

    else if (!$('#enddateTxt').val()) {
        Message.Warning("End Date is required");
    }
    else if (!$('#endTimeTxt').val()) {
        Message.Warning("End Time is required");
    }
    else {
        beginningTime = new Date($('#beginningdateTxt').val() + ' ' + $('#beginningTimeTxt').val());
        endTime = new Date($('#enddateTxt').val() + ' ' + $('#endTimeTxt').val());
    }
    Manager.LoadEmployeeDDL(beginningTime.toUTCString(), endTime.toUTCString());

    $("div.MeetingCaller").css("display", "block");

});

$(document).on('click', '.spnAddMembers', function () {
    var rowData = dTable.row($(this).parent()).data();
    _id = rowData.MeetingId;
    Manager.LoadMembers(_id);
});

$(document).on('click', '.spnMeetingDetails', function () {
    var rowData = dTable.row($(this).parent()).data();
    _id = rowData.MeetingId;
    Manager.MeetingDetails(_id);
});

$(document).on('click', '#clearButton', function () {
    Manager.ResetForm();
    $('#VenueId').empty();
    $("div.abc").css("display", "none");
});
