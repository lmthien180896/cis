var footer = {
    init: function () {
        footer.registerEvents();
    },

    registerEvents: function () {
        $('.btnSaveFooter').off('click').on('click', function () {
            var id = $(this).data("id");
            var footerName = "#footerName-" + id;
            var footerContent = "#footerContent-" + id;
            var name = $(footerName).val();
            var content = $(footerContent).val();
            var data = {
                id: id,
                name: name,
                content: content
            };
            $.ajax({
                url: '/Admin/InterfaceComponent/UpdateFooter',
                type: 'POST',
                datatype: 'json',
                data: { model: JSON.stringify(data) },
                success: function (response) {
                    if (response.status == true) {
                        window.location.reload();
                    }
                    else {
                        console.log(response.message);
                    }
                }
            });
        });

        $('#btnDelteImage').off('click').on('click', function (e) {
            e.preventDefault();
            $('#txtImage').val("");
            $('#img').prop("src", "");
            $('#btnDelteImage').addClass('hide');
        });

        $('.btnDeleteSlide').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data("id");
            bootbox.confirm("Bạn có chắc muốn xoá bản ghi này?", function (result) {
                if (result) {
                    $.ajax({
                        url: "/Admin/InterfaceC/Delete",
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


        $('.btnSaveSlide').off('click').on('click', function () {
            var id = $(this).data("id");
            var slideName = "#slideName-" + id;
            var slideImage = "#slideImage-" + id;
            var name = $(slideName).val();
            var image = $(slideImage).val();
            var data = {
                id: id,
                name: name,
                image: image
            };
            $.ajax({
                url: '/Admin/InterfaceComponent/UpdateSlide',
                type: 'POST',
                datatype: 'json',
                data: { model: JSON.stringify(data) },
                success: function (response) {
                    if (response.status == true) {
                        window.location.reload();
                    }
                    else {
                        console.log(response.message);
                    }
                }
            });
        });
    },    
};

footer.init();