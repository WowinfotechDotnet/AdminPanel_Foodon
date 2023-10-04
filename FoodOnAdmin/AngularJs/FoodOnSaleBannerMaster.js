app.service("AdminService", function ($http) {

    this.TotalRecordCount = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/FoodOnSaleBannerMaster/TotalRecordCount",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };


    this.getRecordbyPaging = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/FoodOnSaleBannerMaster/GetallAdmin",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };

    this.AddUpdateAdmin = function (tb_Admin) {
        var response = $http({
            method: "POST",
            url: "/FoodOnSaleBannerMaster/AddUpdateFoodOnSaleBanner",
            data: JSON.stringify(tb_Admin),
            dataType: "json"
        });
        return response;
    };

    this.ChangeStatus = function (id) {
        var response = $http({
            method: "POST",
            url: "/FoodOnSaleBannerMaster/ChangeStatus",
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

app.controller("FoodOnSaleBannerCtrl", function ($scope, AdminService) {

    
   
    $scope.PageNo = 1;
    $scope.pageSize = 50;
    $scope.BANNER_NAME = null;
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

        if ($scope.BANNER_NAME === undefined || $scope.BANNER_NAME === "" || $scope.BANNER_NAME === null) {
            $scope.BANNER_NAME = null;
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
            BANNER_NAME: $scope.BANNER_NAME,
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
        $scope.BANNER_ID = "";
        $scope.BANNER_NAME1 = "";
        $scope.BANNER_URL = "";
        $("#Admin_Addupdate").val("");
        setTimeout(function () {
            $scope.PreviewImage = "";
            $scope.$apply(); //this triggers a $digest
        });
    }


    function GetAllFoodCategory() {

        var getAdmin = AdminService.GetAllFoodCategory();
        getAdmin.then(function (response) {
            $scope.FoodCategoryList = response.data;
        });
    };




    $scope.AdminClick = function () {
        $scope.Admin_Action = "Add Banner Details";
        $scope.ACTION = "ADD";
        Clear();
        $("#Admin_Addupdate").modal("show");
        GetAllFoodCategory();
    };


    $scope.Update = function (admin) {
        Clear();
        $scope.Admin_Action = "Update Banner Details";
        $scope.ACTION = "UPDATE";
        $("#Admin_Addupdate").modal("show");
        $scope.BANNER_NAME1 = admin.BANNER_NAME;
        $scope.BANNER_ID = admin.BANNER_ID;
        $scope.FOOD_CATEGORY_ID1 = admin.FOOD_CATEGORY_ID;
        $scope.BANNER_URL = admin.BANNER_URL;
        GetAllFoodCategory();
        setTimeout(function () {
            $scope.PreviewImage = $scope.BANNER_URL;
            $scope.$apply(); //this triggers a $digest
        });
    };



    $scope.AddAdmin = function () {
        tb_Admin = {
            BANNER_NAME: $scope.BANNER_NAME1,
            BANNER_ID: parseInt($scope.BANNER_ID), // for Banner URL Update
            FOOD_CATEGORY_ID: parseInt($scope.FOOD_CATEGORY_ID1), 
            ACTION: $scope.ACTION,
        };
        if ($scope.Admin_Action === "Add Banner Details") {
            tb_Admin = getImageData(chooseimageFileUploader_AddBanner, tb_Admin);

            tb_Admin.BANNER_URL = tb_Admin.IsImageChoosen;

            if (tb_Admin.BANNER_URL === "No") {
                alert("Please Choose Banner Image");
                return false;
            }
            AddAdminRecord(tb_Admin);
        }
        else if ($scope.Admin_Action === "Update Banner Details") {
            tb_Admin = getImageData(chooseimageFileUploader_AddBanner, tb_Admin);
            if (tb_Admin.IsImageChoosen === "Yes") {
                tb_Admin.BANNER_URL = "Yes";
            }
            else {
                tb_Admin.BANNER_URL = $scope.BANNER_URL;
                //alert($scope.IMAGE_URL);
            }
            EditAdminRecord(tb_Admin);
        }
    };



    function AddAdminRecord(tb_Admin) {
        var datalist = AdminService.AddUpdateAdmin(tb_Admin);
        datalist.then(function (d) {
            if (d.data.success === true) {
                Clear(); GetRecordbyPaging();
                alert("Banner Details added successfully.");
                $("#Admin_Addupdate").modal("hide");
                
            }
            else if (d.data.success === false) {
                alert("Banner Details already added.");
                
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
                alert("Banner Details Updated successfully.");
                $("#Admin_Addupdate").modal("hide");
            }
            else if (d.data.success === false) {
                alert("Banner Details already added.");
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
       
        var getStatus = AdminService.ChangeStatus(admin.BANNER_ID);
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


    ///////////////////////////////// Image Validation ///////////////////////////////////////////////


    var chooseimageFileUploader_AddBanner = $('#chooseimageFileUploader_AddBanner');
    //var chooseimageFileUploader_UpdateCategory = $('#chooseimageFileUploader_UpdateCategory');

    var reader = new FileReader();
    var fileName;
    var contentType;

    chooseimageFileUploader_AddBanner.change(function () {
        //alert("Image Changed");
        ReadUploadedFilesData($(this));
    });
    //});

    //chooseimageFileUploader_AddCategory.change(function () {
    //    //alert("Image Changed");
    //    ReadUploadedFilesData($(this));
    //});


    function ReadUploadedFilesData(fileuploader) {
        var file = $(fileuploader[0].files);

        //To Check Cancel Button Is Clicked In File Uploader
        if (file.length > 0) {
            //alert("File Selected");

        }
        else {
            //Here Cancel Button Is Clicked In File Uploader
            //alert("File Not Selected");
            //$scope.PreviewImage = "";
            //$("#PostImage_img").removeAttr("src").attr("ng-src", "");
            //////$scope.$apply();
            setTimeout(function () {
                $scope.PreviewImage = "";
                console.log('Image Not Selected:' + $scope.PreviewImage);
                $scope.$apply(); //this triggers a $digest
            });

        }


        fileName = file[0].name;
        contentType = file[0].type;
        reader.readAsDataURL(file[0]);

        //alert("image Details");
        //alert(fileName);
        //alert(JSON.stringify(contentType));


    }

    function validateFileReader(fileuploader) {
        if (typeof (FileReader) !== "undefined") {
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;

            if (fileuploader.val() === '') {
                return "Please Choose Image First";
            }
            else {
                var file = $(fileuploader[0].files);
                var fileExtension = file[0].name.substr((file[0].name.lastIndexOf('.') + 1));
                var imgName = 'image.' + fileExtension;
                if (regex.test(imgName.toLowerCase())) {
                    //if (regex.test(file[0].name.toLowerCase())) {

                    //var imageSize = Math.round(file[0].size / 1024);
                    ////Check Image Size
                    //if (imageSize < 1024) {
                    //    return "SaveImage";
                    //}
                    //else {
                    //    return 'Image Size Exceeded';
                    //}


                    if ($scope.IsImageEdited === false) {
                        var imageSize = Math.round(file[0].size / 1024);
                        //Check Image Size
                        //alert("Image Size : " + imageSize);
                        if (imageSize < 2560) {
                            return "SaveImage";
                        }
                        else {
                            return 'Image Size Exceeded';
                        }

                    }
                    else {
                        let base64String = $scope.PreviewImage;
                        let padding;
                        let inBytes;
                        let base64StringLength;
                        if (base64String.endsWith('==')) { padding = 2; }
                        else if (base64String.endsWith('=')) { padding = 1; }
                        else { padding = 0; }

                        base64StringLength = base64String.length;
                        console.log(base64StringLength);
                        inBytes = (base64StringLength / 4) * 3 - padding;
                        console.log(inBytes);
                        this.kbytes = inBytes / 1000;
                        //return this.kbytes;
                        //alert("Edited Image Size : " + this.kbytes);

                        if (this.kbytes < 2560) {
                            return "SaveImage";
                        }
                        else {
                            return 'Image Size Exceeded';
                        }
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
            //var imageName = fileName.substring(0, fileName.lastIndexOf('.'));
            //var imageExtension = '.' + fileName.substring(fileName.lastIndexOf('.') + 1);
            //var imageBase64Data = reader.result;
            //imageBase64Data = imageBase64Data.split(';')[1].replace("base64,", "");


            var imageName = fileName.substring(0, fileName.lastIndexOf('.'));
            var imageExtension = '.' + fileName.substring(fileName.lastIndexOf('.') + 1);
            var imageBase64Data = "";
            if ($scope.IsImageEdited === false) {
                imageBase64Data = reader.result;
                imageBase64Data = imageBase64Data.split(';')[1].replace("base64,", "");
            }
            else {
                if ($scope.PreviewImage !== undefined && $scope.PreviewImage !== null && $scope.PreviewImage !== "") {
                    imageBase64Data = $scope.PreviewImage.split(';')[1].replace("base64,", "");
                }

            }
        }
        //else {
        //    alert(result);
        //}

        tb_object.IsImageChoosen = IsImageChoosen;
        tb_object.ImageName = imageName;
        tb_object.ImageExtension = imageExtension;
        tb_object.ImageBase64Data = imageBase64Data;

        tb_object.result = result;

        return tb_object;
    }


    $scope.OpenFileUploader_AddBanner = function () {

        chooseimageFileUploader_AddBanner.click();
    };


    $scope.IsImageEdited = false;
    $scope.SelectFile = function (e) {
        //Code To Preview Image
        var reader = new FileReader();
        reader.onload = function (e) {
            //$scope.PreviewImage = e.target.result;
            //$scope.$apply();

            setTimeout(function () {
                $scope.PreviewImage = e.target.result;
                console.log('Image Selected:' + $scope.PreviewImage);
                $scope.$apply();
            });
        };
        reader.readAsDataURL(e.target.files[0]);

        //Code To Edit Image
        var img = e.target.files[0];
        if (!pixelarity.open(img, false, function (res) {
            //$("#result").attr("src", res);
            $scope.PreviewImage = res;

            //alert($scope.PreviewImage);
            $scope.IsImageEdited = true;

            $scope.$apply();
        }, "jpg", 0.7)) {
            alert("Whoops! That is not an image!");
        }

    };

});