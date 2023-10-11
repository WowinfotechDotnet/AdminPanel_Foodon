﻿app.service("AdminService", function ($http) {

    this.TotalRecordCount = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/FoodOnMasterVideo/TotalRecordCount",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };


    this.getRecordbyPaging = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/FoodOnMasterVideo/GetallAdmin",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };

    this.AddUpdateAdmin = function (tb_Admin) {
        var response = $http({
            method: "POST",
            url: "/FoodOnMasterVideo/AddUpdateFoodOnVideoURLs",
            data: JSON.stringify(tb_Admin),
            dataType: "json"
        });
        return response;
    };

    this.ChangeStatus = function (id) {
        var response = $http({
            method: "POST",
            url: "/FoodOnMasterVideo/ChangeStatus",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;
    };

    this.GetAllFoodCategory = function () {
        var response = $http({
            method: "POST",
            url: "/FoodOnSaleBannerMaster/GetAllFoodCategory"
        });
        return response;
    };

   




});

app.controller("FoodOnVideoCtrl", function ($scope, AdminService) {

    
   
    $scope.PageNo = 1;
    $scope.pageSize = 50;
    $scope.VIDEO_NAME = null;
    $scope.FOOD_CATEGORY_ID = null;
    GetTotalcount();
    GetAllFoodCategory();

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

        if ($scope.VIDEO_NAME === undefined || $scope.VIDEO_NAME === "" || $scope.VIDEO_NAME === null) {
            $scope.VIDEO_NAME = null;
        }
        if ($scope.FOOD_CATEGORY_ID === undefined || $scope.FOOD_CATEGORY_ID === "" || $scope.FOOD_CATEGORY_ID === null) {
            $scope.FOOD_CATEGORY_ID = null;
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
            VIDEO_NAME: $scope.VIDEO_NAME,
            FOOD_CATEGORY_ID: $scope.FOOD_CATEGORY_ID,
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



    function Clear() {
        $scope.FOOD_CATEGORY_ID1 = "";
        $scope.VID_BANNER_ID = "";
        $scope.VIDEO_NAME1 = "";
        $scope.VIDEO_URL_LINK = "";
    }

    function GetAllFoodCategory() {

        var getAdmin = AdminService.GetAllFoodCategory();
        getAdmin.then(function (response) {
            $scope.FoodCategoryList = response.data;
        });
    };

    

    $scope.UpdateSearchName = function () {
        var Cat = $scope.FoodCategoryList.filter(x => x.CATEGORY_ID == $scope.FOOD_CATEGORY_ID)[0];
        $scope.searchCategory = Cat.CATEGORY_NAME;
    };



    $scope.AdminClick = function () {
        $scope.Admin_Action = "Add Video Details";
        $scope.ACTION = "ADD";
        Clear();
        $("#Admin_Addupdate").modal("show");
        GetAllFoodCategory();
    };


    $scope.Update = function (admin) {
        Clear();
        $scope.Admin_Action = "Update Video Details";
        $scope.ACTION = "UPDATE";
        $("#Admin_Addupdate").modal("show");
        $scope.VIDEO_NAME1 = admin.VIDEO_NAME;
        $scope.VID_BANNER_ID = admin.VID_BANNER_ID;
        $scope.VIDEO_URL_LINK = admin.VIDEO_URL_LINK;
        $scope.FOOD_CATEGORY_ID1 = admin.FOOD_CATEGORY_ID;
        GetAllFoodCategory();
    };



    $scope.AddAdmin = function () {
        tb_Admin = {
            VIDEO_NAME: $scope.VIDEO_NAME1,
            VID_BANNER_ID: parseInt($scope.VID_BANNER_ID), // for Video URL Update
            VIDEO_URL_LINK: $scope.VIDEO_URL_LINK,
            FOOD_CATEGORY_ID: parseInt($scope.FOOD_CATEGORY_ID1), 
            ACTION: $scope.ACTION,
        };
        if ($scope.Admin_Action === "Add Video Details") {
            AddAdminRecord(tb_Admin);
        }
        else if ($scope.Admin_Action === "Update Video Details") {
            EditAdminRecord(tb_Admin);
        }
    };



    function AddAdminRecord(tb_Admin) {
        var datalist = AdminService.AddUpdateAdmin(tb_Admin);
        datalist.then(function (d) {
            if (d.data.success === true) {
                Clear(); GetRecordbyPaging();
                alert("Video Details added successfully.");
                $("#Admin_Addupdate").modal("hide");
                
            }
            else if (d.data.success === false) {
                alert("Video Details already added.");
                
            }
            else {
                alert("Error.");
                
            }
        },
            function () {

                alert("Error.");
                
            });
    }





    function EditAdminRecord(tb_Admin) {
        var datalist = AdminService.AddUpdateAdmin(tb_Admin);
        datalist.then(function (d) {
            if (d.data.success === true) {
                Clear(); GetRecordbyPaging();
                alert("Video Details Updated successfully.");
                $("#Admin_Addupdate").modal("hide");
            }
            else if (d.data.success === false) {
                alert("Video Details already added.");
            }
            else {
                alert("Error.");
            }
        },
            function () {

                alert("Error.");
                
            });
    }



    $scope.ChangeStatus = function (admin) {
       
        var getStatus = AdminService.ChangeStatus(admin.VID_BANNER_ID);
        getStatus.then(function (response) {
            GetRecordbyPaging();
            //$.notify(response.data, "error");
        }, function () {
            $.notify("Error to load data...", "error");
           
        });

    };








    $scope.GoToPreviousNextPage = function (pagehistory) {
        if (pagehistory === "Previous") {
            history.back(); //Go to the previous page
        }
    }


});