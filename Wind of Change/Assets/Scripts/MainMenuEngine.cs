using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuEngine : MonoBehaviour
{
    [Header("Menu Part")]
    public Animator Buttons;
    
    public Button PlayBtn;
    public Button MusicOnOFFButton;
    public TMP_Text MusicBtnText;
    public Button QuitButton;

    public AudioSource BgAudio;

    [Space(10)]
    [Header("Menu Part")]
    public Animator Levels;

    public Button BackToMenuBtn;

    [Space(10)]
    [Header("Other")]
    public Animator Fade;

    private void Awake()
    {
        PlayBtn.onClick.AddListener(()=> { StartCoroutine( ShowLevels()); });
        MusicOnOFFButton.onClick.AddListener(MusicOnOff);
        QuitButton.onClick.AddListener(QuitGame);

        BackToMenuBtn.onClick.AddListener(() => { StartCoroutine(BackToMenu()); });
    }

    private IEnumerator Start()
    {
        Fade.gameObject.SetActive(true);
        Fade.Play("SlowCanvasGroupHide");
        yield return new WaitForSeconds(1);
        Fade.gameObject.SetActive(false);

        Buttons.gameObject.SetActive(true);
        Levels.gameObject.SetActive(false);

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

    IEnumerator ShowLevels() {
        Buttons.Play("CanvasGroupHide");
        yield return new WaitForSeconds(0.35f);
        Buttons.gameObject.SetActive(false);
        Levels.gameObject.SetActive(true);
        Levels.Play("CanvasGroupShow");
    }

    IEnumerator BackToMenu() {
        Levels.Play("CanvasGroupHide");
        yield return new WaitForSeconds(0.35f);
        Levels.gameObject.SetActive(false);
        Buttons.gameObject.SetActive(true);
        Buttons.Play("CanvasGroupShow");
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

    public void ClickedLevel(int level) {
        GameSettings.Instance.SelectedLevel = (GameSettings.Level)level;
        GameSettings.Instance.MusicOnOff = PlayerPrefs.GetInt("BgMusic");

        Fade.gameObject.SetActive(true);
        Fade.Play("SlowCanvasGroupShow");

        Invoke("OpenGameScene", 1);
    }

    void OpenGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    void QuitGame() {
        Debug.Log("This will not work in the Editor.");
        Application.Quit();
    }
}
