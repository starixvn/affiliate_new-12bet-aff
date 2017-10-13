namespace Fanex.Affiliate.DataService.CheckSum.DTO
{
    public class DailyMember
    {
        public decimal Deposit { get; set; }

        public decimal Withdrawal { get; set; }

        public decimal Bonus { get; set; }

        public decimal Adjustment { get; set; }

        public decimal Turnover { get; set; }

        public decimal Winlost { get; set; }

        public string Description { get; set; }
    }
}
