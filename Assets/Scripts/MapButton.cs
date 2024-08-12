using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private ContentCard contentCard;
    [SerializeField] private CardAnimation cardAnimation;
    [SerializeField] private BannerAnimation bannerAnimation;
    [SerializeField] private GameController gameController;

    [Space]
    [SerializeField] private CardSetScriptableObject cardSetScriptableObject;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    public async void OnPointerClick(PointerEventData eventData)
    {
        contentCard.SetContent(cardSetScriptableObject);
        cardAnimation.HideCard();
        await bannerAnimation.HideBanner();
        await gameController.HideMap();
        await bannerAnimation.ShowBanner();
        await cardAnimation.ShowCards();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _transform.DOScale(Vector3.one * 1.2f, 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _transform.DOScale(Vector3.one, 0.3f);
    }
}
