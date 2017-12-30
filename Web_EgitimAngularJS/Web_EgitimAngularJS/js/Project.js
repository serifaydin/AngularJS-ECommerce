var app = angular.module("ECommerceApp", ['ui.router', 'ngMaterial']);

app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("/");

    $stateProvider
        .state("/", {
            url: "/",
            templateUrl: "/Pages/Default.html",
            controller: 'AdminPanelController'
        })
        .state("contact", {
            url: "/Pages/Contact",
            templateUrl: "/Pages/Contact.html",
            controller: 'AdminPanelController'
        })
        .state("adminpanel", {
            url: "/Pages/AdminPanel",
            templateUrl: "/Pages/AdminPanel.html",
            controller: 'AdminPanelController'
        })
        .state("kategoriUrunler", {
            url: "/Pages/KategoriUrunler/:id",
            templateUrl: "/Pages/Default.html",
            controller: "AdminPanelController"
        })
        .state("TedarikUrunler", {
            url: "/Pages/TedarikUrunler/:id",
            templateUrl: "/Pages/Default.html",
            controller: "AdminPanelController"
        })
        .state("urunDetay", {
            url: "/Pages/UrunDetay/:urunID",
            templateUrl: "/Pages/ProductDetail.html",
            controller: "UrunDetayController"
        })
});