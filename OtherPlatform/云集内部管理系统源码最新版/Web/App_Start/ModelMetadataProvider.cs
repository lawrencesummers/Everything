using System.Web.Mvc;
using LanguagePack;
using ModelMetadataExtensions;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Web.App_Start.ModelMetadataProvider), "Start")]
namespace Web.App_Start
{
    public static class ModelMetadataProvider
    {
        public static void Start()
        {
            ModelMetadataProviders.Current = new ConventionalModelMetadataProvider(false, typeof(lang));
        }
    }
}