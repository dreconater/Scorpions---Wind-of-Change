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
        Setup();

        bgAudio = GetComponent<AudioSource>();

        Fade.SetActive(true);
        yield return new WaitForSeconds(2);
        Fade.SetActive(false);
    }

    void Setup() { 
        
    }
}
