SoJSLib = {
    numberFormat: function (number, decimals, dec_point, thousands_sep) {
        var n = number,
	        prec = decimals;
        n = !isFinite(+n) ? 0 : +n;
        prec = !isFinite(+prec) ? 0 : Math.abs(prec);
        var sep = (typeof thousands_sep == "undefined") ? '.' : thousands_sep;
        var dec = (typeof dec_point == "undefined") ? ',' : dec_point;
        var s = (prec > 0) ? n.toFixed(prec) : Math.round(n).toFixed(prec);
        var abs = Math.abs(n).toFixed(prec);
        var _, i;
        if (abs >= 1000) {
            _ = abs.split(/\D/);
            i = _[0].length % 3 || 3;
            _[0] = s.slice(0, i + (n < 0)) + _[0].slice(i).replace(/(\d{3})/g, sep + '$1');
            s = _.join(dec);
        } else {
            s = s.replace(',', dec);
        }
        return s;
    },

    input_numbers_only: function (myfield, e, dec) {
        var key;
        var keychar;
        if (window.event) key = window.event.keyCode;
        else if (e) key = e.which;
        else return true;
        keychar = String.fromCharCode(key);
        if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) return true;
        else if ((("0123456789").indexOf(keychar) > -1)) return true;
        else if (dec && (keychar == ".")) {
            return true;
        } else return false;
    },

    util_trim: function (str) {
        return (/string/).test(typeof str) ? str.replace(/^\s+|\s+$/g, "") : "";
    },

    is_phone: function (num) {
        return (/^(01([0-9]{1})|09)(\d{8})$/i).test(num);
    },

    is_mail: function (str) {
        return (/^[a-z][a-z-_0-9\.]+@[a-z-_=>0-9\.]+\.[a-z]{2,3}$/i).test(this.util_trim(str));
    },

    is_email: function (str) {
        return this.is_mail(str);
    },

    join: function (str) {
        var store = [str];
        return function extend(other) {
            if (other != null && 'string' == typeof other) {
                store.push(other);
                return extend;
            }
            return store.join('');
        }
    },

    setCookie: function (name, value, expires, path, domain, secure) {
        expires instanceof Date ? expires = expires.toGMTString() : typeof (expires) == 'number' && (expires = (new Date(+(new Date) + expires * 1e3)).toGMTString());
        if (expires == undefined) {
            var today = new Date();
            today.setTime(today.getTime());
            expires = expires * 1000 * 60 * 60 * 24;
            expires = new Date(today.getTime() + (expires));
        }
        var r = [name + "=" + escape(value)],
            s, i;
        for (i in s = {
            expires: expires,
            path: (path == undefined) ? '/' : path,
            domain: domain
        }) {
            s[i] && r.push(i + "=" + s[i])
        }
        return secure && r.push("secure"), document.cookie = r.join(";"), true
    },
    getCookie: function (c_name) {
        if (document.cookie.length > 0) {
            c_start = document.cookie.indexOf(c_name + "=");
            if (c_start != -1) {
                c_start = c_start + c_name.length + 1;
                c_end = document.cookie.indexOf(";", c_start);
                if (c_end == -1) c_end = document.cookie.length;
                return unescape(document.cookie.substring(c_start, c_end))
            }
        }
        return false
    },
    delCookie: function (name, path, domain) {
        if (Bm.cookie.get(name)) document.cookie = name + "=" + ((path) ? ";path=" + path : "") + ((domain) ? ";domain=" + domain : "") + ";expires=Thu, 01-Jan-1970 00:00:01 GMT";
    },
    checkCookie: function () {
        var cookieEnabled = (navigator.cookieEnabled) ? true : false
        if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled) {
            document.cookie = "testcookie";
            cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? true : false;
        }
        return (cookieEnabled) ? true : this.showCookieFail();
    },
    showCookieFail: function () {
        $('#mCheckCookies').html("Vui lòng bật Cookies trên trình duyệt");
    }
}

