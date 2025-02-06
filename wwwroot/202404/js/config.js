(function () {
  var mode = localStorage.getItem("mode") || "light";
  var primary = localStorage.getItem("primary") || "#006666";
  var primary = localStorage.getItem("primary") || "#006666";
  var secondary = localStorage.getItem("secondary") || "#FE6A49";

  window.RihoAdminConfig = {
    // Theme Primary Color
      mode: mode,
    primary: primary,
    // theme secondary color
    secondary: secondary,
  };
    // Thêm class 'dark-only' vào phần tử body
   

    // Xoá class 'light' khỏi phần tử body
    document.body.classList.remove('light');
    document.body.classList.remove('dark-only');
    document.body.classList.add(mode);
})();
