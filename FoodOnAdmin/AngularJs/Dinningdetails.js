app.service("Dinningdeatilsservice", function ($http) {


    this.GetDinningDetails = function () {
        var response = $http({
            method: "POST",
            url: "/Resturant_Registration/GetDinningDetails"
        });
        return response;
    };



});



app.controller("DinningdeatilsCtrl", function ($scope, Dinningdeatilsservice) {

    //alert();


    GetDinningDetails();

    function GetDinningDetails() {
        var getrecord = Dinningdeatilsservice.GetDinningDetails();
        getrecord.then(function (response) {
            $scope.DinningDeatils = response.data;
            $scope.USER_ID = $scope.DinningDeatils[0].USER_ID;
            $scope.TOTAL_PRICE = parseInt($scope.DinningDeatils[0].TOTAL_PRICE);
            $scope.TOTAL_DISCOUNT = parseInt($scope.DinningDeatils[0].TOTAL_DISCOUNT);
            $scope.REMARK = $scope.DinningDeatils[0].RES_REMARK;
            $scope.DRINK_DISCOUNT = parseInt($scope.DinningDeatils[0].DRINK_DISCOUNT);
            $scope.ADVANCED_AMT = ($scope.DinningDeatils[0].ADVANCED_AMT);
            $scope.ADVANCED_AMOUNT = parseInt($scope.DinningDeatils[0].PAYMENT_AMOUNT);
            $scope.MAX_ADVANCE_AMOUNT = (parseFloat($scope.DinningDeatils[0].MINIMUM_BUDGET) + parseFloat($scope.DinningDeatils[0].MINIMUM_BUDGET_DRINKS)).toFixed(2);
            //alert(JSON.stringify($scope.DinningDeatils));
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