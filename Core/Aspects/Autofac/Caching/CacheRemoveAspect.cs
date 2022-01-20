using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    /// <summary>
    /// CacheRemoveAspect ne zaman çalışır???
    /// Cevap:
    /// Datamız bozulduğu zaman. Ne zaman bozulur?? --> 
    /// Yeni data eklenirse, data guncellenirse, data silinirse.
    /// O yüzden eüer bir Managerda Cache yönetimi yapılıyorsa, o Manager da
    /// veriyi manupule eden metodlara CacheRemoveAspect uygulanır
    /// </summary>
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        /// <summary>
        /// Eğer metot başarılı olursa (veri manupule edilebilirse) Remove Aspect çalışsın.
        /// Mesela eğer ekleme işlemim başarısız olmuşsa cache i silme
        /// </summary>
        /// <param name="invocation"></param>
        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
