app.service("HomeServices", function ($http) {

    this.GetallHomeCount = function () {
        //alert(JSON.stringify(res_id));

        var response = $http({
            method: "POST",
            url: "/Home/GetallHomeCount"
        });
        return response;
    };

});


app.controller("HomeCtrl", function ($scope, HomeServices) {


    $("#loader").css("display", '');

    GetallHomeCount();
    function GetallHomeCount() {
        var getrecord = HomeServices.GetallHomeCount();
        getrecord.then(function (response) {
            $scope.DashboardCount = response.data;
            $scope.TOTAL_RESTAURANTS = $scope.DashboardCount[0].TOTAL_RESTAURANTS;

            $scope.TOTAL_DINNING = $scope.DashboardCount[0].TOTAL_DINNING;
            $scope.TOTAL_FOOD_ORDER = $scope.DashboardCount[0].TOTAL_FOOD_ORDER;
            $scope.TOTAL_PREFERRED_MENU = $scope.DashboardCount[0].TOTAL_PREFERRED_MENU;

            $scope.TODAY_DINNING = $scope.DashboardCount[0].TODAY_DINNING;
            $scope.TODAY_FOOD_ORDER = $scope.DashboardCount[0].TODAY_FOOD_ORDER;
            $scope.TODAY_PREFERRED_MENU = $scope.DashboardCount[0].TODAY_PREFERRED_MENU;

            //alert(JSON.stringify($scope.NEW_ORDER_STATUS));

            $("#loader").css("display", 'none');
            ;
        }, function () {
            alert("Error to load data...", "error");
            $("#loader").css("display", 'none');


        });
    }


});