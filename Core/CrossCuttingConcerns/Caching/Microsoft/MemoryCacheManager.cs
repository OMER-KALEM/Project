using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    /// <summary>
    /// Microsoft elemanı zaten benim burda yazdığım metodları yazmış ben neden bir daha yazıyorum ki?
    /// Cevap:
    /// Benim derdim sadece Microsoft 'un Memory Cache 'ine eklemek değil. Eğer onu eklersem yarın öbürgün
    /// başka bir cache yönetiminde patlarım. Çümkü ben her yere gidip hard coded Microsoft 'un Memory Cache
    /// kodlarını yazarsam yarın öbürgün farklı bir cache sistemine geçmek istediğim de tüm kodları bulup
    /// tek tek değişitirmem gerekir. Oysa ben bu yazdığım sistem ile .Net Core dan gelen kodu kendime
    /// uyarlıyorum (Adapter Pattern) tek bir class değişikliği ile yeni bir cache yönetimine geçebilirim.
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {


        /// <summary>
        /// Burada "IMemoryCache _memoryCache;" için Constructor injection yapamıyoruz çünkü;
        /// Sıra şu şekilde --> WebApi --> Business --> DataAccess
        /// Aspect bambaşka bir zincirin içinde dolayısıyla bağımlılık zincirinin içinde değil
        /// Biz de bunun için servis Tool u yazdık
        /// Servis toolumuzu da injekte etmiştik. Bir tane ICoreModule (CoreModule) diye.
        /// Orada serviceCollection.AddMemoryCache(); dediğimizde artık bir değeri var.
        /// </summary>
        IMemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            //TryGetValue ikinci parametre olarak döndüreceği value yu senden bekliyor.
            //Ben ise o value ile ilgilenmediğim için bu şekilde boş geçiyorum.
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        /// <summary>
        /// Çalışma anında bellekten silme yarıyor.
        /// Elimde bir sınıfın isntance ı var bellekte ve ona çalışma anında müdahale etmek istiyorum.
        /// Bunu reflection ile yapıyorum
        /// </summary>
        /// <param name="pattern"></param>
        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
