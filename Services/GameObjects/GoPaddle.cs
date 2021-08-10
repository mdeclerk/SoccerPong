namespace SoccerPong.Services.GameObjects
{
    public class GoPaddle
    {
        readonly GameService _gameService;
        bool _isLeftPaddle;

        public Vector2 Position { get; private set; }
        public Vector2 Size { get; private set; }
        public double Speed { get; private set; }

        public GoPaddle(GameService gameService, bool isLeftPaddle) =>
            (_gameService, _isLeftPaddle) = (gameService, isLeftPaddle);

        public void Reset(Config config)
        {
            if (_isLeftPaddle)
            {
                Position = new Vector2(config.LeftPaddleBorderOffset, config.PitchHeight / 2);
                Size = new Vector2(config.LeftPaddleWidth, config.LeftPaddleHeight);
                Speed = config.LeftPaddleSpeed;
            }
            else
            {
                Position = new Vector2(config.PitchWidth - config.RightPaddleBorderOffset, config.PitchHeight / 2);
                Size = new Vector2(config.RightPaddleWidth, config.RightPaddleHeight);
                Speed = config.RightPaddleSpeed;
            }
        }

        public void MoveUp()
        {
            var newY = Position.Y - Speed;
            if (newY - Size.Y / 2 >= 0)
            {
                Position.Y = newY;
            }
        }

        public void MoveDown()
        {
            var newY = Position.Y + Speed;
            if (newY + Size.Y / 2 <= _gameService.Pitch.Height)
            {
                Position.Y = newY;
            }
        }

        public void AutoMove()
        {
            if (Position.Y + Size.Y / 2 < _gameService.Ball.Position.Y)
                MoveDown();
            else if (Position.Y - Size.Y / 2 > _gameService.Ball.Position.Y)
                MoveUp();
        }
    }
}