app.service("PreferredMenuDetailsService", function ($http) {

    this.GetAllProducts = function () {
        var response = $http({
            method: "GET",
            url: "/Resturant_Registration/GetAllProducts"
        });
        return response;
    };


    this.GetPreferredMenuDetails = function () {
        var response = $http({
            method: "POST",
            url: "/Resturant_Registration/GetPreferredMenuDetails"
        });
        return response;
    };

    this.GetPreferredMenuProductList = function () {
        var response = $http({
            method: "POST",
            url: "/Resturant_Registration/GetPreferredMenuProductList"
        });
        return response;
    };

});



app.controller("PreferredMenuDetailsController", function ($scope, PreferredMenuDetailsService) {

    //alert();
    GetAllProducts();
    GetPreferredMenuDetails();
   
    function GetPreferredMenuDetails() {
       
        var getrecord = PreferredMenuDetailsService.GetPreferredMenuDetails();
        getrecord.then(function (response) {
            $scope.PreferredMenuDetailsList = response.data;
            $scope.PreferredMenu_ID = $scope.PreferredMenuDetailsList[0].PreferredMenu_ID;
            $scope.USER_ID = $scope.PreferredMenuDetailsList[0].USER_ID;
            $scope.SHOW_MOBILE = $scope.PreferredMenuDetailsList[0].SHOW_MOBILE;
            //$scope.TOTAL_AMOUNT = parseInt($scope.PreferredMenuDetailsList[0].TOTAL_AMOUNT);
            $scope.TOTAL_DISCOUNT = Math.round(parseFloat($scope.PreferredMenuDetailsList[0].TOTAL_DISCOUNT));
            //$scope.GST = parseInt($scope.PreferredMenuDetailsList[0].GST);
            $scope.DELIVERY_CHARGES = Math.round(parseFloat($scope.PreferredMenuDetailsList[0].DELIVERY_CHARGES));
            $scope.REMARK = $scope.PreferredMenuDetailsList[0].REMARK;

            var getAdmin = PreferredMenuDetailsService.GetPreferredMenuProductList();
            getAdmin.then(function (response) {
                $scope.PreferredMenuProductList = response.data;
                $scope.PRODUCT_TOTAL = 0;
                $scope.PRODUCT_QUANTITY1 = 0;
                for (let i = 0; i < $scope.PreferredMenuProductList.length; i++) {
                    $scope.PRODUCT_QUANTITY1 = $scope.PRODUCT_QUANTITY1 + $scope.PreferredMenuProductList[i].QUANTITY;
                    $scope.PRODUCT_TOTAL = Math.round(parseFloat($scope.PRODUCT_TOTAL) + parseFloat($scope.PreferredMenuProductList[i].AMOUNT) * $scope.PreferredMenuProductList[i].QUANTITY);
                }
                $scope.GST = Math.round($scope.PRODUCT_TOTAL * 18 / 100);
                //$scope.TOTAL_AMOUNT = parseFloat($scope.PRODUCT_TOTAL) + parseFloat($scope.GST) + parseFloat($scope.DELIVERY_CHARGES).toFixed(2);
                $scope.TOTAL_AMOUNT = Math.round(parseFloat($scope.PRODUCT_TOTAL) + parseFloat($scope.GST) + parseFloat($scope.DELIVERY_CHARGES));
                //$scope.FINAL_TOTAL = Math.round(parseFloat($scope.TOTAL_AMOUNT) - parseFloat($scope.TOTAL_AMOUNT * $scope.TOTAL_DISCOUNT / 100).toFixed(2));
                $scope.FINAL_TOTAL = Math.round(parseFloat($scope.TOTAL_AMOUNT) - parseFloat($scope.TOTAL_AMOUNT * $scope.TOTAL_DISCOUNT / 100));
            });

            //alert(JSON.stringify($scope.PreferredMenuDetailsList));
            $("#loader").css("display", 'none');
            ;
        }, function () {
            alert("Error to load data...", "error");
            $("#loader").css("display", 'none');


        });
    }


    function GetAllProducts() {
        var getAdmin = PreferredMenuDetailsService.GetAllProducts();
        getAdmin.then(function (response) {
            $scope.ProductList = response.data;
        });
    }


    $scope.GoToPreviousNextPage = function (pagehistory) {
        if (pagehistory === "Previous") {
            history.back(); //Go to the previous page
        }
    }


});