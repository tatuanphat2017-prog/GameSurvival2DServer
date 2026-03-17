namespace GameSurvival2DServer.MODELL
{
    public class Leaderboard
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Score { get; set; }
        public int SurvivalTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
