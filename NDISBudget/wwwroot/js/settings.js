$(() => {
    var currpageurl = window.location.href.toLowerCase();
    var area = "Admin";
    var manageCategoriesUrl = websiteurl + "/" + area + "/Settings/Categories";
    var managePricesUrl = websiteurl + "/" + area + "/Settings/Prices";
    var createItemsUrl = websiteurl + "/" + area + "/Settings/Items";

    
    // Manage Promotions Page
    if (currpageurl.indexOf(manageCategoriesUrl.toLowerCase()) != -1) {

        //alert(1)
        getCategories();
        $(document).on('click', '#addentitry', function (event) {
            window.location.href = '/Admin/Settings/AddCategories';
        });

        $(document).on('click', '#btnEdit', function (event) {
            var id = $(this).attr('data-cid');
            window.location.href = '/Admin/Settings/AddCategories?id=' + id;
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
                    gl.ajaxreqloader(apiurl + "Settings/DeleteCategory", "GET", { id: id }, function (response) {
                        Swal.fire('Deleted!', 'The item has been deleted.', 'success');
                        getCategories();
                    });
                }
            });
        });
        $('#kt_filter_search').on('input', function () {
            var searchStr = $(this).val();
            BindCategories(1, $('#ddlpagesize').val(), 1, searchStr);
        });

        $(document).on('click', '#shwallbtn', function (event) {
            $('#kt_filter_search').val('');
            BindCategories(1, $('#ddlpagesize').val(), 1, '');
        });

        $(document).on("click", ".d-paging", function (event) {
            var pagesize = $('#ddlpagesize').val();
            var pageindex = $(this).attr('_id');
            var searchStr = $('#kt_filter_search').val();
            BindCategories(pageindex, pagesize, 1, searchStr);
        });

        $(document).on("change", '#ddlpagesize', function (event) {
            var searchStr = $('#kt_filter_search').val();
            var pagesize = $('#ddlpagesize').val();
            BindCategories(1, pagesize, 1, searchStr);  // Reset to page 1 on page size change
        });
        function getCategories() {
            BindCategories(1, 10, 1, '');           
        }
        function BindCategories(pageindex, pagesize, sortby, searchString) {
            searchString = (searchString && searchString.trim() !== "") ? searchString.trim() : "";
            //alert(strname)
            var dataParams = {
                CompanyId: $('#CompanyId').val(),
                pgsize: pagesize,
                pgindex: pageindex,
                str: searchString,
                sortby: sortby,
                "fromDate": "",
                "toDate": "",
                "committee": 0
            }
            console.log(dataParams);
            var row = '';
            var reccount = 0;
            gl.ajaxreqloader(apiurl + "Settings/GetAdminCategoriyList", "POST", dataParams, function (response) {
                console.log(response);
                if (response.length > 0) {
                    reccount = response[0].totalRecords;
                    $.each(response, function (i, item) {

                        row += "<tr><td>" + item.supportCategoryName + "</td><td>" + item.status + "</td><td>" + item.createdDateStr + "</td><td><a href='javascript:;' title='Edit' id='btnEdit' data-toggle='modal' data-target='#update' data-cid=" + item.supportCategoryId + "><i class='fa fa-edit' style='font-size:20px;color:#7979e9'></i></a>&nbsp;&nbsp;&nbsp;<a href='javascript:;' title='Delete' id='btndelete'  data-cid=" + item.supportCategoryId + "><i class='fas fa-trash-alt' style='font-size:20px;color:#e77b7b'></i></a></td></tr>";

                    });
                    $("#tbldata").html(row);
                    setPagination(response[0].totalRecords, pageindex, pagesize);
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
            BindCategories(page, pageSize, 1, searchStr);
        });

        $(document).on('click', '.pagination-prev', function () {
            var page = $(this).data('page');
            var pageSize = $('#ddlpagesize').val();
            var searchStr = $('#kt_filter_search').val();
            BindCategories(page, pageSize, 1, searchStr);
        });

        $(document).on('click', '.pagination-next', function () {
            var page = $(this).data('page');
            var pageSize = $('#ddlpagesize').val();
            var searchStr = $('#kt_filter_search').val();
            BindCategories(page, pageSize, 1, searchStr);
        });

        $('#ddlpagesize').on('change', function () {
            var pageSize = $(this).val();
            var searchStr = $('#kt_filter_search').val();
            BindCategories(1, pageSize, 1, searchStr);  // Reset to page 1 on page size change
        });


    }
    else if (currpageurl.indexOf(createItemsUrl.toLowerCase()) != -1) {
        getItems();
        $(document).on('click', '#addentitry', function (event) {
            window.location.href = '/Admin/Settings/AddItems';
        });

        $(document).on('click', '#btnEdit', function (event) {
            var id = $(this).attr('data-cid');
            window.location.href = '/Admin/Settings/AddItems?id=' + id;
        });
        $(document).on('click', '#btncopy', function (event) {
            var id = $(this).attr('data-cid');
            window.location.href = '/Admin/Settings/AddItems?id=' + id+'&Copy=1';
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
                    gl.ajaxreqloader(apiurl + "Settings/DeleteItem", "GET", { id: id }, function (response) {
                        Swal.fire('Deleted!', 'The item has been deleted.', 'success');
                        getItems();
                    });
                }
            });
        });
        $('#kt_filter_search').on('input', function () {
            var searchStr = $(this).val();
            BindItems(1, $('#ddlpagesize').val(), 1, searchStr);
        });

        $(document).on('click', '#shwallbtn', function (event) {
            $('#kt_filter_search').val('');
            BindItems(1, $('#ddlpagesize').val(), 1, '');
        });

        $(document).on("click", ".d-paging", function (event) {
            var pagesize = $('#ddlpagesize').val();
            var pageindex = $(this).attr('_id');
            var searchStr = $('#kt_filter_search').val();
            BindItems(pageindex, pagesize, 1, searchStr);
        });

        $(document).on("change", '#ddlpagesize', function (event) {
            var searchStr = $('#kt_filter_search').val();
            var pagesize = $('#ddlpagesize').val();
            BindItems(1, pagesize, 1, searchStr);  // Reset to page 1 on page size change
        });
        function getItems() {
            BindItems(1, 10, 1, '');
                  }
        function setCustomName(x) {
            return x == null ? '' : x;
        }
        function BindItems(pageindex, pagesize, sortby, searchString) {
            searchString = (searchString && searchString.trim() !== "") ? searchString.trim() : "";
            //alert(strname)
            var dataParams = {
                CompanyId: $('#CompanyId').val(),
                pgsize: pagesize,
                pgindex: pageindex,
                str: searchString,
                sortby: sortby,
                "fromDate": "",
                "toDate": "",
                "committee": 0
            }
            console.log(dataParams);
            var row = '';
            var reccount = 0;
            gl.ajaxreqloader(apiurl + "Settings/GetAdminSupportItemList", "POST", dataParams, function (response) {
                console.log(response);
                if (response.length > 0) {
                    reccount = response[0].totalRecords;
                    $.each(response, function (i, item) {

                        row += "<tr><td>" + item.supportItemNumber + "</td><td>" + item.supportItemName + "</td><td>" + item.supportCategoryName + "</td><td>" + setCustomName(item.customName) + "</td><td>" + item.isActive + "</td><td><a href='javascript:;' id='btnEdit' data-toggle='modal' data-target='#update' data-cid=" + item.supportItemId + "><i class='fa fa-edit' style='font-size:20px;color:#7979e9'></i></a>&nbsp;&nbsp;&nbsp;<a href='javascript:;' title='Delete' id='btncopy'  data-cid=" + item.supportItemId + "><i class='fas fa-trash-alt' style='font-size:20px;color:#e77b7b'></i></a>&nbsp;&nbsp;&nbsp;<a href='javascript:;' title='Copy' id='btncopy'  data-cid=" + item.supportItemId + "><i class='fa fa-copy m-r-5' style='font-size:20px;color:black'></i></a></td></tr>";

                    });
                    $("#tbldata").html(row);
                    setPagination(response[0].totalRecords, pageindex, pagesize);
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
            BindItems(page, pageSize, 1, searchStr);
        });

        $(document).on('click', '.pagination-prev', function () {
            var page = $(this).data('page');
            var pageSize = $('#ddlpagesize').val();
            var searchStr = $('#kt_filter_search').val();
            BindItems(page, pageSize, 1, searchStr);
        });

        $(document).on('click', '.pagination-next', function () {
            var page = $(this).data('page');
            var pageSize = $('#ddlpagesize').val();
            var searchStr = $('#kt_filter_search').val();
            BindItems(page, pageSize, 1, searchStr);
        });

        $('#ddlpagesize').on('change', function () {
            var pageSize = $(this).val();
            var searchStr = $('#kt_filter_search').val();
            BindItems(1, pageSize, 1, searchStr);  // Reset to page 1 on page size change
        });
    }
    else if (currpageurl.indexOf(managePricesUrl.toLowerCase()) != -1) {
        getPrices();
        $(document).on('click', '#addentitry', function (event) {
            window.location.href = '/Admin/Settings/AddPrices';
        });

        $(document).on('click', '#btnEdit', function (event) {
            var id = $(this).attr('data-cid');
            window.location.href = '/Admin/Settings/AddPrices?id=' + id;
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
                    gl.ajaxreqloader(apiurl + "Settings/DeletePrices", "GET", { id: id }, function (response) {
                        Swal.fire('Deleted!', 'The item has been deleted.', 'success');
                        getCategories();
                    });
                }
            });
        });
   

        $('#kt_filter_search').on('input', function () {
            var searchStr = $(this).val();
            BindPrices(1, $('#ddlpagesize').val(), 1, searchStr);
        });

        $(document).on('click', '#shwallbtn', function (event) {
            $('#kt_filter_search').val('');
            BindPrices(1, $('#ddlpagesize').val(), 1, '');
        });

        $(document).on("click", ".d-paging", function (event) {
            var pagesize = $('#ddlpagesize').val();
            var pageindex = $(this).attr('_id');
            var searchStr = $('#kt_filter_search').val();
            BindPrices(pageindex, pagesize, 1, searchStr);
        });

        $(document).on("change", '#ddlpagesize', function (event) {
            var searchStr = $('#kt_filter_search').val();
            var pagesize = $('#ddlpagesize').val();
            BindPrices(1, pagesize, 1, searchStr);  // Reset to page 1 on page size change
        });
        function getPrices() {
            BindPrices(1, 10, 1, '');
                    }
        function BindPrices(pageindex, pagesize, sortby, searchString) {
            searchString = (searchString && searchString.trim() !== "") ? searchString.trim() : "";
            //alert(strname)
            var dataParams = {
                CompanyId: $('#CompanyId').val(),
                pgsize: pagesize,
                pgindex: pageindex,
                str: searchString,
                sortby: sortby,
                "fromDate": "",
                "toDate": "",
                "committee": 0
            }
            console.log(dataParams);
            var row = '';
            var reccount = 0;
            gl.ajaxreqloader(apiurl + "Settings/GetAdminSupportItemPriceList", "POST", dataParams, function (response) {
                console.log(response);
                if (response.length > 0) {
                    reccount = response[0].totalRecords;
                    $.each(response, function (i, item) {

                        row += "<tr><td>" + item.supportItemName + "</td><td>" + item.stateName + "</td><td>" + item.price + "</td><td>" + item.isActive + "</td><td>" + item.createdDateStr + "</td><td><a href='javascript:;' title='Edit' id='btnEdit' data-toggle='modal' data-target='#update' data-cid=" + item.supportItemPriceListId + "><i class='fa fa-edit' style='font-size:20px;color:#7979e9'></i></a>&nbsp;&nbsp;&nbsp;<a href='javascript:;' title='Delete' id='btndelete'  data-cid=" + item.supportItemPriceListId + "><i class='fas fa-trash-alt' style='font-size:20px;color:#e77b7b'></i></a></td></tr>";

                    });
                    $("#tbldata").html(row);
                    setPagination(response[0].totalRecords, pageindex, pagesize);
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
            BindPrices(page, pageSize, 1, searchStr);
        });

        $(document).on('click', '.pagination-prev', function () {
            var page = $(this).data('page');
            var pageSize = $('#ddlpagesize').val();
            var searchStr = $('#kt_filter_search').val();
            BindPrices(page, pageSize, 1, searchStr);
        });

        $(document).on('click', '.pagination-next', function () {
            var page = $(this).data('page');
            var pageSize = $('#ddlpagesize').val();
            var searchStr = $('#kt_filter_search').val();
            BindPrices(page, pageSize, 1, searchStr);
        });

        $('#ddlpagesize').on('change', function () {
            var pageSize = $(this).val();
            var searchStr = $('#kt_filter_search').val();
            BindPrices(1, pageSize, 1, searchStr);  // Reset to page 1 on page size change
        });

    }



});




