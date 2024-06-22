using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    private Animator cardAnim;

    private void Start()
    {
        cardAnim = GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cardAnim != null)
        {
            cardAnim.Play("CardOpen");
        }
    }
}
