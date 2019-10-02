var footerController = {
    init: function () {
        footerController.registerEvents();
    },

    registerEvents: function () {
        $('.btnSave').off('click').on('click', function () {
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
                url: '/Admin/Footer/UpdateFooter',
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
    }
};

footerController.init();