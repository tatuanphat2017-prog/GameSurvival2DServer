namespace GameSurvival2DServer.MODELL
{
    public class AdminLog
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public int TargetAccountId { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
