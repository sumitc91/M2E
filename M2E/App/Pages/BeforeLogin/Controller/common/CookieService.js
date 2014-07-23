BeforeLoginApp.factory('CookieUtil', function ($rootScope, $location, $cookieStore, $routeParams) {

    return {
        setAuthToken: function (AuthToken, keepMeSignedIn) {
            if (keepMeSignedIn) {               
                $.cookie('AuthToken', AuthToken, { expires: 365, path: '/' });
            }
            else {                
                $.cookie('AuthToken', AuthToken, { path: '/' });
            }
        },
        setAuthKey: function (AuthKey, keepMeSignedIn) {
            if (keepMeSignedIn) {
                $.cookie('AuthKey', AuthKey, { expires: 365, path: '/' });
            }
            else {
                $.cookie('AuthKey', AuthKey, { path: '/' });
            }
        },
        setAuthValue: function (AuthValue, keepMeSignedIn) {
            if (keepMeSignedIn) {
                $.cookie('AuthValue', AuthValue, { expires: 365, path: '/' });
            }
            else {
                $.cookie('AuthValue', AuthValue, { path: '/' });
            }
        },
        getAuthToken: function () {
            return $.cookie('AuthToken');
        },
        getAuthKey: function () {
            return $.cookie('AuthKey');
        },
        getAuthValue: function () {
            return $.cookie('AuthValue');
        },
        removeAuthToken: function () {
            $.removeCookie('AuthToken', { path: '/' });
        },
        removeAuthKey: function () {
            $.removeCookie('AuthKey', { path: '/' });
        },
        removeAuthValue: function () {
            $.removeCookie('AuthValue', { path: '/' });
        }
    };

});
