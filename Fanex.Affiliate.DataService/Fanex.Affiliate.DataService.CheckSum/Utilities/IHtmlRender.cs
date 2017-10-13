namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System;

    public interface IRazorWrapper
    {
        string Generate(string templateContent, string name, object viewModel);
    }
}