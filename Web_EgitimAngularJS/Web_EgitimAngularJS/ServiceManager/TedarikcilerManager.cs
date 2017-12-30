using System;
using System.Collections.Generic;
using System.Linq;

namespace Web_EgitimAngularJS.ServiceManager
{
    public class TedarikcilerManager
    {
        private static TedarikcilerManager _tedarikciManager;
        static object _LockObject = new object();
        private TedarikcilerManager() { }

        public static TedarikcilerManager CreateAsSingleton()
        {
            lock (_LockObject)
                return _tedarikciManager ?? (_tedarikciManager = new TedarikcilerManager());
        }

        TedarikciProcessManager _manager = new TedarikciProcessManager(new TedarikciProcess());

        public string TedarikciEkle(Models.Tedarikciler model)
        {
            return _manager.TedarikciEkle(model);
        }

        public List<Models.Tedarikciler> TedarikcilerListe()
        {
            return _manager.TedarikcilerListele();
        }

        public string TedarikciSil(int KategoriID)
        {
            return _manager.TedarikciSil(KategoriID);
        }

        public string TedarikciGuncelle(Models.Tedarikciler model)
        {
            return _manager.TedarikciGuncelle(model);
        }

        public Models.Tedarikciler TedarikciGetir(int TedarikciID)
        {
            return _manager.TedarikciGetir(TedarikciID);
        }
    }

    abstract class TedarikciFactory
    {
        public abstract List<Models.Tedarikciler> TedarikcilerListele();
        public abstract string TedarikciEkle(Models.Tedarikciler model);
        public abstract string TedarikciGuncelle(Models.Tedarikciler model);
        public abstract string TedarikciSil(int TedarikciID);
        public abstract Models.Tedarikciler TedarikciGetir(int TedarikciID);
    }

    class TedarikciProcess : TedarikciFactory
    {
        Services.ETicaretContext _context = new Services.ETicaretContext();

        public override string TedarikciEkle(Models.Tedarikciler model)
        {
            string mesaj = "";

            try
            {
                _context.Tedarikciler.Add(model);
                _context.SaveChanges();

                mesaj = "Kayıt Başarılı.";
            }
            catch (Exception ex)
            {
                mesaj = ex.Message;
            }
            return mesaj;
        }

        public override Models.Tedarikciler TedarikciGetir(int TedarikciID)
        {
            return _context.Tedarikciler.SingleOrDefault(k => k.TabloID == TedarikciID);
        }

        public override string TedarikciGuncelle(Models.Tedarikciler model)
        {
            string mesaj = "";

            try
            {
                var _tedarikci = _context.Tedarikciler.FirstOrDefault(w => w.TabloID == model.TabloID);

                _tedarikci.TedarikciAdi = model.TedarikciAdi;
                _tedarikci.Adres = model.Adres;
                _tedarikci.VergiNo = model.VergiNo;

                _context.SaveChanges();

                mesaj = "Güncelleme işlemi başarılı";
            }
            catch (Exception ex)
            {
                mesaj = ex.Message;
            }

            return mesaj;
        }

        public override List<Models.Tedarikciler> TedarikcilerListele()
        {
            return _context.Tedarikciler.ToList();
        }

        public override string TedarikciSil(int TedarikciID)
        {
            string mesaj = "";
            try
            {
                var _tedarikci = _context.Tedarikciler.RemoveRange(_context.Tedarikciler.Where(k => k.TabloID == TedarikciID));
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

    class TedarikciProcessManager
    {
        TedarikciFactory _tedarikciFactory;

        public TedarikciProcessManager(TedarikciFactory tedarikciFactory)
        {
            _tedarikciFactory = tedarikciFactory;
        }

        public string TedarikciEkle(Models.Tedarikciler model)
        {
            return _tedarikciFactory.TedarikciEkle(model);
        }

        public List<Models.Tedarikciler> TedarikcilerListele()
        {
            return _tedarikciFactory.TedarikcilerListele();
        }

        public string TedarikciSil(int TedarikciID)
        {
            return _tedarikciFactory.TedarikciSil(TedarikciID);
        }

        public string TedarikciGuncelle(Models.Tedarikciler model)
        {
            return _tedarikciFactory.TedarikciGuncelle(model);
        }

        public Models.Tedarikciler TedarikciGetir(int TedarikciID)
        {
            return _tedarikciFactory.TedarikciGetir(TedarikciID);
        }
    }
}