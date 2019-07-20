var _id = null;
var dTable = null;

$(document).ready(function () {

    
    Manager.GetDataForTable(0);
    $('#clearButton').click(function () {
        Manager.ResetForm();
    });

});

var Manager = {

    ResetForm: function () {
        $('#employeeTypeForm')[0].reset();
    },

    SaveEmployeeType: function () {
        if (!$('#employeeTypeNameTxt').val()) {
            Message.Warning("Employee Type name is required");
         
        }

        else if (!$('#employeeTypeDescriptionTxt').val()) {
            Message.Warning("Employee Type Description is required");
        }
        
        
        else {
            if (Message.Prompt()) {
                var jsonParam = $('#employeeTypeForm').serialize();
                var serviceURL = "/EmployeeType/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }
        }
        
        function onSuccess(JsonData) {
            if (JsonData == "0") {
                Message.Error("Saved");

            }
            else {              
                $('#myModal').modal('hide');
                Manager.ResetForm();
                Message.Success("save");
                Manager.GetDataForTable(1);
            }
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    EditEmployeeType: function (id) {
        if (Message.Prompt()) {
            var jsonParam = $('#employeeTypeForm').serialize() + '&EmployeeTypeId=' + id;
            var serviceURL = "/EmployeeType/Edit/ ";
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

    },

    DeleteEmployeeType: function (id) {
        debugger;
        if (Message.Prompt()) {
            var jsonParam = { employeeTypeId: id };
            var serviceURL = "/EmployeeType/Delete/";
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
        var serviceURL = "/EmployeeType/Index/";
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
                        title: 'EmployeeType'
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
                        data: 'EmployeeTypeName',
                        name: 'EmployeeTypeName',
                        title: 'EmployeeTypeName',
                        width: 100,
                    },
                    {
                        data: 'EmployeeTypeDescription',
                        name: 'EmployeeTypeDescription',
                        title: 'Description',
                        width: 100,
                    },
                    //{
                    //    data: 'EmployeeTypeName',
                    //    name: 'EmployeeTypeName',
                    //    title: 'EmpType',
                    //    width: 100,
                    //},

                    {
                        name: 'Option',
                        title: 'Option',
                        width: 50,
  
                        render: function (data, type, row) {
                            var deleteBtn = '';
                            deleteBtn = '<span class="glyphicon glyphicon-trash spnDataTableDelete id="deleteBtn" title="Click to delete"></span>';
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
    $('#employeeTypeNameTxt').val(rowData.EmployeeTypeName);
    $('#employeeTypeDescriptionTxt').val(rowData.EmployeeTypeDescription);
   
    _id = rowData.EmployeeTypeId;

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

$(document).on('click', '#saveButton', function () {
    Manager.SaveEmployeeType();
});

$(document).on('click', '#editButton', function () {
    Manager.EditEmployeeType(_id);
});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().EmployeeTypeId;
    Manager.DeleteEmployeeType(_id);
});