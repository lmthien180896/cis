var slideController = {
    init: function () {
        slideController.registerEvents();
    },

    registerEvents: function () {
        $('#btnDelteImage').off('click').on('click', function (e) {
            e.preventDefault();
            slideController.deleteImage();           
        });               
        
        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data("id");
            bootbox.confirm("Bạn có chắc muốn xoá bản ghi này?", function (result) {
                if (result) {
                    $.ajax({
                        url: "/Admin/Slide/Delete",
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

        $('.btnUpdate').off('click').on('click', function (e) {
            e.preventDefault();
            $('#modalAddUpdate').modal('show');
            var id = $(this).data("id");
            slideController.loadDetail(id);
        });  

        $('#btnSubmit').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $('#txtID').val();
            var name = $('#txtName').val();
            var image = $('#txtImage').val();
            var data = {
                id: id,
                name: name,
                image: image
            };
            $.ajax({
                url: "/Admin/Slide/AddOrUpdate",
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

        $('#btnAdd').off('click').on('click', function (e) {
            e.preventDefault();            
            slideController.resetForm();
        });
    },

    loadDetail: function (id) {
        $.ajax({
            url: "/Admin/Slide/LoadDetail",
            data: { id: id },
            dataType: "json",
            type: "GET",
            success: function (response) {
                if (response.status === true) {
                    var data = response.data;
                    $('#txtID').val(data.ID);
                    $('#txtName').val(data.Name);
                    $('#txtImage').val(data.Image);
                    $('#img').prop("src", data.Image);
                    $('#btnDelteImage').removeClass('hide');
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
        slideController.deleteImage();

    },

    deleteImage: function () {
        $('#txtImage').val("");
        $('#img').prop("src", "");
        $('#btnDelteImage').addClass('hide');
    }
};

slideController.init();