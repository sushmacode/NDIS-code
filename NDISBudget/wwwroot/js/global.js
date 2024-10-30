var apiurl = 'https://localhost:7001/api/';
var websiteurl = 'https://localhost:7001';
//var apiurl = 'http://www.tripletwosys.com/api/';
//var apiurl = window.location.protocol + '//' + window.location.hostname + '/api/';
//var apiurl = location.protocol + '://' + location.hostname + '/api/';
$(document).ready(function () {
    gettempimg();

    

    $("input").attr("autocomplete", "off");
    //$(".sidebar-dropdown > a").click(function () {
    //    $(".sidebar-submenu").slideUp(200);
    //    if (
    //      $(this)
    //        .parent()
    //        .hasClass("active")
    //    ) {
    //        $(".sidebar-dropdown").removeClass("active");
    //        $(this)
    //          .parent()
    //          .removeClass("active");
    //    } else {
    //        $(".sidebar-dropdown").removeClass("active");
    //        $(this)
    //          .next(".sidebar-submenu")
    //          .slideDown(200);
    //        $(this)
    //          .parent()
    //          .addClass("active");
    //    }
    //});
    //$("#close-sidebar").click(function () {
    //    $(".page-wrapper").removeClass("toggled");
    //});
    //$("#show-sidebar").click(function () {
    //    $(".page-wrapper").addClass("toggled");
    //});
    var pageurl = window.location.href.toLowerCase();
    if (pageurl.indexOf("communications") != -1 || pageurl.indexOf("createcommunication") != -1) {
        $("#m-communication").removeClass('hide')
    }
    else if (pageurl.indexOf("viewmembers") != -1) {
        $("#m-mem").removeClass('hide');
    }
    else if (pageurl.indexOf("surveyreports") != -1 || pageurl.indexOf("quizresult") != -1 || pageurl.indexOf("smartquizresult") != -1 || pageurl.indexOf("spellbeequizresult") != -1) {
        $("#m-report").removeClass('hide');
    }
    else
    {
        $("#m-marketing").removeClass('hide');
    }
});


$('#Primarycolor').on('change', function () {
    $('#txtprimary').val(this.value);
});
$('#txtprimary').on('change', function () {
    $('#Primarycolor').val(this.value);
});
$('#Secondarycolor').on('change', function () {
    $('#txtsecondary').val(this.value);
});
$('#txtsecondary').on('change', function () {
    $('#Secondarycolor').val(this.value);
});
$('#Thirdcolor').on('change', function () {
    $('#txtthird').val(this.value);
});
$('#txtthird').on('change', function () {
    $('#Thirdcolor').val(this.value);
});
$('#Textcolor').on('change', function () {
    $('#Txtcolor').val(this.value);
});
$('#Txtcolor').on('change', function () {
    $('#Textcolor').val(this.value);
});

