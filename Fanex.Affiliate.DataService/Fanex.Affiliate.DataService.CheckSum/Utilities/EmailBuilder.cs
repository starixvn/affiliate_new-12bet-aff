namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using DTO;
    using Enum;
    using ViewModel;

    public class EmailBuilder : IEmailBuilder
    {
        private static readonly CultureInfo DefaultCulture = new CultureInfo("en-US");

        private readonly IFileSystem _fileSystem;
        private readonly IHtmlRender _razorWrapper;

        public EmailBuilder() : this(null, null)
        {
        }

        public EmailBuilder(IFileSystem fileSystem, IHtmlRender razorWrapper)
        {
            _fileSystem = fileSystem ?? new FileSystem();
            _razorWrapper = razorWrapper ?? new HtmlRender();
        }

        private static string GenerateDateParameter(int dayToAdd)
        {
            return DateTime.Now.AddDays(dayToAdd).ToLongDateString();
        }

        public string GenerateDailyMemberContent(IList<DailyMember> data, int dayToAdd)
        {
            const string FilePath = "Template\\DailyMemberEmail.cshtml";
            var templateContent = _fileSystem.ReadFile(FilePath);

            var viewModel = new DailyMemberViewModel
            {
                DateString = GenerateDateParameter(dayToAdd),
                Data = data
            };

            return _razorWrapper.Generate(templateContent, "DailyMemberEmail", viewModel);
        }

        public string GenerateIncomeAccessContent(IList<DailyMemberIncomeAccess> data, int dayToAdd)
        {
            const string FilePath = "Template\\DailyMemberIncomeAccessEmail.cshtml";
            var templateContent = _fileSystem.ReadFile(FilePath);
            var viewModel = new DailyMemberIncomeAccessViewModel
            {
                DateString = GenerateDateParameter(dayToAdd),
                Data = data
            };

            return _razorWrapper.Generate(templateContent, "DailyMemberIncomeAccessEmail", viewModel);
        }
    }
}