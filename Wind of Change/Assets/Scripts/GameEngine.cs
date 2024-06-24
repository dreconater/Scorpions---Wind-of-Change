using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class GameEngine : MonoBehaviour
{
    [Header("Header")]
    public Button MenuBtn;

    [Space(10)]
    [Header("Game")]
    public Sprite[] Sprites;

    public Card CardPrefab;
    private Card SelectedCard = null;

    public GridLayoutGroup Container;

    public Animator Fade;

    private AudioSource bgAudio;
    public AudioSource GameAudio;

    public AudioClip WrongAudio;
    public AudioClip RightAudio;

    private List<Card> Cards = new List<Card>();

    [Space(10)]
    [Header("Timer and Score")]
    public TMP_Text TimerText;

    public float TotalTime;
    private float timeRemaining;

    private bool timerRunning = false;

    public TMP_Text ScoreText;

    private int score = 0;
    private int maximumScore = 0;

    [Space(10)]
    [Header("Wondows")]
    public GameObject WinWindow;
    public GameObject GameOverWindow;

    public Button RestartBtn;
    public Button GoToMenuBtn;


    private void Awake()
    {
        MenuBtn.onClick.AddListener(GoToMenu);
        GoToMenuBtn.onClick.AddListener(GoToMenu);
        RestartBtn.onClick.AddListener(() => { SceneManager.LoadScene("Game"); });
    }

    private IEnumerator Start()
    {
        Setup(GameSettings.Instance.SelectedLevel);

        bgAudio = GetComponent<AudioSource>();

        if (GameSettings.Instance.MusicOnOff == 1)
        {
            bgAudio.Play();
        }

        Fade.gameObject.SetActive(true);
        Fade.Play("SlowCanvasGroupHide");
        yield return new WaitForSeconds(0.6f);
        Fade.gameObject.SetActive(false);

        UpdateScoreText();
    }

    void Setup(GameSettings.Level level) {
        Vector2 cellSize = Vector2.zero;
        Vector2 spacing = Vector2.zero;

        int constraintCount = 0;
        int cardCount = 0;

        switch (level)
        {
            case GameSettings.Level.TwoAndTwo:

                cellSize = new Vector2(300, 300);
                spacing = new Vector2(80, 40);
                constraintCount = 2;
                cardCount = 4;
                TotalTime = 20;
                maximumScore = 20;

                break;

            case GameSettings.Level.TwoAndThree:

                cellSize = new Vector2(300, 300);
                spacing = new Vector2(80, 40);
                constraintCount = 2;
                cardCount = 6;
                TotalTime = 50;
                maximumScore = 30;

                break;

            case GameSettings.Level.FiveAndSix:

                cellSize = new Vector2(120, 120);
                spacing = new Vector2(20, 40);
                constraintCount = 5;
                cardCount = 30;
                TotalTime = 120;
                maximumScore = 150;

                break;
        }

        timeRemaining = TotalTime;
        timerRunning = true;
        UpdateTimerText();

        SetupContainer(cellSize, spacing, constraintCount);
        CreateCards(cardCount);
    }

    private void SetupContainer(Vector2 cellSize, Vector2 spacing, int constraintCount)
    {
        Container.cellSize = cellSize;
        Container.spacing = spacing;
        Container.constraintCount = constraintCount;
    }

    private void CreateCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newCard = Instantiate(CardPrefab, Container.transform);
            Cards.Add(newCard);
            Button buttonComponent = newCard.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => StartCoroutine(CardClick(newCard)));
        }

        AssignIcons();
    }

    IEnumerator CardClick(Card clickedCard)
    {
        foreach (Transform buttons in Container.transform)
        {
            buttons.GetComponent<Button>().enabled = false;
        }

        if (SelectedCard == null)
        {
            SelectedCard = clickedCard;

            foreach (Transform buttons in Container.transform)
            {
                buttons.GetComponent<Button>().enabled = true;
            }
        }
        else
        {
            if (SelectedCard.name == clickedCard.name)
            {
                yield return new WaitForSeconds(0.7f);

                GameAudio.clip = RightAudio;
                GameAudio.Play();
                SelectedCard = null;
                AddScore(10);

                foreach (Transform buttons in Container.transform)
                {
                    buttons.GetComponent<Button>().enabled = true;
                }
            }
            else
            {
                yield return new WaitForSeconds(0.7f);

                SelectedCard.CloseCard();
                clickedCard.CloseCard();

                GameAudio.clip = WrongAudio;
                GameAudio.Play();

                SelectedCard = null;

                foreach (Transform buttons in Container.transform)
                {
                    buttons.GetComponent<Button>().enabled = true;
                }
            }
        }
    }

    void AssignIcons()
    {
        const int TwoCardCount = 4;
        const int ThreeCardCount = 6;
        const int ThirtyCardCount = 30;

        if (Cards.Count == TwoCardCount || Cards.Count == ThreeCardCount || Cards.Count == ThirtyCardCount)
        {
            Shuffle(Sprites);

            List<Sprite> selectedSprites = GetUniqueRandomSprites(Cards.Count == TwoCardCount ? 2 : (Cards.Count == ThreeCardCount ? 3 : 15));

            for (int i = 0; i < Cards.Count; i++)
            {
                Cards[i].Icon.sprite = selectedSprites[i % selectedSprites.Count];
                Cards[i].name = selectedSprites[i % selectedSprites.Count].name;
            }
        }
    }

    List<Sprite> GetUniqueRandomSprites(int count)
    {
        List<Sprite> selectedSprites = new List<Sprite>();
        List<Sprite> spritePool = new List<Sprite>(Sprites);

        while (selectedSprites.Count < count)
        {
            int randomIndex = Random.Range(0, spritePool.Count);
            Sprite sprite = spritePool[randomIndex];
            selectedSprites.Add(sprite);
            spritePool.RemoveAt(randomIndex);
        }

        return selectedSprites;
    }

    void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0f)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                timeRemaining = 0f;
                UpdateTimerText();
                GameOver();
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        TimerText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        ScoreText.text = "Score: " + score.ToString("000");

        if (score >= maximumScore)
        {
            StopTimer();
            WinWindow.SetActive(true);
        }
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    void GameOver()
    {
        StopTimer();
        GameOverWindow.SetActive(true);
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
