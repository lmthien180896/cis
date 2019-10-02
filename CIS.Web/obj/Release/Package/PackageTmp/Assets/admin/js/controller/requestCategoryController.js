var requestCategoryController = {
    init: function () {
        requestCategoryController.registerEvents();
    },

    registerEvents: function () {
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
                        url: "/Admin/RequestCategory/DeleteAll",
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

        $('.btnStatus').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data("id");
            $.ajax({
                url: "/Admin/RequestCategory/ChangeStatus",
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
        });

        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data("id");
            bootbox.confirm("Bạn có chắc muốn xoá bản ghi này?", function (result) {
                if (result) {
                    $.ajax({
                        url: "/Admin/RequestCategory/Delete",
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

        $('#btnSubmit').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $('#txtID').val();
            var name = $('#txtName').val();
            var status = $('#chkStatus').prop('checked');
            var data = {
                id: id,
                name: name,
                status: status
            };
            $.ajax({
                url: "/Admin/RequestCategory/AddOrUpdate",
                data: { model: JSON.stringify(data) },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status === true) {
                        window.location.reload();
                    }
                    else {
                        window.location.reload();
                    }
                }
            });
        });

        $('.btnUpdate').off('click').on('click', function (e) {
            e.preventDefault();
            $('#modalAddUpdate').modal('show');
            var id = $(this).data("id");            
            requestCategoryController.loadDetail(id);
        });        
    },

    loadDetail: function (id) {
        $.ajax({
            url: "/Admin/RequestCategory/LoadDetail",
            data: { id: id},
            dataType: "json",
            type: "GET",
            success: function (response) {
                if (response.status === true) {
                    var data = response.data;
                    $('#txtID').val(data.ID);
                    $('#txtName').val(data.Name);
                    $('#chkStatus').prop('checked', data.Status);                    
                }
                else {
                    console.log(response.message);
                }
            }
        });
    },

    resetForm: function () {
        $('#txtID').val(0);
        $('#txtName').val("");
        $('#chkStatus').prop('checked', false);
    }
};

requestCategoryController.init();