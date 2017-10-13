namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System;
    using System.IO;
    using RazorEngine;
    using RazorEngine.Configuration;
    using RazorEngine.Templating;

    public class HtmlRender : IHtmlRender
    {
        public string Generate(string templateContent, string name, object viewModel)
        {
            return Engine.Razor.RunCompile(templateContent, name, viewModel.GetType(), viewModel);
        }
    }
}