using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameEngine : MonoBehaviour
{
    [Header("Header")]
    public Button MenuBtn;

    [Space(10)]
    [Header("Game")]
    public Sprite[] Sprites; 

    public Card CardPrefab;

    public GridLayoutGroup Container;

    public Animator Fade;

    private AudioSource bgAudio;

    private List<Card> Cards = new List<Card>();

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
        yield return new WaitForSeconds(0.6f);
        Fade.gameObject.SetActive(false);

        Cards.Clear();
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

                break;

            case GameSettings.Level.TwoAndThree:

                cellSize = new Vector2(300, 300);
                spacing = new Vector2(80, 40);
                constraintCount = 2;
                cardCount = 6;

                break;

            case GameSettings.Level.FiveAndSix:

                cellSize = new Vector2(120, 120);
                spacing = new Vector2(20, 40);
                constraintCount = 5;
                cardCount = 30;

                break;
        }

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
        }

        AssignIcons();
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

    void GoToMenu() {
        Fade.gameObject.SetActive(true);
        Fade.Play("SlowCanvasGroupShow");
        Invoke("ChangeScene", 1f);
    }

    void ChangeScene() {
        SceneManager.LoadScene("Menu");
    }
}