function gettempimg() {
    $("#Image").change(function (e) {
        readURL(this);
        PicName = e.target.files[0].name;
    });
    $("#background").change(function (e) {
        readURL(this);
        PicName = e.target.files[0].name;
    });
    $("#banner").change(function (e) {
        readURL(this);
        PicName = e.target.files[0].name;
    });
    $("#Menu").change(function (e) {
        readURL(this);
        PicName = e.target.files[0].name;
    });
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var arrayBuffer = this.result,
            array = new Uint8Array(arrayBuffer),
            binaryString = String.fromCharCode.apply(null, array);
            $("#hidbinary").val(arrayBuffer)
            //console.log(binaryString);
            $('#tempimg').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var arrayBuffer = this.result,
            array = new Uint8Array(arrayBuffer),
            binaryString = String.fromCharCode.apply(null, array);
            $("#hidbinary").val(arrayBuffer)
            //console.log(binaryString);
            $('#backtempimg').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var arrayBuffer = this.result,
            array = new Uint8Array(arrayBuffer),
            binaryString = String.fromCharCode.apply(null, array);
            $("#hidbinary").val(arrayBuffer)
            //console.log(binaryString);
            $('#bannertempimg').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var arrayBuffer = this.result,
            array = new Uint8Array(arrayBuffer),
            binaryString = String.fromCharCode.apply(null, array);
            $("#hidbinary").val(arrayBuffer)
            //console.log(binaryString);
            $('#menutempimg').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function readMultiURL(input, tgresultdiv) {
    var imgs = [];
    $(tgresultdiv).html('');
    $(input.files).each(function () {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(tgresultdiv).append('<img src="' + e.target.result + '" class="fileupimgprev" style="padding:1px;border:1px #000;">');
        }
        reader.readAsDataURL(this);
    });

}
function Getgroupsddl() {
    $.get(apiurl + "getgroupsdropdown", { orgid: $("#orgid").val() }, function (response) {
        $(response).each(function (i, data) {
            //console.log(data);
            $("#grpid").append($('<option/>', { value: data.grpid }).html(data.GroupName));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#grpid").attr('bindval');
    $("#grpid").val(defvalue);
}

function GetCountryddl() {
    gl.ajaxreq(apiurl + "GetCountries", "get", {}, function (response) {
        $(response).each(function (i, data) {
            $("#CountryId").append($('<option/>', { value: data.CountryId }).html(data.CountryName));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#CountryId").attr('bindval');
    $("#CountryId").val(defvalue);
}

function GetStateddl() {
    var cnid = $('#CountryId').val();
    $("#StateId").empty();
    $("#StateId").append("<option value='0'>State</option>");
    gl.ajaxreq(apiurl + "GetStatebyCountryId", "get", { cnid: cnid }, function (response) {
        $.each(response, function (index, row) {
            $("#StateId").append("<option value='" + row.StateId + "'>" + row.State + "</option>")
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#StateId").attr('bindval');
    $("#StateId").val(defvalue);
}

function GetCityddl() {
    var cnid = $('#CountryId').val();
    $("#CityId").empty();
    $("#CityId").append("<option value='0'>Select City</option>");
    gl.ajaxreq(apiurl + "GetCitybyCountryId", "get", { cnid: cnid }, function (response) {
        $.each(response, function (index, row) {
            $("#CityId").append("<option value='" + row.CityId + "'>" + row.CityName + "</option>")
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#CityId").attr('bindval');
    $("#CityId").val(defvalue);
}

function Getgroupsddl() {
    $.get(apiurl + "getgroupsdropdown", { orgid: $("#orgid").val() }, function (response) {
        //$("#ParticipantId").append("<option value=0></option>");
        $(response).each(function (i, data) {
            //console.log(data);
            $("#grpid").append($('<option/>', { value: data.grpid }).html(data.GroupName));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#grpid").attr('bindval');
    $("#grpid").val(defvalue);
}

function GetOrganizationsddl() {
    gl.ajaxreq(apiurl + "getorganizationddl", "get", {}, function (response) {

        $(response).each(function (i, data) {
            $("#orgid").append($('<option/>', { value: data.OrgId }).html(data.OrgName));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#orgid").attr('bindval');
    $("#orgid").val(defvalue);
    $("#orgid").trigger('change');
}

function GetGroupTypeddl() {
    gl.ajaxreq(apiurl + "GetGroupTypesddl", "get", {}, function (response) {

        $(response).each(function (i, data) {
            $("#grouptypeid").append($('<option/>', { value: data.GroupTypeId }).html(data.GroupType));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#grouptypeid").attr('bindval');
    $("#grouptypeid").val(defvalue);
}

function GetPartnerTypeddl() {
    gl.ajaxreq(apiurl + "GetPartnerTypesddl", "get", {}, function (response) {

        $(response).each(function (i, data) {
            $("#PartnerTypeId").append($('<option/>', { value: data.PartnerTypeId }).html(data.PartnerType));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#PartnerTypeId").attr('bindval');
    $("#PartnerTypeId").val(defvalue);
}
function Partnersddl() {
    var orgid = $('#orgid').val();
    gl.ajaxreq(apiurl + "GetddlPartners", "get", { orgid: orgid }, function (response) {

        $(response).each(function (i, data) {
            $("#PartnerId").append($('<option/>', { value: data.PartnerId }).html(data.PartnerName));
        });
    }, '', '', '', '', false, false);
    var defvalue =String($("#PartnerId").attr('bindvalue')).split(',');
    $("#PartnerId").val(defvalue);
}
function GetLocationddl() {
    var pid = $('#pid').val();
    gl.ajaxreq(apiurl + "GetddlBusinessLocations", "get", { pid: pid }, function (response) {

        $(response).each(function (i, data) {
            $("#ddLocations").append($('<option/>', { value: data.LocationId }).html(data.LocationName));
        });
    }, '', '', '', '', false, false);
    var defvalue = String($("#ddLocations").attr('bindvalue')).split(',');
    $("#ddLocations").val(defvalue);
}

function GetOrganizationTypeddl() {
    gl.ajaxreq(apiurl + "getorganizationtypes", "get", {}, function (response) {

        $(response).each(function (i, data) {
            $("#orgtypeid").append($('<option/>', { value: data.OrgTypeId }).html(data.OrgType));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#orgtypeid").attr('bindval');
    $("#orgtypeid").val(defvalue);
}


function GetSearchTagddl() {
    gl.ajaxreq(apiurl + "getsearchtagddl", "get", {}, function (response) {

        $(response).each(function (i, data) {
            $("#searchtagids").append($('<option/>', { value: data.SearchTagIds }).html(data.SearchTag));
        });
    }, '', '', '', '', false, false);
    var defvalue = String($("#searchtagids").attr('bindval')).split(',');
    $("#searchtagids").val(defvalue);
}

function GetFrequencyddl() {
    gl.ajaxreq(apiurl + "GetFrequencyddl", "get", {}, function (response) {
        $(response).each(function (i, data) {
            $("#gamefrequency").append($('<option/>', { value: data.GameFrequencyId }).html(data.Frequency));
        });
    }, '', '', '', '', false, false);
    var defvalue = $("#gamefrequency").attr('bindvalue');
    $("#gamefrequency").val(defvalue);
}

$(function () {
    if (window.jQuery().datetimepicker) {
        $('.datetime').datetimepicker({
            format: 'DD-MMM-YYYY hh:mm a',
            widgetPositioning: {
                horizontal: 'right',
                vertical: 'bottom'
            },
            // sideBySide: true,
            icons: {
                time: 'fa fa-clock',
                date: 'fa fa-calendar',
                up: 'fa fa-chevron-up',
                down: 'fa fa-chevron-down',
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-check',
                clear: 'fa fa-trash',
                close: 'fa fa-times'
            }
        });

        $('.date').datetimepicker({
            format: 'DD-MMM-YYYY',
            widgetPositioning: {
                horizontal: 'right',
                vertical: 'bottom'
            },
            // sideBySide: true,
            icons: {
                date: 'fa fa-calendar',
                up: 'fa fa-chevron-up',
                down: 'fa fa-chevron-down',
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-check',
                clear: 'fa fa-trash',
                close: 'fa fa-times'
            }
        });
    }
});

// Check html5 support
function IsHtml5Compatible() {
    var test_canvas = document.createElement("canvas");
    return test_canvas.getContext ? true : false;

}

function ExportToexcel(elementid, pagetitle) {
    $('#' + elementid + ' .tbldata  .hiddenprint').remove();
    var fname = pagetitle + '.xls';
    var tab_text = "<table border='1px'>";
    var textRange; var j = 0;
    var tab = document.getElementById('dataTable');
    //  tab = tab.getElementById('dataTable')[0];
    //alert(tab.rows.length);
    for (j = 0 ; j < tab.rows.length ; j++) {

        tab_text = tab_text + "<tr>" + tab.rows[j].innerHTML + "</tr>";
    }
    tab_text = tab_text + "</table>";

    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
    tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params
    //tab_text = tab_text.replace('class="hiddenprint"', 'style=display:none');
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");
    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
    {
        dumiframexls.document.open("txt/html", "replace");
        dumiframexls.document.write(tab_text);
        dumiframexls.document.close();
        dumiframexls.focus();
        sa = dumiframexls.document.execCommand("SaveAs", true, fname);
    }
    else {
        var data_type = 'data:application/vnd.ms-excel';
        var table_div = tab_text;
        var table_html = table_div.replace(/ /g, '%20');

        var link = document.getElementById('dumlnkxls');
        link.download = fname;
        link.href = data_type + ', ' + table_html;
        link.click();
    }

}

function setnavigationurl(url) {

    if (IsHtml5Compatible) {
        history.pushState("", "Protech", url);
    }
    else {
        window.location.replace(url);
    }
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function myFunction() {
    var d = new Date();
    var n = d.toLocaleString([], { hour: '2-digit', minute: '2-digit' });
    document.getElementById("time").innerHTML = n;
}

// Read a page's GET URL variables and return them as an associative array.


var gl = {
    ajaxreq: function (serviceurl, reqtype, data, OnSuccess, resctrl, msg, sucmsg, errmsg, isasync, isheader) {
        try {
            $.ajax({
                url: serviceurl,
                type: reqtype,
                headers: isheader ? { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') } : '',
                data: reqtype.toLowerCase() == 'post' ? JSON.stringify(data) : data,
                contentType: "application/json; charset=utf-8",
                dataType: 'text json',
                async: isasync,
                beforeSend: function () {
                    // ajaxprocessindicator(resctrl, msg, 1, 'suc');
                },
                complete: function () {
                    // ajaxprocessindicator(resctrl, sucmsg, 0, 'suc');
                },
                success: OnSuccess,
                error: function (jqXHR, exception) {
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    console.log(msg);
                }
            });
        }
        catch (err) {
            console.log(err.message);// ajaxprocessindicator(resctrl, errmsgprefix + errmsg, 0, 'err');
        }
    },
    ajaxreqloader: function (serviceurl, reqtype, data, OnSuccess, resctrl, msg, sucmsg, errmsg, isasync, isheader, pageloaderdiv, pagecontentdiv, datatype, isloader) {
        try {
            var pageLoader = pageloaderdiv == undefined ? '.loader' : pageloaderdiv;
            var pageContent = pagecontentdiv == undefined ? '.tblcontent' : pagecontentdiv;
            $.ajax({
                url: serviceurl,
                type: reqtype,
                //headers:  //isheader ? { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') } : '',
                data: reqtype.toLowerCase() == 'post' ? JSON.stringify(data) : data,
                contentType: "application/json; charset=utf-8",
                dataType: datatype == undefined ? 'text json' : datatype,
                async: isasync,
                beforeSend: function () {
                    if (isloader) {
                        $(pageLoader).removeClass('hide');
                        $(pageContent).hide();
                    }
                },
                complete: function () {
                    if (isloader) {
                        $(pageLoader).addClass('hide');
                        $(pageContent).show();
                    }
                },
                success: OnSuccess,
                error: function (jqXHR, exception) {
                    $(pageLoader).addClass('hide');
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    console.log(msg);
                }
            });
        }
        catch (err) { console.log(err.message); }
    },

    ajaxpartialreq: function (serviceurl, reqtype, data, OnSuccess, isasync, isheader) {
        try {
            $.ajax({
                url: serviceurl,
                type: reqtype,
                headers: isheader ? { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') } : '',
                data: reqtype.toLowerCase() == 'post' ? JSON.stringify(data) : data,
                contentType: "application/json; charset=utf-8",
                dataType: 'html',
                async: isasync,
                beforeSend: function () {
                    // ajaxprocessindicator(resctrl, msg, 1, 'suc');
                },
                complete: function () {
                    // ajaxprocessindicator(resctrl, sucmsg, 0, 'suc');
                },
                success: OnSuccess,
                error: function (jqXHR, exception) {
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    console.log(msg);
                }
            });
        }
        catch (err) {
            console.log(err.message);// ajaxprocessindicator(resctrl, errmsgprefix + errmsg, 0, 'err');
        }
    },

}


function GetPageLengthArray(reccount) {
    if (reccount <= 100) {
        return [10, 25, 50];
    }
    if (reccount <= 500) {
        return [10, 25, 50];
    }
    else {
        return [10, 25, 50];
    }
}
// to set custome pagging  -- reccount:TotalRecord Cound
function setPagging(reccount, pageindex, pagesize) {
    //console.log(reccount);
    //console.log(pageindex);
    var fromDisplayNumber = 1;
    var toDisplayNumber = 1;
    var numoffpages = 1;
    if ((parseInt(reccount) % parseInt(pagesize)) == 0) {   // number of pages divides with pagesize: ex reccount 5 ,pagesize 2 then num of pages 5/2=2 + 1=3 ;if reccount 4 then 4%2==0 so 4/2=2
        numoffpages = parseInt(reccount / (parseInt(pagesize) == -1 ? parseInt(reccount) : parseInt(pagesize)));
    }
    else {
        numoffpages = parseInt(parseInt(reccount) / parseInt(pagesize)) + 1;
    }

    if (parseInt(numoffpages) < 5) {      // 5-4 --> page index links displayed
        fromDisplayNumber = 1;
        toDisplayNumber = numoffpages;
    }
    else {
        if (parseInt(pageindex) >= parseInt(numoffpages) - 3) {
            fromDisplayNumber = parseInt(numoffpages) - 3;
            toDisplayNumber = numoffpages;
        }
        else {
            fromDisplayNumber = (parseInt(pageindex) > 1) ? (parseInt(pageindex) - 1) : parseInt(pageindex);
            toDisplayNumber = (parseInt(pageindex) > 1) ? (parseInt(pageindex) + 2) : 4;
        }
    }
    // load page size dropdown
    $('#ddlpagesize').empty();
    var pagesizes = GetPageLengthArray(reccount);
     //alert(pagesizes);
    $(pagesizes).each(function () {
        $('#ddlpagesize').append('<option value=' + this + ' ' + (parseInt(this) == parseInt(pagesize) ? 'selected' : '') + '>' + (parseInt(this) == -1 ? 'All' : this) + '</option>');
    });
    //alert(reccount);
    loadPagination(numoffpages, pageindex, fromDisplayNumber, toDisplayNumber);
    $('#totalrec').html(reccount);
    $('#showpageinfo').html('Displaying Page ' + pageindex + ' of ' + numoffpages);
}

// to load pagination bar
function loadPagination(numOfPages, pageindex, fromDisplayNumber, toDisplayNumber) {
    // load pagenation ul.
    console.log(fromDisplayNumber);
    console.log(toDisplayNumber);
    console.log(numOfPages);
    console.log(pageindex);
    $('.pagination').html('');
    $('.pagination').append('<li style="padding-left: 5px;" class=' + (parseInt(numOfPages) == 1 || parseInt(pageindex) == 1 ? 'avoid-clicks' : '') + '><a class="d-paging" href="javascript:;" _id="1"><i class="fa fa-angle-double-left" aria-hidden="true"></i></a></li>');
    $('.pagination').append('<li style="padding-left: 5px;" class=' + (parseInt(numOfPages) == 1 || parseInt(pageindex) == 1 ? 'avoid-clicks' : '') + '><a class="d-paging" href="javascript:;" _id=' + (parseInt(pageindex) - 1) + '><i class="fa fa-angle-left" aria-hidden="true"></i></a></li>');
    for (var i = fromDisplayNumber; i <= toDisplayNumber; i++) {
        if (i == pageindex) {
            $('.pagination').append('<li class="active" style="padding-left: 5px;"><a href="#" _id=' + i + '>' + i + '   </a></li>');

        }
        else {
            $('.pagination').append('<li style="padding-left: 5px;"><a class="d-paging" href="#" _id=' + i + '>' + i + '   </a></li>');
        }
    }
    $('.pagination').append('<li style="padding-left: 5px;" class=' + (parseInt(numOfPages) == 1 || parseInt(pageindex) == parseInt(numOfPages) ? 'avoid-clicks' : '') + '><a class="d-paging" href="#" _id=' + (parseInt(pageindex) + 1) + '><i class="fa fa-angle-right" aria-hidden="true"></i></a></li>');
    $('.pagination').append('<li style="padding-left: 5px;" class=' + (parseInt(numOfPages) == 1 || parseInt(pageindex) == parseInt(numOfPages) ? 'avoid-clicks' : '') + '><a class="d-paging" href="#" _id=' + parseInt(numOfPages) + '><i class="fa fa-angle-double-right" aria-hidden="true"></i></a></li>');

}



function formValidate() {

    var error = []
    $('.isvalidate').each(function () {
        var type = $(this).prop('type');
        // alert(type);
        if (type == 'text' || type == 'textarea' || type == 'password') {
            var value = $(this).val();

            if (value.trim() == '' || value == undefined) {
                error.push($(this).attr('errormsg'));
            }
            else {
                var isemail = $(this).hasClass('email');
                if (isemail) {
                    if (!validateEmail(value)) {
                        error.push('Enter Correct Email.');
                    }
                }
            }
        }
        if (type == 'select-one') {
            var value = $(this).val();
            var defult = $(this).attr('default');

            if (value == defult) {
                error.push($(this).attr('errormsg'));
            }
        }
        if (type == 'file') {
            var file = $(this).val();

            if (file == '') {


                error.push('please select File');
            }
            else {
                var acceptfiles = $(this).attr('fileformates').split(',');
                var fextension = file.split('.')[1];
                if (acceptfiles.indexOf(fextension) == -1) {
                    error.push('incorrect file formate');
                }
            }
        }
    });

    return error;
}
//preview image
function readURL(input, tgresult) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(tgresult).attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}
function validateEmail(sEmail) {
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (filter.test(sEmail)) {
        return true;
    }

    else {
        return false;

    }

}

function cycleImages() {
    var $active = $('#cycler .active');
    var $next = ($active.next().length > 0) ? $active.next() : $('#cycler img:first');
    $next.css('z-index', 2);//move the next image up the pile
    $active.fadeOut(1500, function () {//fade out the top image
        $active.css('z-index', 1).show().removeClass('active');//reset the z-index and unhide the image
        $next.css('z-index', 3).addClass('active');//make the next image the top one
    });
}

$(document).ready(function () {
    // run every 7s
    setInterval('cycleImages()', 5000);
})

$(document).ready(function () {
    $("#Searchstr").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tbldata tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});

//$(document).ready(function () {
//    $.uploadPreview({
//        input_field: "#image-upload",   // Default: .image-upload
//        preview_box: "#image-preview",  // Default: .image-preview
//        label_field: "#image-label",    // Default: .image-label
////        label_default: "Choose File",   // Default: Choose File
////        label_selected: "Change File",  // Default: Change File
////        no_label: false                 // Default: false
////    });
////});

//$(document).ready(function () {
//    $.uploadPreview({
//        input_field: "#backimage-upload",   // Default: .image-upload
//        preview_box: "#backimage-preview",  // Default: .image-preview
//        label_field: "#image-label",    // Default: .image-label
//        label_default: "Choose File",   // Default: Choose File
//        label_selected: "Change File",  // Default: Change File
//        no_label: false                 // Default: false
//    });
//});

//$(document).ready(function () {
//    $.uploadPreview({
//        input_field: "#bannerimage-upload",   // Default: .image-upload
//        preview_box: "#bannerimage-preview",  // Default: .image-preview
//        label_field: "#image-label",    // Default: .image-label
//        label_default: "Choose File",   // Default: Choose File
//        label_selected: "Change File",  // Default: Change File
//        no_label: false                 // Default: false
//    });
//});

//$(document).ready(function () {
//    $.uploadPreview({
//        input_field: "#menuimage-upload",   // Default: .image-upload
//        preview_box: "#menuimage-preview",  // Default: .image-preview
//        label_field: "#image-label",    // Default: .image-label
//        label_default: "Choose File",   // Default: Choose File
//        label_selected: "Change File",  // Default: Change File
//        no_label: false                 // Default: false
//    });
//});


//$(document).ready(function () {
//    $.uploadPreview({
//        input_field: "#image-upload",   // Default: .image-upload
//        preview_box: "#image-preview",  // Default: .image-preview
//        label_field: "#image-label",    // Default: .image-label
//        label_default: "Choose File",   // Default: Choose File
//        label_selected: "Change File",  // Default: Change File
//        no_label: false                 // Default: false
//    });
//});

//$(document).ready(function () {
//    $('.inputfile-preview').each(function () {
//        $.uploadPreview({
//            input_field: $(this).find("input"),   // Default: .image-upload
//            preview_box: $(this),  // Default: .image-preview
//            label_field: $(this).find("label"),    // Default: .image-label
//            label_default: "Choose File",   // Default: Choose File
//            label_selected: "Change File",  // Default: Change File
//            no_label: false                 // Default: false
//        });
//    });

//});

//function fileuploadimgpreview(inputfile, prvbox) {
//    $.uploadPreview({
//        input_field: inputfile,            // Default: .image-upload
//        preview_box: prvbox,             // Default: .image-preview
//        label_field: $(prvbox).children('label'),    // Default: .image-label
//        label_default: "",   // Default: Choose File
//        label_selected: "",  // Default: Change File
//        no_label: false                 // Default: false
//    }
//    );

//    $(prvbox).children('label').css("background-color", "transparent");
//    var ccoutputimg = $(inputfile).attr('data-ccoutput');
//    if (".background-image" != null) {
//        $("#labelimg").hide();
//    }

//    if (ccoutputimg != undefined) {

//        ccoutputprivewimage(inputfile, '.' + ccoutputimg);
//    }
//}

function SetQueryStDefaultVal(qs, defval) {
    var q = querySt(qs);
    q = q == undefined ? defval : q == "" ? defval : q;
    return q;
}

function GetUrlWithNoQueryStrings() { return (location.protocol + '//' + location.host + location.pathname).toLowerCase(); }

function querySt(ji) {
    hu = window.location.search.substring(1);
    hu = decodeURI(hu);
    gy = hu.split("&");
    for (i = 0; i < gy.length; i++) { ft = gy[i].split("="); if (ft[0] == ji) { return ft[1] } }
}

var app = {
    setSearchToDate: function (dt) {
        var todate = moment(dt).add(1, 'days');
        return moment(todate).format('DD MMM YYYY');
    },
    checkAndSetNullDefVal: function (n, l) { return (n == null || n == "null" || n == "" || n == "undefined" || n == undefined || n == "") ? l : n },
    /**
    * set priceformat
    * @param {any} p
    */
    setPriceFormat: function (p) { if (p == null || p == "" || p == "undefined" || p == "0") { return "0.00"; } else { if (p.toString().indexOf('.') == -1) { return p.toString() + ".00"; } else { return p; } } },
    /**
  *Input number validate. 
  */
    validateNumber: function (e) { var i = function (value) { return /^\d*$/.test(value); };["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (t) { e.addEventListener(t, function () { i(this.value) ? (this.oldValue = this.value, this.oldSelectionStart = this.selectionStart, this.oldSelectionEnd = this.selectionEnd) : this.hasOwnProperty("oldValue") ? (this.value = this.oldValue, this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd)) : this.value = "" }) }) },
    /**
   *Input alpha numberic validate. 
   */
    validateAlphaNumeric: function (e) { var i = function (value) { return /^[a-zA-Z0-9]*$/.test(value); };["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (t) { e.addEventListener(t, function () { i(this.value) ? (this.oldValue = this.value, this.oldSelectionStart = this.selectionStart, this.oldSelectionEnd = this.selectionEnd) : this.hasOwnProperty("oldValue") ? (this.value = this.oldValue, this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd)) : this.value = "" }) }) },
    /**
  *Input alphabates validate. 
  */
    validateAlphabates: function (e) { var i = function (value) { return /^[a-zA-Z]*$/.test(value); };["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (t) { e.addEventListener(t, function () { i(this.value) ? (this.oldValue = this.value, this.oldSelectionStart = this.selectionStart, this.oldSelectionEnd = this.selectionEnd) : this.hasOwnProperty("oldValue") ? (this.value = this.oldValue, this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd)) : this.value = "" }) }) },
    /**
  *Input alphanumeric with hyphen validate. 
  */
    validateAlphaNumericHyphen: function (e) { var i = function (value) { return /^[a-zA-Z0-9-]*$/.test(value); };["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (t) { e.addEventListener(t, function () { i(this.value) ? (this.oldValue = this.value, this.oldSelectionStart = this.selectionStart, this.oldSelectionEnd = this.selectionEnd) : this.hasOwnProperty("oldValue") ? (this.value = this.oldValue, this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd)) : this.value = "" }) }) },
    /**
*Input alpha numberic with space validate. 
*/
    validateAlphaNumericSpace: function (e) { var i = function (value) { return /^[a-zA-Z0-9 ]*$/.test(value); };["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (t) { e.addEventListener(t, function () { i(this.value) ? (this.oldValue = this.value, this.oldSelectionStart = this.selectionStart, this.oldSelectionEnd = this.selectionEnd) : this.hasOwnProperty("oldValue") ? (this.value = this.oldValue, this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd)) : this.value = "" }) }) },
    /**
  *Input money validate. 
  */
    validateMoney: function (e) { var i = function (value) { return /^([1-9]{1}[0-9]{0,2}(\,[0-9]{3})*(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$/.test(value); };["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (t) { e.addEventListener(t, function () { i(this.value) ? (this.oldValue = this.value, this.oldSelectionStart = this.selectionStart, this.oldSelectionEnd = this.selectionEnd) : this.hasOwnProperty("oldValue") ? (this.value = this.oldValue, this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd)) : this.value = "" }) }) },
    /**
  * Returns query string or route param dfault value(null-'') .
  * @param {n} n  querystring name.
  * @param {i} i  route param index.
  * @param {v} v  querystring or route param default value. 
  */
    getQueryStRouteParamValues: function (n, i, v) { return -1 != currpageurl.indexOf("?") ? app.setQueryStDefaultVal(n, v) : app.setRouteValueByIndex(i, v) },
    /**
   * Returns query string dfault value(null-'').
   * @param {l} l route param index.
   * @param {n} n route param default value. 
   */
    setRouteValueByIndex: function (l, n) { var loc = location.pathname.replace(/^\/+/g, "").split("/"); var t = ""; return loc.length > l ? loc[l] : null == n || "null" == n || "undefined" == n ? "" : n },
    /**
    * Returns query string dfault value(null-'').
    * @param {l} l querystring name.
    * @param {n} n querystring default value. 
    */
    setQueryStDefaultVal: function (l, n) { var t = this.querySt(l); return null == t || null == t || "null" == t || "" == t ? n : t },
    /**
    * Returns query string dfault value(null/undefined-'').
    * @param {l} l querystring name.
    * @param {n} n querystring default value. 
    */
    setQStUndefNullDefaultVal: function (l, n) { var t = this.querySt(l); return null == t || null == t || "null" == t || "undefined" == t || "" == t ? n : t },
    /**
     * Returns querystring value. 
     * @param {t} t querystring name.
     */
    querySt: function (t) { for (hu = window.location.search.substring(1), hu = decodeURI(hu), gy = hu.split("&"), i = 0; i < gy.length; i++) if (ft = gy[i].split("="), ft[0] == t) return ft[1] },
    /**
     * Returns Url only without Querystrings.
     */
    getUrlWithNoQuerySt: function () { return (location.protocol + '//' + location.host + location.pathname).toLowerCase(); },
    /**
     * Restrict input to allow only numbers.
     * @param {e} e input controlid.
     */
    allowNumbers: function (e) { $("#" + e).keydown(function (e) { 46 == e.keyCode || 8 == e.keyCode || 9 == e.keyCode || 27 == e.keyCode || 13 == e.keyCode || 65 == e.keyCode && !0 === e.ctrlKey || 35 <= e.keyCode && e.keyCode <= 39 || (e.shiftKey || (e.keyCode < 48 || 57 < e.keyCode) && (e.keyCode < 96 || 105 < e.keyCode)) && e.preventDefault() }) },
    /**
     * Restrict input to allow only numbers and float.
     * @param {e} e input controlid.
     */
    allowFloat: function (e) { $("#" + e).keydown(function (e) { 46 == e.keyCode || 8 == e.keyCode || 9 == e.keyCode || 27 == e.keyCode || 13 == e.keyCode || 65 == e.keyCode && !0 === e.ctrlKey || 35 <= e.keyCode && e.keyCode <= 39 || 190 == e.keyCode || (e.shiftKey || (e.keyCode < 48 || 57 < e.keyCode) && (e.keyCode < 96 || 105 < e.keyCode)) && e.preventDefault() }) },
    /**
     * Process ajax request.
     * @param {serviceurl} - serviceurl.
     * @param {reqtype} - serviceurl request type(GET/POST...).
     * @param {data} - data data.
     * @param {onsuccess} - onsuccess success function.
     * @param {resctrl} - resctrl result control.
     * @param {msg} - msg message.
     * @param {sucmsg} - sucmsg success message.
     * @param {errmsg} - errmsg error message.
     * @param {isasync} - isasync async request true/false.
     * @param {isheader} - isheader header authorization.
     * @param {pageloaderdiv} - pageloaderdiv pageloaderdiv.
     * @param {pagecontentdiv} - pageloaderdiv page content div.
     * @param {btnctrl} - request from button click event.
     */
    ajaxreq: function (serviceurl, reqtype, data, onsuccess, resctrl, msg, sucmsg, errmsg, isasync, isheader, pageloaderdiv, pagecontentdiv, btnctrl) {
        try {
            var pageLoader = pageloaderdiv == undefined ? '.pageloader' : pageloaderdiv;
            var pageContent = pagecontentdiv == undefined ? '.pagecontent' : pagecontentdiv;
            $.ajax({
                url: serviceurl,
                type: reqtype,
                headers: isheader ? { 'Access-Control-Allow-Origin': '*', 'Access-Control-Allow-Headers': 'X-Requested-With', 'Content-Type': 'application/json; charset=utf-8' } : '',
                data: reqtype.toLowerCase() == 'post' ? JSON.stringify(data) : data,
                contentType: "application/json; charset=utf-8",
                dataType: 'text json',
                async: isasync,
                beforeSend: function () { },
                complete: function () { hidePageLoader(); },
                success: onsuccess,
                error: function (jqXHR, exception) {
                    hidePageLoader(), 0 === jqXHR.status ? msg = "Not connect.\n Verify Network." : 404 == jqXHR.status ? msg = "Requested page not found. [404]" : 500 == jqXHR.status ? msg = "Internal Server Error [500]." : "parsererror" === exception ? msg = "Requested JSON parse failed." : "timeout" === exception ? msg = "Time out error." : "abort" === exception ? msg = "Ajax request aborted." : msg = "Uncaught Error.\n" + jqXHR.responseText;
                    if (btnctrl != null && btnctrl != undefined && btnctrl != '') {
                        btnLoaderReset(btnctrl);
                    }
                    console.log(msg);
                }
            });
        }
        catch (err) {
            hidePageLoader();
            if (btnctrl != null && btnctrl != undefined && btnctrl != '') {
                btnLoaderReset(btnctrl);
            }
            $('.d-login_err').html(appMessages.baseErr);
            console.log(err.message);
        }
    },
    ajaxreqfileuploaddata: function (serviceurl, reqtype, data, onsuccess, resctrl, msg, sucmsg, errmsg, isasync, isheader, pageloaderdiv, pagecontentdiv, btnctrl) {
        try {
            var pageLoader = pageloaderdiv == undefined ? '.pageloader' : pageloaderdiv;
            var pageContent = pagecontentdiv == undefined ? '.pagecontent' : pagecontentdiv;
            $.ajax({
                url: serviceurl,
                type: reqtype,
                headers: isheader ? { 'Access-Control-Allow-Origin': '*', 'Access-Control-Allow-Headers': 'X-Requested-With', 'Content-Type': 'application/json; charset=utf-8' } : '',
                data: data,
                contentType: false,
                enctype: 'multipart/form-data',
                processData: false,
                async: isasync,
                beforeSend: function () { },
                complete: function () { hidePageLoader(); },
                success: onsuccess,
                error: function (jqXHR, exception) {
                    hidePageLoader(), 0 === jqXHR.status ? msg = "Not connect.\n Verify Network." : 404 == jqXHR.status ? msg = "Requested page not found. [404]" : 500 == jqXHR.status ? msg = "Internal Server Error [500]." : "parsererror" === exception ? msg = "Requested JSON parse failed." : "timeout" === exception ? msg = "Time out error." : "abort" === exception ? msg = "Ajax request aborted." : msg = "Uncaught Error.\n" + jqXHR.responseText;
                    if (btnctrl != null && btnctrl != undefined && btnctrl != '') {
                        btnLoaderReset(btnctrl);
                    }
                    console.log(msg);
                }
            });
        }
        catch (err) {
            hidePageLoader();
            if (btnctrl != null && btnctrl != undefined && btnctrl != '') {
                btnLoaderReset(btnctrl);
            }
            $('.d-login_err').html(appMessages.baseErr);
            console.log(err.message);
        }
    },
    /**
   * Check dates difference.
   * @param {e} - e starttime.
   * @param {t} - t endtime.
   * @param {a} - a days differnece limit(0 - no limit).
   * @param {n} - n datemandatory(1/0).
   */
    checkDatesDiff: function (e, t, a, n) { if (1 == n && "" == e) return "Please select FromDate"; if ("" == e && "" != t) return "Please select FromDate"; var r = new Date(e), i = new Date(t).getTime() - r.getTime(), l = Math.ceil(i / 864e5); return -1 != l.toString().indexOf("-") ? "ToDate is greater than FromDate." : 0 < a && a < l ? "Invalid date range selection (Only " + a + " days allowed)." : "" },
    /**
     * Check dates difference.
     * @param {e} - e starttime.
     * @param {t} - t endtime.
     * @param {a} - a days differnece limit(0 - no limit).
     * @param {r} - r datemandatory(1/0).
     */
    checkDates: function (e, t, a, r) { if (1 == r) { if ("" == e) return "Please select FromDate"; if ("" == t) return "Please select ToDate" } if ("" == e && "" != t) return "Please select FromDate"; if ("" == t && "" != e) return "Please select ToDate"; var n = new Date(e), i = new Date(t).getTime() - n.getTime(), l = Math.ceil(i / 864e5); return -1 != l.toString().indexOf("-") ? "ToDate is greater than FromDate." : 0 < a && a < l ? "Invalid date range selection (Only " + a + " days allowed)." : "" },
    /**
   * Create cookie.
   * @param {e} - e cookiename.
   * @param {o} - o value.
   * @param {t} - t cookie expiry days.
   */
    createCookie: function (e, o, t) { if (t) { var i = new Date; i.setTime(i.getTime() + 24 * t * 60 * 60 * 1e3); var n = "; expires=" + i.toGMTString() } else n = ""; document.cookie = e + "=" + encodeURIComponent(o) + n + "; path=/" },
    /**
    * Read cookie.
    * @param {n} - n cookiename.
    */
    readCookie: function (n) { for (var e = n + "=", t = document.cookie.split(";"), r = 0; r < t.length; r++) { for (var o = t[r]; " " == o.charAt(0);) o = o.substring(1, o.length); if (0 == o.indexOf(e)) return decodeURIComponent(o.substring(e.length, o.length)) } return null },
    /**
    * Read dictionary cookie.
    * @param {i} - i cookie name.
    * @param {n} - n cookie key.
    */
    readDictionaryCookie: function (i, n) { var r = readCookie(i).toString(); return r = (r = r.substring(r.indexOf(n + "=") + 4)).substring(0, r.indexOf("&")) },
    /**
    * Read cookie.
    * @param {n} - n cookiename.
    */
    eraseCookie: function (n) { this.createCookie(n, "", -1) },

    /**
     * Set date picker control
     * @param {any} ctrls- date picker controls ('#from,#to')
     */
    setDatePickerCtrl: function (ctrls) {
        alert(ctrls)
        $(ctrls).datepicker({ "format": "dd MM yyyy", "autoclose": true, "orientation": "bottom auto", todayHighlight: true });
        //allow backspace and delete keys in date ctrl
        var ctrl = ctrls.split(","); $.each(ctrl, function (e, o) { $(o).keydown(function (e) { return (46 == e.keyCode || 8 == e.keyCode || 9 == e.keyCode || 37 == e.keyCode || 39 == e.keyCode) && void 0 }) });
    },
    /** Set datepicker properties */
    datePickerProp: { "format": "dd MM yyyy", "autoclose": true, "orientation": "bottom auto", todayHighlight: true },
    /**
     * Input text not allowed to enter.
     * @param {any} ctrls
     */
    textnotAllowed: function (ctrls) {
        var ctrl = ctrls.split(","); $.each(ctrl, function (e, o) { $(o).keydown(function (e) { return (46 == e.keyCode || 8 == e.keyCode || 9 == e.keyCode || 37 == e.keyCode || 39 == e.keyCode) && void 0 }) });
    },

    /**
     * Get currentpage search parameters from url(fr,to,pi,ps...) to display previous page grid.
     * @param {any} url- Current page url.
     */
    getSearchParamsfromUrl: function (url) {
        if (url.indexOf('?') == -1 || url.indexOf('&') == -1) {
            return "";
        }
        else {
            return url.substring(url.indexOf('&')).replace('&', '?');
        }
    },
    getParameterByName: function(name){
        var url = window.location.search;
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)");
        var results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }
};
