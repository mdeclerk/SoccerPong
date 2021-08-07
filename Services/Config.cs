namespace SoccerPong.Services
{
    public class Config
    {
        #region General

        public int WinningScore { get; set; } = 11;
        public int TickDelay { get; set; } = 20;

        #endregion

        #region Game Objects

        public int PitchWidth { get; set; } = 800;
        public int PitchHeight { get; set; } = 600;

        public int LeftPaddleWidth { get; set; } = 30;
        public int LeftPaddleBorderOffset { get; set; } = 30;
        public int LeftPaddleHeight { get; set; } = 160;
        public int LeftPaddleSpeed { get; set; } = 50;

        public int RightPaddleWidth { get; set; } = 30;
        public int RightPaddleBorderOffset { get; set; } = 30;
        public int RightPaddleHeight { get; set; } = 160;
        public int RightPaddleSpeed { get; set; } = 50;

        public double BallSize { get; set; } = 30;
        public double BallInitialSpeed { get; set; } = 0.5;
        public double BallSpeedIncreasePercentage { get; set; } = 0.02;
        public double MaxBallSpeed { get; set; } = 2.0;
        public double BallRotationSpeed { get; set; } = 0.4;

        #endregion

        #region Controls

        public string PauseResumeKey { get; set; } = "Space";
        public string QuitKey { get; set; } = "Escape";

        public bool LeftPaddleIsComputer { get; set; } = false;
        public string LeftPaddleMoveUpKey { get; set; } = "ArrowUp";
        public string LeftPaddleMoveDownKey { get; set; } = "ArrowDown";

        public bool RightPaddleIsComputer { get; set; } = true;
        public string RightPaddleMoveUpKey { get; set; } = "KeyQ";
        public string RightPaddleMoveDownKey { get; set; } = "KeyA";

        #endregion
    }
}