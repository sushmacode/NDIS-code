$(document).ready(function () {
    //alert(1)
    getSchedule();
    $(document).on('click', '#addentitry', function (event) {
        window.location.href = '/admin/company/add';
    });

    $(document).on('click', '#btnEdit', function (event) {
        var id = $(this).attr('data-cid');
        window.location.href = '/admin/company/add?id='+id;
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
                gl.ajaxreqloader(apiurl + "company/Delete", "GET", { id: id }, function (response) {
                    Swal.fire('Deleted!', 'The item has been deleted.', 'success');
                    getSchedule();
                });
            }
        });
    });
   

    $('#kt_filter_search').on('input', function () {
        var searchStr = $(this).val();
        BindSchedule(1, $('#ddlpagesize').val(), 1, searchStr);
    });

    $(document).on('click', '#shwallbtn', function (event) {
        $('#kt_filter_search').val('');
        BindSchedule(1, $('#ddlpagesize').val(), 1, '');
    });

    $(document).on("click", ".d-paging", function (event) {
        var pagesize = $('#ddlpagesize').val();
        var pageindex = $(this).attr('_id');
        var searchStr = $('#kt_filter_search').val();
        BindSchedule(pageindex, pagesize, 1, searchStr);
    });

    $(document).on("change", '#ddlpagesize', function (event) {
        var searchStr = $('#kt_filter_search').val();
        var pagesize = $('#ddlpagesize').val();
        BindSchedule(1, pagesize, 1, searchStr);  // Reset to page 1 on page size change
    });
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
function getParam(param) {
    const params = new URLSearchParams(window.location.search);
    return params.get(param);
}
function BindSchedule(pageindex, pagesize, sortby, searchString) {
    //alert(searchString)
    searchString = (searchString && searchString.trim() !== "") ? searchString.trim() : "";
    var dataParams = {
        pgsize: pagesize,
        pgindex: pageindex,
        str: searchString,
        sortby: sortby,
        "fromDate": "",
        "toDate": "",
        "committee": 0,
        Status: getParam('status')
    }
    var row = '';
    var reccount = 0;
    gl.ajaxreqloader(apiurl + "Company/GetAdminCompanyList", "POST", dataParams, function (response) {
        console.log(response);
        if (response.length > 0) {
            reccount = response[0].totalRecords;
            $.each(response, function (i, item) {

                row += "<tr><td>" + item.companyName + "</td><td>" + item.companyType + "</td><td>" + item.contactNumber + "</td><td>" + item.emailId + "</td><td>" + item.status + "</td><td>" + item.createdDateStr + "</td><td><a href='javascript:;' title='Edit' id='btnEdit' data-toggle='modal' data-target='#update' data-cid=" + item.companyId + "><i class='fa fa-edit' style='font-size:20px;color:#7979e9'></i></i></a>&nbsp;&nbsp;&nbsp;<a href='javascript:;' title='Delete' id='btndelete'  data-cid=" + item.companyId + "><i class='fas fa-trash-alt' style='font-size:20px;color:#e77b7b'></i></a></td></tr>";

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
$(document).on('click', '.pagination-link', function () {
    var page = $(this).data('page');
    var pageSize = $('#ddlpagesize').val();
    var searchStr = $('#kt_filter_search').val();
    BindSchedule(page, pageSize, 1, searchStr);
});

$(document).on('click', '.pagination-prev', function () {
    var page = $(this).data('page');
    var pageSize = $('#ddlpagesize').val();
    var searchStr = $('#kt_filter_search').val();
    BindSchedule(page, pageSize, 1, searchStr);
});

$(document).on('click', '.pagination-next', function () {
    var page = $(this).data('page');
    var pageSize = $('#ddlpagesize').val();
    var searchStr = $('#kt_filter_search').val();
    BindSchedule(page, pageSize, 1, searchStr);
});

$('#ddlpagesize').on('change', function () {
    var pageSize = $(this).val();
    var searchStr = $('#kt_filter_search').val();
    BindSchedule(1, pageSize, 1, searchStr);  // Reset to page 1 on page size change
});
