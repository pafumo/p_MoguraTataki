using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace View
{
    public class MoleManager : SingletonMonoBehaviour<MoleManager>
    {
        // Rayの距離
        [SerializeField] private float rayDistance = 15f;

        // モグラのPrefab
        [SerializeField] private GameObject molePrefab;

        // モグラを出現させる場所
        [SerializeField] private List<Transform> moleSpawnTranList = new List<Transform>();

        // モグラのTag名
        [SerializeField] private string moleTag = "Mole";

        /*
         * イベント
         */

        // モグラがクリックされた時のイベント
        public IObservable<Unit> OnClickMole => _onClickMole;

        private readonly Subject<Unit> _onClickMole = new Subject<Unit>();

        // モグラの生成処理

        private void Start()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ =>
                {
                    Debug.Log("左クリックが入力されました");

                    // カメラのNullチェック
                    if (Camera.main == null)
                    {
                        Debug.LogError("カメラが存在しません");
                        return;
                    }

                    var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    var hit = Physics2D.Raycast(mousePos, Vector2.zero, rayDistance);

                    // ヒットしたオブジェクトがモグラであることを確認
                    if (hit.collider != null && hit.collider.CompareTag(moleTag))
                    {
                        // モグラがクリックされていた場合の処理
                        Debug.Log("モグラがクリックされました");

                        // イベントを発行
                        _onClickMole.OnNext(Unit.Default);

                        // クリックされたモグラを削除する
                        RemoveMole(hit.collider.gameObject);
                    }
                })
                .AddTo(this);
        }

        /// <summary>
        /// モグラを生成する
        /// </summary>
        [ContextMenu("SpawnMole")] // デバッグ用
        public void SpawnMole()
        {
            // ランダムな位置にモグラを出現させる
            var randomNum = Random.Range(0, moleSpawnTranList.Count);
            var spawnTran = moleSpawnTranList[randomNum];
            Instantiate(molePrefab, spawnTran.position, Quaternion.identity);
        }

        /// <summary>
        /// モグラを削除する
        /// </summary>
        /// <param name="mole">削除対象のモグラ</param>
        private void RemoveMole(GameObject mole)
        {
            Destroy(mole);
        }
    }
}