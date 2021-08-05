using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using SoccerPong.Services.GameObjects;

namespace SoccerPong.Services
{
    public class GameService : IDisposable
    {
        public int TickDelay { get; set; }
        public event Action OnGameTick;

        public bool IsRunning => _gameLoop != null;
        public bool IsPaused { get; set; }
        public bool IsGameOver { get; private set; }

        ConfigService _configService;

        public GameStats GameStats { get; set; }

        CancellationTokenSource _cts;
        Task _gameLoop;

        public GoPitch Pitch { get; private set; }
        public GoPaddle LeftPaddle { get; private set; }
        public GoPaddle RightPaddle { get; private set; }
        public GoBall Ball { get; private set; }

        public GameService(ConfigService configService)
        {
            _configService = configService;

            Pitch = new GoPitch(this);

            LeftPaddle = new GoPaddle(this, true);
            RightPaddle = new GoPaddle(this, false);

            Ball = new GoBall(this);
            Ball.OnScore += OnScore;

            GameStats = new GameStats();
        }

        public async Task InitAsync()
        {
            await _configService.LoadFromLocalStorageAsync();

            Reset();
        }

        void Reset()
        {
            TickDelay = _configService.Config.TickDelay;

            IsPaused = false;
            IsGameOver = false;

            Pitch.Reset(_configService.Config);

            LeftPaddle.Reset(_configService.Config);
            RightPaddle.Reset(_configService.Config);

            Ball.Reset(_configService.Config);

            GameStats.Reset(_configService.Config);
        }

        void OnScore(bool leftPlayerScored)
        {
            if (leftPlayerScored)
                ++GameStats.LeftPlayerScore;
            else
                ++GameStats.RightPlayerScore;

            if (GameStats.LeftPlayerScore >= _configService.Config.WinningScore ||
                GameStats.RightPlayerScore >= _configService.Config.WinningScore)
            {
                IsGameOver = true;
            }
            else
            {
                LeftPaddle.Reset(_configService.Config);
                RightPaddle.Reset(_configService.Config);

                Ball.Reset(_configService.Config);
            }
        }

        public async Task StartAsync()
        {
            await StopAsync();

            _cts = new CancellationTokenSource();
            _gameLoop = GameLoop();
        }

        public async Task StopAsync()
        {
            if (_gameLoop == null) return;

            try
            {
                _cts.Cancel();
            }
            finally
            {
                await _gameLoop;
                _gameLoop = null;

                Reset();
            }
        }

        async Task GameLoop()
        {
            while (!_cts.IsCancellationRequested && !IsGameOver)
            {
                if (!IsPaused)
                {
                    if (_configService.Config.LeftPaddleIsComputer && Ball.Speed.X < 0)
                        LeftPaddle.AutoMove();
                    if (_configService.Config.RightPaddleIsComputer && Ball.Speed.X > 0)
                        RightPaddle.AutoMove();

                    Ball.Move();

                    OnGameTick?.Invoke();
                }

                try
                {
                    await Task.Delay(TickDelay, _cts.Token);
                }
                catch (TaskCanceledException) { }
            }
        }

        public void ProcessKeyboardEvent(KeyboardEventArgs args)
        {
            // System.Console.WriteLine(args.Key + " " + args.Code);

            if (args.Code == _configService.Config.QuitKey)
            {
                _ = StopAsync();
            }

            if (args.Code == _configService.Config.PauseResumeKey)
            {
                if (!IsGameOver)
                {
                    if (IsRunning)
                        IsPaused = !IsPaused;
                    else
                        _ = StartAsync();
                }
            }

            if (IsRunning && !IsPaused && !IsGameOver)
            {
                if (!_configService.Config.LeftPaddleIsComputer)
                {
                    if (args.Code == _configService.Config.LeftPaddleMoveUpKey)
                    {
                        LeftPaddle.MoveUp();
                    }
                    else if (args.Code == _configService.Config.LeftPaddleMoveDownKey)
                    {
                        LeftPaddle.MoveDown();
                    }
                }
                if (!_configService.Config.RightPaddleIsComputer)
                {
                    if (args.Code == _configService.Config.RightPaddleMoveUpKey)
                    {
                        RightPaddle.MoveUp();
                    }
                    else if (args.Code == _configService.Config.RightPaddleMoveDownKey)
                    {
                        RightPaddle.MoveDown();
                    }
                }
            }
        }

        void IDisposable.Dispose() => _cts?.Cancel();
    }
}