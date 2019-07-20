//var deleteFolderUrl = '';
//var windowWidth = $(window).width();
var AjaxManager = {

    SendJson: function (serviceUrl, jsonParams, successCalback, errorCallback) {

        $.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: serviceUrl,
            data: jsonParams,
            success: successCalback,
            error: errorCallback
        });

    },

    SendJsonAsyncON: function (serviceUrl, jsonParams, successCalback, errorCallback) {

        $.ajax({
            cache: false,
            async: false,
            type: "GET",
            url: serviceUrl,
            data: jsonParams,
            success: successCalback,
            error: errorCallback
        });

    },

    populateCombo: function (container, data, defaultText) {
        var cbmOptions = "<option value=\"0\">" + defaultText + "</option>";
        $.each(data, function () {
            cbmOptions += '<option value=\"' + this.Id + '\">' + this.Name + '</option>';
        });

        $('#' + container).html(cbmOptions);

    },

    changeDateFormat: function (value, isTime) {
        var dateFormat = "";
        if (value != "" && value != null) {
            var time = value.replace(/\/Date\(/g, "").replace(/\)\//g, "");
            var date = new Date();
            date.setTime(time);
            var dd = (date.getDate().toString().length == 2 ? date.getDate() : '0' + date.getDate()).toString();
            var mm = ((date.getMonth() + 1).toString().length == 2 ? (date.getMonth() + 1) : '0' + (date.getMonth() + 1)).toString();
            var yyyy = date.getFullYear().toString();
            var timeformat = "";
            if (isTime != 0) {
                timeformat = (date.getHours().toString().length == 2 ? date.getHours() : '0' + date.getHours()) + ':' + (date.getMinutes().toString().length == 2 ? date.getMinutes() : '0' + date.getMinutes()) + ':' + (date.getSeconds().toString().length == 2 ? date.getSeconds() : '0' + date.getSeconds());
                dateFormat = dd + '-' + mm + '-' + yyyy + ' ' + timeformat;
            } else {
                dateFormat = mm + '/' + dd + '/' + yyyy;
            }
        }
        return dateFormat;
    },


    changeToSQLDateFormat: function (value, isTime) {
        if (value != "" && value != null) {
            var time = value.replace(/\/Date\(/g, "").replace(/\)\//g, "");
            var date = new Date();
            date.setTime(time);
            var dd = (date.getDate().toString().length == 2 ? date.getDate() : '0' + date.getDate()).toString();
            var mm = ((date.getMonth() + 1).toString().length == 2 ? (date.getMonth() + 1) : '0' + (date.getMonth() + 1)).toString();
            var yyyy = date.getFullYear().toString();
            var timeformat = "";
            var sqlFormatedDate = "";
            //if (isTime !=0) {
            //    timeformat = '<br> ' + (date.getHours().toString().length == 2 ? date.getHours() : '0' + date.getHours()) + ':' + (date.getMinutes().toString().length == 2 ? date.getMinutes() : '0' + date.getMinutes()) + ':' + (date.getSeconds().toString().length == 2 ? date.getSeconds() : '0' + date.getSeconds());
            //    sqlFormatedDate = dd + '-' + mm + '-' + yyyy + ' ' + timeformat;
            //}
            //else {
            sqlFormatedDate = mm + '/' + dd + '/' + yyyy;
            // }
            return sqlFormatedDate;
        }

    },
    changeToSQLDateFormat1: function (value, isTime) {

        if (value != "" && value != null) {
            var time = value.replace(/\/Date\(/g, "").replace(/\)\//g, "");
            var date = new Date();
            date.setTime(time);
            var dd = (date.getDate().toString().length == 2 ? date.getDate() : '0' + date.getDate()).toString();
            var mm = ((date.getMonth() + 1).toString().length == 2 ? (date.getMonth() + 1) : '0' + (date.getMonth() + 1)).toString();
            var yyyy = date.getFullYear().toString();
            var timeformat = "";
            var sqlFormatedDate = "";
            //if (isTime !=0) {
            //    timeformat = '<br> ' + (date.getHours().toString().length == 2 ? date.getHours() : '0' + date.getHours()) + ':' + (date.getMinutes().toString().length == 2 ? date.getMinutes() : '0' + date.getMinutes()) + ':' + (date.getSeconds().toString().length == 2 ? date.getSeconds() : '0' + date.getSeconds());
            //    sqlFormatedDate = dd + '-' + mm + '-' + yyyy + ' ' + timeformat;
            //}
            //else {
            sqlFormatedDate = dd + '-' + mm + '-' + yyyy;
            // }
            return sqlFormatedDate;
        } else {
            return "";
        }

    },
    changeToSQLDateTimeFormat: function (value, isTime) {

        if (value != "" && value != null) {
            //&& value != "/Date(-6816290400000)/" && value != "/Date(-62135596800000)/" && value != null
            var time = value.replace(/\/Date\(/g, "").replace(/\)\//g, "");
            var date = new Date();
            date.setTime(time);
            var dd = (date.getDate().toString().length == 2 ? date.getDate() : '0' + date.getDate()).toString();
            var mm = ((date.getMonth() + 1).toString().length == 2 ? (date.getMonth() + 1) : '0' + (date.getMonth() + 1)).toString();
            var yyyy = date.getFullYear().toString();
            var timeformat = "";
            var sqlFormatedDate = "";
            if (isTime != 0) {
                timeformat = '<br> ' + (date.getHours().toString().length == 2 ? date.getHours() : '0' + date.getHours()) + ':' + (date.getMinutes().toString().length == 2 ? date.getMinutes() : '0' + date.getMinutes()) + ':' + (date.getSeconds().toString().length == 2 ? date.getSeconds() : '0' + date.getSeconds());
                sqlFormatedDate = dd + '-' + mm + '-' + yyyy + ' ' + timeformat;
            } else {
                sqlFormatedDate = dd + '-' + mm + '-' + yyyy;
            }
            return sqlFormatedDate;
        } else {
            return "";
        }

    },

    changeToSQLDateTimeFormatMMddyyyy: function (value, isTime) {

        if (value != "" && value != null) {
            var time = value.replace(/\/Date\(/g, "").replace(/\)\//g, "");
            var date = new Date();
            date.setTime(time);
            var dd = (date.getDate().toString().length == 2 ? date.getDate() : '0' + date.getDate()).toString();
            var mm = ((date.getMonth() + 1).toString().length == 2 ? (date.getMonth() + 1) : '0' + (date.getMonth() + 1)).toString();
            var yyyy = date.getFullYear().toString();
            var timeformat = "";
            var sqlFormatedDate = "";
            if (isTime != 0) {
                timeformat = (date.getHours().toString().length == 2 ? date.getHours() : '0' + date.getHours()) + ':' + (date.getMinutes().toString().length == 2 ? date.getMinutes() : '0' + date.getMinutes()) + ':' + (date.getSeconds().toString().length == 2 ? date.getSeconds() : '0' + date.getSeconds());
                sqlFormatedDate = mm + '/' + dd + '/' + yyyy + ' ' + timeformat;
            } else {
                sqlFormatedDate = mm + '/' + dd + '/' + yyyy;
            }
            return sqlFormatedDate;
        } else {
            return "";
        }

    },

    changeToSQLYearFormat: function (value, isTime) {

        if (value != "" && value != null) {
            var time = value.replace(/\/Date\(/g, "").replace(/\)\//g, "");
            var date = new Date();
            date.setTime(time);
            var dd = (date.getDate().toString().length == 2 ? date.getDate() : '0' + date.getDate()).toString();
            var mm = ((date.getMonth() + 1).toString().length == 2 ? (date.getMonth() + 1) : '0' + (date.getMonth() + 1)).toString();
            var yyyy = date.getFullYear().toString();
            var timeformat = "";
            var sqlFormatedDate = "";
            //if (isTime !=0) {
            //    timeformat = '<br> ' + (date.getHours().toString().length == 2 ? date.getHours() : '0' + date.getHours()) + ':' + (date.getMinutes().toString().length == 2 ? date.getMinutes() : '0' + date.getMinutes()) + ':' + (date.getSeconds().toString().length == 2 ? date.getSeconds() : '0' + date.getSeconds());
            //    sqlFormatedDate = dd + '-' + mm + '-' + yyyy + ' ' + timeformat;
            //}
            //else {
            sqlFormatedDate = yyyy;
            // }
            return sqlFormatedDate;
        }
    },

    jqGridDate: function (el, cellval, opts) {
        //
        if (cellval != "" && cellval != "/Date(-6816290400000)/" && cellval != "/Date(-62135596800000)/")
            //            
            $(el).html(AjaxManager.changeToSQLDateFormat(cellval, 0));
    },
    jqGridDateTime: function (el, cellval, opts) {
        if (cellval != "" && cellval != "/Date(-6816290400000)/" && cellval != "/Date(-62135596800000)/")
            $(el).html(AjaxManager.changeDateFormat(cellval, 1));
    },
    changeTimeFormat: function (value) {
        var time = value.Hours + ':' + value.Minutes;
        return time;
    },
    jqGridTime: function (el, cellval, opts) {
        if (cellval != "" && cellval != "/Date(-6816290400000)/" && cellval != "/Date(-62135596800000)/")
            $(el).html(AjaxManager.changeTimeFormat(cellval));
    },
    DMYToMDY: function (value) {
        var datePart = value.match(/\d+/g);
        var day = datePart[0];
        var month = datePart[1];
        var year = datePart[2];
        return month + '/' + day + '/' + year;
    },
    MDYToDMY: function (value) {
        var datePart = value.match(/\d+/g);
        var month = datePart[0];
        var day = datePart[1];
        var year = datePart[2];
        return day + '/' + month + '/' + year;
    },

    DMYToYMD: function (value) {
        var datePart = value.match(/\d+/g);
        var day = datePart[0];
        var month = datePart[1];
        var year = datePart[2];
        return year + '/' + month + '/' + day;
    },

    MDYToDashDMY: function (value) {
        if (value != "") {
            var datePart = value.match(/\d+/g);
            var month = datePart[0];
            var day = datePart[1];
            var year = datePart[2];
            return day + '-' + month + '-' + year;
        }
    },

    dateDiff: function (value) {
        var dte = new Date(value.replace(/(\d{2})-(\d{2})-(\d{4})/, "$2/$1/$3")) / (1000 * 60 * 60 * 24);
        return dte;
    },
    HourDiff: function (value) {
        var dte = new Date(value.replace(/(\d{2})-(\d{2})-(\d{4})/, "$2/$1/$3")) / (1000 * 60 * 60);
        return dte;
    },

}