namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using DTO;
    using Enum;

    public class EmailContentBuilder : IEmailContentBuilder
    {
        private static readonly CultureInfo DefaultCulture = new CultureInfo("en-US");

        private readonly IFileSystem _fileSystem;
        private readonly IRazorWrapper _razorWrapper;

        public EmailContentBuilder() : this(null, null)
        {
        }

        public EmailContentBuilder(IFileSystem fileSystem, IRazorWrapper razorWrapper)
        {
            _fileSystem = fileSystem ?? new FileSystem();
            _razorWrapper = razorWrapper ?? new RazorWrapper();
        }

        public string Build(SystemType systemType, object dailyMemberList, int dayToAdd)
        {
            var dateString = DateTime.Now.AddDays(dayToAdd).ToLongDateString();

            if (systemType == SystemType.IncomeAccess)
            {
                return BuildForIncomeAccess((IEnumerable<DailyMemberIncomeAccess>)dailyMemberList, dateString);
            }

            return BuildForAff12Bet((IEnumerable<DailyMember>)dailyMemberList, dateString);
        }

        private static string DecFormat(decimal value, byte digit = 2)
        {
            var format = "N" + digit.ToString();
            return value.ToString(format, DefaultCulture.NumberFormat);
        }

        private string BuildForAff12Bet(IEnumerable<DailyMember> dailyMemberList, string dateString)
        {
            const string FilePath = "Template\\DailyMemberEmail.cshtml";
            var templateContent = _fileSystem.ReadFile(FilePath);

            var contentData = dailyMemberList.Select(item => new
            {
                Deposit = DecFormat(item.Deposit),
                Withdrawal = DecFormat(item.Withdrawal),
                Bonus = DecFormat(item.Bonus),
                Adjustment = DecFormat(item.Adjustment),
                Turnover = DecFormat(item.Turnover),
                Winlost = DecFormat(item.Winlost),
                Description = item.Description
            });

            return _razorWrapper.Compile(templateContent, "DailyMemberEmail", null, new { DateString = dateString, Data = contentData });
        }

        private string BuildForIncomeAccess(IEnumerable<DailyMemberIncomeAccess> dailyMemberIncomeAccessList, string dateString)
        {
            const string FilePath = "Template\\DailyMemberIncomeAccessEmail.cshtml";
            var templateContent = _fileSystem.ReadFile(FilePath);

            var contentData = dailyMemberIncomeAccessList.Select(item => new
            {
                Deposit = DecFormat(item.Deposit),
                Withdrawal = DecFormat(item.Withdrawal),
                Bonus = DecFormat(item.Bonus),
                Adjustment = DecFormat(item.Adjustment),
                Turnover = DecFormat(item.Turnover),
                Outstanding = DecFormat(item.Outstanding),
                Winlost = DecFormat(item.Winlost),
                DrawGame = DecFormat(item.DrawGame),
                TurnoverHR = DecFormat(item.TurnoverHR),
                OutstandingHR = DecFormat(item.OutstandingHR),
                WinlostHR = DecFormat(item.WinlostHR),
                DrawGameHR = DecFormat(item.DrawGameHR),
                Description = item.Description
            });

            return _razorWrapper.Compile(templateContent, "DailyMemberIncomeAccessEmail", null, new { DateString = dateString, Data = contentData });
        }
    }
}