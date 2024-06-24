using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    private Animator cardAnim;

    private AudioSource clickingAudio;

    public Image Icon;

    private void Start()
    {
        clickingAudio = GetComponent<AudioSource>();
        cardAnim = GetComponent<Animator>();
    }

    public void Clicked()
    {
        cardAnim.Play("CardOpen");
        if (clickingAudio != null)
        {
            clickingAudio.Play();
        }
    }

    public void CloseCard()
    {
        if (cardAnim != null)
        {
            cardAnim.Play("CardClose");
        }
    }
}
