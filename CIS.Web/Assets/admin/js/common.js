jQuery(document).ready(function ($) {
    'use strict';
    // ==============================================================
    // Alert Box
    // ==============================================================
    $("#toast-container").removeClass('hide');
    $("#toast-container").delay(5000).slideUp(500);

    $('#chkAll').off('click').on('click', function () {
        var isChecked = $(this).prop('checked');
        $('.chkDelete').prop('checked', isChecked);
    });
});