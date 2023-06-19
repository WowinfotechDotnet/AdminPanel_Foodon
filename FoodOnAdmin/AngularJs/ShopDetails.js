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
  



});

app.controller('ShopController', function ($scope, plot1Service) {
    // alert();
    GetAll();
   

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

   


    $scope.OnCategoryClick = function (id) {

        $(".catmenu_button_Desc").removeClass().addClass('catmenu_button_Desc catStyle_Desc');
        $('#' + id + '_catId_Desc').removeClass().addClass('catmenu_button_Desc catStyle_active_Desc');

        if (id === '1')
        {
            GetAllImages('Image');
            document.getElementById('Demoexample_1').style.display = "block";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "none";
            document.getElementById('Demoexample_7').style.display = "none";
            document.getElementById('Demoexample_8').style.display = "none";
            document.getElementById('Demoexample_9').style.display = "none";
            document.getElementById('Demoexample_10').style.display = "none";
            

        }
        else if (id === '2')
        {
            GetAllImages('About');
            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "block";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "none";
            document.getElementById('Demoexample_7').style.display = "none";
            document.getElementById('Demoexample_8').style.display = "none";
            document.getElementById('Demoexample_9').style.display = "none";
            document.getElementById('Demoexample_10').style.display = "none";
            
        }

        else if (id === '3') {
            GetAllImages('Review');
            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "block";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "none";
            document.getElementById('Demoexample_7').style.display = "none";
            document.getElementById('Demoexample_8').style.display = "none";
            document.getElementById('Demoexample_9').style.display = "none";
            document.getElementById('Demoexample_10').style.display = "none";
          

        }

        else if (id === '4') {

            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "block";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "none";
            document.getElementById('Demoexample_7').style.display = "none";
            document.getElementById('Demoexample_8').style.display = "none";
            document.getElementById('Demoexample_9').style.display = "none";
            document.getElementById('Demoexample_10').style.display = "none";


        }

        else if (id === '5') {

            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "block";
            document.getElementById('Demoexample_6').style.display = "none";
            document.getElementById('Demoexample_7').style.display = "none";
            document.getElementById('Demoexample_8').style.display = "none";
            document.getElementById('Demoexample_9').style.display = "none";
            document.getElementById('Demoexample_10').style.display = "none";

        }

        else if (id === '6') {

            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "block";
            document.getElementById('Demoexample_7').style.display = "none";
            document.getElementById('Demoexample_8').style.display = "none";
            document.getElementById('Demoexample_9').style.display = "none";
            document.getElementById('Demoexample_10').style.display = "none";


        }
        else if (id === '7') {

            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "none";
            document.getElementById('Demoexample_7').style.display = "block";
            document.getElementById('Demoexample_8').style.display = "none";
            document.getElementById('Demoexample_9').style.display = "none";
            document.getElementById('Demoexample_10').style.display = "none";
        }

        else if (id === '8') {

            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "none";
            document.getElementById('Demoexample_7').style.display = "none";
            document.getElementById('Demoexample_8').style.display = "block";
            document.getElementById('Demoexample_9').style.display = "none";
            document.getElementById('Demoexample_10').style.display = "none";


        }
        else if (id === '9') {

            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "none";
            document.getElementById('Demoexample_7').style.display = "none";
            document.getElementById('Demoexample_8').style.display = "none";
            document.getElementById('Demoexample_9').style.display = "block";
            document.getElementById('Demoexample_10').style.display = "none";


        }
        else if (id === '10') {

            document.getElementById('Demoexample_1').style.display = "none";
            document.getElementById('Demoexample_2').style.display = "none";
            document.getElementById('Demoexample_3').style.display = "none";
            document.getElementById('Demoexample_4').style.display = "none";
            document.getElementById('Demoexample_5').style.display = "none";
            document.getElementById('Demoexample_6').style.display = "none";
            document.getElementById('Demoexample_7').style.display = "none";
            document.getElementById('Demoexample_8').style.display = "none";
            document.getElementById('Demoexample_9').style.display = "none";
            document.getElementById('Demoexample_10').style.display = "block";


        }

    }



  


});