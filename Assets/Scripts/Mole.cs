using System;
using UniRx;
using UnityEngine;

public class Mole : MonoBehaviour
{
    private void Start()
    {
        // 生成されて1秒後に自動で削除されるようにする
        Observable.Timer(TimeSpan.FromSeconds(1f))
            .Subscribe(_ =>
            {
                Destroy(gameObject);
            })
            .AddTo(this);
    }
}
