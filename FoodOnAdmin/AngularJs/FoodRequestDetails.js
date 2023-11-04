app.service("FoodRequestDetailsService", function ($http) {


    this.GetFoodRequestDetails = function () {
        var response = $http({
            method: "POST",
            url: "/Resturant_Registration/GetFoodRequestDetails"
        });
        return response;
    };



});



app.controller("FoodRequestDetailsCtrl", function ($scope, FoodRequestDetailsService) {

    //alert();

    GetFoodRequestDetails();

    function GetFoodRequestDetails() {
        
        var getrecord = FoodRequestDetailsService.GetFoodRequestDetails();
        getrecord.then(function (response) {
            $scope.FoodRequestDetailsList = response.data;
            $scope.USER_ID = $scope.FoodRequestDetailsList[0].USER_ID;
            $scope.TOTAL_AMOUNT = Math.round(parseFloat($scope.FoodRequestDetailsList[0].TOTAL_AMOUNT));
            $scope.GST = Math.round(parseFloat($scope.FoodRequestDetailsList[0].GST));
            $scope.DISCOUNT = Math.round(parseFloat($scope.FoodRequestDetailsList[0].DISCOUNT));
            $scope.DELIVERY_CHARGES = Math.round(parseFloat($scope.FoodRequestDetailsList[0].DELIVERY_CHARGES));
            $scope.FINAL_TOTAL = Math.round($scope.TOTAL_AMOUNT);
            $scope.ORDER_STATUS = $scope.FoodRequestDetailsList[0].STATUS;
            $scope.REMARK = $scope.FoodRequestDetailsList[0].RES_REMARK;
            //alert(JSON.stringify($scope.FoodRequestDetailsList));
            $("#loader").css("display", 'none');
        }, function () {
            alert("Error to load data...", "error");
            $("#loader").css("display", 'none');


        });
    }

    $scope.GoToPreviousNextPage = function (pagehistory) {
        if (pagehistory === "Previous") {
            history.back(); //Go to the previous page
        }
    }

});