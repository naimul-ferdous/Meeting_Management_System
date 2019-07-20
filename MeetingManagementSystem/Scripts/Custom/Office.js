var _id = null;
var dTable = null;

$(document).ready(function () {

    //  Manager.LoadAddressDDL();
    Manager.GetDataForTable(0);
    $('#clearButton').click(function () {
        Manager.ResetForm();
    });
    Manager.LoadCountryDDL();
    Manager.LoadDistrictDDL();

});

var Manager = {

    ResetForm: function () {
        $('#officeForm')[0].reset();
    },

    SaveOffice: function () {

        if (!$('#officeNameTxt').val()) {
             Message.Warning("Office name is required");
        }
        else if (!$('#houseNoTxt').val()) {
                Message.Warning('HouseNo is required');
        }
        else if (!$('#roadNoTxt').val()) {
            Message.Warning('RoadNo is required');
        }
        else if (!$('#blockTxt').val()) {
            Message.Warning('Block is required');
        }
        else if (!$('#areaTxt').val()) {
            Message.Warning('Area is required');
        } else if (!$('#postCodeTxt').val()) {
            Message.Warning('Post Code is required');
        } 
    
        else {
            if (Message.Prompt()) {
                var jsonParam = $('#officeForm').serialize();
                var serviceURL = "/Office/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }
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
    },

    EditOffice: function (id) {
        if (!$('#officeNameTxt').val()) {
            Message.Warning("Office name is required");
        }
        else if (!$('#houseNoTxt').val()) {
            Message.Warning("House No is required");
        }
        else if (!$('#roadNoTxt').val()) {
            Message.Warning("Road No is required");
        }
        else if (!$('#blockTxt').val()) {
            Message.Warning("Block is required");
        }
        else if (!$('#areaTxt').val()) {
            Message.Warning("Area is required");
        }
        else if (!$('#postCodeTxt').val()) {
            Message.Warning("Post Code is required");
        }
        else {
            if (Message.Prompt()) {
                var jsonParam = $('#officeForm').serialize() + '&OfficeId=' + id;
                var serviceURL = "/Office/Edit/ ";
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
    DeleteOffice: function (id) {
        if (Message.Prompt()) {
            var jsonParam = { officeId: id };
            var serviceURL = "/Office/Delete/";
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

    LoadCountryDDL: function () {
        debugger;
        $.ajax({
            url: '/Office/GetCountryInfo/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#CountryId').append('<option value="0" selected ="Selected">Select Country </option>');
                $.each(jsonData, function (key, value) {
                    $('#CountryId').append(
                        '<option value="' + value.CountryId + '">' + value.CountryName + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },

    LoadDistrictDDL: function () {
        debugger
        $.ajax({
            url: '/Office/GetDistrictInfo/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#DistrictId').append('<option value="0" selected ="Selected">Select District </option>');
                $.each(jsonData, function (key, value) {
                    $('#DistrictId').append(
                        '<option value="' + value.DistrictId + '">' + value.DistrictName + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },


    GetDataForTable: function (refresh) {
        var jsonParam = '';
        var serviceURL = "/Office/Index/";
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
                        title: 'Office'
                    }, 'print', 'pdfHtml5'
                ],

                scrollY: "300px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[5, 10, 15, 40], [5, 10, 15, 40, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { className: "text-left", targets: [0, 1, 2, 3, 4, 5, 6] }
                ],
                columns: [
                    {
                        data: 'OfficeName',
                        name: 'OfficeName',
                        title: 'Office',
                        width: 50,
                    },
                    {
                        data: 'HouseNo',
                        name: 'HouseNo',
                        title: 'HouseNo',
                        width: 100,
                    },
                    {
                        data: 'RoadNo',
                        name: 'RoadNo',
                        title: 'RoadNo',
                        width: 50,
                    },
                    {
                        data: 'Area',
                        name: 'Area',
                        title: 'Area',
                        width: 50,
                    },
                    {
                        data: 'CountryName',
                        name: 'CountryName',
                        title: 'CountryName',
                        width: 80,
                    },

                    {
                        data: 'DistrictName',
                        name: 'DistrictName',
                        title: 'DistrictName',
                        width: 50,
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
                data: data

            });
        } else {
            dTable.clear().rows.add(data).draw();
        }
    }
}

$(document).on('click', '.spnDataTableEdit', function () {
    var rowData = dTable.row($(this).parent()).data();
    $('#officeNameTxt').val(rowData.OfficeName);
    $('#houseNoTxt').val(rowData.HouseNo);
    $('#roadNoTxt').val(rowData.RoadNo);
    $('#blockTxt').val(rowData.Block);
    $('#areaTxt').val(rowData.Area);
    $('#postCodeTxt').val(rowData.PostCode);
    $('#CountryId').val(rowData.CountryId);
    $('#DistrictId').val(rowData.DistrictId);
    _id = rowData.OfficeId;



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
    Manager.SaveOffice();
});

$(document).on('click', '#editButton', function () {
    Manager.EditOffice(_id);
});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().OfficeId;
    Manager.DeleteOffice(_id);
});

$('#CountryId').change(function () {
    var countryId = $('#CountryId').val();
    var districtId = $('#DistrictId');

    var jsonData = (countryId, districtId);
    debugger;
    $.ajax({
        url: '/Office/GetDistrictsByCountry/',
        data: jsonData,
        type: "POST",
        data: { countryId: countryId },
        datatype: "Json",
        success: function (response) {
            $(districtId).empty();

            var options = "<option>Select.....</option>";
            $.each(response,
                function (key, district) {
                    options += "<option value='" + district.DistrictId + "'>" + district.DistrictName + "</option>";
                });
            $(districtId).append(options);
        },
        error: function () {

        }
    });
    //GetDistrictByCountry(countryId, districtId);

});