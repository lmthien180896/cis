var postController = {
    init: function () {
        postController.registerEvents();
    },

    registerEvents: function () {
        $('#btnDelteImage').off('click').on('click', function (e) {
            e.preventDefault();
            $('#txtImage').val("");
            $('#img').prop("src", "");
            $('#btnDelteImage').addClass('hide');
        });

        $('#txtName').on('change', function () {
            var input = $('#txtName').val();
            $.ajax({
                url: "/Admin/Post/GetAlias",
                data: { title: input },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status === true) {
                        $('#txtAlias').val(response.data);
                    }
                    else {
                        console(response.message);
                    }
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
                        url: "/Admin/Post/DeleteAll",
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
                url: "/Admin/Post/ChangeStatus",
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
                        url: "/Admin/Post/Delete",
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

        $('#sltFilter').on('change', function () {
            var categoryId = $(this).val();
            url = "/Admin/Post/Index?filterCategoryId=" + categoryId;
            window.location.replace(url);
        });
    }
};

postController.init();