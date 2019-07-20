var _id = null;
var dTable = null;
$(document).ready(function () {

    Manager.GetDataForTable(0);

});

var Manager = {

    ResetForm: function () {
        $('#designationForm')[0].reset();
    },

    SaveDesignation: function () {
        if (!$('#designationNameTxt').val()) {
            Message.Warning("Designation name is required");
        }
        else {
            if (Message.Prompt()) {
                var jsonParam = $('#designationForm').serialize();
                var serviceURL = "/Designation/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }
            function onSuccess(JsonData) {
                if (JsonData == "0") {
                    Message.Error("Saved");

                }
                else {

                    Manager.ResetForm();
                    $('#myModal').modal('hide');
                    Message.Success("save");
                    Manager.GetDataForTable(1);

                }
            }
            function onFailed(xhr, status, err) {
                Message.Exception(xhr);
            }
        }
    },

    EditDesignation: function (id) {
        debugger;
        if (!$('#designationNameTxt').val()) {
            Message.Warning("Designation name is required");
        }
        else {
            if (Message.Prompt()) {
                var jsonParam = $('#designationForm').serialize() + '&DesignationId=' + id;
                var serviceURL = "/Designation/Edit/ ";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }


            function onSuccess(JsonData) {
                if (JsonData == "0") {
                    Message.Error("update");
                }
                else {
                    Manager.ResetForm();
                    $('#myModal').modal('hide');
                    Message.Success("update");
                    Manager.GetDataForTable(1);
                }
            }
            function onFailed(xhr, status, err) {
                Message.Exception(xhr);
            }
        }

    },

    DeleteDesignation: function (id) {
        if (Message.Prompt()) {
            var jsonParam = { designationId: id };
            var serviceURL = "/Designation/Delete/";
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }

        function onSuccess(JsonData) {
            if (JsonData == 0) {
                Message.Error("delete");
            }
            else {
                Message.Success("delete");
                Manager.GetDataForTable(1);
            }
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    GetDataForTable: function (refresh) {
        var jsonParam = '';
        var serviceURL = "/Designation/Index/";
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            console.log('OK');
            Manager.LoadDataTable(jsonData, refresh);
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    LoadDataTable: function (data, refresh) {
        if (refresh == "0") {
            dTable = $('#tableElement').DataTable({
                dom: 'lB<"toolbar">frtip',
                buttons: [
                    {
                        extend: 'csvHtml5',
                        exportOptions: {
                            columns: [0, 1]
                        },
                        title: 'Designation'
                    }, 'print', 'pdfHtml5'
                ],

                scrollY: "300px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[5, 10, 15, 40], [5, 10, 15, 40, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { className: "text-left", targets: [0, 1] }
                ],
                columns: [
                    {
                        data: 'DesignationName',
                        name: 'DesignationName',
                        title: 'Designation Name',
                        width: 100,
                    },

                    {
                        name: 'Option',
                        title: 'Option',
                        width: 100,

                        render: function (data, type, row) {
                            var deleteBtn = '';
                            deleteBtn = '<span class="glyphicon glyphicon-trash spnDataTableDelete" id="deleteBtn" title="Click to delete"></span>';
                            return '<span class="glyphicon glyphicon-edit spnDataTableEdit id="editButton" title="Edit"></span>' + deleteBtn;
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


$(document).on('click', '.spnDataTableEdit', function () {
    var rowData = dTable.row($(this).parent()).data();
    $('#designationNameTxt').val(rowData.DesignationName);
    _id = rowData.DesignationId;

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
    Manager.EditDesignation(_id);
});

$(document).on('click', '#saveButton', function () {
    Manager.SaveDesignation();
});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().DesignationId;
    Manager.DeleteDesignation(_id);
});


