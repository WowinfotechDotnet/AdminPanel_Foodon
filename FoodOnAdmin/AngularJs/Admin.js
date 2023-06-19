app.service("AdminService", function ($http) {

    this.TotalRecordCount = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/AdminMaster/TotalRecordCount",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };


    this.getRecordbyPaging = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/AdminMaster/GetallAdmin",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };


    this.GetadminById = function (id) {
        var response = $http({
            method: "GET",
            url: "/AdminMaster/GetadminById",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;
    };


    this.AddAdmin = function (tb_Admin) {
        var response = $http({
            method: "POST",
            url: "/AdminMaster/AddAdmin",
            data: JSON.stringify(tb_Admin),
            dataType: "json"
        });
        return response;
    };


    this.EditAdmin = function (tb_Admin) {
        var response = $http({
            method: "POST",
            url: "/AdminMaster/EditAdmin",
            data: JSON.stringify(tb_Admin),
            dataType: "json"
        });
        return response;
    };


    this.ChangeStatus = function (id) {
        var response = $http({
            method: "POST",
            url: "/AdminMaster/ChangeStatus",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;
    };



   




});

app.controller("AdminCtrl", function ($scope, AdminService) {

    
   
    $scope.PageNo = 1;
    $scope.pageSize = 50;
    $scope.FARMER_SEARCH = null;
    $scope.STATE_SEARCH = null;
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

        if ($scope.FARMER_SEARCH === undefined || $scope.FARMER_SEARCH === "" || $scope.FARMER_SEARCH === null) {
            $scope.FARMER_SEARCH = null;
        }
        if ($scope.STATE_SEARCH === undefined || $scope.STATE_SEARCH === "" || $scope.STATE_SEARCH === "0") {
            $scope.STATE_SEARCH = null;
        }


        var SearchingConditions = {
            PageNo: $scope.PageNo,
            pageSize: $scope.pageSize,
            FARMER_NAME: $scope.FARMER_SEARCH,
            STATE_ID: $scope.STATE_SEARCH

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
            $scope.FarmerList = {};

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
        $scope.ADMIN_ID = "";
        $scope.ADMIN_NAME = "";
        $scope.MOBILE_NO = "";
        $scope.ADDRESS = "";
        $scope.EMAIL = "";
        $scope.ROLE_ID = "";
        $scope.PASSWORD = "";
        $scope.ADMIN_PHOTO = "";
        $scope.ZM_ID = "";
    }


    $scope.getAdmin = function (admin) {

        var getAdmin = AdminService.GetadminById(parseInt(admin.ADMIN_ID));
        getAdmin.then(function (response) {
            $scope._Party = response.data;
            $scope.ADMIN_ID = $scope._Party.ADMIN_ID;
        });
    };




    $scope.AdminClick = function () {
        $scope.Admin_Action = "Add Admin";
        Clear();
        $("#Admin_Addupdate").modal("show");

    };


    $scope.Update = function (admin) {
        $scope.Admin_Action = "Update Admin";
        $("#Admin_Addupdate").modal("show");
        var getAdmin = AdminService.GetadminById(admin.ADMIN_ID);
        getAdmin.then(function (response) {
            $scope._Party = response.data;
            $scope.ADMIN_NAME = $scope._Party.ADMIN_NAME;
            $scope.MOBILE_NO = parseInt($scope._Party.MOBILE_NO);
            $scope.ADDRESS = $scope._Party.ADDRESS;
            $scope.EMAIL = $scope._Party.EMAIL;
            $scope.PASSWORD = $scope._Party.PASSWORD;
            $scope.ADMIN_ID = $scope._Party.ADMIN_ID;
            $scope.ROLE_ID = $scope._Party.ROLE_ID;

            setTimeout(function myfunction() {
                var blankSelectOptions = $('option[value$="?"]');
                if (blankSelectOptions.length > 0) {
                    $(blankSelectOptions).remove();
                }
                $("#ROLE_ID").val($scope.ROLE_ID);
            }, 500);


        });
    };



    $scope.AddAdmin = function () {
        $("#loader").css("display", '');
        tb_Admin = {
            ADMIN_ID: $scope.ADMIN_ID, //for update table
            ADMIN_NAME: $scope.ADMIN_NAME,
            MOBILE_NO: $scope.MOBILE_NO,
            ADDRESS: $scope.ADDRESS,
            EMAIL: $scope.EMAIL,
            ROLE_ID: $scope.ROLE_ID,
            PASSWORD: $scope.PASSWORD,
            ADMIN_PHOTO: $scope.ADMIN_PHOTO
        };
        if ($scope.Admin_Action === "Add Admin") {
            AddAdminRecord(tb_Admin);
        }
        else if ($scope.Admin_Action === "Update Admin") {
            EditAdminRecord(tb_Admin);
        }
    };



    function AddAdminRecord(tb_Admin) {
        var datalist = AdminService.AddAdmin(tb_Admin);
        datalist.then(function (d) {
            if (d.data.success === true) {
                Clear(); GetRecordbyPaging();
                alert("Admin added successfully.");
                $("#Admin_Addupdate").modal("hide");
                $("#loader").css("display", 'none');
            }
            else if (d.data.success === false) {
                alert("Admin allready added.");
                $("#loader").css("display", 'none');
            }
            else {
                alert("Error.");
                $("#loader").css("display", 'none');
            }
        },
            function () {

                alert("Error.");
                $("#loader").css("display", 'none');
            });
    }





    function EditAdminRecord(tb_Admin) {
        var datalist = AdminService.EditAdmin(tb_Admin);
        datalist.then(function (d) {
            if (d.data.success === true) {
                Clear(); GetRecordbyPaging();
                alert("Admin update successfully.");
                $("#Admin_Addupdate").modal("hide");
                $("#loader").css("display", 'none');
            }
            else if (d.data.success === false) {
                alert("Admin allready added.");
                $("#loader").css("display", 'none');
            }
            else {
                alert("Error.");
                $("#loader").css("display", 'none');
            }
        },
            function () {

                alert("Error.");
                $("#loader").css("display", 'none');
            });
    }



    $scope.ChangeStatus = function (admin) {
       
        var getStatus = AdminService.ChangeStatus(admin.ADMIN_ID);
        getStatus.then(function (response) {
            GetRecordbyPaging();
            $.notify(response.data, "error");
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