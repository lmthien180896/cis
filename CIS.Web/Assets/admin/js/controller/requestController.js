var requestController = {
    init: function () {
        requestController.registerEvents();
        requestController.loadAllRequests();        
    },

    registerEvents: function () {
        $('#btnSubmit').off('click').on('click', function () {
            var id = $('#txtID').val();
            var categoryId = $('#sltCategoryID').val();
            var senderName = $('#txtSenderName').val();
            var place = $('#txtPlace').val();
            var email = $('#txtEmail').val();
            var createdDate = $('#txtCreatedDate').val();
            var detail = $('#txtDetail').val();
            var progress = $('.progress').val();
            var data = {
                id: id,
                categoryId: categoryId,
                senderName: senderName,
                place: place,
                email: email,
                createdDate: createdDate,
                detail: detail,
                progress: progress
            };
            $.ajax({
                url: "/Admin/Request/AddOrUpdate",
                type: "POST",
                data: { model: JSON.stringify(data) },
                datatype: "json",
                success: function (response) {
                    if (response.status) {
                        alert("yeah");
                        requestController.registerEvents();
                    }
                }
            });      
        });
    },

    loadAllRequests: function () {
        $.ajax({
            url: "/Admin/Request/LoadAllRequests",
            type: "GET",
            data: {},
            datatype: "json",
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template-allRequests').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            RequestCategory: item.RequestCategory,
                            SenderName: item.SenderName,                            
                            Place: item.Place,
                            Progress: item.Progress === "waiting" ? "<span class=\"label label-warning label-progress\" ><i class=\"fa fa-refresh\"></i></span>" : item.Progress === "Đang xử lí" ? "<span class=\"label label-info label-progress\" ><i class=\"fa fa-wrench\"></i></span>" : " <span class=\"label label-success label-progress\"><i class=\"fa fa-check\"></i></span>",
                            SentDate: item.SentDate
                        });
                    });
                    $('#tblAllRequests').html(html);
                    requestController.paging("all", response.totalRow, function () {
                        requestController.loadAllIssues();
                    });
                    requestController.registerEvents();
                }
            }
        });      
    }
};

requestController.init();