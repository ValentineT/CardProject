using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private CardAnimation cardAnimation;
    [SerializeField] private ContentCard contentCard;
    [SerializeField] private CardType cardType;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //ttt
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //ttt
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickCard();
    }

    private async void ClickCard()
    {
        if (cardType != CardType.Reverse)
        {
            contentCard.CheckRequestItem(cardType);
            cardAnimation.FlipCard(cardType);
        }
        else
        {
            contentCard.ChangeCountHP();

            if (contentCard.RemoveItem) await contentCard.RemoveItemFromInventory();
            
            await  contentCard.SetItemToInventory();
            await cardAnimation.HoldReverseCard();
            await contentCard.HoldBanner();
            await contentCard.ChangeLocationSprite();
            contentCard.SetContent(contentCard.NextCardSetScriptableObject);
            await UniTask.Delay(600);
            await contentCard.ShowBanner();
            await cardAnimation.ShowCards();
        }
    }
}
