namespace GameSurvival2DServer.MODELL
{
    public class SaveGame
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Slot { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public int PlayerHP { get; set; }
        public int PlayerStamina { get; set; }
        public DateTime SavedAt { get; set; }
    }
}
