using System;
using UniRx;
using UnityEngine;

namespace Model
{
    public static class LeftTimeModel
    {
        // ゲームの残り時間
        public static IReadOnlyReactiveProperty<float> LeftTime => _leftTime;

        private static readonly FloatReactiveProperty _leftTime = new FloatReactiveProperty(0);
        
        // 毎フレーム残り時間を更新するイベント
        private static IDisposable _updateLeftTime;
        
        /// <summary>
        /// カウントダウンを開始する
        /// </summary>
        public static void StartCountDown()
        {
            // TODO --> !!! 定数として別途定義すべき !!!
            // 時間の初期化
            _leftTime.Value = 30f;
            
            // 毎フレーム残り時間を更新する
            _updateLeftTime = Observable.EveryUpdate()
                .Subscribe(_ => { UpdateLeftTime(); });

            // TODO --> !!! 将来的に購読解除を実装する必要が出てくる !!!
        }

        /// <summary>
        /// 残り時間を更新する
        /// </summary>
        private static void UpdateLeftTime()
        {
            _leftTime.Value -= Time.deltaTime;
        }

        /// <summary>
        /// カウントダウン処理を終了する
        /// </summary>
        public static void StopCountDown()
        {
            _updateLeftTime.Dispose();
        }
    }
}