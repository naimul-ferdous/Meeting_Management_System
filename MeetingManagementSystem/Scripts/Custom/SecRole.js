var _id = null;
var dTable = null;
$(document).ready(function () {

    Manager.GetDataForTable(0);
    Manager.LoadRoleDDL();

});

var Manager = {

    LoadRoleDDL: function () {
        $.ajax({
            url: '/SecRole/GetRoleInfo/',
            type: "GET",
            datatype: "Json",
            success: function (jsonData) {
                $('#SecRoleId').empty();
                $('#SecRoleId').append('<option value="0" selected ="Selected">Select Role </option>');
                $.each(jsonData, function (key, value) {
                    $('#SecRoleId').append(
                        '<option value="' + value.SecRoleId + '">' + value.RoleName + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },

    ResetForm: function () {
        $('#secRoleForm')[0].reset();
    },

    SaveSecRole: function () {
        if (!$('#RoleNameTxt').val()) {
            Message.Warning("Role name is required");

        } else {
            if (Message.Prompt()) {
                var jsonParam = $('#secRoleForm').serialize();
                var serviceURL = "/SecRole/Create/";
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
                    Manager.LoadRoleDDL();
                    Manager.GetDataForTable(1);

                }
            }

            function onFailed(xhr, status, err) {
                Message.Exception(xhr);

            }
    },

    EditSecRole: function (id) {
        if (!$('#RoleNameTxt').val()) {
            Message.Warning("Role name is required");

        } else {

            if (Message.Prompt()) {
                var jsonParam = $('#secRoleForm').serialize() + '&SecRoleId=' + id;
                var serviceURL = "/SecRole/Edit/ ";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }


            function onSuccess(JsonData) {
                if (JsonData == "0") {
                    Message.Error("update");
                } else {
                    Manager.ResetForm();
                    $('#myModal').modal('hide');
                    Message.Success("update");
                    Manager.LoadRoleDDL();
                    Manager.GetDataForTable(1);
                }
            }

            function onFailed(xhr, status, err) {
                Message.Exception(xhr);
            }
        }

    },

    DeleteSecRole: function (id) {
        if (Message.Prompt()) {
            var jsonParam = { secRoleId: id };
            var serviceURL = "/SecRole/Delete/";
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }

        function onSuccess(JsonData) {
            if (JsonData == 0) {
                Message.Error("delete");
            }
            else {
                Message.Success("delete");
                Manager.LoadRoleDDL();
                Manager.GetDataForTable(1);
            }
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    GetDataForTable: function (refresh) {
        var jsonParam = '';
        var serviceURL = "/SecRole/GetAllSecRole/";
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
                        title: 'SecUser'
                    }, 'print', 'pdfHtml5'
                ],

                scrollY: "300px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[5, 10, 15, 40], [5, 10, 15, 40, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { className: "text-left", targets: [0, 1, 2] }
                ],
                columns: [
                    {
                        data: 'RoleName',
                        name: 'RoleName',
                        title: 'Role Name',
                        width: 80
                    },

                    {
                        data: 'Status',
                        name: 'Status',
                        title: 'Status',
                        width: 50
                    },

                    {
                        name: 'Option',
                        title: 'Option',
                        width: 30,

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
    var createdDate = moment(rowData.CreatedDate);
    var formatCreatedDate = createdDate.format('YYYY-MM-DD');
    var modifiedDate = moment(rowData.ModifiedDate);
    var formatModifiedDate = modifiedDate.format('YYYY-MM-DD');
    $('#RoleNameTxt').val(rowData.RoleName);
   $('#createdByTxt').val(rowData.CreatedBy);
    $('#CreatedDate').val(formatCreatedDate);
    $('#modifiedByTxt').val(rowData.ModifiedBy);
    $('#ModifiedDate').val(formatModifiedDate);
    $('#StatusId').prop('checked', rowData.Status);
    _id = rowData.SecRoleId;

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

$(document).on('change', '#StatusId', function () {
    debugger;
    if ($(this).prop("checked") == true) {

        $('#Status').val('true');
    } else {
        $('#Status').val('false');
    }

});
$(document).on('click', '#editButton', function () {
    Manager.EditSecRole(_id);
});

$(document).on('click', '#saveButton', function () {
    Manager.SaveSecRole();
   

    //alert("OK");
});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().SecRoleId;
    Manager.DeleteSecRole(_id);
});


