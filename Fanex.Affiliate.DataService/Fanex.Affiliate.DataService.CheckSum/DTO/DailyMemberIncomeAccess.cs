namespace Fanex.Affiliate.DataService.CheckSum.DTO
{
    public class DailyMemberIncomeAccess
    {
        public decimal Deposit { get; set; }

        public decimal Withdrawal { get; set; }

        public decimal Bonus { get; set; }

        public decimal Adjustment { get; set; }

        public decimal Turnover { get; set; }

        public decimal Outstanding { get; set; }

        public decimal Winlost { get; set; }

        public decimal DrawGame { get; set; }

        public decimal TurnoverHR { get; set; }

        public decimal OutstandingHR { get; set; }

        public decimal WinlostHR { get; set; }

        public decimal DrawGameHR { get; set; }

        public string Description { get; set; }
    }
}
