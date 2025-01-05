using Definitions.Enums;
using UniRx;

namespace Model
{
    public static class GameStatusModel
    {
        // ゲームの状態
        public static IReadOnlyReactiveProperty<GameStatus> GameStatus => _gameStatus;

        private static readonly ReactiveProperty<GameStatus> _gameStatus = new(Definitions.Enums.GameStatus.PreGame);
    
        /// <summary>
        /// ゲームの状態を変更する
        /// </summary>
        public static void ChangeGameStatus(GameStatus gameStatus)
        {
            _gameStatus.Value = gameStatus;
        }
    }
}
