var jobController = {
    init: function () {
        jobController.registerEvents();
    },

    registerEvents: function () {       
        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data("id");
            bootbox.confirm("Bạn có chắc muốn xoá bản ghi này?", function (result) {
                if (result) {
                    $.ajax({
                        url: "/Admin/Job/Delete",
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
            jobController.loadDetail(id);
        });

        $('#btnSubmit').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $('#txtID').val();
            var name = $('#txtName').val();
            txtDescription.updateElement();
            var description = $('#txtDescription').val();
            var status = $('#chkStatus').prop('checked');
            var data = {
                id: id,
                name: name,
                description: description,
                status: status
            };
            $.ajax({
                url: "/Admin/Job/AddOrUpdate",
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
            jobController.resetForm();
        });
    },

    loadDetail: function (id) {
        $.ajax({
            url: "/Admin/Job/LoadDetail",
            data: { id: id },
            dataType: "json",
            type: "GET",
            success: function (response) {
                if (response.status === true) {
                    var data = response.data;
                    $('#txtID').val(data.ID);
                    $('#txtName').val(data.Name);
                    $('#txtDescription').val(data.Description);
                    $('#chkStatus').prop('checked', data.Status);
                    txtDescription.setData(data.Description);
                    jobController.registerEvent();
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
        $('#txtDescription').val("");
        txtDescription.setData("");
        $('#chkStatus').prop('checked', false);

    }   
};

jobController.init();