namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.Text;
    using Common.Enum;

    public static class EmailContentBuilder
    {
        private const string FirstLine = "This is an auto-mail. Please do not reply. <br /><br />";

        private const string DepositColumnName = "Deposit";
        private const string WithdrawalColumnName = "Withdrawal";
        private const string BonusColumnName = "Bonus";
        private const string AdjustmentColumnName = "Adjustment";
        private const string TurnoverColumnName = "Turnover";
        private const string WinlostColumnName = "Winlost";
        private const string DescriptionColumnName = "Description";

        private const string OutstandingColumnName = "Outstanding";
        private const string DrawGameColumnName = "DrawGame";
        private const string TurnoverHRColumnName = "TurnoverHR";
        private const string OutstandingHRColumnName = "OutstandingHR";
        private const string WinlostHRColumnName = "WinlostHR";
        private const string DrawGameHRColumnName = "DrawGameHR";

        private const int Aff12BetColumnCount = 7;
        private const int IncomeAccessColumnCount = 13;

        private static readonly CultureInfo DefaultCulture = new CultureInfo("en-US");

        public static string Build(SystemType systemType, DataTable dataTable, int dayToAdd)
        {
            if (systemType == SystemType.IncomeAccess)
            {
                return BuildForIncomeAccess(dataTable, dayToAdd);
            }

            return BuildForAff12Bet(dataTable, dayToAdd);
        }

        private static string BuildForAff12Bet(DataTable dataTable, int dayToAdd)
        {
            var stringBuilder = new StringBuilder();

            var headerTemplate = BuildRowTemplate(Aff12BetColumnCount, true);
            stringBuilder.Append(string.Format(
                headerTemplate,
                DepositColumnName,
                WithdrawalColumnName,
                BonusColumnName,
                AdjustmentColumnName,
                TurnoverColumnName,
                WinlostColumnName,
                DescriptionColumnName));

            var dataRowTemplate = BuildRowTemplate(Aff12BetColumnCount);
            foreach (DataRow dr in dataTable.Rows)
            {
                stringBuilder.Append(
                    string.Format(
                        dataRowTemplate,
                        DecFormat(dr[DepositColumnName]),
                        DecFormat(dr[WithdrawalColumnName]),
                        DecFormat(dr[BonusColumnName]),
                        DecFormat(dr[AdjustmentColumnName]),
                        DecFormat(dr[TurnoverColumnName]),
                        DecFormat(dr[WinlostColumnName]),
                        dr[DescriptionColumnName]));
            }

            var tableContent = stringBuilder.ToString();
            const string CheckSumName = "MemberDaily";

            return BuildTemplate(CheckSumName, dayToAdd, tableContent);
        }

        private static string BuildForIncomeAccess(DataTable dataTable, int dayToAdd)
        {
            var stringBuilder = new StringBuilder();

            var headerTemplate = BuildRowTemplate(IncomeAccessColumnCount, true);
            stringBuilder.Append(
                string.Format(
                    headerTemplate,
                    DepositColumnName,
                    WithdrawalColumnName,
                    BonusColumnName,
                    AdjustmentColumnName,
                    TurnoverColumnName,
                    OutstandingColumnName,
                    WinlostColumnName,
                    DrawGameColumnName,
                    TurnoverHRColumnName,
                    OutstandingHRColumnName,
                    WinlostHRColumnName,
                    DrawGameHRColumnName,
                    DescriptionColumnName));

            var dataRowTemplate = BuildRowTemplate(IncomeAccessColumnCount);
            foreach (DataRow dr in dataTable.Rows)
            {
                stringBuilder.Append(
                    string.Format(
                        dataRowTemplate,
                        DecFormat(dr[DepositColumnName]),
                        DecFormat(dr[WithdrawalColumnName]),
                        DecFormat(dr[BonusColumnName]),
                        DecFormat(dr[AdjustmentColumnName]),
                        DecFormat(dr[TurnoverColumnName]),
                        DecFormat(dr[OutstandingColumnName]),
                        DecFormat(dr[WinlostColumnName]),
                        DecFormat(dr[DrawGameColumnName]),
                        DecFormat(dr[TurnoverHRColumnName]),
                        DecFormat(dr[OutstandingHRColumnName]),
                        DecFormat(dr[WinlostHRColumnName]),
                        DecFormat(dr[DrawGameHRColumnName]),
                        dr[DescriptionColumnName]));
            }

            var tableContent = stringBuilder.ToString();
            const string CheckSumName = "MemberDailyIA";

            return BuildTemplate(CheckSumName, dayToAdd, tableContent);
        }

        private static string BuildTemplate(string checksumName, int dayToAdd, string tableContent)
        {
            var longDateString = DateTime.Now.AddDays(dayToAdd).ToLongDateString();
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(FirstLine);
            stringBuilder.Append($"There is an error occur when checking sum data between <b>Bettrans and {checksumName} in {longDateString}</b>.<br /><br />");
            stringBuilder.Append("<table width=\"100%\" border=\"1px\">");
            stringBuilder.Append(tableContent);
            stringBuilder.Append("</table>");
            stringBuilder.Append("<br /><br /><b>Auto Email Checksum.</b>");

            return stringBuilder.ToString();
        }

        private static string BuildRowTemplate(int columnCount, bool isHeader = false)
        {
            var tableRow = isHeader ? "<tr bgcolor=\"#e1ebf2\">" : "<tr>";
            var builder = new StringBuilder();
            builder.Append(tableRow);

            for (int i = 0; i < columnCount; i++)
            {
                builder.Append("<td>{" + i + "}</td>");
            }

            builder.Append("</tr>");

            return builder.ToString();
        }

        private static string DecFormat(object value, byte digit = 2)
        {
            var decimalValue = Convert.ToDecimal(value);
            var format = "N" + digit.ToString();
            return decimalValue.ToString(format, DefaultCulture.NumberFormat);
        }
    }
}