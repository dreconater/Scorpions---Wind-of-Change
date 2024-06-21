using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuEngine : MonoBehaviour
{
    [Header("Menu Part")]
    public Animator Buttons;
    
    public Button PlayBtn;
    public Button MusicOnOFFButton;
    public TMP_Text MusicBtnText;
    public Button QuitButton;

    public AudioSource BgAudio;

    private void Awake()
    {
        MusicOnOFFButton.onClick.AddListener(MusicOnOff);
        QuitButton.onClick.AddListener(QuitGame);
    }

    private void Start()
    {
        int bgMusicValue = PlayerPrefs.GetInt("BgMusic");

        if (bgMusicValue == 0)
        {
            BgAudio.Pause();
            MusicBtnText.text = "Music - Off";
        }
        else
        {
            BgAudio.Play();
            MusicBtnText.text = "Music - On";
        }
    }

    void MusicOnOff() { 
        if (BgAudio.isPlaying) {
            BgAudio.Pause();
            MusicBtnText.text = "Music - Off";
            PlayerPrefs.SetInt("BgMusic", 0);
        }
        else
        {
            BgAudio.Play();
            MusicBtnText.text = "Music - On";
            PlayerPrefs.SetInt("BgMusic", 1);
        }
    }

    void QuitGame() {
        // this will not work inside editor

        Application.Quit();
    }
}
