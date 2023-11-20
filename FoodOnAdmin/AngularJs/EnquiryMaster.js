app.service("AdminService", function ($http) {

    this.TotalRecordCount = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/RegistrationEnquiry/TotalRecordCount",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };


    this.getRecordbyPaging = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/RegistrationEnquiry/GetallAdmin",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };


});

app.controller("EnquiryCtrl", function ($scope, AdminService) {

    
   
    $scope.PageNo = 1;
    $scope.pageSize = 50;
    $scope.RESTAURANT_NAME = null;
    GetTotalcount();

    function GetTotalcount() {

        var SearchingConditions = GetSearchingConditions();
        var getcount = AdminService.TotalRecordCount(SearchingConditions);
        getcount.then(function (d) {
            $scope.totalRecordCount = d.data.success;
            if ($scope.totalRecordCount === 0) {
                $scope.AdminList = "";
            }
           
            initController();
        }, function () {
            $.notify("Error to load data...", "error");

        });

    }



    function GetSearchingConditions() {

        if ($scope.RESTAURANT_NAME === undefined || $scope.RESTAURANT_NAME === "" || $scope.RESTAURANT_NAME === null) {
            $scope.RESTAURANT_NAME = null;
        }

        $scope.START_DATE = $("#START_DATE").val();
        $scope.END_DATE = $("#END_DATE").val();

        if ($("#START_DATE").val() === undefined || $("#START_DATE").val() === null || $("#START_DATE").val() === "") {
            $scope.START_DATE = null;
        }
        if ($("#END_DATE").val() === undefined || $("#END_DATE").val() === null || $("#END_DATE").val() === "") {
            $scope.END_DATE = null;
        }


        var SearchingConditions = {
            PageNo: $scope.PageNo,
            pageSize: $scope.pageSize,
            RESTAURANT_NAME: $scope.RESTAURANT_NAME,
            START_DATE: $scope.START_DATE,
            END_DATE: $scope.END_DATE,

        };

        return SearchingConditions;

    }



    function initController() {
        // initialize to page 1
        setPage(1);
    }

    $scope.setPage = function (page) {
        setPage(page);
    };


    $scope.setPage = function (page) {
        setPage(page);
    };
    function setPage(page) {

        var totalPages = Math.ceil($scope.totalRecordCount / $scope.pageSize);
        if (page < 0 || page > totalPages) {

            $scope.pager.pages.length = 0;
            $scope.AdminList = {};

            return;
        }
        $scope.pager = GetPager($scope.totalRecordCount, page, $scope.pageSize);
        $scope.PageNo = $scope.pager.currentPage;

        GetRecordbyPaging();
    }

    function GetRecordbyPaging() {
       
        var SearchingConditions = GetSearchingConditions();
        var getrecord = AdminService.getRecordbyPaging(SearchingConditions);
        getrecord.then(function (response) {
            $scope.AdminList = response.data;

           
        }, function () {
            $.notify("Error to load data...", "error");
           
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

        GetTotalcount();
    };


    $scope.GoToPreviousNextPage = function (pagehistory) {
        if (pagehistory === "Previous") {
            history.back(); //Go to the previous page
        }
    }


});