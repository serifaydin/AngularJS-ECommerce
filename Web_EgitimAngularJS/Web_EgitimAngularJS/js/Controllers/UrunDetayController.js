app.controller("UrunDetayController", function ($scope, $stateParams, AdminPanelFactory) {

    var _urunID = $stateParams.urunID;

    function UrunDetayBilgileriFunction() {
        AdminPanelFactory.UrunDetayGetir(_urunID).then(function (response) {
            $scope.UrunDetay = response.data.model;
        });
    };
    UrunDetayBilgileriFunction();

});