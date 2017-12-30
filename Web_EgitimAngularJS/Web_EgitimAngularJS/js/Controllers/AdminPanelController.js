app.controller('AdminPanelController', function ($scope, $stateParams, $mdDialog, AdminPanelFactory, $location) {

    $scope.customFullscreen = false;

    var id = $stateParams.id;

    var _location = $location.$$path;

    $scope.Urunler = [];

    if (_location.includes("KategoriUrunler") | _location.includes("TedarikUrunler")) {
        if (_location.includes("KategoriUrunler")) {
            AdminPanelFactory.KategoriUrunlerListele(id).then(function (response) {
                $scope.Urunler = response.data.list;
            }).catch(function (err) {

            }).finally(function () {
                EnSonUrun();
            });
        }
        if (_location.includes("TedarikUrunler")) {
            AdminPanelFactory.TedarikciUrunlerListele(id).then(function (response) {
                $scope.Urunler = response.data.list;
            }).catch(function (err) {

            }).finally(function () {
                EnSonUrun();
            });
        }
    }
    else {
        function UrunlerListele() {
            AdminPanelFactory.UrunlerListele().then(function (response) {
                $scope.Urunler = response.data.list;
            }).catch(function (err) {

            }).finally(function () {
                EnSonUrun();
            });
        };

        UrunlerListele();
    }

    $scope.KategoriShowAdvanced = function () {
        $mdDialog.show({
            controller: KategoriDialogController,
            templateUrl: 'MaterialPage/Kategori.tmpl.html',
            parent: angular.element(document.body),
            clickOutsideToClose: true,
            fullscreen: $scope.customFullscreen
        })
    };

    $scope.TedarikciShowAdvanced = function () {
        $mdDialog.show({
            controller: TedarikciDialogController,
            templateUrl: 'MaterialPage/Tedarikci.tmpl.html',
            parent: angular.element(document.body),
            clickOutsideToClose: true,
            fullscreen: $scope.customFullscreen
        })
    };

    function KategorilerListeleFunction() {
        AdminPanelFactory.KategorilerListele().then(function (response) {
            $scope.Kategoriler = response.data.list;
        })
    };

    KategorilerListeleFunction();

    function TedarikcilerListeleFunction() {
        AdminPanelFactory.TedarikcilerListele().then(function (response) {
            $scope.Tedarikciler = response.data.list;
        })
    };

    TedarikcilerListeleFunction();

    function EnSonUrun() {
        $scope.SonUrun = $scope.Urunler[0];
    };


    function KategoriDialogController($scope, $mdDialog, AdminPanelFactory) {

        $scope.guncellemeMi = false;
        $scope.KategoribtnText = "Ekle";

        $scope.hide = function () {
            $mdDialog.hide();
        };

        $scope.Kategoriler = [];

        function KategorilerListeleFunction() {
            AdminPanelFactory.KategorilerListele().then(function (response) {
                $scope.Kategoriler = response.data.list;
            })
        };

        KategorilerListeleFunction();

        $scope.KategoriEkleGuncelleFunction = function () {
            if ($scope.guncellemeMi == false) {
                AdminPanelFactory.kategoriEkle($scope.kategori).then(function (response) {
                    alert(response.data.status);
                    $scope.kategori = null;
                }).catch(function (err) {

                }).finally(function () {
                    KategorilerListeleFunction();
                })
            }
            else {
                AdminPanelFactory.kategoriGuncelle($scope.kategori).then(function (response) {
                    alert(response.data.status);

                    $scope.kategori = null;
                    $scope.KategoribtnText = "Ekle";
                    $scope.guncellemeMi = false;
                }).catch(function (err) {

                }).finally(function () {
                    KategorilerListeleFunction();
                })
            }
        };

        $scope.KategoriSilFunction = function (index, id) {
            AdminPanelFactory.KategoriSil(id).then(function (response) {
                $scope.Kategoriler.splice(index, 1);
            });
        };

        $scope.KategoriGucelleItem = function (id) {
            $scope.kategori = AdminPanelFactory.KategoriGetir(id).then(function (response) {
                $scope.kategori = response.data.model;
            });

            $scope.guncellemeMi = true;
            $scope.KategoribtnText = "Güncelle";
        };

        $scope.iptal = function () {
            $scope.guncellemeMi = false;
            $scope.KategoribtnText = "Ekle";
            $scope.kategori = null;
        };
    }

    function TedarikciDialogController($scope, $mdDialog, AdminPanelFactory) {

        $scope.guncellemeMi = false;
        $scope.TedarikcibtnText = "Ekle";

        $scope.hide = function () {
            $mdDialog.hide();
        };

        $scope.Tedarikciler = [];

        function TedarikcilerListeleFunction() {
            AdminPanelFactory.TedarikcilerListele().then(function (response) {
                $scope.Tedarikciler = response.data.list;
            })
        };

        TedarikcilerListeleFunction();

        $scope.TedarikciEkleGuncelleFunction = function () {
            if ($scope.guncellemeMi == false) {
                AdminPanelFactory.TedarikciEkle($scope.tedarikci).then(function (response) {

                    alert(response.data.status);
                    $scope.tedarikci = null;

                }).catch(function (err) {

                }).finally(function () {
                    TedarikcilerListeleFunction();
                })
            }
            else {
                AdminPanelFactory.TedarikciGuncelle($scope.tedarikci).then(function (response) {
                    alert(response.data.status);

                    $scope.tedarikci = null;
                    $scope.TedarikcibtnText = "Ekle";
                    $scope.guncellemeMi = false;

                }).catch(function (err) {

                }).finally(function () {
                    TedarikcilerListeleFunction();
                })
            }
        };

        $scope.TedarikciSilFunction = function (index, id) {
            AdminPanelFactory.TedarikciSil(id).then(function (response) {
                $scope.Tedarikciler.splice(index, 1);
            });
        };

        $scope.TedarikciGuncelleItem = function (id) {
            AdminPanelFactory.TedarikciGetir(id).then(function (response) {
                $scope.tedarikci = response.data.model;
            });

            $scope.guncellemeMi = true;
            $scope.TedarikcibtnText = "Güncelle";
        };

        $scope.iptal = function () {
            $scope.guncellemeMi = false;
            $scope.TedarikcibtnText = "Ekle";
            $scope.tedarikci = null;
        };

    }

    $scope.IletisimEkleFunction = function () {
        AdminPanelFactory.iletisimEkle($scope.iletisim).then(function (response) {
            alert(response.data.status);
            $scope.iletisim = null;
        });
    };

    $scope.UrunEkleFunction = function () {
        AdminPanelFactory.UrunEkle($scope.urun).then(function (response) {
            alert(response.data.status);

            $scope.urun = null;
        });
    };
});

