/**
 * @license Copyright (c) 2003-2023, CKSource Holding sp. z o.o. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	 /*config.language = 'fr';*/
	config.uiColor = '#F88634';

	config.toolbarGroups = [
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
		{ name: 'document', groups: ['mode', 'document', 'doctools'] },
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'forms', groups: ['forms'] },
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
		{ name: 'links', groups: ['links'] },
		{ name: 'insert', groups: ['insert'] },
		{ name: 'tools', groups: ['tools'] },
		{ name: 'styles', groups: ['styles'] },
		{ name: 'colors', groups: ['colors'] },
		{ name: 'others', groups: ['others'] },
		{ name: 'about', groups: ['about'] }
	];

	config.removeButtons = 'Form,TextField,Button,ImageButton,HiddenField,CopyFormatting,RemoveFormat,Anchor,SpecialChar,PageBreak,Iframe,Format,Font,FontSize,BGColor,ShowBlocks,TextColor,About,Source,Save,NewPage,ExportPdf,Preview,Print,Image,HorizontalRule,Styles';
};

