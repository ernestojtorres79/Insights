app.controller('HomeController', function ($scope, $http, $location, $routeParams, loginHandler ) {
    $scope.GetCampaigns = {};

    $scope.getAllCampaigns = function () {
        $http.get('/api/apiCampaign/').success(function (data, status) {
            $scope.GetCampaigns = data;
            console.log(data);
        }).error(function (data, status) {
            alert("Not connected to API Campaign" + status);
        })
    }
    console.log($scope.GetCampaigns.CampaignName);
})