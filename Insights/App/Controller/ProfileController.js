app.controller('ProfileController', function ($scope, $http, $location, profileFactory) {
    $scope.profile = profileFactory.getProfile();
});