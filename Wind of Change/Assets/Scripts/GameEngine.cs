using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{
    [Header("Header")]
    public Button MenuBtn;

    [Space(10)]
    [Header("Game")]
    public Card CardPrefab;

    public GridLayoutGroup Container;

    public Animator Fade;

    private AudioSource bgAudio;

    private void Awake()
    {
        MenuBtn.onClick.AddListener(GoToMenu);
    }

    private IEnumerator Start()
    {
        Setup(GameSettings.Instance.SelectedLevel);

        bgAudio = GetComponent<AudioSource>();

        Fade.gameObject.SetActive(true);
        Fade.Play("SlowCanvasGroupHide");
        yield return new WaitForSeconds(1);
        Fade.gameObject.SetActive(false);
    }

    void Setup(GameSettings.Level level) {
        switch (level)
        {
            case GameSettings.Level.TwoAndTwo:

                Container.cellSize = new Vector2(300, 300);
                Container.spacing = new Vector2(80, 40);
                Container.constraintCount = 2;

                for (int i = 0; i < 4; i++)
                {
                    var newCard = Instantiate(CardPrefab, Container.transform);
                }
                break;

            case GameSettings.Level.TwoAndThree:

                Container.cellSize = new Vector2(300, 300);
                Container.spacing = new Vector2(80, 40);
                Container.constraintCount = 2;

                for (int i = 0; i < 6; i++)
                {
                    var newCard = Instantiate(CardPrefab, Container.transform);
                }

                break;

            case GameSettings.Level.FiveAndSix:

                Container.cellSize = new Vector2(120, 120);
                Container.spacing = new Vector2(20, 40);
                Container.constraintCount = 5;

                for (int i = 0; i < 30; i++)
                {
                    var newCard = Instantiate(CardPrefab, Container.transform);
                }
                break;
        }
    }

    void GoToMenu() {
        Fade.gameObject.SetActive(true);
        Fade.Play("SlowCanvasGroupShow");
        Invoke("ChangeScene", 1f);
    }

    void ChangeScene() {
        SceneManager.LoadScene("Menu");
    }
}
