app.service("AdminService", function ($http) {

    this.TotalRecordCount = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/Package/TotalRecordCount",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };


    this.getRecordbyPaging = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/Package/GetallAdmin",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };


    this.GetadminById = function (id) {
        var response = $http({
            method: "GET",
            url: "/Package/GetPackageById",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;
    };


    this.AddAdmin = function (tb_Admin) {
        var response = $http({
            method: "POST",
            url: "/Package/AddAdmin",
            data: JSON.stringify(tb_Admin),
            dataType: "json"
        });
        return response;
    };


    this.EditAdmin = function (tb_Admin) {
        var response = $http({
            method: "POST",
            url: "/Package/EditAdmin",
            data: JSON.stringify(tb_Admin),
            dataType: "json"
        });
        return response;
    };


    this.ChangeStatus = function (id) {
        var response = $http({
            method: "POST",
            url: "/Package/ChangeStatus",
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
        $scope.P_ID = "";
        $scope.PACKAGE_NAME = "";
        $scope.PACKAGE_DESCRIPTION = "";
        $scope.PACKAGE_VALIDITY = "";
        $scope.MRP = "";
        $scope.OFFER_PRICE = "";
        $scope.POST_COUNT = "";
        CKEDITOR.instances.PACKAGE_DESCRIPTION.setData($scope.PACKAGE_DESCRIPTION);

    }


    $scope.getAdmin = function (admin) {

        var getAdmin = AdminService.GetadminById(parseInt(admin.P_ID));
        getAdmin.then(function (response) {
            $scope._Party = response.data;
            $scope.P_ID = $scope._Party.P_ID;
        });
    };




    $scope.AdminClick = function () {
        $scope.Admin_Action = "Add Package";
        Clear();
        $("#Admin_Addupdate").modal("show");

    };


    $scope.Update = function (admin) {
        $scope.Admin_Action = "Update Package";
        $("#Admin_Addupdate").modal("show");
        var getAdmin = AdminService.GetadminById(admin.P_ID);
        getAdmin.then(function (response) {
            $scope._Party = response.data;
            $scope.PACKAGE_NAME = $scope._Party.PACKAGE_NAME;
            $scope.PACKAGE_DESCRIPTION = $scope._Party.PACKAGE_DESCRIPTION;
            $scope.PACKAGE_VALIDITY = parseInt($scope._Party.PACKAGE_VALIDITY);
            $scope.MRP = parseFloat($scope._Party.MRP);
            $scope.OFFER_PRICE = parseFloat($scope._Party.OFFER_PRICE);
            $scope.POST_COUNT = parseInt($scope._Party.POST_COUNT);
            $scope.P_ID = $scope._Party.P_ID;

            var editor = CKEDITOR.instances.PACKAGE_DESCRIPTION;
            if (editor) { editor.destroy(true); }

            CKEDITOR.replace('PACKAGE_DESCRIPTION', {
                //language: 'fr',
                uiColor: '#9AB8F3'
            });
            CKEDITOR.instances.PACKAGE_DESCRIPTION.setData($scope.PACKAGE_DESCRIPTION);

        });
    };



    $scope.AddAdmin = function () {
        $("#loader").css("display", '');
        if (CKEDITOR.instances.PACKAGE_DESCRIPTION.getData().length < 1) {
            alert("Package Description is required.");
            return false;
        }
        tb_Admin = {
            P_ID: $scope.P_ID, //for update table
            PACKAGE_NAME: $scope.PACKAGE_NAME,
            //PACKAGE_DESCRIPTION: $scope.PACKAGE_DESCRIPTION,
            PACKAGE_VALIDITY: parseInt($scope.PACKAGE_VALIDITY),
            MRP: parseFloat($scope.MRP),
            OFFER_PRICE: parseFloat($scope.OFFER_PRICE),
            POST_COUNT: parseInt($scope.POST_COUNT),
            PACKAGE_DESCRIPTION: CKEDITOR.instances.PACKAGE_DESCRIPTION.getData(),
        };
        if ($scope.Admin_Action === "Add Package") {
            AddAdminRecord(tb_Admin);
        }
        else if ($scope.Admin_Action === "Update Package") {
            EditAdminRecord(tb_Admin);
        }
    };



    function AddAdminRecord(tb_Admin) {
        var datalist = AdminService.AddAdmin(tb_Admin);
        datalist.then(function (d) {
            if (d.data.success === true) {
                Clear(); GetRecordbyPaging();
                alert("Package added successfully.");
                $("#Admin_Addupdate").modal("hide");
                $("#loader").css("display", 'none');
            }
            else if (d.data.success === false) {
                alert("Package already added.");
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
                alert("Package Updated successfully.");
                $("#Admin_Addupdate").modal("hide");
                $("#loader").css("display", 'none');
            }
            else if (d.data.success === false) {
                alert("Package already added.");
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
       
        var getStatus = AdminService.ChangeStatus(admin.P_ID);
        getStatus.then(function (response) {
            GetRecordbyPaging();
            //$.notify(response.data, "error");
        }, function () {
            $.notify("Error to load data...", "error");
           
        });

    };



    var editor = CKEDITOR.instances.PACKAGE_DESCRIPTION;
    if (editor) { editor.destroy(true); }

    CKEDITOR.replace('PACKAGE_DESCRIPTION', {
        //language: 'fr',
        uiColor: '#9AB8F3'
    });


    $scope.GoToPreviousNextPage = function (pagehistory) {
        if (pagehistory === "Previous") {
            history.back(); //Go to the previous page
        }
    }


});