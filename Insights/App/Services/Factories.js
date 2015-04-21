//Profile Factory

app.factory('profileFactory', function ($http, $window, $q) {
    var profile = {}

    var getProfile = function () {
        return profile;
    }

    var getProfileInfo = function () {
        var q = $q.defer();
        $http({
            url: '/api/apiProfile',
            method: 'GET',
        }).success(function (data) {
            profile = data;
            q.resolve(data);
        }).error(function (status) {
            q.rejecte(status);
        });
        return q.promise;
    }
    return {
        getProfileInfo: getProfileInfo,
        getProfile: getProfile,
    }
});
