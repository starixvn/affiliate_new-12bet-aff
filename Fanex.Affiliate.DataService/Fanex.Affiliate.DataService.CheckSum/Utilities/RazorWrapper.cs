namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System;
    using System.IO;
    using RazorEngine;
    using RazorEngine.Configuration;
    using RazorEngine.Templating;

    public class RazorWrapper : IRazorWrapper
    {
        public RazorWrapper() : this(null)
        {
        }

        public RazorWrapper(IRazorEngineService razorService)
        {
            Engine.Razor = razorService ?? CreateDefaultRazorConfig();
        }

        public string Compile(string templateSource, string name, Type modelType = null, object model = null)
        {
            var currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(currentDirectory, templateSource);

            return Engine.Razor.RunCompile(filePath, name, modelType, model);
        }

        private IRazorEngineService CreateDefaultRazorConfig()
        {
            var config = new TemplateServiceConfiguration();
            config.DisableTempFileLocking = true;
            config.CachingProvider = new DefaultCachingProvider(t => { });

            return RazorEngineService.Create(config);
        }
    }
}