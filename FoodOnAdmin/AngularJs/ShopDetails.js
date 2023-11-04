app.service("plot1Service", function ($http) {




    this.GetallUserdetails = function () {
        return $http.get("/Resturant_Registration/GetallShopdetails");
    };

   
    //this.GetallHotelImages = function () {
    //    return $http.get("/Resturant_Registration/GetallHotelImages");
    //};


    
    this.GetallHotelImages = function (id) {
        var response = $http({
            method: "GET",
            url: "/Resturant_Registration/GetallHotelImages",
            params: {
                id: id
            }
        });
        return response;
    };
  
    this.TotalRecordCount = function (SearchingConditions, Ordertype) {
        if (Ordertype === "FoodRequest") {
            var response = $http({
                method: "POST",
                url: "/Resturant_Registration/TotalFoodRequestCount",
                data: JSON.stringify(SearchingConditions)
            });
            return response;
        }
        else if (Ordertype === "DiningEnquiry") {
            var response = $http({
                method: "POST",
                url: "/Resturant_Registration/TotalDinningRecordCount",
                data: JSON.stringify(SearchingConditions)
            });
            return response;
        }
        else if (Ordertype === "PreferredMenu") {
            var response = $http({
                method: "POST",
                url: "/Resturant_Registration/TotalPreferredMenuCount",
                data: JSON.stringify(SearchingConditions)
            });
            return response;
        }
    };


    this.getRecordbyPaging = function (SearchingConditions, Ordertype) {
        if (Ordertype === "FoodRequest") {
            var response = $http({
                method: "POST",
                url: "/Resturant_Registration/GetAllTotalFoodRequest",
                data: JSON.stringify(SearchingConditions)
            });
            return response;
        }
        else if (Ordertype === "DiningEnquiry") {
            var response = $http({
                method: "POST",
                url: "/Resturant_Registration/GetallTotalsDinningRequest",
                data: JSON.stringify(SearchingConditions)
            });
            return response;
        }
        else if (Ordertype === "PreferredMenu") {
            var response = $http({
                method: "POST",
                url: "/Resturant_Registration/GetAllTotalPreferredMenu",
                data: JSON.stringify(SearchingConditions)
            });
            return response;
        }
        
    };



});

