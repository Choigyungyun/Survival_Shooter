using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    GameObject gameObject = new GameObject(typeof(T).Name, typeof(T));
                    return instance = gameObject.GetComponent<T>();
                }
            }

            return instance;
        }
    }

    protected void DontDestroy()
    {
        if (instance != null)
        {
            return;
        }

        if (transform.parent != null && transform.root != null) //부모 오브젝트가 있거나, 최상위에 오브젝트가 있을 때
        {
            DontDestroyOnLoad(transform.root.gameObject); //최상위의 오브젝트를 파괴하지 않는다.
        }
        else //스스로가 최상위 오브젝트일 때
        {
            DontDestroyOnLoad(gameObject); //씬이 전환되도 오브젝트가 파괴되지 않는다. (해당 오브젝트가 하위에 포함되어있다면 제대로 동작하지 않는다.)
        }
    }
}
