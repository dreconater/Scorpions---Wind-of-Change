using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    private Animator cardAnim;

    public Image Icon;

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
