using UniRx;

namespace Model
{
    public static class ScoreModel
    {
        // ゲームのスコア
        public static IReadOnlyReactiveProperty<int> Score => _score;

        private static readonly IntReactiveProperty _score = new IntReactiveProperty(0);
    
        /// <summary>
        /// 叩いたモグラの数を加算する
        /// モグラの数 = スコア となるのでインクリメント処理にしている
        /// </summary>
        public static void AddScore()
        {
            _score.Value ++;
        }
    }
}
