using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Urun eklendi";
        public static string ProductNameInvalid = "Urun ismi gecersiz";
        internal static string MaintenanceTime = "Sistem Bakimda";
        internal static string ProductListed = "urunler listelendi";
        public static string ProductCountOfCategoryError = "Bir categoride en fazla 10 urun olabilir";
        public static string ProductNameAlreadyExists = "Bu isimde zaten baska bir urun var";
        internal static string CategoryLimitExceded = "Kategori limiti asildigi icin yeni urun eklenemiyor";
    }
}
