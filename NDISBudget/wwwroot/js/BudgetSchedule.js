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
        getSupportItem();
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
            //console.log(response);
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

    $(document).on('change', '#SupportItemId', function (event) {
        var dataParams = {
            StateId: $("#StateId").val(),
            SupportItemId: $(this).val(),
        }
        
        gl.ajaxreqloader(apiurl + "Client/GetSupportItemPrice", "POST", dataParams, function (response) {
            //console.log(response.price);
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
            SupportItemId: $('#SupportItemId').val(),
            ItemBudget: $('#itemprice').val()
        }

        if (dataParams.SupportItemId == 0) {
            alert("Select Item");
        }
        else { 
        console.log(dataParams)
        gl.ajaxreqloader(apiurl + "Client/AddClientSupportItem", "POST", dataParams, function (response) {
            //console.log(response);
            if (response > 0) {
                alert("Item added to client budget");
                getSupportItem();
            }
            else {
                getSupportItem();
                alert("Item already added to client budget");
            }
        });
        }
        
    });

    
    $(document).on('click', '#addentitry', function (event) {
        var id = $(this).attr('data-cid');
        window.location.href = '/company/client/addbudgetItem?id=' + id;
    });

    function filterById(array, id) {
        return $.grep(array, function (obj) {
            //console.log(obj.clientSupportItemId);
            return obj.clientSupportItemId == id;
        });
    }
    var item = {};
    $(document).on('click', '#btnEdit', function (event) {
        var id = $(this).attr('data-cid');
        item = filterById(items, id);
        if (item != null) {
            //console.log(item[0].startDate)
            $('#StartDate').val(item[0].startDate)
            //$('#ShiftStartTime').val(item.shiftStartTime)
            //$('#scheduledFrequencies').val(item[0].frequencyId)

            $('#scheduledFrequencies option').each(function (index, element) {
                var value = $(element).val();
                if (value.startsWith(item[0].frequencyId + "_")) {
                    $("#scheduledFrequencies").val(value);
                }
            });

            $('#EndDate').val(item[0].endDate)
            //$('#ShiftEndTime').val(item.shiftEndTime)
            var weeks = item[0].weekDayIds.split(',');
            console.log(weeks)
            $('#WeekArray').val(weeks)
        }
        //console.log(id)
        //console.log(items)
        console.log(item)
    });

    $(document).on('click', '#btnsave', function (event) {
        var dataParams = {
            ClientSupportItemId: item[0].clientSupportItemId,
            FrequencyId: $('#scheduledFrequencies').val().split('_')[0],
            WeekArray: $('#WeekArray').val().join(','),
            StartDate: $('#StartDate').val(),
            EndDate: $('#EndDate').val()
        }
        //console.log(dataParams);
        gl.ajaxreqloader(apiurl + "Client/UpdateclientSupportItem", "POST", dataParams, function (response) {
            alert("Item Updated successfully.")

            //if (response == 1) {
                getSupportItem();
            //}

        });
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
    var size = $("#TotalVisits").html();
    //alert(size)
    BindClients(1, isNaN(size) ? 10 : size, 1, '');
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
var items = {};
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
            items = response;
            reccount = response[0].totalRecords;
            $.each(response, function (i, item) {
                i = parseInt(i) + 1;
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


