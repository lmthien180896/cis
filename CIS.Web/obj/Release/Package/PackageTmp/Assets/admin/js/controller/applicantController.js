var applicantController = {
    init: function () {
        applicantController.registerEvents();
    },

    registerEvents: function () {
        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data("id");
            bootbox.confirm("Bạn có chắc muốn xoá bản ghi đã chọn?", function (result) {
                if (result) {
                    applicantController.delete(id);
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
                        url: "/Admin/Applicant/DeleteAll",
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

        $('.btnSendMail').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $('#modalSendMail').modal('show');
            applicantController.getEmail(id);
        });

        $('#btnSubmit').off('click').on('click', function (e) {
            e.preventDefault();
            var email = $('#txtEmail').val();
            txtMessage.updateElement();           
            var message = $('#txtMessage').val();
            var data = {
                toemail: email,
                message: message
            };
            $.ajax({
                url: "/Admin/Applicant/SendEmail",
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

    delete: function (id) {
        $.ajax({
            url: '/Admin/Applicant/Delete',
            type: 'POST',
            datatype: 'json',
            data: { id: id },
            success: function (response) {
                if (response.status == true) {
                    toasrt.success(response.message);
                }
                else {
                    toasrt.error(response.message);
                }
            }            
        });
    },

    getEmail: function (id) {
        $.ajax({
            url: '/Admin/Applicant/GetEmail',
            type: 'GET',
            datatype: 'json',
            data: { id: id },
            success: function (response) {
                if (response.status == true) {
                    $('#txtEmail').val(response.data);                   
                }
                else {
                    console.log(response.message);
                }
            }
        });
    }
};

applicantController.init();