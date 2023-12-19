using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    // access Object
    public static T Ins
    {
        get
        {
            if (instance == null)
            {
                // search Type Object
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    // create Object with new Type
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                    // don't delete in Scene
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            // curren instance
            instance = this as T;
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // if Object default delete Object curren
            Destroy(gameObject);
        }
    }
}