/*
 mSport frameword v1.0
 author : Toaihv
 created : 05/17/2014
 */

var mpStart = {

};


mpStart.loadAsync = function () {
    $(".async").each(function(index, item) {
        var url = $(item).data("url");
        if (url && url.length > 0) {
            $(item).html("<span>Loading...</span>");
            $(item).load(url, function (response, status, xhr)
            {
                if (status === "error") {
                    $(item).html("<span class='error'>Không thể tải dữ liệu</span>");
                }

            });
        }

    });
};


