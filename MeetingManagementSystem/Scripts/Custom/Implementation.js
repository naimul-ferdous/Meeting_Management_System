var _id = null;
var dTable = null;

$(document).ready(function () {

    //ImplementationManager.LoadMeetingDDL();

    //ImplementationManager.LoadEmployeeDDL();

    ImplementationManager.GetDataForTable(0);

    $('#clearButton').click(function () {
        ImplementationManager.ResetForm();
    });

});

var ImplementationManager = {

    ResetForm: function () {
        $('#implementationForm')[0].reset();
    },

    SaveImplementation: function () {
        if (!$('#implementationDescriptionTxt').val()) {
            Message.Warning("Implementation description is required");
        }
        else {
            if (Message.Prompt()) {
                var jsonParam = $('#implementationForm').serialize();
                var serviceURL = "/Implementation/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }
        }

        function onSuccess(JsonData) {
            if (JsonData == "0") {
                Message.Error("Saved");

            }
            else {
                ImplementationManager.ResetForm();
                $('#myImplementationModal').modal('hide');
                Message.Success("save");
                ImplementationManager.GetDataForTable(1);
            }
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    EditImplementation: function (id) {
        if (!$('#implementationDescriptionTxt').val()) {
            Message.Warning("Implementation description is required");
        } else {
            if (Message.Prompt()) {
                var jsonParam = $('#implementationForm').serialize() + '&implementationId=' + id;
                var serviceURL = "/Implementation/Edit/ ";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }
        }

        function onSuccess(JsonData) {
            if (JsonData == "0") {
                Message.Error("update");
            } else {
                $('#myImplementationModal').modal('hide');
                Message.Success("update");
                ImplementationManager.ResetForm();

                ImplementationManager.GetDataForTable(1);
            }
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }

    },

    DeleteImplementation: function (id) {
        if (Message.Prompt()) {
            var jsonParam = { implementationId: id };
            var serviceURL = "/Implementation/Delete/";
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }

        function onSuccess(JsonData) {
            if (JsonData == 0) {
                Message.Error("delete");
            }
            else {
                Message.Success("delete");
                ImplementationManager.GetDataForTable(1);
            }
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    LoadMeetingDDL: function () {
        $.ajax({
            url: '/Implementation/GetMeetingInfo/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#MeetingId').append('<option value="0" selected ="Selected">Select Meeting </option>');
                $.each(jsonData, function (key, value) {
                    $('#MeetingId').append(
                        '<option value="' + value.MeetingId + '">' + value.MeetingName + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },

    LoadEmployeeDDL: function () {
        $.ajax({
            url: '/Employee/GetAllEmployees/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#implementationEmployeeId').append('<option value="0" selected ="Selected">Select Employee </option>');
                $.each(jsonData, function (key, value) {
                    $('#ImplementationEmployeeId').append(
                        '<option value="' + value.EmployeeId + '">' + value.EmployeeName + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },

    GetDataForTable: function (refresh) {
        var jsonParam = '';
        var id = $('#meetingId').val();
        var serviceURL = "/Implementation/Index/" + id;
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            console.log('OK');
            ImplementationManager.LoadDataTable(jsonData, refresh);
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    LoadDataTable: function (data, refresh) {
        if (refresh == "0") {
            dTable = $('#implementationTableElement').DataTable({
                dom: 'lB<"toolbar">frtip',
                buttons: [
                    {
                        extend: 'csvHtml5',
                        exportOptions: {
                            columns: [0, 1]
                        },
                        title: 'Implementation'
                    }, 'print', 'pdfHtml5'
                ],

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
                        data: 'ImplementationDescription',
                        name: 'ImplementationDescription',
                        title: 'Implementation Description',

                    },

                    {
                        data: 'EmployeeName',
                        name: 'EmployeeName',
                        title: 'Employee Name',

                    },

                    {
                        name: 'Option',
                        title: 'Option',
                        width: 50,

                        render: function (data, type, row) {
                            var deleteBtn = '';
                            deleteBtn = '<span class="glyphicon glyphicon-trash spnImpDataTableDelete" id="deleteBtn" title="Click to delete"></span>';
                            return '<span class="glyphicon glyphicon-edit spnImpDataTableEdit id="implementationEditButton" title="Edit"></span>' + deleteBtn;
                        }

                    }

                ],
                data: data

            });
        } else {
            dTable.clear().rows.add(data).draw();
        }
    }
}

$(document).on('click', '.spnImpDataTableEdit', function () {
    var rowData = dTable.row($(this).parent()).data();
    $('#implementationDescriptionTxt').val(rowData.ImplementationDescription);
    $('#MeetingId').val(rowData.MeetingId);
    $('#EmployeeId').val(rowData.EmployeeId);
    _id = rowData.ImplementationId;

    $('#implementationSaveButton').text('Update');
    $('#implementationSaveButton').removeClass('btn-success');
    $('#implementationSaveButton').addClass('btn-warning');
    $('#implementationSaveButton').prop('id', 'implementationEditButton');
    $('#myImplementationModal').modal('show');
});

$("#myImplementationModal").on('hidden.bs.modal', function () {
    $('#implementationEditButton').text('Save');
    $('#implementationEditButton').removeClass('btn-warning');
    $('#implementationEditButton').addClass('btn-success');
    $('#implementationEditButton').prop('id', 'implementationSaveButton');
});

$(document).on('click', '#implementationSaveButton', function () {
    ImplementationManager.SaveImplementation();
});

$(document).on('click', '#implementationEditButton', function () {
    ImplementationManager.EditImplementation(_id);
});

$(document).on('click', '.spnImpDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().ImplementationId;
    ImplementationManager.DeleteImplementation(_id);
});