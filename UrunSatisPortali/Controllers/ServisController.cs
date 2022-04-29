using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UrunSatisPortali.Models;
using UrunSatisPortali.ViewModel;

namespace UrunSatisPortali.Controllers
{
    public class ServisController : ApiController
    {
        DBEntities db = new DBEntities();
        SonucModel sonuc = new SonucModel();

        #region Kullanici
        [HttpGet]
        [Route("api/KullaniciListe")]
        public List<KullaniciModel> KullaniciListe()
        {
            List<KullaniciModel> liste = db.Kullanici.Select(x => new KullaniciModel()
            {
                KullaniciId = x.KullaniciId,
                AdSoyad = x.AdSoyad,
                KullaniciAdi = x.KullaniciAdi,
                Sifre = x.Sifre,
                Email = x.Email,
                TelNo = x.TelNo,
                KayitTarihi = x.KayitTarihi,
                KullaniciYetki = x.KullaniciYetki
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/KullaniciById/{KullaniciId}")]
        public KullaniciModel KullaniciUyeById(int KullaniciId)
        {
            KullaniciModel kayit = db.Kullanici.Where(s => s.KullaniciId == KullaniciId).Select(x => new KullaniciModel()
            {
                KullaniciId = x.KullaniciId,
                AdSoyad = x.AdSoyad,
                KullaniciAdi = x.KullaniciAdi,
                Sifre = x.Sifre,
                Email = x.Email,
                TelNo = x.TelNo,
                KayitTarihi = x.KayitTarihi,
                KullaniciYetki = x.KullaniciYetki
            }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/KullaniciEkle")]
        public SonucModel KullaniciEkle(KullaniciModel model)
        {
            if (db.Kullanici.Count(s => s.KullaniciAdi == model.KullaniciAdi || s.Email == model.Email) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcı Adı veya E-Posta Adresi Kayıtlıdır!";
                return sonuc;
            }
            Kullanici yeni = new Kullanici();
            yeni.AdSoyad = model.AdSoyad;
            yeni.KullaniciAdi = model.KullaniciAdi;
            yeni.Sifre = model.Sifre;
            yeni.Email = model.Email;
            yeni.TelNo = model.TelNo;
            yeni.KayitTarihi = model.KayitTarihi;
            yeni.KullaniciYetki = model.KullaniciYetki;
            db.Kullanici.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanici Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/KullaniciDuzenle")]
        public SonucModel KullaniciDuzenle(KullaniciModel model)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.KullaniciId == model.KullaniciId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            kayit.AdSoyad = model.AdSoyad;
            kayit.KullaniciAdi = model.KullaniciAdi;
            kayit.Sifre = model.Sifre;
            kayit.Email = model.Email;
            kayit.TelNo = model.TelNo;
            kayit.KayitTarihi = model.KayitTarihi;
            kayit.KullaniciYetki = model.KullaniciYetki;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanici Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/KullaniciSil/{KullaniciId}")]
        public SonucModel KullaniciSil(int KullaniciId)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.KullaniciId == KullaniciId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            if (db.Urun.Count(s => s.KullaniciId == KullaniciId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Urun Kaydı Olan Kullanıcı Silinemez!";
                return sonuc;
            }
            db.Kullanici.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanici Silindi";
            return sonuc;
        }
        #endregion

        #region Sepet
        [HttpGet]
        [Route("api/SepetListe")]
        public List<SepetModel> SepetListe()
        {
            List<SepetModel> liste = db.Sepet.Select(x => new SepetModel()
            {
                SepetId = x.SepetId,
                UrunId = x.UrunId,
                Adet = x.Adet,
                KullaniciId = x.KullaniciId
            }).ToList();
            return liste;
        }
        [HttpPost]
        [Route("api/SepetEkle")]
        public SonucModel SepetEkle(SepetModel model)
        {
            Sepet yeni = new Sepet();
            yeni.UrunId = model.UrunId;
            yeni.Adet = model.Adet;
            yeni.KullaniciId = model.KullaniciId;

            db.Sepet.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sepet Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/SepetDuzenle")]
        public SonucModel SepetDuzenle(SepetModel model)
        {
            Sepet kayit = db.Sepet.Where(s => s.SepetId == model.SepetId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayit Bulunamadı!";
                return sonuc;
            }
            kayit.UrunId = model.UrunId;
            kayit.Adet = model.Adet;
            kayit.KullaniciId = model.KullaniciId;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sepet Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/SepetSil/{SepetId}")]
        public SonucModel SepetSil(int SepetId)
        {
            Sepet kayit = db.Sepet.Where(s => s.SepetId == SepetId).SingleOrDefault(
           );
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.Sepet.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sepet Silindi";
            return sonuc;
        }
        [HttpGet]
        [Route("api/FavoriById/{KullaniciId}")]
        public List<SepetModel> sepetbykullaniciid(int KullaniciId)
        {
            List<SepetModel> kayit = db.Sepet.Where(s => s.SepetId == KullaniciId).Select(x => new SepetModel()
            {
                SepetId = x.SepetId,
                UrunId = x.UrunId,
                Adet = x.Adet,
                KullaniciId = x.KullaniciId
            }).ToList();
            return kayit;
        }
        #endregion

        #region Kategori
        [HttpGet]
        [Route("api/KategoriListe")]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                KategoriId = x.KategoriId,
                KategoriAdi = x.KategoriAdi,
                KategoriUrunSay = x.Urun.Count()
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/kategoriById/{KategoriId}")]
        public KategoriModel KategoriById(int KategoriId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.KategoriId == KategoriId).Select(x => new KategoriModel()
            {
                KategoriId = x.KategoriId,
                KategoriAdi = x.KategoriAdi,
                KategoriUrunSay = x.Urun.Count()
            }).FirstOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/KategoriEkle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.KategoriAdi == model.KategoriAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Adı Kayıtlıdır!";
                return sonuc;
            }
            Kategori yeni = new Kategori();
            yeni.KategoriAdi = model.KategoriAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/KategoriDuzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.KategoriId == model.KategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.KategoriAdi = model.KategoriAdi;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/KategoriSil/{KategoriId}")]
        public SonucModel KategoriSil(int KategoriId)
        {
            Kategori kayit = db.Kategori.Where(s => s.KategoriId == KategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            if (db.Urun.Count(s => s.KategoriId == KategoriId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Ürün Kaydı Olan Kategori Silinemez!";
                return sonuc;
            }
            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";
            return sonuc;
        }
        #endregion

        #region Fiyat
        [HttpGet]
        [Route("api/FiyatListe")]
        public List<FiyatModel> FiyatListe()
        {
            List<FiyatModel> liste = db.Fiyat.Select(x => new FiyatModel()
            {
                FiyatId = x.FiyatId,
                AlisFiyati = x.AlisFiyati,
                SatisFiyati = x.SatisFiyati
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/FiyatById/{KategoriId}")]
        public FiyatModel FiyatById(int FiyatId)
        {
            FiyatModel kayit = db.Fiyat.Where(s => s.FiyatId == FiyatId).Select(x => new FiyatModel()
            {
                FiyatId = x.FiyatId,
                AlisFiyati = x.AlisFiyati,
                SatisFiyati = x.SatisFiyati
            }).FirstOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/FiyatEkle")]
        public SonucModel FiyatEkle(FiyatModel model)
        {
            Fiyat yeni = new Fiyat();
            yeni.AlisFiyati = model.AlisFiyati;
            yeni.SatisFiyati = model.SatisFiyati;

            db.Fiyat.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Fiyat Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/FiyatDuzenle")]
        public SonucModel FiyatDuzenle(FiyatModel model)
        {
            Fiyat kayit = db.Fiyat.Where(s => s.FiyatId == model.FiyatId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.AlisFiyati = model.AlisFiyati;
            kayit.SatisFiyati = model.SatisFiyati;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "FiyatKategori Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/FiyatSil/{FiyatId}")]
        public SonucModel FiyatSil(int FiyatId)
        {
            Fiyat kayit = db.Fiyat.Where(s => s.FiyatId == FiyatId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            if (db.Urun.Count(s => s.FiyatId == FiyatId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Ürün Kaydı Olan Fiyat Silinemez!";
                return sonuc;
            }
            db.Fiyat.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Fiyat Silindi";
            return sonuc;
        }
        #endregion

        #region Urun

        [HttpGet]
        [Route("api/UrunListe")]
        public List<UrunModel> UrunListe()
        {
            List<UrunModel> liste = db.Urun.Select(x => new UrunModel()
            {
                UrunId = x.UrunId,
                UrunAdi = x.UrunAdi,
                KategoriId = x.KategoriId,
                KategoriAdi = x.Kategori.KategoriAdi,
                MarkaAdi = x.MarkaAdi,
                Stok = x.Stok,
                Kargo = x.Kargo,
                FiyatId = x.FiyatId,
                KullaniciId = x.KullaniciId

            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/UrunListeByKatId/{katId}")]
        public List<UrunModel> UrunListeByKatId(int katId)
        {
            List<UrunModel> liste = db.Urun.Where(s => s.KategoriId == katId).Select
           (x => new UrunModel()
           {
               UrunId = x.UrunId,
               UrunAdi = x.UrunAdi,
               KategoriId = x.KategoriId,
               KategoriAdi = x.Kategori.KategoriAdi,
               MarkaAdi = x.MarkaAdi,
               Stok = x.Stok,
               Kargo = x.Kargo,
               FiyatId = x.FiyatId,
               KullaniciId = x.KullaniciId
           }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/UrunListeByUyeId/{KullaniciId}")]
        public List<UrunModel> UrunListeByUyeId(int KullaniciId)
        {
            List<UrunModel> liste = db.Urun.Where(s => s.UrunId == KullaniciId).Select(x =>
           new UrunModel()
           {
               UrunId = x.UrunId,
               UrunAdi = x.UrunAdi,
               KategoriId = x.KategoriId,
               KategoriAdi = x.Kategori.KategoriAdi,
               MarkaAdi = x.MarkaAdi,
               Stok = x.Stok,
               Kargo = x.Kargo,
               FiyatId = x.FiyatId,
               KullaniciId = x.KullaniciId
           }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/UrunById/{UrunId}")]
        public UrunModel UrunById(int UrunId)
        {
            UrunModel kayit = db.Urun.Where(s => s.UrunId == UrunId).Select(x =>
           new UrunModel()
           {
               UrunId = x.UrunId,
               UrunAdi = x.UrunAdi,
               KategoriId = x.KategoriId,
               KategoriAdi = x.Kategori.KategoriAdi,
               MarkaAdi = x.MarkaAdi,
               Stok = x.Stok,
               Kargo = x.Kargo,
               FiyatId = x.FiyatId,
               KullaniciId = x.KullaniciId
           }).SingleOrDefault();
            return kayit;
        }
        [HttpGet]
        [Route("api/UrunDetayListe/{UrunId}")]
        public List<UrunModel> UrunDetayListe(int UrunId)
        {
            List<UrunModel> liste = db.Urun.Where(s => s.UrunId == UrunId).Select(x => new UrunModel()
            {
                UrunId = x.UrunId,
                UrunAdi = x.UrunAdi,
                KategoriId = x.KategoriId,
                KategoriAdi = x.Kategori.KategoriAdi,
                MarkaAdi = x.MarkaAdi,
                Stok = x.Stok,
                Kargo = x.Kargo,
                FiyatId = x.FiyatId,
                KullaniciId = x.KullaniciId

            }).ToList();
            foreach (var Urun in liste)
            {
                Urun.resimbilgi = resimbyurunid(Urun.UrunId);
                Urun.yorumbilgi = YorumListeByurunId(Urun.UrunId);
            }
            return liste;
        }
        [HttpPost]
        [Route("api/UrunEkle")]
        public SonucModel UrunEkle(UrunModel model)
        {
            
            Urun yeni = new Urun();
            yeni.UrunAdi = model.UrunAdi;
            yeni.KategoriId = model.KategoriId;
            yeni.MarkaAdi = model.MarkaAdi;
            yeni.Stok = model.Stok;
            yeni.Kargo = model.Kargo;
            yeni.FiyatId = model.FiyatId;
            yeni.KullaniciId = model.KullaniciId;


            db.Urun.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Urun Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/UrunDuzenle")]
        public SonucModel UrunDuzenle(UrunModel model)
        {
            Urun kayit = db.Urun.Where(s => s.UrunId == model.UrunId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.UrunAdi = model.UrunAdi;
            kayit.KategoriId = model.KategoriId;
            kayit.MarkaAdi = model.MarkaAdi;
            kayit.Stok = model.Stok;
            kayit.Kargo = model.Kargo;
            kayit.FiyatId = model.FiyatId;
            kayit.KullaniciId = model.KullaniciId;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Urun Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/UrunSil/{UrunId}")]
        public SonucModel UrunSil(int UrunId)
        {
            Urun kayit = db.Urun.Where(s => s.UrunId == UrunId).SingleOrDefault(
           );
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }


            db.Urun.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Urun Silindi";
            return sonuc;
        }

        #endregion

        #region Resim
        [HttpGet]
        [Route("api/ResimListe")]
        public List<ResimModel> resimListe()
        {
            List<ResimModel> liste = db.Resim.Select(x => new ResimModel()
            {
                ResimId = x.ResimId,
                ResimAdi = x.ResimAdi,
                UrunId = x.UrunId
            }).ToList();
            return liste;
        }
        [HttpPost]
        [Route("api/ResimEkle")]
        public SonucModel ResimEkle(ResimModel model)
        {
            Resim yeni = new Resim();
            yeni.ResimAdi = model.ResimAdi;
            yeni.UrunId = model.UrunId;

            db.Resim.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Urun Resim Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/ResimDuzenle")]
        public SonucModel ResimDuzenle(ResimModel model)
        {
            Resim kayit = db.Resim.Where(s => s.ResimId == model.ResimId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayit Bulunamadı!";
                return sonuc;
            }
            kayit.ResimAdi = model.ResimAdi;
            kayit.UrunId = model.UrunId;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Resim Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/ResimSil/{ResimId}")]
        public SonucModel ResimSil(int ResimId)
        {
            Resim kayit = db.Resim.Where(s => s.ResimId == ResimId).SingleOrDefault(
           );
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.Resim.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Resim Silindi";
            return sonuc;
        }
        [HttpGet]
        [Route("api/ResimById/{UrunId}")]
        public List<ResimModel> resimbyurunid(int UrunId)
        {
            List<ResimModel> kayit = db.Resim.Where(s => s.UrunId == UrunId).Select(x => new ResimModel()
            {
                ResimId = x.ResimId,
                ResimAdi = x.ResimAdi,
                UrunId = x.UrunId
            }).ToList();
            return kayit;
        }
        #endregion

        #region Yorum
        [HttpGet]
        [Route("api/YorumListe")]
        public List<YorumModel> YorumListe()
        {
            List<YorumModel> liste = db.Yorum.Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                YorumMetni = x.YorumMetni,
                Puani = x.Puani,
                KullaniciId = x.KullaniciId,
                UrunId = x.UrunId
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/YorumListeByUyeId/{uyeId}")]
        public List<YorumModel> YorumListeByUyeId(int KullaniciId)
        {
            List<YorumModel> liste = db.Yorum.Where(s => s.KullaniciId == KullaniciId).Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                YorumMetni = x.YorumMetni,
                Puani = x.Puani,
                KullaniciId = x.KullaniciId,
                UrunId = x.UrunId
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/YorumListeByUrunId/{UrunId}")]
        public List<YorumModel> YorumListeByurunId(int UrunId)
        {
            List<YorumModel> liste = db.Yorum.Where(s => s.UrunId == UrunId).Select(
           x => new YorumModel()
           {
               YorumId = x.YorumId,
               YorumMetni = x.YorumMetni,
               Puani = x.Puani,
               KullaniciId = x.KullaniciId,
               UrunId = x.UrunId
           }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/YorumListeSonEklenenler/{s}")]
        public List<YorumModel> YorumListeSonEklenenler(int s)
        {
            List<YorumModel> liste = db.Yorum.OrderByDescending(o => o.UrunId).Take(s)
           .Select(x => new YorumModel()
           {
               YorumId = x.YorumId,
               YorumMetni = x.YorumMetni,
               Puani = x.Puani,
               KullaniciId = x.KullaniciId,
               UrunId = x.UrunId
           }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/YorumById/{yorumId}")]
        public YorumModel YorumById(int yorumId)
        {
            YorumModel kayit = db.Yorum.Where(s => s.YorumId == yorumId).Select(x => new
           YorumModel()
            {
                YorumId = x.YorumId,
                YorumMetni = x.YorumMetni,
                Puani = x.Puani,
                KullaniciId = x.KullaniciId,
                UrunId = x.UrunId
            }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/YorumEkle")]
        public SonucModel YorumEkle(YorumModel model)
        {
            if (db.Yorum.Count(s => s.KullaniciId == model.KullaniciId && s.UrunId == model.UrunId && s.YorumMetni == model.YorumMetni) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aynı Kişi, Aynı Ürüne Aynı Yorumu Yapamaz!";
                return sonuc;
            }
            Yorum yeni = new Yorum();
            yeni.YorumId = model.YorumId;
            yeni.YorumMetni = model.YorumMetni;
            yeni.Puani = model.Puani;
            yeni.KullaniciId = model.KullaniciId;
            yeni.UrunId = model.UrunId;

            db.Yorum.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yorum Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/YorumDuzenle")]
        public SonucModel YorumDuzenle(YorumModel model)
        {
            Yorum kayit = db.Yorum.Where(s => s.YorumId == model.YorumId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.YorumId = model.YorumId;
            kayit.YorumMetni = model.YorumMetni;
            kayit.Puani = model.Puani;
            kayit.KullaniciId = model.KullaniciId;
            kayit.UrunId = model.UrunId;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yorum Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/YorumSil/{yorumId}")]
        public SonucModel YorumSil(int yorumId)
        {
            Yorum kayit = db.Yorum.Where(s => s.YorumId == yorumId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            db.Yorum.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yorum Silindi";
            return sonuc;
        }
        #endregion
    }
}
