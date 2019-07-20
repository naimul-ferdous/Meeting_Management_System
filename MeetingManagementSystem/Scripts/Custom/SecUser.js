var _id = null;
var dTable = null;
$(document).ready(function () {

    Manager.GetDataForTable(0);


});

var Manager = {

    ResetForm: function () {
        $('#secUserForm')[0].reset();
    },

    SaveSecUser: function () {
        if (!$('#loginNameTxt').val()) {
            Message.Warning("Login name is required");
        }
        else if (!$('#passwordTxt').val()) {
            Message.Warning("Password is required");
        }
       // else if (!$('#emailIdTxt').val()) {
          //  Message.Warning("Email Id is required");
        //}

        else {
       
            if (Message.Prompt()) {
                var jsonParam = $('#secUserForm').serialize();
                var serviceURL = "/SecUser/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }

            function onSuccess(JsonData) {
                if (JsonData == "0") {
                    Message.Error("Saved");

                } else {


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

    EditSecUser: function (id) {
        if (!$('#loginNameTxt').val()) {
            Message.Warning("Login name is required");
        }
        else if (!$('#passwordTxt').val()) {
            Message.Warning("Password is required");
        }
        else if (!$('#emailIdTxt').val()) {
            Message.Warning("Email Id is required");
        } else {

            if (Message.Prompt()) {
                var jsonParam = $('#secUserForm').serialize() + '&SecUserId=' + id;
                var serviceURL = "/SecUser/Edit/ ";
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

    DeleteSecUser: function (id) {
        if (Message.Prompt()) {
            var jsonParam = { secUserId: id };
            var serviceURL = "/SecUser/Delete/";
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
        var serviceURL = "/SecUser/GetAllSecUser/";
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
                    { className: "text-left", targets: [0, 1,2, 3, 4 ] }
                ],
                columns: [
                    {
                        data: 'LoginName',
                        name: 'LoginName',
                        title: 'Login Name',
                        width: 80,
                    },

                    {
                        data: 'Password',
                        name: 'Password',
                        title: 'Password',
                        width: 30,
                    },

                    {
                        data: 'Status',
                        name: 'Status',
                        title: 'Status',
                        width: 50,
                    },

                    {
                        data: 'EmailId',
                        name: 'EmailId',
                        title: 'Email',
                        width: 100,
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
    $('#loginNameTxt').val(rowData.LoginName);
    $('#passwordTxt').val(rowData.Password);
    $('#emailIdTxt').val(rowData.EmailId);
    $('#createdByTxt').val(rowData.CreatedBy);
    $('#CreatedDate').val(formatCreatedDate);
    $('#modifiedByTxt').val(rowData.ModifiedBy);
    $('#ModifiedDate').val(formatModifiedDate);
    $('#StatusId').prop('checked',rowData.Status);
    _id = rowData.SecUserId;

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
    Manager.EditSecUser(_id);
});

$(document).on('click', '#saveButton', function () {
    Manager.SaveSecUser();
});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().SecUserId;
    Manager.DeleteSecUser(_id);
});


