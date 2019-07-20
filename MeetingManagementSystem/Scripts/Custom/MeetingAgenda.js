var _agendaId;
var n = 1;

$(document).ready(function () {
    AgendaManager.LoadEmployeeDDL();

});
var AgendaManager = {
    ResetForm: function() {
        $('#meetingAgendaForm')[0].reset();
    },
    SaveAgenda: function () {
        if ($('#EmployeeId').val() < 1) {
            Message.Warning("Presenter is required");
        } else {
            if (Message.Prompt()) {
                var jsonParam = $('#meetingAgendaForm').serialize();
                var serviceURL = "/MeetingAgenda/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }
        }
        

        function onSuccess(JsonData) {
            if (JsonData == "0") {
                Message.Error("Saved");

            } else {
                AgendaManager.ResetForm();
                $('#myModal').modal('hide');
                Message.Success("save");
                n = 1;
                AgendaManager.GetDataForTable(1);

            }
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    LoadEmployeeDDL: function () {

        $.ajax({
            url: '/Employee/GetAllEmployees/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
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
    SaveMeetingFile: function () {
        var jsonParam = $('#meetingFileForm').serialize();
        var serviceURL = "/MeetingMembers/Upload/";
        AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);

        function onSuccess(JsonData) {
            if (JsonData == "0") {
                Message.Error("Saved");

            }
            
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    EditMeetingAgenda: function(id) {

        if (Message.Prompt()) {
            var jsonParam = $('#meetingAgendaForm').serialize() + '&MeetingAgendaId=' + id;
            var serviceURL = "/MeetingAgenda/Edit/ ";
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }

        function onSuccess(JsonData) {
            if (JsonData == "0") {
                Message.Error("update");
            } else {
                AgendaManager.ResetForm();
                $('#myModal').modal('hide');
                Message.Success("update");
                n = 1;
                AgendaManager.GetDataForTable(1);
            }
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }


    },

    DeleteMeetingAgenda: function(id) {
        if (Message.Prompt()) {
            var jsonParam = " ";
            var serviceURL = "/MeetingAgenda/Delete/" + id;
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }

        function onSuccess(JsonData) {
            if (JsonData == 0) {
                Message.Error("delete");
            } else {
                Message.Success("delete");
                n = 1;
                AgendaManager.GetDataForTable(1);
            }
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    GetDataForTable: function(refresh) {
        var jsonParam = '';
        var id = $('#meetingId').val();

        var serviceURL = "/MeetingAgenda/Index/" + id;
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);

        function onSuccess(jsonData) {
            console.log('OK');
            AgendaManager.LoadDataTable(jsonData, refresh);
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    LoadDataTable: function(data, refresh) {
        if (refresh == "0") {
            agendaTable = $('#agendaTableElement').DataTable({
                //dom: 'lB<"toolbar">frtip',
                bAutoWidth: false,
                scrollY: "200px",
                scrollX: true,
                scrollCollapse: true,
                bPaginate: false,
                bFilter: false,
                columns: [
                    {
                        //data: 'SL',
                        name: 'SL',
                        title: '# SL',
                        width: 10,
                        render: function () {
                            return n++;
                        }

                    },
                //    {
                //        data: 'MeetingAgendaId',
                //        name: 'MeetingAgendaId',
                //        title: 'Meeting Agenda Id',
                //    width: 100,
                //},
                    {
                        data: 'MeetingAgendaName',
                        name: 'MeetingAgendaName',
                        title: 'Meeting Agenda',
                        width: 100,
                    },
                    {
                        data: 'EmployeeName',
                        name: 'Presenter',
                        title: 'Presenter',
                        width: 100,
                    },
                    {
                        name: 'Option',
                        title: 'Option',
                        width: 100,

                        render: function(data, type, row) {
                            var deleteBtn = '';
                            deleteBtn =
                                '<span class="glyphicon glyphicon-trash spnDataTableDelete" id="deleteBtn" title="Click to delete"></span>';
                            return '<span class="glyphicon glyphicon-edit spnDataTableEdit id="editButton" title="Edit"></span>' +
                                deleteBtn;
                        }

                    }
                ],
                data: data

            });
        } else {
            agendaTable.clear().rows.add(data).draw();
        }
    }
};

$(document).ready(function () {

    AgendaManager.GetDataForTable(0);
   

});


$(document).on('click', '#saveFile', function () {
    AgendaManager.SaveMeetingFile();
   
});

function Agenda() {
    $('#agendaMsg').text(' ');
}

//function meetingPresenter() {
//    $('#presenterMsg').text(' ');
//}


$(document).on('click', '#saveButton', function () {

    var agentFlag = 0;
    var presenterFlag = 0;
    if (!$('#meetingAgendaNameTxt').val()) {
       
        $('#agendaMsg').text('agenda is required');
        agentFlag = 1;
    }
    //if (!$('#presenterTxt').val()) {
    //    $('#presenterMsg').text('Presenter is required');
    //    presenterFlag = 1;
    //}
    //if (agentFlag == 0 && presenterFlag==0) {
    //    AgendaManager.SaveAgenda();
    //    AgendaManager.ResetForm();
    //    $('#myModal').modal('hide');
        
    //}
    if (agentFlag == 0) {
        AgendaManager.SaveAgenda();
        AgendaManager.ResetForm();
        $('#myModal').modal('hide');

    }
});

$(document).on('click', '#crossClose', function () {
    Agenda();
    //meetingPresenter();
    AgendaManager.ResetForm();
});

$(document).on('click', '#closeButton', function () {
    Agenda();
    //meetingPresenter();
    AgendaManager.ResetForm();
});
$(document).on('click', '.spnDataTableEdit', function () {
    var rowData = agendaTable.row($(this).parent()).data();
    $('#meetingAgendaNameTxt').val(rowData.MeetingAgendaName);
    //$('#presenterTxt').val(rowData.Presenter);
    $('#EmployeeId').val(rowData.EmployeeId);
    _agendaId = rowData.MeetingAgendaId;

    $('#saveButton').text('Update');
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
});

$(document).on('click', '#editButton', function () {
    AgendaManager.EditMeetingAgenda(_agendaId);
});

$(document).on('click', '.spnDataTableDelete', function () {
    _agendaId = agendaTable.row($(this).parents('tr')).data().MeetingAgendaId;
    AgendaManager.DeleteMeetingAgenda(_agendaId);
});
function alertWorld() {
    location.reload();
}
$('.abc').click(function () {
    var id = $(this).attr('value');
    $.ajax({
        url: "/MeetingMembers/DeleteFile/",
        dataType: "text json",
        type: "GET",
        data: { id: id },
        success: function (JsonData) {
            if (JsonData == 0) {
                Message.Error("delete");
            } else {
                Message.Success("delete");
                //sleep(2000);
                setTimeout(alertWorld, 3000);
            }
        }
    });
});
