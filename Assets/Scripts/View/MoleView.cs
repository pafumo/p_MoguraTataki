using System;
using UnityEngine;
using UniRx;

public class MoleView : MonoBehaviour
{
    [SerializeField] private float rayDistance = 15f;
    [SerializeField] private string moleTag = "Mole";
    
    // イベント
    public IObservable<Unit> OnClickMole => _onClickMole;
    
    private readonly Subject<Unit> _onClickMole = new Subject<Unit>();
    
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
                }
            })
            .AddTo(this);
    }
}
