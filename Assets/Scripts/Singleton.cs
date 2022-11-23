using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    //Debug.Log("An instance of " + typeof(T) + " is added in the scene.");
                    instance = new GameObject().AddComponent<T>();
                    instance.name = instance.GetType().FullName;
                }
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}