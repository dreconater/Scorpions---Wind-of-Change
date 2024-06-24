using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEngine : MonoBehaviour
{
    public Card CardPrefab;

    public GridLayoutGroup Container;

    public GameObject Fade;

    private AudioSource bgAudio;

    private IEnumerator Start()
    {
        Setup(GameSettings.Instance.SelectedLevel);

        bgAudio = GetComponent<AudioSource>();

        Fade.SetActive(true);
        yield return new WaitForSeconds(2);
        Fade.SetActive(false);
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
}
