var Lock = function () {
    var handleLoginLock = function () {
        $('.loginLock-form').validate({
            submitHandler: function (form) {
                AjaxRequest.submit(
                   "/Account/LoginLock",
                   $(form).serialize(),
                  function (result) {
                      if ("操作成功" != STATUS[result.StatusCode]) {
                          Modal.alert({ msg: STATUS[result.StatusCode], title: '错误信息', btnok: '确定' });
                      } else {
                          window.location.href = '/Home/Index';
                      }
                  },
                  function (XMLHttpRequest) {
                  });
            }
        });

        $('.loginLock-form input').keypress(function (e) {
            if (e.which == 13) {
                $('.loginLock-form').submit();
                return false;
            }
        });
    }
    return {
        init: function () {
             handleLoginLock();
             $.backstretch([
              "/Media/image/bg/1.jpg",
              "/Media/image/bg/2.jpg",
              "/Media/image/bg/3.jpg",
              "/Media/image/bg/4.jpg"
             ], {
                 fade: 1000,
                 duration: 8000
             });
        }

    };

}();

jQuery(document).ready(function() {
    Lock.init();
});
