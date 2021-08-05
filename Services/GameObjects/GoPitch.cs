namespace SoccerPong.Services.GameObjects
{
    public class GoPitch
    {
        readonly GameService _gameService;

        public double Width { get; private set; }
        public double Height { get; private set; }

        public GoPitch(GameService gameService) =>
            _gameService = gameService;

        public void Reset(Config config)
        {
            Width = config.PitchWidth;
            Height = config.PitchHeight;
        }
    }
}