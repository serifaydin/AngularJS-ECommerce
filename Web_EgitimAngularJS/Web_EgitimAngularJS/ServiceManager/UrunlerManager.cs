using System;
using System.Linq;
using Web_EgitimAngularJS.Models;

namespace Web_EgitimAngularJS.ServiceManager
{
    public class UrunlerManager
    {
        private static UrunlerManager _urunlerManager;
        static object _LockObject = new object();
        private UrunlerManager() { }

        public static UrunlerManager CreateAsSingleton()
        {
            lock (_LockObject)
                return _urunlerManager ?? (_urunlerManager = new UrunlerManager());
        }

        UrunlerProcessManager _manager = new UrunlerProcessManager(new UrunlerProcess());

        public string UrunEkle(Models.Urunler model)
        {
            return _manager.UrunlerEkle(model);
        }

        public object UrunlerListele()
        {
            return _manager.UrunlerListele();
        }

        public object KategoriUrunlerListele(int KategoriID)
        {
            return _manager.KategoriUrunlerListele(KategoriID);
        }

        public object TedarikciUrunlerListele(int TedarikciID)
        {
            return _manager.TedarikciUrunlerListele(TedarikciID);
        }

        public Models.Urunler UrunDetayGetir(int UrunID)
        {
            return _manager.UrunDetayGetir(UrunID);
        }
    }

    abstract class UrunlerFactory
    {
        public abstract string UrunlerEkle(Models.Urunler model);
        public abstract object UrunlerListele();
        public abstract object KategoriUrunlerListele(int KategoriID);
        public abstract object TedarikciUrunlerListele(int TedarikciID);

        public abstract Models.Urunler UrunDetay(int UrunID);
    }

    class UrunlerProcess : UrunlerFactory
    {
        Services.ETicaretContext _context = new Services.ETicaretContext();

        public override object KategoriUrunlerListele(int KategoriID)
        {
            var result = _context.Urunler.Where(k => k.KategoriID == KategoriID).OrderByDescending(o => o.TabloID).ToList();

            return result;
        }

        public override object TedarikciUrunlerListele(int TedarikciID)
        {
            var result = _context.Urunler.Where(k => k.TedarikciID == TedarikciID).OrderByDescending(o => o.TabloID).ToList();

            return result;
        }

        public override Urunler UrunDetay(int UrunID)
        {
            return _context.Urunler.SingleOrDefault(u => u.TabloID == UrunID);
        }

        public override string UrunlerEkle(Urunler model)
        {
            string mesaj = "";

            try
            {
                string fullPath = "../UrunResimler/" + model.ResimUrl + ".jpg";

                model.ResimUrl = fullPath;
                model.OlusturmaTarih = DateTime.Now;
                _context.Urunler.Add(model);
                _context.SaveChanges();

                mesaj = "Kayıt Başarılı.";
            }
            catch (Exception ex)
            {
                mesaj = ex.Message;
            }
            return mesaj;
        }

        public override object UrunlerListele()
        {
            var result = _context.Urunler.OrderByDescending(o => o.TabloID).ToList();

            return result;
        }
    }

    class UrunlerProcessManager
    {
        UrunlerFactory _urunlerFactory;

        public UrunlerProcessManager(UrunlerFactory urunlerFactory)
        {
            _urunlerFactory = urunlerFactory;
        }

        public string UrunlerEkle(Models.Urunler model)
        {
            return _urunlerFactory.UrunlerEkle(model);
        }

        public object UrunlerListele()
        {
            return _urunlerFactory.UrunlerListele();
        }

        public object KategoriUrunlerListele(int KategoriID)
        {
            return _urunlerFactory.KategoriUrunlerListele(KategoriID);
        }

        public object TedarikciUrunlerListele(int TedarikciID)
        {
            return _urunlerFactory.TedarikciUrunlerListele(TedarikciID);
        }

        public Models.Urunler UrunDetayGetir(int UrunID)
        {
            return _urunlerFactory.UrunDetay(UrunID);
        }
    }
}