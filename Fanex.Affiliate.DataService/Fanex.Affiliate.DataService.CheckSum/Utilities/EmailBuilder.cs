namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using DTO;
    using ViewModel;

    public class EmailBuilder : IEmailBuilder
    {
        private readonly IFileSystem _fileSystem;
        private readonly IHtmlRender _htmlRender;

        public EmailBuilder() : this(null, null)
        {
        }

        public EmailBuilder(IFileSystem fileSystem, IHtmlRender htmlRender)
        {
            _fileSystem = fileSystem ?? new FileSystem();
            _htmlRender = htmlRender ?? new HtmlRender();
        }

        public string GenerateDailyMemberContent(DailyMemberViewModel data)
        {
            const string FilePath = "Template\\DailyMemberEmail.cshtml";
            var templateContent = _fileSystem.ReadFile(FilePath);
            return _htmlRender.Generate(templateContent, "DailyMemberEmail", data);
        }

        public string GenerateDailyMemberIncomeAccessContent(DailyMemberIncomeAccessViewModel data)
        {
            const string FilePath = "Template\\DailyMemberIncomeAccessEmail.cshtml";
            var templateContent = _fileSystem.ReadFile(FilePath);

            return _htmlRender.Generate(templateContent, "DailyMemberIncomeAccessEmail", data);
        }
    }
}