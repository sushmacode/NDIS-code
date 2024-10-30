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
        if (ClientId != 0) {
            bindclient(ClientId);
            //getClientBudget(ClientId);
            //window.location.href = "/company/client/addClientbudget?id=" + ClientId;
            }
        else {
            window.location.href = websiteurl + "/Company/Client/addClientbudget";
            //$("#FirstName").val('');
            //$("#MobileNumber").val('');
            //$("#ClientAddress").val('');
            //$("#LastName").val('');
            //$("#EmailId").val('');
            //$("#DateOfBirth").val('');
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
                    $("#DateOfBirth").val(response.dateOfBirth),
                    $("#SCEmail").val(response.scEmail),
                    $("#SCPhone").val(response.scPhone),
                    $("#SupportCoordinator").val(response.supportCoordinator),
                    $("#SCCompany").val(response.scCompany),
                    $("#PlanManager").val(response.planManager),
                    $("#PMEmail").val(response.pmEmail),
                    $("#PMPhone").val(response.pmPhone)
                //$("#divitems").hide();
            }
            else {
                $("#itemprice").val(0.00)
            }
        });
    }
    var priceperhour = 0.00;

    function GetItemPPH(SupportItemId) {
        
        return new Promise(function (resolve, reject) {
            var dataParams = {
                StateId: $("#StateId").val(),
                SupportItemId: SupportItemId,
            }
            console.log(dataParams);
            gl.ajaxreqloader(apiurl + "Client/GetSupportItemPrice", "POST", dataParams, function (response) {
                console.log(response);
                if (response.price > 0) {
                    priceperhour = response.price;
                }
                else {
                    priceperhour = 0.00;
                }
            });
            setTimeout(function () {
                //console.log("Function executed");
                resolve(); // Indicate that the operation is complete
            }, 2000); // 2 seconds delay
            //return priceperhour;
        });
    }
    $(document).on('change', '#SupportItemId', function (event) {
        var dataParams = {
            StateId: $("#StateId").val(),
            SupportItemId: $(this).val(),
        }
        console.log(dataParams);
        gl.ajaxreqloader(apiurl + "Client/GetSupportItemPrice", "POST", dataParams, function (response) {
            console.log(response);
            if (response.price > 0) {
                priceperhour = response.price;
            }
            else {
                priceperhour = 0.00;
            }
        });
    });
    $(document).on('click', '#btnsave', function (event) {
       
        $('#SupportItemId1 option:selected').each(function () {
            var value = $(this).val();
            if (value != undefined) {
                GetItemPPH(value).then(function (response) {
                    //alert(priceperhour);
                    additem(SelectedSupportCategoryId, value, priceperhour);
                });
            }
        });
        

    });
    var SelectedSupportCategoryId = 0;
    $(document).on('click', '#btnEdit11', function (event) {

        var id = $(this).attr('data-cid');
        
        SelectedSupportCategoryId = id;
        
        
        gl.ajaxreqloader(apiurl + "Client/GetddlNASupportItemBySCId", "GET", { id: id }, function (response) {
            //console.log(response);
            $('#SupportItemId1').empty();
            if (response.length > 0) {
                $('#SupportItemId1').append($('<option>', {
                    value: 0,
                    text: "Select Support Item."
                }));
                $.each(response, function (i, item) {

                    $('#SupportItemId1').append($('<option>', {
                        value: item.supportItemId,
                        text: item.supportItemNumber + '--' + item.supportItemName + '--' + setCustomName(item.customName)
                    }));
                });
            }
            else {
                $('#SupportItemId1').append($('<option>', {
                    value: 0,
                    text: "No items"
                }));
            }
        });

    });

    function setCustomName(x) {
        return x == null ? '' : x;
    }

    $(document).on('click', '#additemp1', function (event) {
        var dataParams = {
            ClientBudgetId: $("#ClientBudgetId").val(),
            SupportCategoryId: $('#SupportCategoryId1').val(),
            CategoryPrice: $('#categoryprice').val()
        }

        if (dataParams.SupportItemId == 0) {
            Swal.fire({
                icon: 'warning',
                title: 'Select Item',
                text: 'Please select an item before proceeding.'
            });
        }
        else {
            console.log(dataParams);
            gl.ajaxreqloader(apiurl + "Client/AddClientSupportCategory", "POST", dataParams, function (response) {
                if (response > 0) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: 'Support Category added to client budget.'
                    });
                    BindCategories(1, 100, 1, '');
                    getSupportItem();
                }
                else if (response == -2) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Support Category Budget exceeds Proposed budget.'
                    });
                }
                else {
                    Swal.fire({
                        icon: 'info',
                        title: 'Info',
                        text: 'Support Category already added to client budget.'
                    });
                }
            });
        }
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

    function additem(SupportCategoryId, SupportItemId, priceperhour) {
        return new Promise(function (resolve, reject) {
        var dataParams = {
            ClientBudgetId: $("#ClientBudgetId").val(),
            ClientSupportItemId: 0,
            CompanyId: $("#CompanyId").val(),
            SupportCategoryId: SupportCategoryId,
            SupportItemId: SupportItemId,
            ItemBudget: 0,
            PricePerHour: priceperhour
        }

        if (dataParams.SupportItemId == 0) {
            alert("Select Item");
        }
        else {
            //console.log(dataParams)
            gl.ajaxreqloader(apiurl + "Client/AddClientSupportItem", "POST", dataParams, function (response) {
                //alert(response);
                //if (response > 0) {
                //    alert("Item added to client budget");
                    getSupportItem();
                //}
                //else if (response == -2) {
                //    alert("Item Budget exeeds Proposed budget");
                //}
                //else {
                //    alert("Item already added to client budget");
                //}
            });
            }

            setTimeout(function () {
                //console.log("Function executed");
                resolve(); // Indicate that the operation is complete
            }, 2000); // 2 seconds delay
            //return priceperhour;
            $("#btnclose").click();
        });
        
    };


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

    BindCategories(1, 100, 1, '');

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
                row += "<tr><td>" + i + "</td><td>" + item.supportCategoryName + "</td><td>" + item.supportItemNumber + "</td><td>" + item.supportItemName + "</td><td>" + item.supportCategoryBudgetStr + "</td><td><a href='javascript:;' title='Edit' id='btnEdit' data-toggle='modal' data-target='#update' data-cid=" + item.clientSupportItemId + "><i class='fa fa-edit' style='font-size: 20px; color:#7979e9'></i></a></td></tr>";
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

function BindCategories(pageindex, pagesize, sortby, strname) {
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
    gl.ajaxreqloader(apiurl + "Client/GetClientSupportCategoryBudgetList", "POST", dataParams, function (response) {

        supportitems = response;
        //console.log(supportitems);
        if (response.length > 0) {
            reccount = response[0].totalRecords;
            $.each(response, function (i, item) {
                i = parseInt(i) + 1;
                row += "<tr><td>" + i + "</td><td>" + item.supportCategoryName + "</td><td>" + item.itemPriceStr + "</td><td>" + item.createdDateStr + "</td><td><a href='javascript:;' title='Add Support Item' id='btnEdit11' data-bs-toggle='modal' data-bs-target='#modal-block-select2' data-cid=" + item.supportCategoryId + "><i class='fa fa-edit' style='font-size: 20px; color:#7979e9'></i></a></td></tr>";
            });
            $("#tbldata1").html(row);
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


