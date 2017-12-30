using System;
using Web_EgitimAngularJS.Models;

namespace Web_EgitimAngularJS.ServiceManager
{
    public class IletisimManager
    {
        private static IletisimManager _iletisimManager;
        static object _lockObject = new object();
        private IletisimManager() { }

        public static IletisimManager CreateAsSingleton()
        {
            lock (_lockObject)
                return _iletisimManager ?? (_iletisimManager = new IletisimManager());
        }

        public bool IletisimEkle(Models.Iletisim model)
        {
            IletisimProcessManager _manager = new IletisimProcessManager(new IletisimProcess());
            return _manager.IletisimEkle(model);
        }
    }

    abstract class IletisimFactory
    {
        public abstract bool IletisimEkle(Models.Iletisim model);
    }

    class IletisimProcess : IletisimFactory
    {
        Services.ETicaretContext _context = new Services.ETicaretContext();

        public override bool IletisimEkle(Iletisim model)
        {
            bool durum = true;

            try
            {
                Iletisim _model = new Iletisim
                {
                    Ad = model.Ad,
                    Soyad = model.Soyad,
                    Telefon = model.Telefon,
                    Sirket = model.Sirket,
                    Mesaj = model.Mesaj,
                    Tarih = DateTime.Now
                };

                _context.Iletisim.Add(_model);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                durum = false;

                LoggerManager _loggerManager = LoggerManager.CreateAsSingleton();
                _loggerManager.LogEkleMethot(Settings.AppSettings.LogPath, ex.Message);
            }

            return durum;
        }
    }

    class IletisimProcessManager
    {
        private IletisimFactory _iletisimFactory;

        public IletisimProcessManager(IletisimFactory iletisimFactory)
        {
            _iletisimFactory = iletisimFactory;
        }

        public bool IletisimEkle(Models.Iletisim model)
        {
            return _iletisimFactory.IletisimEkle(model);
        }
    }
}