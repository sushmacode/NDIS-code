$(() => {
    //alert(1)
    getProvider();
    $(document).on('click', '#addentitry', function (event) {
        window.location.href = '/admin/provider/add';
    });

    $(document).on('click', '#btnEdit', function (event) {
        var id = $(this).attr('data-cid');
        window.location.href = '/admin/provider/add?id='+id;
    });

    $(document).on('click', '#btndelete', function (event) {
        var id = $(this).attr('data-cid');
        //alert(id)
        gl.ajaxreqloader(apiurl + "provider/Delete", "GET", { id:id }, function (response) {
            //alert(response)
            getProvider();
        });
    });

});

function getProvider() {
    BindProvider(1, 10, 1, '');
    $('#shwbtn').click(function (event) {
        event.preventDefault();
        //alert($('#Searchstr').val())
        BindProvider(1, 10, 1, $('#Searchstr').val());
    });

    $(document).on('click', '#shwallbtn', function (event) {
        $('#Searchstr').val('')
        BindProvider(1, 10, 1, '');
    });
    $(document).on("click", ".d-paging", function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = $(this).attr('_id');
        var Searchstr = $('#Searchstr').val();
        BindProvider(pageindex, pagesize, 1, Searchstr);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var Searchstr = $('#SearchStr').val();
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        var sortby = 1; //$('#SortBy').val();
        BindProvider(pageindex, pagesize, 1, Searchstr);
    });
}
function BindProvider(pageindex, pagesize, sortby, strname) {
    //alert(strname)
    var dataParams = {
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
    gl.ajaxreqloader(apiurl + "Provider/GetAdminProviderList", "POST", dataParams, function (response) {
        console.log(response);
        if (response.length > 0) {
            reccount = response[0].totalRecords;
            $.each(response, function (i, item) {

                row += "<tr><td>" + item.providerName + "</td><td>" + item.contactNumber + "</td><td>" + item.emailId + "</td><td>" + item.status + "</td><td>" + item.createdDateStr + "</td><td><a href='javascript:;' title='Edit' id='btnEdit' data-toggle='modal' data-target='#update' data-cid=" + item.providerId + "><i class='fa fa-edit m-r-5'></i></a><a href='javascript:;' title='Delete' id='btndelete'  data-cid=" + item.providerId + "><i class='fa fa-remove m-r-5'></i></a></td></tr>";

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


