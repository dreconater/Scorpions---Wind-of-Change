using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private static GameSettings instance;

    public int MusicOnOff;

    public string Level;

    public static GameSettings Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("GameSettings");
                instance = go.AddComponent<GameSettings>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
