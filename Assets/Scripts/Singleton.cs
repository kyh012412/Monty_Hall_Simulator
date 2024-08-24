using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance{
        get{
            if(instance==null){
                instance = (T)FindAnyObjectByType(typeof(T));

                if(instance ==null){
                    GameObject obj = new GameObject(typeof(T).Name,typeof(T));
                    instance = obj.GetComponent<T>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(transform.parent != null && transform.root != null){
            DontDestroyOnLoad(this.transform.root.gameObject);
            return;
        }
        // DontDestroyOnLoad 씬 바로 밑에 있지 않고  
        // 어느 객체의 하위에 있을경우 제대로 동작하지 않을 수도 있음
        DontDestroyOnLoad(this.gameObject);
    }
}
