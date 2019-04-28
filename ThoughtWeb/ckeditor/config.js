/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	//配置默认配置 
config.language = 'zh-cn'; //配置语言 
 config.uiColor = '#FFF'; //背景颜色 
 config.width = 800; //宽度 
 config.height = 300; //高度 
 config.skin = 'v2'; //编辑器皮肤样式 
// 取消 “拖拽以改变尺寸”功能 
 config.resize_enabled = false; 
// 使用基础工具栏 
 config.toolbar = "Basic"; 
// 使用全能工具栏 
config.toolbar = "Full"; 
//使用自定义工具栏 
 config.toolbar = 
 [ 
 ['Source', 'Preview', '-'], 
 ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', ], 
 ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'], 
 ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar','PageBreak'], 
 ['Bold', 'Italic', 'Underline', '-', 'Subscript', 'Superscript'], 
 ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'], 
 ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'], 
 ['Link', 'Unlink', 'Anchor'], 

 ['Format', 'Font', 'FontSize'], 
 ['TextColor', 'BGColor'], 
 ['Maximize', 'ShowBlocks', '-', 'About'] 
 ]; 
 
 config.filebrowserBrowseUrl= '/ckfinder/ckfinder.html'; //上传文件时浏览服务文件夹
 config.filebrowserImageBrowseUrl= '/ckfinder/ckfinder.html?Type=Images'; //上传图片时
//浏览服务文件夹
config.filebrowserFlashBrowseUrl= '/ckfinder/ckfinder.html?Type=Flash';  //上传Flash时

//浏览服务文件夹

config.filebrowserUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files'; //上传文件按钮(标签)

config.filebrowserImageUploadUrl= '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images'; //上传图片按钮(标签)

config.filebrowserFlashUploadUrl= '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'; //上传Flash按钮(标签)


};
