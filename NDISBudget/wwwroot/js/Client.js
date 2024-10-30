$(document).ready(function () {
    getClints();

    $(document).on('click', '#addentitry', function (event) {
        window.location.href = '/company/client/add';
    });

    $(document).on('click', '#btnEdit', function (event) {
        var id = $(this).attr('data-cid');
        window.location.href = '/company/client/add?id=' + id;
    });

    $(document).on('click', '#btndelete', function (event) {
        var id = $(this).attr('data-cid');  // Fetch the client ID

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
                gl.ajaxreqloader(apiurl + "Client/Delete", "GET", { id: id }, function (response) {
                    Swal.fire('Deleted!', 'The item has been deleted.', 'success');
                    getClints();
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

function getClints() {
    BindClients(1, 10, 1, '');
}

function BindClients(pageindex, pagesize, sortby, searchString) {
    // Ensure searchString is trimmed and not null/undefined
    searchString = (searchString && searchString.trim() !== "") ? searchString.trim() : "";

    var dataParams = {
        CompanyId: $('#CompanyId').val(),
        pgsize: pagesize,
        pgindex: pageindex,
        str: searchString,  // Send search term to the API
        sortby: sortby,
        fromDate: "",
        toDate: "",
        committee: 0,
        Status: getParam('status')
    };

    gl.ajaxreqloader(apiurl + "Client/GetAdminClientsList", "POST", dataParams, function (response) {
        console.log(response);
        var row = '';
        if (response.length > 0) {
            $.each(response, function (i, item) {
                row += "<tr><td>" + item.name + "</td><td>" + item.mobileNumber + "</td><td>" + item.emailId + "</td><td>" + item.status + "</td><td>" + item.createdDateStr +
                    "</td><td class='text-centre'><a href='javascript:;' title='Edit' id='btnEdit' data-toggle='modal' data-target='#update' data-cid=" +
                    item.clientId + "><i class='fa fa-edit' style='font-size:20px;color:#7979e9'></i></a>&nbsp;&nbsp;&nbsp;<a href='javascript:;' title='Delete' id='btndelete' data-cid=" + item.clientId + "><i class='fas fa-trash-alt' style='font-size:20px;color:#e77b7b'></i></a></td></tr>";
            });
            $("#tbldata").html(row);
            setPagination(response[0].totalRecords, pageindex, pagesize);
        } else {
            $("#tbldata").html("<tr><td colspan='6'>No Data Found</td></tr>");
        }
    });
}
function setPagination(totalRecords, currentPage, pageSize) {
    var totalPages = Math.ceil(totalRecords / pageSize);
    var visiblePages = 3; // Number of page links to show at once
    var paginationHtml = '';

    if (totalPages > 1) {
        // Previous Button
        if (currentPage > 1) {
            paginationHtml += `<a href="javascript:;" class="btn btn-sm btn-outline-primary pagination-prev" data-page="${currentPage - 1}">Previous</a>`;
        }

        // Page Numbers
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

        // Next Button
        if (currentPage < totalPages) {
            paginationHtml += `<a href="javascript:;" class="btn btn-sm btn-outline-primary pagination-next" data-page="${currentPage + 1}">Next</a>`;
        }
    }

    $("#pagination").html(paginationHtml);
}

// Event listeners for pagination and page size change
$(document).on('click', '.pagination-link', function () {
    var page = $(this).data('page');
    var pageSize = $('#ddlpagesize').val();
    var searchStr = $('#kt_filter_search').val();
    BindClients(page, pageSize, 1, searchStr);
});

$(document).on('click', '.pagination-prev', function () {
    var page = $(this).data('page');
    var pageSize = $('#ddlpagesize').val();
    var searchStr = $('#kt_filter_search').val();
    BindClients(page, pageSize, 1, searchStr);
});

$(document).on('click', '.pagination-next', function () {
    var page = $(this).data('page');
    var pageSize = $('#ddlpagesize').val();
    var searchStr = $('#kt_filter_search').val();
    BindClients(page, pageSize, 1, searchStr);
});

$('#ddlpagesize').on('change', function () {
    var pageSize = $(this).val();
    var searchStr = $('#kt_filter_search').val();
    BindClients(1, pageSize, 1, searchStr);  // Reset to page 1 on page size change
});
