using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisSitesi
{
    class Program
    {
        static void Main(string[] args)
        {
            IEgitimService egitimService = new EgitimManager(new EfEgitiDal(),new YuzdelikEndirim());
            foreach (var Eg in egitimService.ListeleEgitimler())
            {
                Console.WriteLine(Eg.Ad + " " + Eg.Fiyat + " ");
            }
            Console.ReadLine();
        }
    }
    class Egitim : IEntity
    {
        public decimal ID { get; set; }
        public string Ad { get; set; }
        public decimal Fiyat { get; set; }
    }

    interface IEntity
    {
    }

    class EgitimManager : IEgitimService
    {
        IEgitiDal _egitiDal;
        IKampanyaService _kampanyaManager;
        public EgitimManager(IEgitiDal egitiDal, IKampanyaService kampanyaManager)
        {
            _egitiDal = egitiDal;
            _kampanyaManager = kampanyaManager;
        }
        public List<Egitim> ListeleEgitimler()
        {
            var egitimler = _egitiDal.ListeleEgitimler();
            _kampanyaManager.FiyatGuncelle(egitimler);
          
            return egitimler;
        }
    }

    interface IEgitiDal
    {
        List<Egitim> ListeleEgitimler();
    }

    class EfEgitiDal : IEgitiDal
    {
        public List<Egitim> ListeleEgitimler()
        {
            return new List<Egitim>
            {
                new Egitim{ID=1,Ad="C# kursu :",Fiyat=200},
                new Egitim{ID=2,Ad="C++  kursu :",Fiyat=500},
                new Egitim{ID=3,Ad="java  kursu :",Fiyat=300}
            };
        }
    }

    interface IEgitimService
    {
        List<Egitim> ListeleEgitimler();
    }

    interface IKampanyaService
    {
       void FiyatGuncelle(List<Egitim> egitimler);
    }

    class StandartFiyatKampanyaManager : IKampanyaService
    {
        public void FiyatGuncelle(List<Egitim> egitimler)
        {
            foreach (var eg in egitimler)
            {
                eg.Fiyat = GuncelFiyatiGetir();

            }
        }
        private decimal GuncelFiyatiGetir()
        {
            // veri tabanına baglan ve fiyat cek 
            return 30;
        }
    }

    class YuzdelikEndirim : IKampanyaService
    {
        public void FiyatGuncelle(List<Egitim> egitimler)
        {
            foreach (var Ege in egitimler)
            {
                Ege.Fiyat = Ege.Fiyat - (Ege.Fiyat * YyzdeEndirimGetir());
            }
            
        }

        private decimal YyzdeEndirimGetir()
        {
            return Convert.ToDecimal(0.90);
        }
    }
}
