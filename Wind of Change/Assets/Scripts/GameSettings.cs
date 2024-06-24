using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private static GameSettings instance;

    public enum Level
    {
        TwoAndTwo,
        TwoAndThree,
        FiveAndSix
    }

    public int MusicOnOff;

    public Level SelectedLevel;

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
