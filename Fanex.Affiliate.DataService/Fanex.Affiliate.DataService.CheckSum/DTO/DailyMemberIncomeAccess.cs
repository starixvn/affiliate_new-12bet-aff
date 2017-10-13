namespace Fanex.Affiliate.DataService.CheckSum.DTO
{
    public class DailyMemberIncomeAccess : DailyMember
    {
        public decimal Outstanding { get; set; }

        public decimal DrawGame { get; set; }

        public decimal TurnoverHR { get; set; }

        public decimal OutstandingHR { get; set; }

        public decimal WinlostHR { get; set; }

        public decimal DrawGameHR { get; set; }
    }
}