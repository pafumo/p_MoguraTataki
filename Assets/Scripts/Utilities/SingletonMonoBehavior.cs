using System;
using UnityEngine;

namespace Utilities
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour{

        private static T instance;
        public static T Instance
        {
            get{
                if (instance == null) {
                    Type t = typeof(T);

                    instance = (T)FindObjectOfType (t);
                    if (instance == null) {
                        Debug.LogWarning(t + " をアタッチしているGameObjectはありません");
                    }
                }

                return instance;
            }
        }
        
        /// <summary>
        /// シングルトンオブジェクトとして設定する
        /// </summary>
        /// <returns>作成されたかどうか</returns>
        protected bool SetSingleton(){
            if (instance == null) {
                instance = this as T;
                return true;
            }

            if (Instance == this) {
                return true;
            }
            
            // 削除
            Destroy (gameObject);
            return false;
        }

        /// <summary>
        /// シングルトンオブジェクトとして設定 + DontDestroyOnLoadにする
        /// </summary>
        /// <returns>作成されたかどうか</returns>
        protected bool CreateDontDestroyOnLoad(){
            if (instance == null) {
                Debug.Log($"DontDestroyOnLoad: {gameObject.name}");
                DontDestroyOnLoad(gameObject);
                instance = this as T;
                return true;
            }

            if (Instance == this) {
                return true;
            }
            
            // 削除
            Destroy (gameObject);
            return false;
        }
    }
}