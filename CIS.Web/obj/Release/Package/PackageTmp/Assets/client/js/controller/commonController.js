var common = {
    init: function () {
        common.registerEvents();
    },
    registerEvents: function () {
        $('#searchBox').off('keypress').on('keypress', function (e) {
            if (e.which == 13) // Enter
            {
                var value = $(this).val();
                if (!(value == "" | value == null)) {
                    $('#searchNews').submit();
                }
                else {
                     e.preventDefault(e);
                }
            }
        });
    }
};

common.init();