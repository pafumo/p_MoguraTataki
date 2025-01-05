using System;
using Definitions.Enums;
using UniRx;
using UnityEngine;
using View;

namespace Presenter
{
    public class MolePresenter : MonoBehaviour
    {
        private void Start()
        {
            // ゲームが開始状態になった時の処理
            GameStatusModel.GameStatus
                .Where(gameStatus => gameStatus == GameStatus.InGame)
                .Subscribe(_ =>
                {
                    // モグラの出現を開始
                    MoleManager.Instance.StartSpawnMole();
                })
                .AddTo(this);
        }
    }
}