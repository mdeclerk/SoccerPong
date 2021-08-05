namespace SoccerPong.Services.GameObjects
{
    public class GameStats
    {
        public int LeftPlayerScore { get; set; }
        public string LeftPlayerName { get; set; }

        public int RightPlayerScore { get; set; }
        public string RightPlayerName { get; set; }

        public void Reset(Config config)
        {
            LeftPlayerScore = 0;
            LeftPlayerName = config.LeftPaddleIsComputer ? "Computer" : "Left Player";

            RightPlayerScore = 0;
            RightPlayerName = config.RightPaddleIsComputer ? "Computer" : "Right Player";
        }
    }
}