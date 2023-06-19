app.service("AdminService", function ($http) {

    this.GetAdminProfile = function () {
        return $http.get("/AdminMaster/GetAdminProfile");
    };



    this.EditAdmin = function (tb_Admin) {
        var response = $http({
            method: "POST",
            url: "/AdminMaster/EditProfile",
            data: JSON.stringify(tb_Admin),
            dataType: "json"
        });
        return response;
    };





});

app.controller("AdminCtrl", function ($scope, AdminService) {


    $("#loader").css("display", '');
    GetTotalcount();
    function GetTotalcount() {

        var getAdmin = AdminService.GetAdminProfile();
        getAdmin.then(function (response) {
            $scope._Party = response.data;
            $("#loader").css("display", 'none');
            $scope.ADMIN_NAME = $scope._Party.ADMIN_NAME;
            $scope.MOBILE_NO = parseInt($scope._Party.MOBILE_NO);
            $scope.ADDRESS = $scope._Party.ADDRESS;
            $scope.EMAIL = $scope._Party.EMAIL;
            $scope.PASSWORD = $scope._Party.PASSWORD;
            $scope.ADMIN_ID = $scope._Party.ADMIN_ID;
            $scope.ROLE_ID = $scope._Party.ROLE_ID;
        });


    }







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
        var datalist = AdminService.EditAdmin(tb_Admin);
        datalist.then(function (d) {
            if (d.data.success === 1) {
                alert("Profile update successfully.");
                window.location.href = '/Home/Index';
                $("#loader").css("display", 'none');
            }
            else if (d.data.success === -1) {
                alert("Profile not updated.");
                $("#loader").css("display", 'none');
            }
            else {
                alert("Error.");
            }
        },
            function () {
                alert("Error.");
                $("#loader").css("display", 'none');
            });
    };













});