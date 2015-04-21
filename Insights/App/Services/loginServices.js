app.factory('TokenInterceptor', function ($window) {
    var request = function (config) {
        if (config.headers) {
            config.headers = config.headers;
        } else {
            config.headers = {};
        } if ($window.sessionStorage.getItem('token')) {
            config.headers.Authorization = 'Bearer ' + window.sessionStorage.getItem('token');
        }
        return config;
    }
    return {
        request: request
    }
});

app.factory('loginHandler', function ($http, $q, $window, profileFactory) {
    var isLoggedin = false;
    var isAdmin = false;

    var checkLogin = function () {
        if ($window.sessionStorage.getItem('token')) {
            isLoggedin = true;
        } else {
            isLoggedin = false;
        }
        return isLoggedin;
    }

    var checkAdmin = function () {
        if ($window.sessionStorage.getItem('role') == "Admin") {
            isAdmin = true;
        } else {
            isAdmin = false;
        }
        return isAdmin;
    }

    var login = function (username, password) {
        var q = $q.defer();

        $http({
            url: '/Token',
            method: 'POST',
            contentType: 'application/x-www-form-urlencoded',
            data: 'username=' + username + '&password=' + password + '&grant_type=password'
        }).success(function (data) {
            $window.sessionStorage.setItem('token', data.access_token);
            $window.sessionStorage.setItem('role', data.role);
            isLoggedin = checkLogin();
            isAdmin = checkAdmin();
            profileFactory.getProfileInfo().then(function () {
                q.resolve(data);
            });
        }).error(function (data, status) {
            $window.sessionStorage.removeItem('token');
            $window.sessionStorage.removeItem('role');
            q.reject(status);
        });
        return q.promise;
    }
    var logout = function () {
        var q = $q.defer();

        $http({
            method: 'POST',
            url: 'api/account/logout'
        }).success(function () {
            $window.sessionStorage.removeItem('token');
            $window.sessionStorage.removeItem('role');
            isLoggedin = checkLogin();
            isAdmin = checkAdmin();
        }).error(function (data, status) {
            return q.reject(status);
        });
        return q.promise;
    }
    return {
        login: login,
        logout: logout,
        checkLogin: checkLogin,
        checkAdmin: checkAdmin
    }
});