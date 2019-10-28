var usergroupController = {
    init: function () {
        usergroupController.registerEvents();
    },

    registerEvents: function () {
        $('.btnStatus').off('click').on('click', function (e) {
            e.preventDefault();
            var roleId = $(this).data("roleid");
            var usergroupID = $(this).data("usergroupid");
            var data = {
                roleID: roleId,
                usergroupID: usergroupID
            };
            $.ajax({
                url: "/Admin/UserGroup/CreateOrDeleteRole",
                data: { model: JSON.stringify(data) },
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
    },

  
};

usergroupController.init();