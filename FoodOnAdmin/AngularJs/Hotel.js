app.service("AdminService", function ($http) {

    this.TotalRecordCount = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/Resturant_Registration/TotalRecordCount",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };


    this.getRecordbyPaging = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/Resturant_Registration/GetallAdmin",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };


    this.GetadminById = function (id) {
        var response = $http({
            method: "GET",
            url: "/Resturant_Registration/GetadminById",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;
    };


    this.AddAdmin = function (tb_Admin) {
        var response = $http({
            method: "POST",
            url: "/Resturant_Registration/AddAdmin",
            data: JSON.stringify(tb_Admin),
            dataType: "json"
        });
        return response;
    };


    this.EditAdmin = function (tb_Admin) {
        var response = $http({
            method: "POST",
            url: "/Resturant_Registration/EditAdmin",
            data: JSON.stringify(tb_Admin),
            dataType: "json"
        });
        return response;
    };


    this.ChangeStatus = function (id) {
        var response = $http({
            method: "POST",
            url: "/Resturant_Registration/ChangeStatus",
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
        $scope.RES_ID = "";
        $scope.RES_NAME = "";
        $scope.OWNER_NAME = "";
        $scope.MOBILE_NUMBER = "";
        $scope.FOOD_TYPE = "";
        $scope.PASSWORD = "";
        $scope.CONFIRM_PASSWORD = "";
        $scope.RES_LOGO = "";
        $scope.RES_OPEN_TIME = "";
        $scope.RES_CLOSE_TIME = "";
        $scope.ADDRESS = "";
        $scope.LATITUDE = "";
        $scope.LONGITUDE = "";
        $scope.PINCODE = "";
        $scope.STATUS = "";
        $scope.DESCRIPTION = "";
        $scope.FOODON_RATING = "";
        $scope.RESTRO_BOOKING = "";
        $scope.FOOD_DELIVERY = "";
        $scope.Food= "";
        $scope.Sweets= "";
        $scope.Juice= "";
        $scope.Cafeteria= "";            
    }


    $scope.getAdmin = function (admin) {

        var getAdmin = AdminService.GetadminById(parseInt(admin.RES_ID));
        getAdmin.then(function (response) {
            $scope._Party = response.data;
            $scope.RES_ID = $scope._Party.RES_ID;
        });
    };




    $scope.AdminClick = function () {
        $scope.Admin_Action = "Add Restaurant";
        Clear();
        $("#Admin_Addupdate").modal("show");

        if ($scope.RESTRO_BOOKING === "True") {
            $("#RESTRO_BOOKING").prop("checked", true);
        }
        else {
            $("#RESTRO_BOOKING").prop("checked", false);
        }

        if ($scope.FOOD_DELIVERY === "True") {
            $("#FOOD_DELIVERY").prop("checked", true);
        }
        else {
            $("#FOOD_DELIVERY").prop("checked", false);
        }

        if ($scope.Food === "True") {
            $("#Food").prop("checked", true);
        }
        else {
            $("#Food").prop("checked", false);
        }

        if ($scope.Sweets === "True") {
            $("#Sweets").prop("checked", true);
        }

        else {
            $("#Sweets").prop("checked", false);
        }

        if ($scope.Juice === "True") {
            $("#Juice").prop("checked", true);
        }
        else {
            $("#Juice").prop("checked", false);
        }

        if ($scope.Cafeteria === "True") {
            $("#Cafeteria").prop("checked", true);
        }
        else {
            $("#Cafeteria").prop("checked", false);
        }
    };


    $scope.Update = function (admin) {
        $scope.Admin_Action = "Update Restaurant";
        $("#Admin_Addupdate").modal("show");
        var getAdmin = AdminService.GetadminById(admin.RES_ID);
        getAdmin.then(function (response) {
            $scope._Party = response.data;
            $scope.RES_NAME = $scope._Party.RES_NAME;
            $scope.MOBILE_NUMBER = parseInt($scope._Party.MOBILE_NUMBER);
            $scope.FOODON_RATING = parseFloat($scope._Party.RES_RATING.toFixed(1));
            $scope.ADDRESS = $scope._Party.ADDRESS;
            $scope.OWNER_NAME = $scope._Party.OWNER_NAME;
            $scope.PASSWORD = $scope._Party.PASSWORD;
            $scope.CONFIRM_PASSWORD = $scope._Party.CONFIRM_PASSWORD;
            $scope.RES_LOGO = $scope._Party.RES_LOGO;
            $scope.RES_ID = $scope._Party.RES_ID; 
            $scope.FOOD_TYPE = $scope._Party.FOOD_TYPE;
            $scope.RES_OPEN_TIME = $scope._Party.RES_OPEN_TIME;
            $("#ResOpenTime").val($scope._Party.RES_OPEN_TIME);
            const timePicker = document.querySelector("#ResOpenTime");
            const selectedTime = $scope.RES_OPEN_TIME; // 2:30 PM in 24-hour format
            timePicker._flatpickr.setDate(selectedTime, true);

            $scope.RES_CLOSE_TIME = $scope._Party.RES_CLOSE_TIME;
            $("#ResCloseTime").val($scope._Party.RES_CLOSE_TIME);
            const timePicker2 = document.querySelector("#ResCloseTime");
            const selectedTime2 = $scope.RES_CLOSE_TIME; // 2:30 PM in 24-hour format
            timePicker2._flatpickr.setDate(selectedTime2, true);

            $scope.LATITUDE = $scope._Party.LATITUDE;
            $scope.LONGITUDE = $scope._Party.LONGITUDE;
            $scope.PINCODE = $scope._Party.PINCODE;
            $scope.DESCRIPTION = $scope._Party.DESCRIPTION;


            $scope.RESTRO_BOOKING = $scope._Party.RESTRO_BOOKING;
            if ($scope.RESTRO_BOOKING === "True") {
                $("#RESTRO_BOOKING").prop("checked", true);
            }
            else {
                $("#RESTRO_BOOKING").prop("checked", false);
            }

            $scope.FOOD_DELIVERY = $scope._Party.FOOD_DELIVERY;
            if ($scope.FOOD_DELIVERY === "True") {
                $("#FOOD_DELIVERY").prop("checked", true);
            }
            else {
                $("#FOOD_DELIVERY").prop("checked", false);
            }

            $scope.Food = $scope._Party.Food;
            if ($scope.Food === "True") {
                $("#Food").prop("checked", true);
            }
            else {
                $("#Food").prop("checked", false);
            }

            $scope.Sweets = $scope._Party.Sweets;
            if ($scope.Sweets === "True") {
                $("#Sweets").prop("checked", true);
            }
            else {
                $("#Sweets").prop("checked", false);
            }

            $scope.Juice = $scope._Party.Juice;
            if ($scope.Juice === "True") {
                $("#Juice").prop("checked", true);
            }
            else {
                $("#Juice").prop("checked", false);
            }

            $scope.Cafeteria = $scope._Party.Cafeteria;
            if ($scope.Cafeteria === "True") {
                $("#Cafeteria").prop("checked", true);
            }
            else {
                $("#Cafeteria").prop("checked", false);
            }

        });
    };



    $scope.AddAdmin = function () {
        

        $scope.LATITUDE = $("#latitude").val();
        $scope.LONGITUDE = $("#longitude").val();
        $scope.PINCODE = $("#pincode").val();
        //$scope.RES_OPEN_TIME = $("#ResOpenTime").val();
        //$scope.RES_CLOSE_TIME = $("#ResCloseTime").val();

        if ($scope.LATITUDE === undefined || $scope.LATITUDE === "" || $scope.LATITUDE === null) {
            alert("Please Enter Latitude!");
            return;
        }
        if ($scope.LONGITUDE === undefined || $scope.LONGITUDE === "" || $scope.LONGITUDE === null) {
            alert("Please Enter Longitude!");
            return;
        }
        if ($scope.PINCODE === undefined || $scope.PINCODE === "" || $scope.PINCODE === null) {
            alert("Please Enter Pincode!");
            return;
        }
        if ($scope.RES_OPEN_TIME === undefined || $scope.RES_OPEN_TIME === "" || $scope.RES_OPEN_TIME === null) {
            alert("Please Select Open Time!");
            return;
        }
        if ($scope.RES_CLOSE_TIME === undefined || $scope.RES_CLOSE_TIME === "" || $scope.RES_CLOSE_TIME === null) {
            alert("Please Enter Close Time!");
            return;
        }

        $scope.RESTRO_BOOKING = $("#RESTRO_BOOKING").is(":checked");
        $scope.FOOD_DELIVERY = $("#FOOD_DELIVERY").is(":checked");
        $scope.Food = $("#Food").is(":checked");
        $scope.Sweets = $("#Sweets").is(":checked");
        $scope.Juice = $("#Juice").is(":checked");
        $scope.Cafeteria = $("#Cafeteria").is(":checked");
        tb_Admin = {
            RES_ID: $scope.RES_ID, //for update table
            RES_NAME: $scope.RES_NAME,
            OWNER_NAME: $scope.OWNER_NAME,
            MOBILE_NUMBER: $scope.MOBILE_NUMBER,
            FOODON_RATING: parseFloat($scope.FOODON_RATING).toFixed(1),
            FOOD_TYPE: $scope.FOOD_TYPE,
            PASSWORD: $scope.PASSWORD,
            CONFIRM_PASSWORD: $scope.CONFIRM_PASSWORD,
            RES_LOGO: $scope.RES_LOGO,
            RES_OPEN_TIME: $scope.RES_OPEN_TIME,
            RES_CLOSE_TIME: $scope.RES_CLOSE_TIME,
            ADDRESS: $scope.ADDRESS,
            LATITUDE: $scope.LATITUDE,
            LONGITUDE: $scope.LONGITUDE,
            PINCODE: $scope.PINCODE,
            DESCRIPTION: $scope.DESCRIPTION,
            RESTRO_BOOKING: $scope.RESTRO_BOOKING,
            FOOD_DELIVERY: $scope.FOOD_DELIVERY,
            Food: $scope.Food,
            Sweets: $scope.Sweets,
            Juice: $scope.Juice,
            Cafeteria: $scope.Cafeteria,
        };
        if ($scope.Admin_Action === "Add Restaurant") {

            //tb_Admin = getImageData(Profile_photo, tb_Admin);
            //tb_Admin.RES_LOGO = tb_Admin.IsImageChoosen;
            //if (tb_Admin.RES_LOGO === null || tb_Admin.RES_LOGO === "No" || tb_Admin.RES_LOGO === "undefined") {
            //    alert("Image is required.")
            //    $("#loader").css("display", 'none');
            //}
            //else {
            //    AddAdminRecord(tb_Admin);
            //}
            AddAdminRecord(tb_Admin);
        }
        else if ($scope.Admin_Action === "Update Restaurant") {
            //tb_Admin = getImageData(Profile_photo, tb_Admin);
            //if (tb_Admin.IsImageChoosen === "Yes") {
            //    tb_Admin.RES_LOGO = "Yes";
            //}
            //else {
            //    tb_Admin.RES_LOGO = $scope.RES_LOGO;
            //}
            EditAdminRecord(tb_Admin);
        }
    };



    function AddAdminRecord(tb_Admin) {
        var datalist = AdminService.AddAdmin(tb_Admin);
        datalist.then(function (d) {
            if (d.data.success === true) {
                Clear(); GetRecordbyPaging();
                alert("Restaurant added successfully.");
                $("#Admin_Addupdate").modal("hide");
                $("#loader").css("display", 'none');
            }
            else if (d.data.success === false) {
                alert("Restaurant already added.");
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
                alert("Restaurant update successfully.");
                $("#Admin_Addupdate").modal("hide");
                $("#loader").css("display", 'none');
            }
            else if (d.data.success === false) {
                alert("Restaurant already added.");
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

        var getStatus = AdminService.ChangeStatus(admin.RES_ID);
        getStatus.then(function (response) {
            GetRecordbyPaging();
            $.notify(response.data, "error");
        }, function () {
            $.notify("Error to load data...", "error");

        });

    };




    var Profile_photo = $('#Profile_photo');
    var reader = new FileReader();
    var fileName;
    var contentType;



    Profile_photo.change(function () {
        //alert("Image Changed");
        ReadUploadedFilesData($(this));
    });



    function ReadUploadedFilesData(fileuploader) {
        var file = $(fileuploader[0].files);
        fileName = file[0].name;
        contentType = file[0].type;
        reader.readAsDataURL(file[0]);




    }

    function validateFileReader(fileuploader) {
        if (typeof (FileReader) !== "undefined") {
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;

            if (fileuploader.val() === '') {
                return "Please Choose Image First";
            }
            else {
                var file = $(fileuploader[0].files);
                if (regex.test(file[0].name.toLowerCase())) {

                    var imageSize = Math.round(file[0].size / 1024);
                    //Check Image Size
                    if (imageSize < 5120) {
                        return "SaveImage";
                    }
                    else {
                        return 'Please Select Image Less Than 5 MB Size';
                    }

                } else {
                    return "Sorry... Invalid File";
                }
            }

        } else {
            return "Please Use Another Browser, This Browser is Not Supporting Image Uploader.";
        }
    }

    function getImageData(chooseimageFileUploader, tb_object) {
        var result = validateFileReader(chooseimageFileUploader);
        var IsImageChoosen = "No";
        if (result === "SaveImage") {
            IsImageChoosen = "Yes";
            // alert('success Save Image');
            var imageName = fileName.substring(0, fileName.lastIndexOf('.'));
            var imageExtension = '.' + fileName.substring(fileName.lastIndexOf('.') + 1);
            var imageBase64Data = reader.result;
            imageBase64Data = imageBase64Data.split(';')[1].replace("base64,", "");
        }
        //else {
        //    alert(result);
        //}

        tb_object.IsImageChoosen = IsImageChoosen;
        tb_object.ImageName = imageName;
        tb_object.ImageExtension = imageExtension;
        tb_object.ImageBase64Data = imageBase64Data;
        return tb_object;
    }





    $scope.GoToPreviousNextPage = function (pagehistory) {
        if (pagehistory === "Previous") {
            history.back(); //Go to the previous page
        }
    }


});