$(document).ready(function () {
    // Initialize and bind events
    getSupportItem();

    // View schedule button click event
    $(document).on('click', '#btnviewschedule', function (event) {
        var id = $(this).data('cid');
        window.location.href = '/company/Schedule/ConfirmedSchedule?id=' + id;
    });

    // Add new entry
    $(document).on('click', '#addentitry', function () {
        window.location.href = '/company/client/addClientbudget';
    });

    // Edit entry
    $(document).on('click', '#btnEdit', function () {
        var id = $(this).data('cid');
        window.location.href = '/company/client/addClientbudget?id=' + id;
    });

    // Delete entry
    $(document).on('click', '#btndelete', function (event) {
        var id = $(this).attr('data-cid');

        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this action!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                gl.ajaxreqloader(apiurl + "Client/DeleteBusget", "GET", { id: id }, function (response) {
                    Swal.fire('Deleted!', 'The item has been deleted.', 'success');
                    BindClients(1, $('#ddlpagesize').val(), 1, $('#SearchStr').val()); 
                });
            }
        });
    });

    $('#kt_filter_search').on('input', function () {
        var searchStr = $(this).val();
        BindClients(1, $('#ddlpagesize').val(), 1, searchStr);
    });

    $(document).on('click', '#shwallbtn', function (event) {
        $('#kt_filter_search').val('');
        BindClients(1, $('#ddlpagesize').val(), 1, '');
    });

    $(document).on("click", ".d-paging", function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = $(this).attr('_id');
        var searchStr = $('#kt_filter_search').val();
        BindClients(pageindex, pagesize, 1, searchStr);
    });

    $(document).on("change", '#ddlpagesize', function (event) {
        var searchStr = $('#kt_filter_search').val();
        var pagesize = $('#ddlpagesize').val();
        BindClients(1, pagesize, 1, searchStr);  // Reset to page 1 on page size change
    });
});
function getParam(param) {
    const params = new URLSearchParams(window.location.search);
    return params.get(param);
}
function getSupportItem() {
    BindClients(1, $('#ddlpagesize').val(), 1, ''); // Fetch initial data
}

// Main BindClients function
function BindClients(pageIndex, pageSize, sortby, searchString) {
    searchString = (searchString && searchString.trim() !== "") ? searchString.trim() : "";

    var dataParams = {
        CompanyId: $('#CompanyId').val(),
        pgsize: pageSize,
        pgindex: pageIndex,
        str: searchString,
        sortby: sortby, // Sorting parameter
        fromDate: "",
        toDate: "",
        committee: 0,
        Status: getParam('status')
    };

    gl.ajaxreqloader(apiurl + "Client/GetAdminClientBudgetsList", "POST", dataParams, function (response) {
        var rows = '';
        if (response.length > 0) {
            $.each(response, function (i, item) {
                rows += `<tr>
                    <td>${item.name}</td>
                    <td>${item.ndisRefno}</td>
                    <td>${item.proposedBudget}</td>
                    <td>${item.startDateStr}</td>
                    <td>${item.endDateStr}</td>
                    <td>${item.status}</td>
                    <td>${item.createdDateStr}</td>
                    <td>
                        <a href="javascript:;" title="Edit" id="btnEdit" data-cid="${item.clientBudgetId}">
                            <i class="fa fa-edit" style="font-size:20px;color:#7979e9"></i>
                        </a>&nbsp;&nbsp;&nbsp;
                        <a href="javascript:;" title="Delete" id="btndelete" data-cid="${item.clientBudgetId}">
                            <i class="fas fa-trash-alt" style="font-size:20px;color:#e77b7b"></i>
                        </a>
                    </td>
                </tr>`;
            });
            $("#tbldata").html(rows);
            setPagination(response[0].totalRecords, pageIndex, pageSize);
            $('.norec').hide();
            $('.tblcontent').show();
        } else {
            $("#tbldata").html('<tr><td>No Data Found</td></tr>');
            $('.norec').show();
            $('.tblcontent').hide();
        }
    });
}

// Pagination logic
function setPagination(totalRecords, currentPage, pageSize) {
    var totalPages = Math.ceil(totalRecords / pageSize);
    var visiblePages = 3;
    var paginationHtml = '';

    if (totalPages > 1) {
        if (currentPage > 1) {
            paginationHtml += `<a href="javascript:;" class="btn btn-sm btn-outline-primary pagination-prev" data-page="${currentPage - 1}">Previous</a>`;
        }

        var startPage, endPage;
        if (totalPages <= visiblePages) {
            startPage = 1;
            endPage = totalPages;
        } else {
            if (currentPage <= Math.ceil(visiblePages / 2)) {
                startPage = 1;
                endPage = visiblePages;
            } else if (currentPage + Math.floor(visiblePages / 2) >= totalPages) {
                startPage = totalPages - visiblePages + 1;
                endPage = totalPages;
            } else {
                startPage = currentPage - Math.floor(visiblePages / 2);
                endPage = currentPage + Math.floor(visiblePages / 2);
            }
        }

        for (var i = startPage; i <= endPage; i++) {
            paginationHtml += `<a href="javascript:;" class="btn btn-sm ${i === currentPage ? 'btn-primary' : 'btn-light'} pagination-link" data-page="${i}">${i}</a>`;
        }

        if (currentPage < totalPages) {
            paginationHtml += `<a href="javascript:;" class="btn btn-sm btn-outline-primary pagination-next" data-page="${currentPage + 1}">Next</a>`;
        }
    }

    $("#pagination").html(paginationHtml);
}

// Event listeners for pagination
$(document).on('click', '.pagination-link', function () {
    var page = $(this).data('page');
    var pageSize = $('#ddlpagesize').val();
    var searchStr = $('#SearchStr').val();
    BindClients(page, pageSize, 1, searchStr);
});

$(document).on('click', '.pagination-prev', function () {
    var page = $(this).data('page');
    var pageSize = $('#ddlpagesize').val();
    var searchStr = $('#SearchStr').val();
    BindClients(page, pageSize, 1, searchStr);
});

$(document).on('click', '.pagination-next', function () {
    var page = $(this).data('page');
    var pageSize = $('#ddlpagesize').val();
    var searchStr = $('#SearchStr').val();
    BindClients(page, pageSize, 1, searchStr);
});
