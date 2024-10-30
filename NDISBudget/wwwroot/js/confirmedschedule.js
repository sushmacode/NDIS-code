$(() => {
    //alert(1)
    getSchedule();
});

function getSchedule() {
    BindSchedule(1, 10, 1, '');
    $('#shwbtn').click(function (event) {
        event.preventDefault();
        //alert($('#Searchstr').val())
        BindSchedule(1, 10, 1, $('#SearchStr').val());
    });

    $(document).on('click', '#shwallbtn', function (event) {
        $('#SearchStr').val('')
        BindSchedule(1, 10, 1, '');
    });
    $(document).on("click", ".d-paging", function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = $(this).attr('_id');
        var Searchstr = $('#Searchstr').val();
        BindSchedule(pageindex, pagesize, 1, Searchstr);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var Searchstr = $('#SearchStr').val();
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        var sortby = 1; //$('#SortBy').val();
        BindSchedule(pageindex, pagesize, 1, Searchstr);
    });
}
function BindSchedule(pageindex, pagesize, sortby, strname) {
    //alert(strname)
    var url = window.location.search;
    var regex = new RegExp("[?&]id(=([^&#]*)|&|#|$)");
    var results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';

    var dataParams = {
        SupportItemId: decodeURIComponent(results[2].replace(/\+/g, " ")),
        pgsize: pagesize,
        pgindex: pageindex,
        str: strname,
        sortby: sortby,
        "fromDate": "",
        "toDate": "",
        "committee": 0
    }
    console.log(dataParams);
    var row = '';
    var reccount = 0;
    gl.ajaxreqloader(apiurl + "Client/GetClientBudgetItemsConfirmedScheduleList", "POST", dataParams, function (response) {
        console.log(response);
        if (response.length > 0) {
            reccount = response[0].totalRecords;
            $.each(response, function (i, item) {

                row += "<tr><td>" + item.dateStr + "</td><td>" + item.dayofWeek + "</td><td>" + item.supportItemId + "</td><td>" + item.suppItemName + "</td><td>" + item.shiftStartTime + "</td><td>" + item.shiftEndTime + "</td><td>" + item.hoursCount + "</td><td>" + item.itemPriceStr + "</td><td>" + item.itemPriceStr + "</td><td>" + item.workerName + "</td><td>" + item.workerName + "</td></tr>";

            });
            $("#tbldata").html(row);
            setPagging(reccount, pageindex, pagesize);
            $('.norec').addClass('hide');
            $('.tblcontent').removeClass('hide');
        }
        else {
            if (String(strname).length > 0) {
                $('.norec').addClass('hide');
                $('.tblcontent').removeClass('hide');
                $("#tbldata").html("<tr><td>No Data Found</td></td></tr>");
            }
            else {
                $('.norec').removeClass('hide');
                $('.tblcontent').addClass('hide');
            }
        }
    }, '', '', '', '', true, true, '.loader', '.tblcontent', 'text json', 'true');

}