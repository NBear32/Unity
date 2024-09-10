mergeInto(LibraryManager.library, {
  GameOverExtern: function (userName, score) {
    window.dispatchReactUnityEvent("GameOver", UTF8ToString(userName), score);
  },
	NoticesStart: function () {
    window.alert("Notices Start!!!");
  },
  OpenReactWindow: function (roomName) {
    window.dispatchReactUnityEvent("OpenReactWindow", UTF8ToString(roomName));
  },
  GameReady: function() {
    window.dispatchReactUnityEvent("GameReady");
  }
});