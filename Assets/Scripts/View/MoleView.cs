using UnityEngine;
using UniRx;

public class MoleView : MonoBehaviour
{
    [SerializeField] private float rayDistance = 15f;
    [SerializeField] private string moleTag = "Mole";
    
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
                    
                    // TODO --> !!! ここでモグラを叩いたことを通知する処理を追加 !!!
                }
            })
            .AddTo(this);
    }
}
