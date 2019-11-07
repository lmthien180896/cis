var requestController = {
    init: function () {
        requestController.registerEvents();           
    },

    registerEvents: function () {
        $('#btnExportExcel').off('click').on('click', function (e) {
            e.preventDefault();
            var fromDate = $('#txtFromDate').val();
            var toDate = $('#txtToDate').val();    
            $.ajax({
                url: "/Admin/Request/ExportExcel",
                type: "GET",
                data: {
                    fromDate: fromDate,
                    toDate: toDate,                  
                },
                datatype: "json",
                success: function (response) {
                    if (response.status) {
                        toastr.success("Xuất file Excel thành công. Bấm Download để tải về");
                        $('#btnDownload').prop('href', response.downloadPath);
                        $('#btnExportExcel').hide();
                        $('#btnDownload').show();
                    }
                    else {
                        toastr.error(response.message);
                    }
                }
            });
        });

        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data("id");
            bootbox.confirm("Bạn có chắc muốn xoá bản ghi này?", function (result) {
                if (result) {
                    $.ajax({
                        url: "/Admin/Request/Delete",
                        data: { id: id },
                        dataType: "json",
                        type: "POST",
                        success: function (response) {
                            if (response.status === true) {
                                window.location.reload();
                            }
                            else {
                                console(response.message);
                            }
                        }
                    });
                }
            });
        });

        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            bootbox.confirm("Bạn có chắc muốn xoá bản ghi đã chọn?", function (result) {
                if (result) {
                    var listId = "";
                    $.each($('.chkDelete'), function (i, item) {
                        if ($(this).prop('checked')) {
                            listId = listId + $(this).data("id") + "-";
                        }
                    });
                    $.ajax({
                        url: "/Admin/Request/DeleteAll",
                        data: { listId: JSON.stringify(listId) },
                        dataType: "json",
                        type: "POST",
                        success: function (response) {
                            if (response.status === true) {
                                window.location.reload();
                            }
                            else {
                                console(response.message);
                            }
                        }
                    });
                }
            });
        });

        $('.btnUpdate').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data("id");
            $('#modalAddUpdate').modal('show');
            requestController.loadDetail(id);
        });

        $('#btnSubmit').off('click').on('click', function () {
            var id = $('#txtID').val();
            var progress = $('#txtProgress').val();
            var categoryId = $('#sltCategoryID').val();
            var senderName = $('#txtSenderName').val();
            var place = $('#txtPlace').val();
            var email = $('#txtEmail').val();
            var phone = $('#txtPhone').val();
            var createdDate = $('#txtCreatedDate').val();
            var detail = $('#txtDetail').val();
            var data = {
                id: id,
                categoryId: categoryId,
                senderName: senderName,
                place: place,
                email: email,
                phone: phone,
                createdDate: createdDate,
                detail: detail,
                progress: progress
            };
            $.ajax({
                url: "/Admin/Request/AddOrUpdate",
                type: "POST",
                data: { model: JSON.stringify(data) },
                datatype: "json",
                success: function (response) {
                    if (response.status) {
                        $('#modalAddUpdate').modal('hide');
                        window.location.reload();
                    }
                    else {
                        window.location.reload();
                    }
                }
            });
        });

        $('#btnAdd').off('click').on('click', function (e) {
            e.preventDefault();
            requestController.resetForm();
            requestController.registerEvents();
        });

        $('.btnSupport').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data("id");
            requestController.support(id);
        });

        $('.btnSendConfirm').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data("id");
            requestController.sendConfirm(id);
        });

        $('.btnAddNote').off('click').on('click', function (e) {
            $('#modalAddNote').modal('show');            
            var id = $(this).data("id");
            $('#txtRequestId').val(id);
        });

        $('#btnSubmitNote').off('click').on('click', function(e) {
            e.preventDefault();
            var requestId = $('#txtRequestId').val();
            var note = $('#txtNote').val();
            var data = {
                requestId: requestId,
                note: note
            };
            requestController.submitNote(data);
        });
    },   

    loadDetail: function (id) {
        $.ajax({
            url: "/Admin/Request/LoadDetail",
            data: { id: id },
            dataType: "json",
            type: "GET",
            success: function (response) {
                if (response.status === true) {
                    var data = response.data;
                    $('#txtID').val(data.ID);                    
                    $('#sltCategoryID').val(data.CategoryID);
                    $('#txtSenderName').val(data.SenderName);
                    $('#txtPlace').val(data.Place);
                    $('#txtEmail').val(data.Email);
                    $('#txtPhone').val(data.Phone);
                    $('#txtCreatedDate').val(data.SentDate);
                    $('#txtDetail').val(data.Detail);                    
                    $('#txtProgress').val(data.Progress);                   
                }
                else {
                    console.log(response.message);
                }
            }
        });
    },

    resetForm: function () {
        $('#txtID').val(0);
        $('#txtProgress').val("");
        $('#sltCategoryID').val("");
        $('#txtSenderName').val("");
        $('#txtPlace').val("");
        $('#txtEmail').val("");
        $('#txtPhone').val("");
        $('#txtCreatedDate').val("");
        $('#txtDetail').val("");      
    },

    support: function (id) {
        $.ajax({
            url: '/Admin/Request/Support',
            type: 'POST',
            datatype: 'json',
            data: { id: id },
            success: function (response) {
                if (response.status == true) {
                    window.location = "/Admin/Request/SupportingQueue";
                }
                else {
                    console.log(response.message);
                }
            }
        });
    },

    sendConfirm: function (id) {
        $.ajax({
            url: '/Admin/Request/SendConfirm',
            type: 'POST',
            datatype: 'json',
            data: { id: id },
            success: function (response) {
                if (response.status == true) {
                    window.location.reload();
                }
                else {
                    console.log(response.message);
                }
            }
        });
    },   

    submitNote: function (data) {
        $.ajax({
            url: '/Admin/Request/SubmitNote',
            type: 'POST',
            datatype: 'json',
            data: { model: JSON.stringify(data) },
            success: function (response) {
                if (response.status == true) {
                    window.location.reload();
                }               
            }
        });
    }


};

requestController.init();