app.factory("AdminPanelFactory", function ($http) {
    var fac = {};

    var _url = "/AdminPanel/";

    fac.iletisimEkle = function (model) {
        return $http({
            method: "POST", url: _url + "IletisimEkle", data: model
        });
    };

    fac.kategoriEkle = function (model) {
        return $http({
            method: "POST", url: _url + "KategoriEkle", data: model
        });
    };

    fac.KategorilerListele = function () {
        return $http.get(_url + "KategoriListele")
    };

    fac.KategoriSil = function (KategoriID) {
        return $http.post(_url + "KategoriSil", { KategoriID: KategoriID })
    };

    fac.kategoriGuncelle = function (model) {
        return $http({
            method: "POST", url: _url + "KategoriGuncelle", data: model
        });
    };

    fac.KategoriGetir = function (KategoriID) {
        return $http.get(_url + "KategoriGetir", { params: { KategoriID: KategoriID } })
    };

    //----------------------------------------
    fac.TedarikciEkle = function (model) {
        return $http({
            method: "POST", url: _url + "TedarikciEkle", data: model
        });
    };

    fac.TedarikcilerListele = function () {
        return $http.get(_url + "TedarikciListele")
    };

    fac.TedarikciSil = function (TedarikciID) {
        return $http.post(_url + "TedarikciSil", { TedarikciID: TedarikciID })
    };

    fac.TedarikciGuncelle = function (model) {
        return $http({
            method: "POST", url: _url + "TedarikciGuncelle", data: model
        });
    };

    fac.TedarikciGetir = function (TedarikciID) {
        return $http.get(_url + "TedarikciGetir", { params: { TedarikciID: TedarikciID } })
    };

    //-----------------------------------------
    fac.UrunEkle = function (model) {
        return $http({
            method: "POST", url: _url + "UrunEkle", data: model
        });
    }

    fac.UrunlerListele = function () {
        return $http.get(_url + "UrunListele")
    };

    fac.KategoriUrunlerListele = function (KategoriID) {
        return $http.get(_url + "KategoriUrunListele", { params: { KategoriID: KategoriID } })
    };

    fac.TedarikciUrunlerListele = function (TedarikciID) {
        return $http.get(_url + "TedarikciUrunListele", { params: { TedarikciID: TedarikciID } })
    };

    fac.UrunDetayGetir = function (UrunID) {
        return $http.get(_url + "UrundetayBilgisi", { params: { UrunID: UrunID } })
    };

    return fac;
});