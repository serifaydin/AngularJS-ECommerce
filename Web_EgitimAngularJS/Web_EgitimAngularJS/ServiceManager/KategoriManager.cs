using System;
using System.Collections.Generic;
using System.Linq;
using Web_EgitimAngularJS.Models;

namespace Web_EgitimAngularJS.ServiceManager
{
    public class KategoriManager
    {
        private static KategoriManager _kategoriManager;
        static object _LockObject = new object();
        private KategoriManager() { }

        public static KategoriManager CreateAsSingleton()
        {
            lock (_LockObject)
                return _kategoriManager ?? (_kategoriManager = new KategoriManager());
        }

        KategoriProcessManager _manager = new KategoriProcessManager(new KategoriProcess());

        public string KategoriEkle(Models.Kategoriler model)
        {
            return _manager.KategoriEkle(model);
        }

        public List<Models.Kategoriler> KategorilerListe()
        {
            return _manager.KategorilerListele();
        }

        public string KategoriSil(int KategoriID)
        {
            return _manager.KategoriSil(KategoriID);
        }

        public string KategoriGuncelle(Models.Kategoriler model)
        {
            return _manager.KategoriGuncelle(model);
        }

        public Models.Kategoriler KategoriGetir(int KategoriID)
        {
            return _manager.KategoriGetir(KategoriID);
        }
    }

    abstract class KategoriFactory
    {
        public abstract List<Models.Kategoriler> KategorilerListele();
        public abstract string KategoriEkle(Models.Kategoriler model);
        public abstract string KategoriGuncelle(Models.Kategoriler model);
        public abstract string KategoriSil(int KategoriID);
        public abstract Models.Kategoriler KategoriGetir(int KategoriID);
    }

    class KategoriProcess : KategoriFactory
    {
        Services.ETicaretContext _context = new Services.ETicaretContext();

        public override string KategoriEkle(Models.Kategoriler model)
        {
            string mesaj = "";

            try
            {
                _context.Kategoriler.Add(model);
                _context.SaveChanges();

                mesaj = "Kayıt Başarılı.";
            }
            catch (Exception ex)
            {
                mesaj = ex.Message;
            }
            return mesaj;
        }

        public override Kategoriler KategoriGetir(int KategoriID)
        {
            return _context.Kategoriler.SingleOrDefault(k => k.TabloID == KategoriID);
        }

        public override string KategoriGuncelle(Models.Kategoriler model)
        {
            string mesaj = "";

            try
            {
                var _kategori = _context.Kategoriler.FirstOrDefault(w => w.TabloID == model.TabloID);

                _kategori.KategoriAdi = model.KategoriAdi;

                _context.SaveChanges();

                mesaj = "Güncelleme işlemi başarılı";
            }
            catch (Exception ex)
            {
                mesaj = ex.Message;
            }

            return mesaj;
        }

        public override List<Kategoriler> KategorilerListele()
        {
            return _context.Kategoriler.ToList();
        }

        public override string KategoriSil(int KategoriID)
        {
            string mesaj = "";
            try
            {
                var _kategori = _context.Kategoriler.RemoveRange(_context.Kategoriler.Where(k => k.TabloID == KategoriID));
                _context.SaveChanges();

                mesaj = "Silme işlemi başarılı.";
            }
            catch (Exception ex)
            {
                mesaj = ex.Message;
            }
            return mesaj;
        }
    }

    class KategoriProcessManager
    {
        KategoriFactory _kategoriFactory;

        public KategoriProcessManager(KategoriFactory kategoriFactory)
        {
            _kategoriFactory = kategoriFactory;
        }

        public string KategoriEkle(Models.Kategoriler model)
        {
            return _kategoriFactory.KategoriEkle(model);
        }

        public List<Models.Kategoriler> KategorilerListele()
        {
            return _kategoriFactory.KategorilerListele();
        }

        public string KategoriSil(int KategoriID)
        {
            return _kategoriFactory.KategoriSil(KategoriID);
        }

        public string KategoriGuncelle(Models.Kategoriler model)
        {
            return _kategoriFactory.KategoriGuncelle(model);
        }

        public Models.Kategoriler KategoriGetir(int KategoriID)
        {
            return _kategoriFactory.KategoriGetir(KategoriID);
        }
    }
}