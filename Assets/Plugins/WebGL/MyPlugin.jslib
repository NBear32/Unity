mergeInto(LibraryManager.library, {
    MyJSFunction: function(message) {
        console.log(UTF8ToString(message));
    }
});