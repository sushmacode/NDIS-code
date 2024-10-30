$(() => {
    //alert(1)
    getSupportItem();

    //getBudgetDetails();
    $(document).on('click', '#back', function (event) {
        var id = $("#ClientBudgetId").val();
        window.location.href = '/company/client/addbudget?id=' + id;
    });


    $(document).on('click', '#btnEdit', function (event) {
        var budget = parseFloat($(this).closest('tr').find('.textbox').val().trim());
        var id = $(this).closest('tr').find('.sid').html();
        //var id = $(this).attr('data-cid');
        //alert(id);
        if (budget != '') {
            //
            var proposedBudget = parseFloat($('#ProposedBudget').html().trim());
            var usedBudget = parseFloat($('#UsedBudget').html().trim());
            var remainingBudget = parseFloat($('#RemainingBudget').html().trim());
            var newBudget =usedBudget+budget;
            //alert(usedBudget)
            if (newBudget > proposedBudget) {
                alert("Total used budget is getting more than Proposed Budget")
            }
            else {
                var dataParams = {
                    ClientBudgetId: $("#ClientBudgetId").val(),
                    ClientSupportItemId: 0,
                    CompanyId: $("#CompanyId").val(),
                    SupportItemId: id,
                    ItemBudget: budget
                }
                console.log(dataParams)
                gl.ajaxreqloader(apiurl + "Client/AddClientSupportItem", "POST", dataParams, function (response) {
                    console.log(response);
                    if (response > 0) {
                        alert("Item added to client budget");
                        getSupportItem();
                    }
                });
            }
        }
        else {
            alert("Make sure entered budget for selected item.");
        }
        //window.location.href = '/company/client/addbudgetItem?id=' + id;
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
    BindClients(1, 10, 1, '');
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
        SupportCategoryId: $("#SupportCategoryId").val(),
        pgsize: pagesize,
        pgindex: pageindex,
        str: '',
        sortby: sortby,
        "fromDate": "",
        "toDate": "",
        "committee": 0
    }
    console.log(dataParams);
    var row = '';
    var reccount = 0;
    gl.ajaxreqloader(apiurl + "Client/GetAdminClientNASupportItemsList", "POST", dataParams, function (response) {
        console.log(response);
        if (response.length > 0) {
            reccount = response[0].totalRecords;
            $.each(response, function (i, item) {

                row += "<tr><td>" + i++ + "</td><td class='sid'>" + item.supportItemId + "</td><td >" + item.supportItemNumber + "</td><td>" + item.supportItemName + "</td><td> <input type='number' class='form-controle textbox' value=" + item.statePrice +"> </td><td><a href='javascript:;' title='Edit' id='btnEdit' data-toggle='modal' data-target='#update' data-cid=" + item.clientSupportItemId + "><i class='fa fa-add m-r-5'></i></a></td></tr>";

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


