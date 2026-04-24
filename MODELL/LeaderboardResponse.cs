namespace GameSurvival2DServer.MODELL
{
    public class LeaderboardResponse
    {
        public int Rank { get; set; }
        public string DisplayName { get; set; }
        public int Score { get; set; }
        public int SurvivalTime { get; set; }
    }
}