var loginController = {
    init: function () {
        loginController.registerEvents();
    },

    registerEvents: function () {
        $('#btnLogin').off('click').on('click', function (e) {
            e.preventDefault();
            var username = $('#txtUsername').val();
            var password = $('#txtPassword').val();
            var data = {
                username: username,
                password: password
            }
            $.ajax({
                url: '/Admin/Login/CheckAuthen',
                type: 'POST',
                data: { model: JSON.stringify(data) },
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        window.location = "";
                    }
                    else {
                        $('#message').text('Tài khoản hoặc mật khẩu không đúng');
                    }
                }
            });
        });
    }
};

loginController.init();