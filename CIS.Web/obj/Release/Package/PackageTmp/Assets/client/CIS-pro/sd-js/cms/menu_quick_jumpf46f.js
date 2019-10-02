
$(document).ready(function() {
    $(".jump_menu").change(function() {
        var value = $(this).val();

        if (value != '') {
            location.href=value;
        }
    });
});
