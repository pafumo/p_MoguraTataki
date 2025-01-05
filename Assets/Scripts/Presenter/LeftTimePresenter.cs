using Definitions.Enums;
using Model;
using TMPro;
using UniRx;
using UnityEngine;

namespace Presenter
{
    public class LeftTimePresenter : MonoBehaviour
    {
        // View
        [SerializeField] private TextMeshProUGUI leftTimeTextMeshProUGUI;

        private void Start()
        {
            LeftTimeModel.LeftTime
                .Subscribe(leftTime =>
                {
                    // 残り時間が更新された時の処理
                    // 残り時間UIの更新
                    leftTimeTextMeshProUGUI.SetText("LeftTime: " + (int)leftTime);
                })
                .AddTo(this);
            
            GameStatusModel.GameStatus
                .Where(gameStatus => gameStatus == GameStatus.InGame)
                .Subscribe(_ =>
                {
                    // カウントダウンを開始
                    LeftTimeModel.StartCountDown();
                })
                .AddTo(this);

            GameStatusModel.GameStatus
                .Where(gameStatus => gameStatus == GameStatus.GameOver)
                .Subscribe(_ =>
                {
                    // カウントダウンを終了
                    LeftTimeModel.StopCountDown();
                })
                .AddTo(this);
        }
    }
}