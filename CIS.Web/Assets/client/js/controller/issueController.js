var issue = {
    init: function () {
        issue.registerEvents();
    },
    registerEvents: function () {               
        $("#btnSubmit").off('click').on('click', function (e) {
            e.preventDefault();               
            var validated = false;
            //Topic            
            if ($('#sltTopic').val() === "") {
                $("#warning-topic").html("Vui lòng chọn loại yêu cầu");
                validated = false;
            }
            else {                
                $("#warning-topic").hide();
                if ($('#txtFullname').val() === "") {
                    $("#warning-fullname").html("Vui lòng điền họ tên");
                    validated = false;
                }
                else {
                    $("#warning-fullname").hide();
                    var emails = $('#txtEmail').val();
                    emails = emails.replace(" ", "").split(';');
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
                        $("#warning-email").html("Email cần có đuôi 'hcmiu.edu.vn'");
                        validated = false;
                    }
                    else {
                        $("#warning-email").hide();
                        if ($('#txtPlace').val() === "") {
                            $("#warning-place").html("Vui lòng điền nơi gửi yêu cầu");
                        }
                        else {
                            $("#warning-place").hide();
                            if ($('#txtIssueDetails').val() === "") {
                                $("#warning-issuedetails").html("Vui lòng điền chi tiết yêu cầu");
                            }
                            else {
                                $("#warning-issuedetails").hide();
                                if ($('#txtCaptcha').val() !== "4") {
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

            if (validated)
                $("#contactPageForm").submit();           
        });

        $("input[type='file']").on("change", function () {
            if (this.files[0].size > 2000000) {
                alert("Please upload file less than 2MB. Thanks!!");
                $(this).val('');
            }
        });
    }

    //saveData: function () {
    //    var topic = $('#sltTopic').val();
    //    var fullname = $('#txtFullname').val();
    //    var email = $('#txtEmail').val();
    //    var phone = $('#txtPhone').val();
    //    var issueDetails = $('#txtIssueDetails').val();
    //    var place = $('#txtPlace').val();
    //    var progress = "Chờ xử lí";  
    //    var issue = {
    //        Topic: topic,
    //        Fullname: fullname,
    //        Email: email,
    //        Phone: phone,
    //        IssueDetails: issueDetails,
    //        Place: place,
    //        Progress: progress
    //    };
    //    $.ajax({
    //        url: '/Issue/SubmitIssue',
    //        type: 'POST',
    //        datatype: 'json',
    //        data: { model: JSON.stringify(issue) },
    //        success: function (response) {
    //            if (response.status) {
    //                alert(response.message);                
    //            }
    //            else {
    //                alert(response.message);         
    //            }
    //        }
    //    });
    //}
};

issue.init();