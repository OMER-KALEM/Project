using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Urun eklendi";
        public static string ProductNameInvalid = "Urun ismi gecersiz";
        public static string MaintenanceTime = "Sistem Bakimda";
        public static string ProductListed = "urunler listelendi";
        public static string ProductCountOfCategoryError = "Bir categoride en fazla 10 urun olabilir";
        public static string ProductNameAlreadyExists = "Bu isimde zaten baska bir urun var";
        public static string CategoryLimitExceded = "Kategori limiti asildigi icin yeni urun eklenemiyor";
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered = "Kayit oldu";
        public static string UserNotFound = "Kullanci bulunaadi";
        public static string PasswordError = "Paralo Hatasi";
        public static string SuccessfulLogin = "basarili giris";
        public static string UserAlreadyExists = "Kullanci mevcut";
        public static string AccessTokenCreated = "Token Olusturuldu";
    }
}
