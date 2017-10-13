namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System;

    public interface IHtmlRender
    {
        string Generate(string templateContent, string name, object viewModel);
    }
}