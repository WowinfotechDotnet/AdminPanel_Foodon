app.service("EmployeeService", function ($http) {
    this.TotalRecordCount = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/DeliveryBoyMaster/TotalRecordCount",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };

    this.getRecordbyPaging = function (SearchingConditions) {
        var response = $http({
            method: "POST",
            url: "/DeliveryBoyMaster/GetallEmployee",
            data: JSON.stringify(SearchingConditions)
        });
        return response;
    };

    this.AddEmployee = function (tB_Employee) {
        var response = $http({
            method: "POST",
            url: "/DeliveryBoyMaster/AddEmployee",
            data: JSON.stringify(tB_Employee),
            dataType: "json"
        });
        return response;
    };


    this.EditEmployee = function (tB_Employee) {
        var response = $http({
            method: "POST",
            url: "/DeliveryBoyMaster/EditEmployee",
            data: JSON.stringify(tB_Employee),
            dataType: "json"
        });
        return response;
    };

    this.VehicleList = function () {
        return $http.get("/DeliveryBoyMaster/GetVehicleList");
    };

    this.GetEmployeeById = function (id) {
        var response = $http({
            method: "GET",
            url: "/DeliveryBoyMaster/GetEmployeeById",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;
    };

    this.UploadDocument = function (tb_Employee) {
        var response = $http({
            method: "POST",
            url: "/DeliveryBoyMaster/UploadDocument",
            data: JSON.stringify(tb_Employee),
            dataType: "json"
        });
        return response;
    };

    this.ChangeStatus = function (id) {
        var response = $http({
            method: "POST",
            url: "/DeliveryBoyMaster/ChangeStatus",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;
    };
});

app.controller("EmployeeCtrl", function ($scope, EmployeeService) {
    $scope.PageNo = 1;
    $scope.pageSize = 50;
    $scope.FARMER_SEARCH = null;
    $scope.STATE_SEARCH = null;
    $scope.ADHAR_BUTTON = 'orangered';
    $scope.PANCARD_BUTTON = 'orangered';

    GetTotalcount();
    GetAllVehicleList();
        
    function GetTotalcount() {
        var SearchingConditions = GetSearchingConditions();
        var getcount = EmployeeService.TotalRecordCount(SearchingConditions);
        getcount.then(function (d) {
            $scope.totalRecordCount = d.data.success;
            if ($scope.totalRecordCount === 0) {
                $scope.EmployeeList = "";
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

        var SearchingConditions = {
            PageNo: $scope.PageNo,
            pageSize: $scope.pageSize,
            FARMER_NAME: $scope.FARMER_SEARCH
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
            $scope.EmployeeList = {};

            return;
        }
        $scope.pager = GetPager($scope.totalRecordCount, page, $scope.pageSize);
        $scope.PageNo = $scope.pager.currentPage;

        GetRecordbyPaging();
    }

    function GetRecordbyPaging() {
        var SearchingConditions = GetSearchingConditions();
        var getrecord = EmployeeService.getRecordbyPaging(SearchingConditions);
        getrecord.then(function (response) {
            $scope.EmployeeList = response.data;
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
        $scope.EMP_ID = "";
        $scope.EMP_NAME = "";
        $scope.MOBILE_NO = "";
        $scope.ADDRESS = "";
        $scope.EMAIL = "";
        $scope.AreaOfWork = "";
        $scope.PASSWORD = "";
        $scope.AADHAAR = "";
        $scope.PAN = "";
    }

    $scope.AdminClick = function () {
        $scope.Employee_Action = "Add Employee";
        Clear();
        $("#Employee_Addupdate").modal("show");

    };

    $scope.Emp_View = function (employee) {
        $scope.EMP = employee;
        $("#Emp_View").modal("show");
    }

    $scope.AddEmployee = function () {
        $("#loader").css("display", '');
        tB_Employee = {
            EMP_ID: $scope.EMP_ID, //for update table
            EMP_NAME: $scope.EMP_NAME,
            MOBILE_NO: $scope.MOBILE_NO,
            ADDRESS: $scope.ADDRESS,
            EMAIL: $scope.EMAIL,
            AreaOfWork: $scope.AreaOfWork,
            PASSWORD: $scope.PASSWORD,
            AADHAAR: $scope.AADHAAR,
            PAN: $scope.PAN,
            VEHICAL_ID: $scope.VEHICAL_ID
        };
        if ($scope.Employee_Action === "Add Employee") {
            AddEmployeeRecord(tB_Employee);
        }
        else if ($scope.Employee_Action === "Update Employee") {
            EditEmployeeRecord(tB_Employee);
        }
    };

    $scope.Update = function (Employee) {
        $scope.Employee_Action = "Update Employee";
        $("#Employee_Addupdate").modal("show");
        var getEmployee = EmployeeService.GetEmployeeById(Employee.EMP_ID);
        getEmployee.then(function (response) {
            $scope._Party = response.data;
            $scope.EMP_NAME = $scope._Party.EMP_NAME;
            $scope.MOBILE_NO = parseInt($scope._Party.MOBILE_NO);
            $scope.ADDRESS = $scope._Party.ADDRESS;
            $scope.EMAIL = $scope._Party.EMAIL;
            $scope.PASSWORD = $scope._Party.PASSWORD;
            $scope.EMP_ID = $scope._Party.EMP_ID;
            $scope.AreaOfWork = $scope._Party.AreaOfWork;
            $scope.AADHAAR = $scope._Party.AADHAAR;
            $scope.PAN = $scope._Party.PAN;
            $scope.VEHICAL_ID = $scope._Party.VEHICAL_ID;

            setTimeout(function myfunction() {
                var blankSelectOptions = $('option[value$="?"]');
                if (blankSelectOptions.length > 0) {
                    $(blankSelectOptions).remove();
                }
                $("#VEHICAL_ID").val($scope.VEHICAL_ID);
            }, 500);


        });
    };

    function AddEmployeeRecord(tB_Employee) {
        var datalist = EmployeeService.AddEmployee(tB_Employee);
        datalist.then(function (d) {
            if (d.data.success === true) {
                Clear(); GetRecordbyPaging();
                alert("Employee added successfully.");
                $("#Employee_Addupdate").modal("hide");
                $("#loader").css("display", 'none');
            }
            else if (d.data.success === false) {
                alert("Employee already added.");
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

    function EditEmployeeRecord(tB_Employee) {
        var datalist = EmployeeService.EditEmployee(tB_Employee);
        datalist.then(function (d) {
            if (d.data.success === true) {
                Clear(); GetRecordbyPaging();
                alert("Employee update successfully.");
                $("#Employee_Addupdate").modal("hide");
                $("#loader").css("display", 'none');
            }
            else if (d.data.success === false) {
                alert("Employee already added.");
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

    function GetAllVehicleList() {
        var getrecord = EmployeeService.VehicleList();
        getrecord.then(function (response) {
            $scope.VehicleList = response.data;

           //alert(JSON.stringify($scope.VehicleList));

            RemoveBlankOptionFromDDL();

            $("#loader").css("display", 'none');

        }, function (error) {
            alert("Error to load data...", JSON.stringify(error));
            $("#loader").css("display", 'none');
        });
    }

    function RemoveBlankOptionFromDDL() {
        setTimeout(function myfunction() {
            var blankSelectOptions = $('option[value$="?"]'); //Remove first blank option from select list 
            if (blankSelectOptions.length > 0) {
                $(blankSelectOptions).remove(); //no need to loop, using remove() it will remove all blank options Existed on the document
            }
        }, 10);

    }

    $scope.UploadDocument = function (id) {
        tb_Employee =
        {
            EMP_ID: $scope.EMP_ID
        }
        tb_Employee.DOCUMENT_TYPE = id;

        if (id === "Adhar") {
            tb_Employee = getImageData(AADHAAR, tb_Employee, 'Adharcard');
            tb_Employee.AADHAAR = tb_Employee.IsImageChoosen;

            if (tb_Employee.Adharcard_Size === "Large Size") {
                alert("Please Upload Aadhar Card Copy Less Than 2 MB Size.");
                return false;
            }
            else if (tb_Employee.AADHAAR === "undefined" || tb_Employee.AADHAAR === "No" || tb_Employee.AADHAAR === undefined) {
                alert("Please Upload Aadhar Card Copy.");
                return false;
            }
        }

        if (id === "Pancard") {
            tb_Employee = getImageData(PAN, tb_Employee, 'Pancard');
            tb_Employee.PAN = tb_Employee.IsImageChoosenpancard;
            if (tb_Employee.Pancard_Size === "Large Size") {
                alert("Please Upload Pancard Copy Less Than 2 MB Size.");
                return false;
            }
            else if (tb_Employee.PAN === "undefined" || tb_Employee.PAN === "No" || tb_Employee.PAN === undefined) {
                alert(" Please Upload PAN Card Copy.");
                return false;
            }
        }

        var datalist = EmployeeService.UploadDocument(tb_Employee);
        datalist.then(function (response) {

            //if (d.data.success === -1) {
            //    alert("Document Not upload ");
            //}
            //else if (d.data.success == 1) {

                 $scope.empResponse = response.data;
                

                if (id === "Adhar") {
                    $scope.ADHAR_BUTTON = 'green';
                    $scope.AADHAAR = $scope.empResponse[0].AADHAAR;
                }

                if (id === "Pancard") {
                    $scope.PANCARD_BUTTON = 'green';
                    $scope.PAN = $scope.empResponse[0].PAN;
                }
           // }
        });
    }

    var ADHARCARD_COPY = $('#AADHAAR');
    var PANCARD_COPY = $('#PAN');

    ///adhar
    var reader = new FileReader();
    var fileName;
    var contentType;

    // pancard
    var Pancardreader = new FileReader();
    var PancardfileName;
    var PancardcontentType;

    ADHARCARD_COPY.change(function () {
        //alert("Image Changed");
        ReadUploadedFilesData($(this));
    });

    PANCARD_COPY.change(function () {
        //alert("Image Changed");
        PancardReadUploadedFilesData($(this));
    });

    ///adharcvard
    function ReadUploadedFilesData(fileuploader) {
        var file = $(fileuploader[0].files);
        fileName = file[0].name;
        contentType = file[0].type;
        reader.readAsDataURL(file[0]);
    }

    //pancard
    function PancardReadUploadedFilesData(fileuploader) {
        var Pancardfile = $(fileuploader[0].files);
        PancardfileName = Pancardfile[0].name;
        PancardcontentType = Pancardfile[0].type;
        Pancardreader.readAsDataURL(Pancardfile[0]);
    }

    function validateFileReader(fileuploader) {
        if (typeof (FileReader) !== "undefined") {
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp |.pdf)$/;

            if (fileuploader.value === '') {
                return "Please Choose Image First";
            }
            else {
               // var file = $(fileuploader[0].files);
                var file = $(fileuploader.files);
                if (regex.test(file[0].name.toLowerCase())) {

                    var imageSize = Math.round(file[0].size / 1024);
                    //Check Image Size
                    if (imageSize < 2048) {
                        return "SaveImage";
                    }
                    else {
                        return 'Large Size';
                        //return 'Please Select Image Less Than 5 MB Size';
                    }

                } else {
                    return "Sorry... Invalid File";
                }
            }

        } else {
            return "Please Use Another Browser, This Browser is Not Supporting Image Uploader.";
        }
    }


    function getImageData(chooseimageFileUploader, tb_object, Document_Type) {
        var result = validateFileReader(chooseimageFileUploader);
        if (Document_Type === "Adharcard") {
            var IsImageChoosen = "No";
            if (result === "SaveImage") {
                IsImageChoosen = "Yes";
                // alert('Adharcard');
                var imageName = fileName.substring(0, fileName.lastIndexOf('.'));
                var imageExtension = '.' + fileName.substring(fileName.lastIndexOf('.') + 1);
                var imageBase64Data = reader.result;
                imageBase64Data = imageBase64Data.split(';')[1].replace("base64,", "");
            }
            else {
                result === "Large Size";
            }
            tb_object.IsImageChoosen = IsImageChoosen;
            tb_object.ImageName = imageName;
            tb_object.ImageExtension = imageExtension;
            tb_object.ImageBase64Data = imageBase64Data;
            tb_object.Adharcard_Size = result;
            return tb_object;

        }
        else if (Document_Type === "Pancard") {
            var IsImageChoosenpancard = "No";
            if (result === "SaveImage") {
                IsImageChoosenpancard = "Yes";
                // alert('pancard');
                var imageName = PancardfileName.substring(0, PancardfileName.lastIndexOf('.'));
                var imageExtension = '.' + PancardfileName.substring(PancardfileName.lastIndexOf('.') + 1);
                var imageBase64Data = Pancardreader.result;
                imageBase64Data = imageBase64Data.split(';')[1].replace("base64,", "");
            }
            else {
                result === "Large Size";
            }
            tb_object.IsImageChoosenpancard = IsImageChoosenpancard;
            tb_object.PancardImageName = imageName;
            tb_object.PancardImageExtension = imageExtension;
            tb_object.PancardImageBase64Data = imageBase64Data;
            tb_object.Pancard_Size = result;
            return tb_object;
        }
    }

    $scope.ChangeStatus = function () {
        $("#loader").css("display", '');
        var getStatus = EmployeeService.ChangeStatus($scope.EMP.EMP_ID);
        getStatus.then(function (response) {
            //Clear();
            GetRecordbyPaging();
            $("#Emp_View").modal("hide");
            alert(response.data);
            $("#loader").css("display", 'none');

            //$.notify(response.data, "error");
        }, function () {
            $.notify("Error to load data...", "error");
        });

    };

    $scope.ViewImage = function (EmpImage, ImageType) {
        if (ImageType == 'Aadhaar') {
            $scope.IMAGE = EmpImage.AADHAAR
            $scope.IMAGE_CATEGORY = EmpImage.EMP_NAME + ': ' + ImageType
        }
        if (ImageType == 'PAN') {
            $scope.IMAGE = EmpImage.PAN
            $scope.IMAGE_CATEGORY = EmpImage.EMP_NAME + ': ' + ImageType
        }

        $("#viewDocument").modal("show");
    }

    $scope.CloseModel = function () {
        $("#viewDocument").modal("hide");

    }

    $scope.GoToPreviousNextPage = function (pagehistory) {
        if (pagehistory === "Previous") {
            history.back(); //Go to the previous page
        }
    }
});