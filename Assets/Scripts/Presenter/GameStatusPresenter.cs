using System;
using Definitions.Enums;
using Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter
{
    public class GameStatusPresenter : MonoBehaviour
    {
        // View
        [SerializeField] private Button startButton;

        private void Start()
        {
            // スタートボタンが押された時の処理
            startButton.onClick.AddListener(StartGame);

            // 残り時間が0になった時の処理
            LeftTimeModel.LeftTime
                .Where(time => time <= 0 && GameStatusModel.GameStatus.Value == GameStatus.InGame)
                .Subscribe(_ => GameOver())
                .AddTo(this);
        }

        private void StartGame()
        {
            Debug.Log("ゲームスタート");

            // ゲームの状態をゲーム中に変更する
            GameStatusModel.ChangeGameStatus(GameStatus.InGame);
        }

        private void GameOver()
        {
            Debug.Log("ゲームオーバー");

            // ゲームの状態をゲームオーバーに変更する
            GameStatusModel.ChangeGameStatus(GameStatus.GameOver);
        }
    }
}