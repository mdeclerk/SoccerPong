using System;

namespace SoccerPong.Services.GameObjects
{
    public class GoBall
    {
        readonly GameService _gameService;

        public event Action<bool> OnScore;

        public Vector2 Position { get; private set; }
        public double Rotation { get; private set; }
        public double Radius { get; private set; }
        public Vector2 Speed { get; private set; }
        public double SpeedIncreasePercentage { get; private set; }
        public double MaxSpeed { get; private set; }
        public double RotationSpeed { get; private set; }

        public GoBall(GameService gameService) => _gameService = gameService;

        public void Reset(Config config)
        {
            Position = new Vector2(config.PitchWidth / 2, config.PitchHeight / 2);
            Radius = config.BallSize;

            Speed = Vector2.RandomQuad() * config.BallInitialSpeed;
            SpeedIncreasePercentage = config.BallSpeedIncreasePercentage;
            MaxSpeed = config.MaxBallSpeed;

            RotationSpeed = config.BallRotationSpeed;
        }

        public void Move()
        {
            Position += Speed * _gameService.TickDelay;
            Rotation = (Rotation + RotationSpeed * _gameService.TickDelay) % 360;

            if (CollisionWithXBounds())
            {
                OnScore?.Invoke(Speed.X > 0);
                Speed.X *= -1;
            }

            if (CollisionWithYBounds())
            {
                Speed.Y *= -1;
            }

            if ((Speed.X < 0 && CollisionWithPaddle(_gameService.LeftPaddle)) ||
                (Speed.X > 0 && CollisionWithPaddle(_gameService.RightPaddle)))
            {
                Speed.X *= -1;
                IncreaseSpeed();
            }
        }

        private void IncreaseSpeed()
        {
            var oldScale = Speed.Length();
            var newScale = oldScale * (1 + SpeedIncreasePercentage);
            if (newScale > MaxSpeed) newScale = MaxSpeed;
            Speed *= newScale / oldScale;
        }

        private bool CollisionWithXBounds() => Position.X - Radius < 0 || Position.X + Radius > _gameService.Pitch.Width;

        private bool CollisionWithYBounds() => Position.Y - Radius < 0 || Position.Y + Radius > _gameService.Pitch.Height;

        private bool CollisionWithPaddle(GoPaddle paddle)
        {
            var closestX = Math.Clamp(Position.X, paddle.Position.X - paddle.Size.X / 2, paddle.Position.X + paddle.Size.X / 2);
            var closestY = Math.Clamp(Position.Y, paddle.Position.Y - paddle.Size.Y / 2, paddle.Position.Y + paddle.Size.Y / 2);

            var distanceSq = (new Vector2(closestX, closestY) - Position).LengthSq();
            return distanceSq < Radius * Radius;
        }
    }
}