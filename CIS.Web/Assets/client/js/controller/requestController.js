var requestController = {
    init: function () {
        requestController.registerEvents();
    },
    registerEvents: function () {
        $("#btnSubmit").off('click').on('click', function (e) {
            e.preventDefault();
            $("#btnSubmit").prop("disabled", true);
            var validated = false;
            var categoryId = $('#sltCategoryId').val();
            var senderName = $('#txtSenderName').val();
            var emails = $('#txtEmail').val();
            var phone = $('#txtPhone').val();
            var detail = $('#txtDetail').val();
            var place = $('#txtPlace').val();    
            var files = "";
            var uploadedFiles = $('#files')[0].files;
            for (var i = 0; i < uploadedFiles.length; i++) {
                files += "," + uploadedFiles[i];
            }

            if (categoryId == "") {
                $('#sltCategoryId').focus();
                $("#warning-category").html("Vui lòng chọn loại yêu cầu");
                validated = false;
            }
            else {
                $("#warning-category").hide();
                if (senderName == "") {
                    $('#txtSenderName').focus()
                    $("#warning-senderName").html("Vui lòng điền họ tên");
                    validated = false;
                }
                else {
                    $("#warning-senderName").hide();                   
                    emails = emails.replace(" ", "").split(',');
                    var email_validated = true;
                    $.each(emails, function (index, value) {
                        if (!value.endsWith("@hcmiu.edu.vn")) {
                            email_validated = false;
                            return false;
                        }
                        else
                            email_validated = true;
                    });
                    if (email_validated === false) {
                        $('#txtEmail').focus();
                        $("#warning-email").html("Email cần có đuôi 'hcmiu.edu.vn'");
                        validated = false;
                    }
                    else {
                        $("#warning-email").hide();
                        if (place == "") {
                            $('#txtPlace').focus();
                            $("#warning-place").html("Vui lòng điền nơi gửi yêu cầu");
                        }
                        else {
                            $("#warning-place").hide();
                            if (detail == "") {
                                $('#txtDetail').focus();
                                $("#warning-detail").html("Vui lòng điền chi tiết yêu cầu");
                            }
                            else {
                                $("#warning-detail").hide();
                                if ($('#txtCaptcha').val() !== "4") {
                                    $('#txtCaptcha').focus();
                                    $("#warning-captcha").html("Vui lòng trả lời đúng câu hỏi bảo mật");
                                }
                                else {
                                    $("#warning-captcha").hide();
                                    validated = true;
                                }
                            }
                        }
                    }
                }

            }

            if (validated) {
                $('#contactPageForm').submit();
            }
            else {
                $("#btnSubmit").prop("disabled", false);
            }
        });

        $("input[type='file']").on("change", function () {
            if (this.files[0].size > 2000000) {
                alert("Please upload file less than 2MB. Thanks!!");
                $(this).val('');
            }
        });
    }   
};

requestController.init();