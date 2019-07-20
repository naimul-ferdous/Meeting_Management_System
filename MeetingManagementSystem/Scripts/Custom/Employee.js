var _id = null;
var dTable = null;

$(document).ready(function () {

    Manager.LoadDepartmentDDL();
    Manager.LoadDesignationDDL();
    Manager.LoadOfficeDDL();
    Manager.LoadEmployeeTypeDDL();
    Manager.GetDataForTable(0);
    

});
$('#clearButton').click(function () {
    resetValidation();
});
$('#closeIcon').click(function () {
    resetValidation();
});
$('#closeButton').click(function () {
    resetValidation();
});
$('#openModalButton').click(function () {
    Manager.ResetForm();
});
var Manager = {

    ResetForm: function () {
        $('#clearButton').show();
        $('#OfficialId').show();
        $('#passwordBlock').show();
        $('#employeeForm')[0].reset();

    },

    SaveEmployee: function () {
        //if (!$('#employeeNameTxt').val()) {
        //    Message.Warning("Employee name is required");
        //    //$('#employeeNameTxt').val('Employee name is required')
        //}

        //else if (!$('#emailTxt').val()) {
        //    Message.Warning("Email is required");
        //}
        //else if (!$('#Email').val()) {
        //    Message.Warning("Email is required");
        //}
        //else if (!$('#phoneNumberTxt').val()) {
        //    Message.Warning("Phone number is required");
        //}
        //else if (!$('#addressTxt').val()) {
        //    Message.Warning("Address is required");
        //}
        //else if (!$('#passwordTxt').val()) {
        //    Message.Warning("Password is required");
        //} else if (!$('#DepartmentId').val()) {
        //    Message.Warning("Department Name is required");
        //}

        //else {
        //    if (Message.Prompt()) {
        //        var jsonParam = $('#employeeForm').serialize();
        //        var serviceURL = "/Employee/Create/";
        //        AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        //    }
        //}
        var isvalid = $("#employeeForm").valid();  // Tells whether the form is valid

        if (isvalid) {
            if (Message.Prompt()) {
                var jsonParam = $('#employeeForm').serialize();
                var serviceURL = "/Employee/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }
        };

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

    EditEmployee: function (id) {

        //if (!$('#employeeNameTxt').val()) {
        //    Message.Warning("Employee name is required");
        //    //$('#employeeNameTxt').val('Employee name is required')
        //} else if (!$('#emailTxt').val()) {
        //    Message.Warning("Email is required");
        //} else if (!$('#phoneNumberTxt').val()) {
        //    Message.Warning("Phone number is required");
        //} else if (!$('#addressTxt').val()) {
        //    Message.Warning("Address is required");
        //} else if (!$('#passwordTxt').val()) {
        //    Message.Warning("Password is required");
        //} else if (!$('#DepartmentId').val()) {
        //    Message.Warning("Department Name is required");
        //} else {
        //    if (Message.Prompt()) {
        //        var jsonParam = $('#employeeForm').serialize() + '&EmployeeId=' + id;
        //        var serviceURL = "/Employee/Edit/ ";
        //        AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        //    }
        var isvalid = $("#employeeForm").valid();  // Tells whether the form is valid

        if (isvalid) {
            if (Message.Prompt()) {
                var jsonParam = $('#employeeForm').serialize() + '&EmployeeId=' + id;
                var serviceURL = "/Employee/Edit/ ";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }
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

    },
    DeleteEmployee: function (id) {

        if (Message.Prompt()) {
            var jsonParam = { employeeId: id };
            var serviceURL = "/Employee/Delete/";
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

    LoadDepartmentDDL: function () {
        
        $.ajax({
            url: '/Department/Index/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#DepartmentId').append('<option value="0" selected ="Selected">Select Department </option>');
                $.each(jsonData, function (key, value) {
                    $('#DepartmentId').append(
                        '<option value="' + value.DepartmentId + '">' + value.DepartmentName + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },

    LoadDesignationDDL: function () {
        
        $.ajax({
            url: '/Employee/GetDesignationInfo/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#DesignationId').append('<option value="0" selected ="Selected">Select Designation </option>');
                $.each(jsonData, function (key, value) {
                    $('#DesignationId').append(
                        '<option value="' + value.DesignationId + '">' + value.DesignationName + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },

    LoadOfficeDDL: function () {
       
        $.ajax({
            url: '/Employee/GetOfficeInfo/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#OfficeId').append('<option value="0" selected ="Selected">Select Office </option>');
                $.each(jsonData, function (key, value) {
                    $('#OfficeId').append(
                        '<option value="' + value.OfficeId + '">' + value.OfficeName + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },

    LoadEmployeeTypeDDL: function () {
        
        $.ajax({
            url: '/Employee/GetEmployeeTypeInfo/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#EmployeeTypeId').append('<option value="0" selected ="Selected">Select Employee Type </option>');
                $.each(jsonData, function (key, value) {
                    $('#EmployeeTypeId').append(
                        '<option value="' + value.EmployeeTypeId + '">' + value.EmployeeTypeName + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },

    GetDataForTable: function (refresh) {
        var jsonParam = '';
        var serviceURL = "/Employee/Index/";
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
                        title: 'Employee'
                    }, 'print', 'pdfHtml5'
                ],

                scrollY: "300px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[5, 10, 15, 40], [5, 10, 15, 40, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { className: "text-left", targets: [0, 1, 2, 3, 4, 5, 6, 7, 8] }
                ],
                columns: [
                    {
                        data: 'EmployeeName',
                        name: 'EmployeeName',
                        title: 'EmpName',
                        width: 100,
                    },
                    {
                        data: 'Email',
                        name: 'Email',
                        title: 'Email',
                        width: 100,
                    }, {
                        data: 'PhoneNumber',
                        name: 'PhoneNumber',
                        title: 'ContactNo',
                        width: 100,
                    },
                    {
                        data: 'Address',
                        name: 'Address',
                        title: 'Address',
                        width: 100,
                    },
                    {
                        data: 'DepartmentName',
                        name: 'DepartmentName',
                        title: 'Department',
                        width: 100,
                    }, {
                        data: 'DesignationName',
                        name: 'DesignationName',
                        title: 'Designation',
                        width: 120,
                    }, {
                        data: 'OfficeName',
                        name: 'OfficeName',
                        title: 'Office',
                        width: 100,
                    }, {
                        data: 'EmployeeTypeName',
                        name: 'EmployeeTypeName',
                        title: 'EmpType',
                        width: 100,
                    },

                    {
                        name: 'Option',
                        title: 'Option',
                        width: 50,

                        render: function (data, type, row) {
                            var deleteBtn = '';
                            var detailsBtn = '<span class="glyphicon glyphicon-info-sign spnDataTableDetails" id="detailsBtn" title="Details"></span>';
                            deleteBtn = '<span class="glyphicon glyphicon-trash spnDataTableDelete" id="deleteBtn" title="Click to delete"></span>';
                            return detailsBtn + '<span class="glyphicon glyphicon-edit spnDataTableEdit id="editButton" title="Edit"></span>' + deleteBtn;
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
    //$('#employeeNameTxt').val(rowData.EmployeeName);
    //$('#emailTxt').val(rowData.Email);
    //$('#phoneNumberTxt').val(rowData.PhoneNumber);
    //$('#addressTxt').val(rowData.Address);
    //$('#passwordTxt').val(rowData.Password);
    //$('#DepartmentId').val(rowData.DepartmentId);
    //$('#DesignationId').val(rowData.DesignationId);
    //$('#OfficeId').val(rowData.OfficeId);
    //$('#EmployeeTypeId').val(rowData.EmployeeTypeId);
    _id = rowData.EmployeeId;
    $('#EmployeeId').val(rowData.EmployeeId);
    $('#EmployeeOfficialId').val(rowData.EmployeeOfficialId);
    $('#EmployeeName').val(rowData.EmployeeName);
    $('#Email').val(rowData.Email);
    $('#PhoneNumber').val(rowData.PhoneNumber);
    $('#Password').val(rowData.Password);
    $('#ConfirmPassword').val(rowData.Password);
    $('#Address').val(rowData.Address);
    $('#IsActive').prop('checked',rowData.IsActive);


    $('#DepartmentId').val(rowData.DepartmentId);
    $('#DesignationId').val(rowData.DesignationId);
    $('#OfficeId').val(rowData.OfficeId);
    $('#EmployeeTypeId').val(rowData.EmployeeTypeId);
    $('#OfficialId').hide();
    $('#passwordBlock').hide();
    $('#clearButton').hide();

    $('#saveButton').text('Update');
    $('#myModalLabel').text('Edit Employee');
    $('#saveButton').removeClass('btn-success');
    $('#saveButton').addClass('btn-warning');
    $('#saveButton').prop('id', 'editButton');
    $('#myModal').modal('show');
});

$("#myModal").on('hidden.bs.modal', function () {
    $('#EmployeeId').removeAttr('value');;
    $('#editButton').text('Save');
    $('#myModalLabel').text('Add Employee');
    $('#editButton').removeClass('btn-warning');
    $('#editButton').addClass('btn-success');
    $('#editButton').prop('id', 'saveButton');
});

$(document).on('click', '#saveButton', function () {
    Manager.SaveEmployee();
});

$(document).on('click', '#editButton', function () {
    Manager.EditEmployee(_id);
});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().EmployeeId;
    Manager.DeleteEmployee(_id);
});

$(document).on('click', '.spnDataTableDetails', function () {
    var rowData = dTable.row($(this).parent()).data();
    $('#employeeNameShow').text(rowData.EmployeeName);
    $('#emailShow').text(rowData.Email);
    $('#phoneNumberShow').text(rowData.PhoneNumber);
    $('#addressShow').text(rowData.Address);

    $('#departmentNameShow').text(rowData.DepartmentName);
    $('#designationNameShow').text(rowData.DesignationName);
    $('#officeNameShow').text(rowData.OfficeName);
    $('#employeeTypeNameShow').text(rowData.EmployeeTypeName);
    $('#myModalForEdit').modal('show');
});
//re-set all client validation given a jQuery selected form or child
    function resetValidation () {
        debugger;
        var $form = $('#employeeForm');

        //reset jQuery Validate's internals
        $form.validate().resetForm();

        //reset unobtrusive validation summary, if it exists
        $form.find("[data-valmsg-summary=true]")
            .removeClass("validation-summary-errors")
            .addClass("validation-summary-valid")
            .find("ul").empty();

        //reset unobtrusive field level, if it exists
        $form.find("[data-valmsg-replace]")
            .removeClass("field-validation-error")
            .addClass("field-validation-valid")
            .empty();

        return $form;
    };