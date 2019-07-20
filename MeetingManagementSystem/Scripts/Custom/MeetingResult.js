

var _id;
var ResultManager = {
    ResetForm: function () {
        $('#meetingResultForm')[0].reset();
    },

    SaveMeetingResult: function () {
       
            if (Message.Prompt()) {
                var jsonParam = $('#meetingResultForm').serialize();
                var serviceURL = "/MeetingResult/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }

        function onSuccess(JsonData) {
            if (JsonData == "0") {
                Message.Error("Saved");

            } else {
                ResultManager.ResetForm();
                $('#myModals').modal('hide');
                Message.Success("save");
                ResultManager.GetDataForTable(1);

            }
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },


    EditMeetingResult: function (id) {

        if (Message.Prompt()) {
            var jsonParam = $('#meetingResultForm').serialize() + '&MeetingResultId=' + id;
            var serviceURL = "/MeetingResult/Edit/ ";
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }

        function onSuccess(JsonData) {
            if (JsonData == "0") {
                Message.Error("update");
            } else {
                ResultManager.ResetForm();
                $('#myModals').modal('hide');
                Message.Success("update");
                ResultManager.GetDataForTable(1);
            }
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }


    },

    DeleteMeetingResult: function (id) {
        if (Message.Prompt()) {
            var jsonParam = { meetingResultId: id };
            var serviceURL = "/MeetingResult/Delete/";
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }

        function onSuccess(JsonData) {
            if (JsonData == 0) {
                Message.Error("delete");
            }
            else {
                Message.Success("delete");
                ResultManager.GetDataForTable(1);
            }
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    LoadEmployeeDDL: function () {
        $.ajax({
            url: '/MeetingResult/GetEmployeeInfo/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#EmployeeId_2').append('<option value="0" selected ="Selected">Select Employee </option>');
                $.each(jsonData,
                    function (key, value) {
                        $('#EmployeeId_2').append(
                            '<option value="' + value.EmployeeId + '">' + value.EmployeeName + '</option>'
                        );
                    });
            },
            error: function () {
            }
        });
    },

    LoadMeetingDDL: function () {

        $.ajax({
            url: '/MeetingResult/GetMeetingInfo/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#MeetingId_2').append('<option value="0" selected ="Selected">Select Meeting </option>');
                $.each(jsonData,
                    function (key, value) {
                        $('#MeetingId_2').append(
                            '<option value="' + value.MeetingId + '">' + value.MeetingName + '</option>'
                        );
                    });

            },
            error: function () {

            }
        });
    },

    GetDataForTable: function (refresh) {
        var jsonParam = '';
        var id = $('input[name=MeetingId]').val();

        var serviceURL = "/MeetingResult/GetAllMeetingResult/?meetingResultId=" + id;
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);

        function onSuccess(jsonData) {
            console.log('OK');
            ResultManager.LoadDataTable(jsonData, refresh);
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    LoadDataTable: function (data, refresh) {
        if (refresh == "0") {
            resultTable = $('#resultTableElements').DataTable({
                //dom: 'lB<"toolbar">frtip',
                bAutoWidth: false,
                scrollY: "200px",
                scrollX: true,
                scrollCollapse: true,
                bPaginate: false,
                bFilter: false,
                columns: [
                    {
                        data: 'Announcement',
                        name: 'Announcement',
                        title: 'Announcement',
                        width: 100,
                    },
                    {
                        data: 'Result',
                        name: 'Result',
                        title: 'Result',
                        width: 100,
                    },{
                        data: 'Status',
                        name: 'Status',
                        title: 'Status',
                        width: 100,
                    },
                    {
                        data: 'EmployeeName',
                        name: 'EmployeeName',
                        title: 'Employee Name',
                        width: 100,
                    },
                    {
                        data: 'MeetingName',
                        name: 'MeetingName',
                        title: 'Meeting Name',
                        width: 100,
                    },
                    {
                        name: 'Option',
                        title: 'Option',
                        width: 100,

                        render: function (data, type, row) {
                            var deleteBtn = '';
                            deleteBtn =
                                '<span class="glyphicon glyphicon-trash spnResultDataTableDelete" id="deleteButton" title="Click to delete"></span>';
                            return '<span class="glyphicon glyphicon-edit spnResultDataTableEdit id="editBtn" title="Edit"></span>' +
                                deleteBtn;
                        }

                    }
                ],
                data: data

            });
        } else {
            resultTable.clear().rows.add(data).draw();
        }
    }
};

$(document).ready(function () {

    ResultManager.GetDataForTable(0);

    ResultManager.LoadEmployeeDDL();
    ResultManager.LoadMeetingDDL();


});




$(document).on('click', '#saveMeetingResultButton', function () {
    ResultManager.SaveMeetingResult();
    ResultManager.ResetForm();
    $('#myModals').modal('hide');
});

$(document).on('click', '.spnResultDataTableEdit', function () {
    var rowData = resultTable.row($(this).parent()).data();
    $('#announcementTxt').val(rowData.Announcement);
    $('#resultTxt').val(rowData.Result);
    $('#statusTxt').val(rowData.Status);
    $('#EmployeeId').val(rowData.EmployeeId);
    //EmployeeId, EmployeeId_2
    $('#MeetingId_2').val(rowData.MeetingId);
    _id = rowData.MeetingResultId;



    $('#saveMeetingResultButton').text('Update');
    $('#saveMeetingResultButton').removeClass('btn-success');
    $('#saveMeetingResultButton').addClass('btn-warning');
    $('#saveMeetingResultButton').prop('id', 'editBtn');
    $('#myModals').modal('show');
});

$("#myModals").on('hidden.bs.modal', function () {
    $('#editBtn').text('Save');
    $('#editBtn').removeClass('btn-warning');
    $('#editBtn').addClass('btn-success');
    $('#editBtn').prop('id', 'saveMeetingResultButton');
});

$(document).on('click', '#editBtn', function () {
    ResultManager.EditMeetingResult(_id);
});

$(document).on('click', '.spnResultDataTableDelete', function () {
    _id = resultTable.row($(this).parents('tr')).data().MeetingResultId;
    ResultManager.DeleteMeetingResult(_id);
});
