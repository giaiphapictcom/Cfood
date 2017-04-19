/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.enterMode = CKEDITOR.ENTER_BR;
    config.toolbar = 'Full';
    config.filebrowserBrowseUrl = '/Content/Admin/Plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Content/Admin/Plugins/ckfinder/ckfinder.html?type=Images';
    config.filebrowserFlashBrowseUrl = '/Content/Admin/Plugins/ckfinder/ckfinder.html?type=Flash';
    config.filebrowserUploadUrl = '/Content/Admin/Plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Content/Admin/Plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/Content/Admin/Plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    config.filebrowserWindowWidth = '1000';
    config.filebrowserWindowHeight = '700';
};
