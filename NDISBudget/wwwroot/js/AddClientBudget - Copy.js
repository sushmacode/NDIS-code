$(() => {
    //alert(1)

    var ClientBudgetId = $('#ClientBudgetId').val();
    var ClientId = $('#ClientId').val();
    if (ClientId != 0) {
        //alert(ClientId)
        bindclient(ClientId);

    }
    if (ClientBudgetId == 0) {
        //alert(ClientBudgetId)
        $("#divitems").hide();
    }
    else {
        //$('#ClientId').attr("disabled", "disabled")
    }

    $('#SupportItemId').append($('<option>', {
        value: 0,
        text: "No items"
    }));

    $(document).on('change', '#ClientId', function (event) {
        var ClientId = $(this).val();
        if (ClientId != 0)
            bindclient(ClientId);
        else {
            $("#FirstName").val('');
            $("#MobileNumber").val('');
            $("#ClientAddress").val('');
            $("#LastName").val('');
            $("#EmailId").val('');
            $("#DateOfBirth").val('');
        }
           
    });


    function bindclient(ClientId) {
        gl.ajaxreqloader(apiurl + "Client/GetClientById", "Get", { id: ClientId }, function (response) {
            console.log(response);
            if (response != null) {
                $("#FirstName").val(response.firstName),
                    $("#MobileNumber").val(response.mobileNumber),
                    $("#ClientAddress").val(response.clientAddress),
                    $("#LastName").val(response.lastName),
                    $("#EmailId").val(response.emailId),
                    $("#DateOfBirth").val(response.dateOfBirth)
            }
            else {
                $("#itemprice").val(0.00)
            }
        });
    }
    var priceperhour = 0.00;
    $(document).on('change', '#SupportItemId', function (event) {
        var dataParams = {
            StateId: $("#StateId").val(),
            SupportItemId: $(this).val(),
        }
        console.log(dataParams);
        gl.ajaxreqloader(apiurl + "Client/function getSupportItem() {
    // Your implementation to fetch and load data
    BindClients(1, $('#ddlpagesize').val(), $('#SearchStr').val());
    }
Price", "POST", dataParams, function (response) {
            console.log(response);
            if (response.price > 0) {
                priceperhour = response.price;
            }
            else {
                priceperhour = 0.00;
            }
        });
    });

    $(document).on('change', '#SupportCategoryId', function (event) {

        var id = $(this).val();
        
        
        var firstMatchingItem = null;
        
        $.each(supportitems, function (index, item) {
            
            if (item.supportCategoryId == id) {
                
                firstMatchingItem = item.itemBudget;
                return false; // Break the loop
            }
        });
       
        console.log(firstMatchingItem);
        if (firstMatchingItem != null) {
            $('#itemprice').val(firstMatchingItem);
            $('#itemprice').prop('disabled', true);
        } else {
            $('#itemprice').val(0.00)
            $('#itemprice').prop('disabled', false);
        }
        
        gl.ajaxreqloader(apiurl + "Client/GetddlNASupportItemBySCId", "GET", { id: id }, function (response) {
            //console.log(response);
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
            SupportCategoryId: $('#SupportCategoryId').val(),
            SupportItemId: $('#SupportItemId').val(),
            ItemBudget: $('#itemprice').val(),
            PricePerHour: priceperhour
        }

        if (dataParams.SupportItemId == 0) {
            alert("Select Item");
        }
        else { 
        console.log(dataParams)
        gl.ajaxreqloader(apiurl + "Client/AddClientSupportItem", "POST", dataParams, function (response) {
            //alert(response);
            if (response > 0) {
                alert("Item added to client budget");
                getSupportItem();
            }
            else if (response == -2) {
                alert("Item Budget exeeds Proposed budget");
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
var supportitems = {}
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
    gl.ajaxreqloader(apiurl + "Client/GetClientBudgetItemsList", "POST", dataParams, function (response) {
        
        supportitems = response;
        console.log(supportitems);
        if (response.length > 0) {
            reccount = response[0].totalRecords;
            $.each(response, function (i, item) {
                i = parseInt(i) + 1;
                row += "<tr><td>" + i + "</td><td>" + item.supportCategoryName + "</td><td>" + item.supportItemNumber + "</td><td>" + item.supportItemName + "</td><td>" + item.itemBudgetStr + "</td><td>" + item.createdDateStr + "</td><td><a href='javascript:;' title='Edit' id='btnEdit' data-toggle='modal' data-target='#update' data-cid=" + item.clientSupportItemId + "><i class='fa fa-edit m-r-5'></i></a></td></tr>";
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


