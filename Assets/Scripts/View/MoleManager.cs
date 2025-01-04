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
        
        // モグラを生成する時間リスト
        private List<float> moleSpawnTimeList = new List<float>();

        /*
         * イベント
         */

        // モグラがクリックされた時のイベント
        public IObservable<Unit> OnClickMole => _onClickMole;

        private readonly Subject<Unit> _onClickMole = new Subject<Unit>();

        // モグラの生成処理

        private void Start()
        {
            // プレイヤーの入力をチェック
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
        /// モグラの生成を開始
        /// </summary>
        [ContextMenu("StartSpawnMole")] // デバッグ用
        public void StartSpawnMole()
        {
            // モグラの生成時間リストを生成
            CreateRandomTimeList();
            
            foreach (var spawnTime in moleSpawnTimeList)
            {
                Observable.Timer(TimeSpan.FromSeconds(spawnTime))
                    .Subscribe(_ => SpawnMole())
                    .AddTo(this);
            }
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
        /// モグラの生成時間リストを生成する
        /// </summary>
        private void CreateRandomTimeList()
        {
            // TODO --> !!! 後に別途定義すること !!!
            var gameTime = 30f;
            var minInterval = 1f;
            var maxInterval = 3f;
            // TODO --> !!! ここまで !!!
            
            var spawnTime = 0f;
            
            while (true)
            {
                // ランダムな間隔を生成
                var interval = (float)Math.Round(
                    new System.Random().NextDouble() * (maxInterval - minInterval) + minInterval,
                    1
                );
                
                // 生成時間を計算
                spawnTime += interval;
                
                // 生成時間が1プレイの時間を超える場合は終了
                if (spawnTime > gameTime) break;
                
                // モグラの生成時間として追加
                moleSpawnTimeList.Add(spawnTime);
            }
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