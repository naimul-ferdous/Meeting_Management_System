var _id = null;
var dTable = null;
$(document).ready(function () {

    Manager.GetDataForTable(0);
    Manager.LoadUserDDL();
    Manager.LoadRoleDDL();

});

var Manager = {

    ResetForm: function () {
        $('#secUserRoleForm')[0].reset();
    },

    LoadUserDDL: function () {
        debugger;
        $.ajax({
            url: '/SecUserRole/GetUserInfo/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#SecUserId').empty();
                $('#SecUserId').append('<option value="0" selected ="Selected">Select User </option>');
                $.each(jsonData, function (key, value) {
                    $('#SecUserId').append(
                        '<option value="' + value.SecUserId + '">' + value.UserName + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },

    LoadRoleDDL: function () {
        debugger;
        $.ajax({
            url: '/SecUserRole/GetRoleInfo/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
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

    SaveSecUserRole: function () {
        if (!$('#SecUserId').val()) {
            Message.Warning("User name is required");
        }
        else if (!$('#SecRoleId').val()) {
            Message.Warning("Role name is required");
        } else {

            if (Message.Prompt()) {
                var jsonParam = $('#secUserRoleForm').serialize();
                var serviceURL = "/SecUserRole/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }

            function onSuccess(JsonData) {
                if (JsonData == "0") {
                    Message.Error("Saved");
                } else {
                    Manager.LoadUserDDL();
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

    EditSecUserRole: function (id) {
        //if (!$('#SecUserId').val()) {
        //    Message.Warning("User name is required");
        //}
        //else
        if (!$('#SecRoleId').val()) {
            Message.Warning("Role name is required");
        } else {
            if (Message.Prompt()) {
                var jsonParam = $('#secUserRoleForm').serialize() + '&SecUserRoleId=' + id;
                var serviceURL = "/SecUserRole/Edit/ ";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }


            function onSuccess(JsonData) {
                if (JsonData == "0") {
                    Message.Error("update");
                } else {
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

    DeleteSecUserRole: function (id) {
        if (Message.Prompt()) {
            var jsonParam = { secUserRoleId: id };
            var serviceURL = "/SecUserRole/Delete/";
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
        debugger;
        var jsonParam = '';
        var serviceURL = "/SecUserRole/Index/";
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
        debugger;
        if (refresh == "0") {
            dTable = $('#tableElement').DataTable({
                dom: 'lB<"toolbar">frtip',
                buttons: [
                    {
                        extend: 'csvHtml5',
                        exportOptions: {
                            columns: [0, 1]
                        },
                        title: 'SecUserRole'
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
                        data: 'UserName',
                        name: 'UserName',
                        title: 'User Name',
                        width: 50
                    },
                    {
                        data: 'RoleName',
                        name: 'RoleName',
                        title: 'Role Name',
                        width: 80
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
};

$(document).on('click', '.spnDataTableEdit', function () {
    var rowData = dTable.row($(this).parent()).data();
        // $('#SecUserId').val(rowData.SecUserId);
    $('#SecUserId').hide();
    $('#userStar').hide();
    $('#userName').text(rowData.UserName);
    $('#SecRoleId').val(rowData.SecRoleId);

    _id = rowData.SecUserRoleId;
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
    Manager.EditSecUserRole(_id);
});

$(document).on('click', '#saveButton', function () {
    Manager.SaveSecUserRole();
});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().SecUserRoleId;
    Manager.DeleteSecUserRole(_id);
});
$(document).on('click', '#openModalButton', function () {
    $('#SecUserId').show();
    $('#userStar').show();
    $('#userName').text(' ');

});

