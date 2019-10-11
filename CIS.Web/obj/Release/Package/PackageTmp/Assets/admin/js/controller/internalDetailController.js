var internalDetail = {
    init: function () {
        internalDetail.registerEvents();
    },

    registerEvents: function () {
        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data("id");
            bootbox.confirm("Bạn có chắc muốn xoá bản ghi đã chọn?", function (result) {
                if (result) {
                    internalDetail.delete(id);
                }
            });
        });

        $('.btnUpdate').off('click').on('click', function (e) {
            e.preventDefault();
            $('#modalAddUpdate').modal('show');
            var id = $(this).data("id");
            internalDetail.loadDetail(id);
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
                        url: "/Admin/InternalDetail/DeleteAll",
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

            $('#btnAdd').off('click').on('click', function (e) {
                e.preventDefault();
                internalDetail.resetForm();
            });
        });        

        $('#btnAdd').off('click').on('click', function (e) {
            e.preventDefault();
            internalDetail.resetForm();
        });

        $('#btnSubmit').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $('#txtID').val();
            var name = $('#txtName').val();
            var fileUrl = $('#txtFileUrl').val();
            var data = {
                id: id,
                name: name,
                fileUrl: fileUrl
            };
            $.ajax({
                url: "/Admin/InternalDetail/AddOrUpdate",
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
    },

    resetForm: function () {
        $('#txtID').val(0);
        $('#txtName').val("");
        $('#txtFileUrl').val("");        
    },

    delete: function (id) {
        $.ajax({
            url: '/Admin/InternalDetail/Delete',
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

    loadDetail: function (id) {
        $.ajax({
            url: '/Admin/InternalDetail/LoadDetail',
            type: 'GET',
            datatype: 'json',
            data: { id: id },
            success: function (response) {
                if (response.status == true) {
                    $('#txtID').val(response.data.ID);
                    $('#txtName').val(response.data.Name);
                    $('#txtFileUrl').val(response.data.FileUrl);    
                }
                else {
                    console.log(response.message);
                }
            }
        });
    }
};

internalDetail.init();