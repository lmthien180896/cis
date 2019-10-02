/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.height = 500;     // 500 pixels wide.
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.languages = 'vi';
    config.filebrowserBrowseUrl = '/Assets/admin/libs/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Assets/admin/libs/ckfinder/ckfinder.html?Types=Images';
    config.filebrowserFlashBrowseUrl = '/Assets/admin/libs/ckfinder/ckfinder.html?Types=Flash';
    config.filebrowserUploadUrl = '/Assets/admin/libs/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=File';
    config.filebrowserImageUploadUrl = '/UploadedFiles';
    config.filebrowserFlashUploadUrl = '/Assets/admin/libs/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    //Chỉnh hình ảnh center align
    config.extraPlugins = 'image';

    CKFinder.setupCKEditor(null, '/Assets/admin/libs/ckfinder/');


};
