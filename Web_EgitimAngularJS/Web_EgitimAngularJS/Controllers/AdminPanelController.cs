using System.Web.Mvc;
using Web_EgitimAngularJS.ServiceManager;

namespace Web_EgitimAngularJS.Controllers
{
    public class AdminPanelController : Controller
    {
        private KategoriManager kategoriManager = KategoriManager.CreateAsSingleton();
        private IletisimManager iletisimManager = IletisimManager.CreateAsSingleton();
        private TedarikcilerManager tedarikciManager = TedarikcilerManager.CreateAsSingleton();
        private UrunlerManager urunlerManager = UrunlerManager.CreateAsSingleton();

        public JsonResult IletisimEkle(Models.Iletisim model)
        {
            return Json(new { status = iletisimManager.IletisimEkle(model) });
        }

        public JsonResult KategoriEkle(Models.Kategoriler model)
        {
            return Json(new { status = kategoriManager.KategoriEkle(model) });
        }

        public JsonResult KategoriListele()
        {
            return Json(new { list = kategoriManager.KategorilerListe() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KategoriSil(int KategoriID)
        {
            return Json(new { status = kategoriManager.KategoriSil(KategoriID) });
        }

        public JsonResult KategoriGuncelle(Models.Kategoriler model)
        {
            return Json(new { status = kategoriManager.KategoriGuncelle(model) });
        }

        public JsonResult KategoriGetir(int KategoriID)
        {
            return Json(new { model = kategoriManager.KategoriGetir(KategoriID) }, JsonRequestBehavior.AllowGet);
        }

        //---------------------------
        public JsonResult TedarikciEkle(Models.Tedarikciler model)
        {
            return Json(new { status = tedarikciManager.TedarikciEkle(model) });
        }

        public JsonResult TedarikciListele()
        {
            return Json(new { list = tedarikciManager.TedarikcilerListe() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TedarikciSil(int TedarikciID)
        {
            return Json(new { status = tedarikciManager.TedarikciSil(TedarikciID) });
        }

        public JsonResult TedarikciGuncelle(Models.Tedarikciler model)
        {
            return Json(new { status = tedarikciManager.TedarikciGuncelle(model) });
        }

        public JsonResult TedarikciGetir(int TedarikciID)
        {
            return Json(new { model = tedarikciManager.TedarikciGetir(TedarikciID) }, JsonRequestBehavior.AllowGet);
        }

        //----------------
        public JsonResult UrunEkle(Models.Urunler model)
        {
            return Json(new { status = urunlerManager.UrunEkle(model) });
        }

        //----------------
        public JsonResult UrunListele()
        {
            return Json(new { list = urunlerManager.UrunlerListele() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KategoriUrunListele(int KategoriID)
        {
            return Json(new { list = urunlerManager.KategoriUrunlerListele(KategoriID) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TedarikciUrunListele(int TedarikciID)
        {
            return Json(new { list = urunlerManager.TedarikciUrunlerListele(TedarikciID) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UrundetayBilgisi(int UrunID)
        {
            return Json(new { model = urunlerManager.UrunDetayGetir(UrunID) }, JsonRequestBehavior.AllowGet);
        }
    }
}