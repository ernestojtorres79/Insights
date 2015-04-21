app.controller('NavBarController', function ($scope, $http, $location, $window, loginHandler, profileFactory) {
    $scope.isLoggedin = loginHandler.checkLogin();
    $scope.user = {};
    $scope.isLogged = false;

    $scope.getProfileCampaigns = function () {
        profileFactory.getProfile();
    }
    $scope.logout = function () {
        $window.sessionStorage.removeItem("token");
        $window.sessionStorage.removeItem("role");
        $scope.isLogged = false;
        $location.path("/");
        $scope.isLoggedin = loginHandler.checkLogin();
    }

    $scope.login = function () {
        loginHandler.login($scope.user.email, $scope.user.password).then(function () {
            $scope.isLogged = true;
            $location.path('/home');
        }, function () {
            alert("error on Login");
            $scope.isLogged = false;
        });
    }
});