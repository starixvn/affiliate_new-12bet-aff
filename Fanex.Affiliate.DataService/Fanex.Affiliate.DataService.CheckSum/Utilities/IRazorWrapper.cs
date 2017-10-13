namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System;

    public interface IRazorWrapper
    {
        string Compile(string templateSource, string name, Type modelType = null, object model = null);
    }
}