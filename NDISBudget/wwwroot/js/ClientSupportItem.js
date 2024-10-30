$(() => {
    //alert(1)

    var ClientBudgetId = $('#ClientBudgetId').val();

    if (ClientBudgetId == 0) {
        //alert(ClientBudgetId)
        $("#divitems").hide();
    }

    $('#SupportItemId').append($('<option>', {
        value: 0,
        text: "No items"
    }));

    $(document).on('change', '#SupportItemId', function (event) {
        var dataParams = {
            StateId: $("#StateId").val(),
            SupportItemId: $(this).val(),
        }
        
        gl.ajaxreqloader(apiurl + "Client/GetSupportItemPrice", "POST", dataParams, function (response) {
            console.log(response.price);
            if (response.price > 0) {
                $("#itemprice").val(response.price)
            }
            else {
                $("#itemprice").val(0.00)
            }
        });
    });

    $(document).on('change', '#SupportCategoryId', function (event) {

        var id = $(this).val();
        
        // alert(selectedValue)

        gl.ajaxreqloader(apiurl + "Client/GetddlNASupportItemBySCId", "GET", { id: id }, function (response) {
            console.log(response);
            $('#SupportItemId').empty();
            if (response.length > 0) {
                $('#SupportItemId').append($('<option>', {
                    value: 0,
                    text: "Select Support Item."
                }));
                $.each(response, function (i, item) {

                    $('#SupportItemId').append($('<option>', {
                        value: item.supportItemId,
                        text: item.supportItemNumber +'--'+item.supportItemName 
                    }));
                });
            }
            else {
                $('#SupportItemId').append($('<option>', {
                    value: 0,
                    text: "No items"
                }));
            }
        });

    });

    $(document).on('click', '#additemp', function (event) {

        var dataParams = {
            ClientBudgetId: $("#ClientBudgetId").val(),
            ClientSupportItemId: 0,
            CompanyId: $("#CompanyId").val(),
            SupportItemId: $('#SupportItemId').val(),
            ItemBudget: $('#itemprice').val()
        }

        if (dataParams.SupportItemId == 0) {
            alert("Select Item");
        }
        else { 
        console.log(dataParams)
        gl.ajaxreqloader(apiurl + "Client/AddClientSupportItem", "POST", dataParams, function (response) {
            console.log(response);
            if (response > 0) {
                alert("Item added to client budget");
                getSupportItem();
            }
            else {
                alert("Item already added to client budget");
            }
        });
    }
    });

    getSupportItem();
    $(document).on('click', '#addentitry', function (event) {
        var id = $(this).attr('data-cid');
        window.location.href = '/company/client/addbudgetItem?id=' + id;
    });


    $(document).on('click', '#btnEdit', function (event) {
        var id = $(this).attr('data-cid');
        window.location.href = '/company/client/editbudgetItem?id=' + id;
    });

    $(document).on('click', '#btnviewschedule', function (event) {
        var id = $(this).attr('data-cid');
        window.location.href = '/company/Schedule/index?id=' + id;
    });

    $(document).on('click', '#addback', function (event) {
        window.history.back();
    });

    $(document).on('click', '#btndelete', function (event) {
        var id = $(this).attr('data-cid');
        //alert(id)
        gl.ajaxreqloader(apiurl + "Client/DeleteBudgetItem", "GET", { id: id }, function (response) {
            //alert(response)
            getSupportItem();
        });
    });

});

function getSupportItem() {
    BindClients(1, 100, 1, '');
    $('#shwbtn').click(function (event) {
        event.preventDefault();
        //alert($('#Searchstr').val())
        BindClients(1, 10, 1, $('#SearchStr').val());
    });

    $(document).on('click', '#shwallbtn', function (event) {
        $('#SearchStr').val('')
        BindClients(1, 10, 1, '');
    });
    $(document).on("click", ".d-paging", function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = $(this).attr('_id');
        var Searchstr = $('#SearchStr').val();
        BindClients(pageindex, pagesize, 1, Searchstr);
    });
    $(document).on("change", '#ddlpagesize', function (event) {
        var Searchstr = $('#SearchStr').val();
        var pagesize = $('#ddlpagesize').val();
        var pageindex = 1;
        var sortby = 1; //$('#SortBy').val();
        BindClients(pageindex, pagesize, 1, Searchstr);
    });
}
function BindClients(pageindex, pagesize, sortby, strname) {
    //alert(strname)
    var dataParams = {
        ClientBudgetId: $("#ClientBudgetId").val(),
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
    gl.ajaxreqloader(apiurl + "Client/GetAdminClientSupportItemsList", "POST", dataParams, function (response) {
        console.log(response);
        if (response.length > 0) {
            reccount = response[0].totalRecords;
            $.each(response, function (i, item) {

                row += "<tr><td>" + i + "</td><td>" + item.supportItemId + "</td><td>" + item.supportItemName + "</td><td>" + item.itemBudgetStr + "</td><td>" + item.createdDateStr + "</td><td><a href='javascript:;' title='Edit' id='btnEdit' data-toggle='modal' data-target='#update' data-cid=" + item.clientSupportItemId + "><i class='fa fa-edit m-r-5'></i></a>&nbsp;&nbsp;&nbsp;&nbsp;<a href='javascript:;' id='btnviewschedule'  data-cid=" + item.clientSupportItemId + "><i class='fa fa-calendar-alt m-r-5'></i></a></td></tr>";

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


