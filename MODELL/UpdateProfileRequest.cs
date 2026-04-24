namespace GameSurvival2DServer.MODELL
{
    public class UpdateProfileRequest
    {
        public int AccountId { get; set; }
        public string DisplayName { get; set; }
        public int Level { get; set; }
        public int TotalPlayTime { get; set; }
    }
}
