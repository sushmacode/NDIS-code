$(() => {
    //alert(1)
    getSchedule();
    $(document).on('click', '#addentitry', function (event) {
        var dataParams = {
            ClientBudgetId : $('#ClientBudgetId').val(),
            SupportItemId : $('#SupportItemId').val()
        }
        console.log(dataParams)
        //return;
        //alert(id)
        gl.ajaxreqloader(apiurl + "Client/ConfirmTempSchedule", "POST", dataParams, function (response) {
            //alert(response)
            //getSchedule();
            window.location.href = '/company/Client/AddClientBudget?id=' + $('#ClientBudgetId').val();
        });
    });

    $(document).on('click', '#btnEdit', function (event) {
        var id = $(this).attr('data-cid');
        window.location.href = '/admin/company/add?id=' + id;
    });

     $(document).on('click', '#addback', function (event) {
        window.history.back();
    });

    $(document).on('click', '#btndelete', function (event) {
        var id = $(this).attr('data-cid');
        //alert(id)
        gl.ajaxreqloader(apiurl + "company/Delete", "GET", { id: id }, function (response) {
            //alert(response)
            getSchedule();
        });
    });

});

function getSchedule() {
    BindSchedule(1, 10, 1, '');
    

    $(document).on('click', '#shwallbtn', function (event) {
        $('#SearchStr').val('')
        BindSchedule(1, 10, 1, '');
    });
    $(document).on("click", ".d-paging", function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = $(this).attr('_id');
        var Searchstr = $('#SearchStr').val();
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
        id: decodeURIComponent(results[2].replace(/\+/g, " ")).split('_')[0]
    }
    console.log(dataParams);
    var row = '';
    var reccount = 0;
    gl.ajaxreqloader(apiurl + "Client/GetClientSupportItemScheduleList", "GET", dataParams, function (response) {
        console.log(response);
        if (response.length > 0) {
            reccount = response[0].totalRecords;
            $.each(response, function (i, item) {

                row += "<tr><td>" + item.dateStr + "</td><td>" + item.dayofWeek + "</td><td>" + item.supportItemId + "</td><td>" + item.suppItemName + "</td><td>" + item.shiftStartTime + "</td><td>" + item.shiftEndTime + "</td><td>" + item.hoursCount + "</td><td>" + item.itemPriceStr + "</td><td>" + item.lineAmountStr + "</td></tr>";

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


