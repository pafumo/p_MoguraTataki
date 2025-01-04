using Model;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using View;

namespace Presenter
{
    public class ScorePresenter: MonoBehaviour
    {
        // View
        [SerializeField] private TextMeshProUGUI scoreTextMeshProUGUI;
    
    
        private void Start()
        {
            MoleManager.Instance.OnClickMole
                .Subscribe(_ =>
                {
                    // モグラがクリックされた時の処理
                    Debug.Log("モグラがクリックされました");
                
                    // スコアを加算
                    ScoreModel.AddScore();
                })
                .AddTo(this);
        
            ScoreModel.Score
                .Subscribe(score =>
                {
                    // スコアが更新された時の処理
                    Debug.Log($"スコアが{score}に更新されました");
                
                    // スコアUIの更新
                    scoreTextMeshProUGUI.SetText("Score: " + score);
                })
                .AddTo(this);
        }
    }
}