app.controller('ShopController', function ($scope, plot1Service) {
    // alert();
    GetAll();
   
    $scope.PageNo = 1;
    $scope.pageSize = 20;
    $scope.CUSTOMER_NAME = null;

    function GetAll() {

        var getseed1 = plot1Service.GetallUserdetails();
        getseed1.then(function (response) {
            $scope.ShopList = response.data;
            GetAllImages('Image');
        }, function () {
            $.notify("Error to load data...", "error");
        });
    };


    function GetAllImages(id) {
  
        var getseed1 = plot1Service.GetallHotelImages(id);
        getseed1.then(function (response) {
            $scope.ShopImageList = response.data;
        }, function () {
            $.notify("Error to load data...", "error");
        });
    };




    function GetTotalcount(Ordertype) {
        var SearchingConditions = GetSearchingConditions();
        var getcount = plot1Service.TotalRecordCount(SearchingConditions, Ordertype);
        getcount.then(function (d) {
            $scope.totalRecordCount = d.data.success;
            if ($scope.totalRecordCount === 0) {
                if (Ordertype === "FoodRequest") {
                    $scope.TotalFoodRequestList = "";
                }
                else if (Ordertype === "DiningEnquiry") {
                    $scope.TotalDinningRequestList = "";
                }
                else if (Ordertype === "PreferredMenu") {
                    $scope.TotalPreferredFoodRequestList = "";
                }

            }
            $("#loader").css("display", 'none');
            initController(Ordertype);
        }, function () {
            alert("Error to load data...", "error");

        });
        
    }


    function GetSearchingConditions() {

        if ($scope.CUSTOMER_NAME === undefined || $scope.CUSTOMER_NAME === "" || $scope.CUSTOMER_NAME === null) {
            $scope.CUSTOMER_NAME = null;
        }

        $scope.START_DATE = $("#START_DATE").val();
        $scope.END_DATE = $("#END_DATE").val();
        if ($("#START_DATE").val() === undefined || $("#START_DATE").val() === null || $("#START_DATE").val() === "") {
            $scope.START_DATE = null;
        }
        if ($("#END_DATE").val() === undefined || $("#END_DATE").val() === null || $("#END_DATE").val() === "") {
            $scope.END_DATE = null;
        }


        var SearchingConditions =
        {
            PageNo: $scope.PageNo,
            pageSize: $scope.pageSize,
            CUSTOMER_NAME: $scope.CUSTOMER_NAME,
            START_DATE: $scope.START_DATE,
            END_DATE: $scope.END_DATE

        };

        return SearchingConditions;

    }

    function initController(Ordertype) {
        // initialize to page 1
        setPage(1, Ordertype);
    }

    $scope.setPage = function (page, Ordertype) {
        setPage(page, Ordertype);
    };


    function setPage(page, Ordertype) {

        var totalPages = Math.ceil($scope.totalRecordCount / $scope.pageSize);
        if (page < 0 || page > totalPages) {

            $scope.pager.pages.length = 0;
            if (Ordertype === "FoodRequest") {
                $scope.TotalFoodRequestList = {};
            }
            else if (Ordertype === "DiningEnquiry") {
                $scope.TotalDinningRequestList = {};
            }
            else if (Ordertype === "PreferredMenu") {
                $scope.TotalPreferredFoodRequestList = {};
            }
           

            return;
        }
        $scope.pager = GetPager($scope.totalRecordCount, page, $scope.pageSize);
        $scope.PageNo = $scope.pager.currentPage;

        GetRecordbyPaging(Ordertype);
    }


    function GetRecordbyPaging(Ordertype) {
        $("#loader").css("display", '');

        var SearchingConditions = GetSearchingConditions();
        var getrecord = plot1Service.getRecordbyPaging(SearchingConditions, Ordertype);
        getrecord.then(function (response) {
            if (Ordertype === "FoodRequest") {
                $scope.TotalFoodRequestList = response.data;
            }
            else if (Ordertype === "DiningEnquiry") {
                $scope.TotalDinningRequestList = response.data;
            }
            else if (Ordertype === "PreferredMenu") {
                $scope.TotalPreferredFoodRequestList = response.data;
            }
            $("#loader").css("display", 'none');
            ;
        }, function () {
            alert("Error to load data...", "error");
            $("#loader").css("display", 'none');


        });
    }

    function GetPager(totalItems, currentPage, pageSize) {
        $scope.page = currentPage - 1;
        currentPage = currentPage || 1;

        pageSize = pageSize || 100;

        var totalPages = Math.ceil(totalItems / pageSize);
        var startPage, endPage;
        if (totalPages <= 10) {

            startPage = 1;
            endPage = totalPages;
        } else {

            if (currentPage <= 6) {
                startPage = 1;
                endPage = 10;
            } else if (currentPage + 4 >= totalPages) {
                startPage = totalPages - 9;
                endPage = totalPages;
            } else {
                startPage = currentPage - 5;
                endPage = currentPage + 4;
            }
        }

        var startIndex = (currentPage - 1) * pageSize;
        var endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);

        var pages = range(startPage, endPage);

        return {
            totalItems: totalItems,
            currentPage: currentPage,
            pageSize: pageSize,
            totalPages: totalPages,
            startPage: startPage,
            endPage: endPage,
            startIndex: startIndex,
            endIndex: endIndex,
            pages: pages
        };
    }
    function range(start, end) {
        var ans = [];
        for (let i = start; i <= end; i++) {
            ans.push(i);
        }
        return ans;
    }


    $scope.SearchAdmin = function () {

        GetTotalcount($scope.Ordertype)
    };




    $scope.OnCategoryClick = function (id) {

        $(".catmenu_button_Desc").removeClass().addClass('catmenu_button_Desc catStyle_Desc');
        $('#' + id + '_catId_Desc').removeClass().addClass('catmenu_button_Desc catStyle_active_Desc');

        if (id === '1')
        {
            $scope.ShowDateFilter = false;
            $scope.Ordertype = "";
            GetAllImages('Image');
            document.getElementById('Demoexample_1').style.display = "block";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "none";
            //document.getElementById('Demoexample_7').style.display = "none";
            //document.getElementById('Demoexample_8').style.display = "none";
            //document.getElementById('Demoexample_9').style.display = "none";
            //document.getElementById('Demoexample_10').style.display = "none";
            

        }
        else if (id === '2')
        {
            $scope.ShowDateFilter = false;
            $scope.Ordertype = "";
            GetAllImages('About');
            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "block";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "none";
            //document.getElementById('Demoexample_7').style.display = "none";
            //document.getElementById('Demoexample_8').style.display = "none";
            //document.getElementById('Demoexample_9').style.display = "none";
            //document.getElementById('Demoexample_10').style.display = "none";
            
        }

        else if (id === '3') {
            $scope.ShowDateFilter = false;
            $scope.Ordertype = "";
            GetAllImages('Review');
            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "block";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "none";
            //document.getElementById('Demoexample_7').style.display = "none";
            //document.getElementById('Demoexample_8').style.display = "none";
            //document.getElementById('Demoexample_9').style.display = "none";
            //document.getElementById('Demoexample_10').style.display = "none";
          

        }

        else if (id === '4') {
            $scope.ShowDateFilter = true;
            $scope.Ordertype = "FoodRequest";
            $scope.CUSTOMER_NAME = "";
            $scope.START_DATE = "";
            $scope.END_DATE = "";
            $("#START_DATE").val("");
            $("#END_DATE").val("");
            GetTotalcount($scope.Ordertype); 
            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "block";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "none";
            //document.getElementById('Demoexample_7').style.display = "none";
            //document.getElementById('Demoexample_8').style.display = "none";
            //document.getElementById('Demoexample_9').style.display = "none";
            //document.getElementById('Demoexample_10').style.display = "none";


        }

        else if (id === '5') {
            $scope.ShowDateFilter = true;
            $scope.Ordertype = "DiningEnquiry";
            $scope.CUSTOMER_NAME = "";
            $scope.START_DATE = "";
            $scope.END_DATE = "";
            $("#START_DATE").val("");
            $("#END_DATE").val("");
            GetTotalcount($scope.Ordertype);
            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "block";
            document.getElementById('Demoexample_6').style.display = "none";
            //document.getElementById('Demoexample_7').style.display = "none";
            //document.getElementById('Demoexample_8').style.display = "none";
            //document.getElementById('Demoexample_9').style.display = "none";
            //document.getElementById('Demoexample_10').style.display = "none";

        }

        else if (id === '6') {
            $scope.ShowDateFilter = true;
            $scope.Ordertype = "PreferredMenu";
            $scope.CUSTOMER_NAME = "";
            $scope.START_DATE = "";
            $scope.END_DATE = "";
            $("#START_DATE").val("");
            $("#END_DATE").val("");
            GetTotalcount($scope.Ordertype); 
            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "block";
            //document.getElementById('Demoexample_7').style.display = "none";
            //document.getElementById('Demoexample_8').style.display = "none";
            //document.getElementById('Demoexample_9').style.display = "none";
            //document.getElementById('Demoexample_10').style.display = "none";


        }
        //else if (id === '7') {

        //    document.getElementById('Demoexample_1').style.display = "none";
        //    document.getElementById('Demoexample_2').style.display = "none";
        //    document.getElementById('Demoexample_3').style.display = "none";
        //    document.getElementById('Demoexample_4').style.display = "none";
        //    document.getElementById('Demoexample_5').style.display = "none";
        //    document.getElementById('Demoexample_6').style.display = "none";
        //    document.getElementById('Demoexample_7').style.display = "block";
        //    document.getElementById('Demoexample_8').style.display = "none";
        //    document.getElementById('Demoexample_9').style.display = "none";
        //    document.getElementById('Demoexample_10').style.display = "none";
        //}

        //else if (id === '8') {

        //    document.getElementById('Demoexample_1').style.display = "none";
        //    document.getElementById('Demoexample_2').style.display = "none";
        //    document.getElementById('Demoexample_3').style.display = "none";
        //    document.getElementById('Demoexample_4').style.display = "none";
        //    document.getElementById('Demoexample_5').style.display = "none";
        //    document.getElementById('Demoexample_6').style.display = "none";
        //    document.getElementById('Demoexample_7').style.display = "none";
        //    document.getElementById('Demoexample_8').style.display = "block";
        //    document.getElementById('Demoexample_9').style.display = "none";
        //    document.getElementById('Demoexample_10').style.display = "none";


        //}
        //else if (id === '9') {

        //    document.getElementById('Demoexample_1').style.display = "none";
        //    document.getElementById('Demoexample_2').style.display = "none";
        //    document.getElementById('Demoexample_3').style.display = "none";
        //    document.getElementById('Demoexample_4').style.display = "none";
        //    document.getElementById('Demoexample_5').style.display = "none";
        //    document.getElementById('Demoexample_6').style.display = "none";
        //    document.getElementById('Demoexample_7').style.display = "none";
        //    document.getElementById('Demoexample_8').style.display = "none";
        //    document.getElementById('Demoexample_9').style.display = "block";
        //    document.getElementById('Demoexample_10').style.display = "none";


        //}
        //else if (id === '10') {

        //    document.getElementById('Demoexample_1').style.display = "none";
        //    document.getElementById('Demoexample_2').style.display = "none";
        //    document.getElementById('Demoexample_3').style.display = "none";
        //    document.getElementById('Demoexample_4').style.display = "none";
        //    document.getElementById('Demoexample_5').style.display = "none";
        //    document.getElementById('Demoexample_6').style.display = "none";
        //    document.getElementById('Demoexample_7').style.display = "none";
        //    document.getElementById('Demoexample_8').style.display = "none";
        //    document.getElementById('Demoexample_9').style.display = "none";
        //    document.getElementById('Demoexample_10').style.display = "block";


        //}


    }



    $scope.GoToPreviousNextPage = function (pagehistory) {
        if (pagehistory === "Previous") {
            history.back(); //Go to the previous page
        }
    }



});