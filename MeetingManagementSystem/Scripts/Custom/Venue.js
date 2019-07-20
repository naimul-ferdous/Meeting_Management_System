var _id = null;
var dTable = null;

$(document).ready(function () {

    Manager.GetDataForTable(0);

    Manager.LoadVenueTypeDDL();

    Manager.LoadCountryDDL();

    Manager.LoadDistrictDDL();


});

var Manager = {

    ResetForm: function () {
        $('#venueForm')[0].reset();
    },

    SaveVenue: function () {
        if (!$('#venueNameTxt').val()) {
            Message.Warning("Venue name is required");
            //$('#venueNameTxt').val('Employee name is required')
        }

        else if (!$('#capacityTxt').val()) {
            Message.Warning("Capacity is required");
        }
        else if (!$('#VenueType').val()) {
            Message.Warning("Venue Type is required");
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
                var jsonParam = $('#venueForm').serialize();
                var serviceURL = "/Venue/Create/";
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

    EditVenue: function (id) {
        debugger;
        if (!$('#venueNameTxt').val()) {
            Message.Warning("Venue name is required");
            //$('#venueNameTxt').val('Employee name is required')
        }

        else if (!$('#capacityTxt').val()) {
            Message.Warning("Capacity is required");
        }
        else if (!$('#VenueType').val()) {
            Message.Warning("Venue Type is required");
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
                var jsonParam = $('#venueForm').serialize() + '&VenueId=' + id;
                var serviceURL = "/Venue/Edit/ ";
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

    DeleteVenue: function (id) {
        if (Message.Prompt()) {
            var jsonParam = { venueId: id };
            var serviceURL = "/Venue/Delete/";
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

    LoadVenueTypeDDL: function () {
        $.ajax({
            url: '/Venue/GetVenueTypeList/',
            type: "GET",
            datatype: "Json",
            success: function (jsonData) {
                $('#VenueType').append('<option value="0" selected ="Selected">Select Venue Type </option>');
                $.each(jsonData, function (key, value) {
                    $('#VenueType').append(
                        '<option value="' + value.Value + '">' + value.Text + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },

    LoadCountryDDL: function () {
        debugger
        $.ajax({
            url: '/Venue/GetCountryInfo/',
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
            url: '/Venue/GetDistrictInfo/',
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
        var serviceURL = "/Venue/GetVenueWithAddressInfo/";
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
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
                        title: 'Venue'
                    }, 'print', 'pdfHtml5'
                ],

                scrollY: "300px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[5, 10, 15, 40], [5, 10, 15, 40, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { className: "dt-center", targets: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10] }
                ],
                columns: [
                    {
                        data: 'VenueName',
                        name: 'VenueName',
                        title: 'Venue Name',
                        width: 120,
                    },
                    {
                        data: 'Capacity',
                        name: 'Capacity',
                        title: 'Capacity',
                        width: 70
                    },
                    {
                        
                        data: 'VenueType',
                        name: 'VenueType',
                        title: 'Venue Type',
                        width: 120,
                        render: function (data, type, row) {
                            switch (data) {
                                case 1:
                                    data = 'Internal';
                                break;
                                case 2:
                                    data = 'External';
                                break;
                            default:
                                data= 'N/A';
                                break;
                            }
                            return data;
                           // return data === 1 ? "Internal" : "External";
                        }
                    },
                    {
                        data: 'HouseNo',
                        name: 'HouseNo',
                        title: 'House No',
                        width: 100,
                    },

                    {
                        data: 'RoadNo',
                        name: 'RoadNo',
                        title: 'Road No',
                        width: 80,
                    },

                    {
                        data: 'Area',
                        name: 'Area',
                        title: 'Area',
                        width: 120,
                    },

                    {
                        data: 'Block',
                        name: 'Block',
                        title: 'Block',
                        width: 80,
                    },

                    {
                        data: 'PostCode',
                        name: 'PostCode',
                        title: 'Post Code',
                        width: 100,
                    },

                    {
                        data: 'CountryName',
                        name: 'Country',
                        title: 'Country',
                        width: 100,
                    },

                    {
                        data: 'DistrictName',
                        name: 'District',
                        title: 'District',
                        width: 100,
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

$(document).on('click', '#saveButton', function () {
    Manager.SaveVenue();
});

$(document).on('click', '.spnDataTableEdit', function () {
    var rowData = dTable.row($(this).parent()).data();
    $('#venueNameTxt').val(rowData.VenueName);
    $('#capacityTxt').val(rowData.Capacity);
    $('#VenueType').val(rowData.VenueType);
    $('#houseNoTxt').val(rowData.HouseNo);
    $('#roadNoTxt').val(rowData.RoadNo);
    $('#blockTxt').val(rowData.Block);
    $('#areaTxt').val(rowData.Area);
    $('#postCodeTxt').val(rowData.PostCode);
    $('#CountryId').val(rowData.CountryId);
    $('#DistrictId').val(rowData.DistrictId);
    _id = rowData.VenueId;

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
    Manager.EditVenue(_id);
});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().VenueId;
    Manager.DeleteVenue(_id);
});

$('#CountryId').change(function() {
    var countryId = $('#CountryId').val();
    var districtId = $('#DistrictId');
   
    var jsonData = (countryId, districtId);
    debugger;
        $.ajax({
            url: '/Venue/GetDistrictsByCountry/',
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
            error: function() {

            }
        });
        //GetDistrictByCountry(countryId, districtId);

    });
