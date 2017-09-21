var TestLib = {
	
	ShowAlert: function(msg) {
		window.alert(Pointer_stringify(msg));
	},
	
}
mergeInto(LibraryManager.library, TestLib);
