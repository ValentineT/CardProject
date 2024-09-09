using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentCard : MonoBehaviour
{
    [SerializeField] private Image portraitSprite, illustrationSprite_L, illustrationSprite_R, reverseSprite, locationSprite;
    [SerializeField] private TMP_Text bannerText, reverseTopText, reverseDownText, cartText_L, cardText_R;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private InputCard input_L, input_R;
    [SerializeField] private MapController mapController;
    [SerializeField] private InventoryAnimation inventoryAnimation;
    [SerializeField] private HPController hpController;
    [SerializeField] private BannerAnimation bannerAnimation;
    [SerializeField] private GameController gameController;
    [SerializeField] private Animator animL, animR;

    private CardSetScriptableObject _cardSetScriptableObject, _nextCardSetScriptableObject;
    private Sprite _giveItem, _haveItem, _location;

    private int _changeLifeCount = 0;
    private bool _changeBanner;
    private bool _removeItem;

    public CardSetScriptableObject CardSetScriptableObject
    {
        get => _cardSetScriptableObject;
        set => _cardSetScriptableObject = value;
    }

    public CardSetScriptableObject NextCardSetScriptableObject
    {
        get => _nextCardSetScriptableObject;
        set => _nextCardSetScriptableObject = value;
    }

    public bool RemoveItem { get => _removeItem; set => _removeItem = value; }

    public void SetContent(CardSetScriptableObject cardSetScriptableObject)
    {
        CardSetScriptableObject = cardSetScriptableObject;

        portraitSprite.sprite = cardSetScriptableObject?.portrait;

        input_L.AnimationClip = cardSetScriptableObject?.leftCard?.animationClip;
        illustrationSprite_L.sprite = cardSetScriptableObject?.leftCard?.illustrationSprite;

        input_R.AnimationClip = cardSetScriptableObject?.rightCard?.animationClip;
        illustrationSprite_R.sprite = cardSetScriptableObject?.rightCard?.illustrationSprite;

        bannerText.text = cardSetScriptableObject?.bannerText;
        cartText_L.text = cardSetScriptableObject?.leftCard.messageText;
        cardText_R.text = cardSetScriptableObject?.rightCard.messageText;
        mapController.ActivateCheckPoint(cardSetScriptableObject.indexCheckPoint);
    }

    public void CheckRequestItem(CardType cardType)
    {
        CardData cardData = null;
        StopAnim();

        if (cardType == CardType.Left) cardData = CardSetScriptableObject.leftCard;
        else if (cardType == CardType.Right) cardData = CardSetScriptableObject.rightCard;

        reverseSprite.enabled = true;
        _changeBanner = cardData.changeBanner; // �������� �� ����� �������

        if (cardData.locationSprite != null) _location = cardData.locationSprite; // �������� �� ����� �������
        else _location = null;

        if (cardData.requiredItemSprite) // ��������, ��������� �� �������
        {
            if (inventoryController.CheckHaveItem(cardData)) // ���� ������� � ���������
            {
                NextCardSetScriptableObject = cardData.nextSetIfItem;
                _changeLifeCount = cardData.changeLifePointsIfItem;
                _haveItem = cardData.requiredItemSprite;

                RemoveItem = cardData.removeItemSprite; // ��������, ����� ������� ������� �� ���������
            }
            else                                       // ��� ��������
            {
                NextCardSetScriptableObject = cardData.nextSetIfNoItem;
                _changeLifeCount = cardData.changeLifePointsIfNoItem;
            }

            reverseSprite.sprite = cardData.requiredItemSprite;
        }
        else
        {
            NextCardSetScriptableObject = cardData.nextSetIfItem;
            _changeLifeCount = cardData.changeLifePointsIfItem;
            reverseTopText.text = cardData.reverseTopTextIfItem;
            reverseDownText.text = cardData.reverseBottomTextIfItem;

            if (cardData.itemSprite) // ��������, ������ �� �������
            {
                reverseSprite.sprite = cardData.itemSprite;
                _giveItem = cardData.itemSprite;
            }
            else
            {
                if (cardData.reverseSpriteIfItem) reverseSprite.sprite = cardData.reverseSpriteIfItem;
                else reverseSprite.enabled = false;
                _giveItem = null;
            }
        }
    }

    public async UniTask SetItemToInventory()
    {
        if (_giveItem)
        {
            await inventoryAnimation
                .MoveItemToInventory(_giveItem, reverseSprite.transform, inventoryController.GetTarget());
            await UniTask.Yield();
        }
    }

    public void ChangeCountHP()
    {
        hpController.ChangeCountHP(_changeLifeCount);
    }

    public async UniTask ShowBanner()
    {
        if (_changeBanner) await bannerAnimation.ShowBanner();
        else await bannerAnimation.ShowMessageBanner();

        await UniTask.Yield();
    }

    public async UniTask HoldBanner()
    {
        if (_changeBanner) await bannerAnimation.HideBanner();
        else await bannerAnimation.HideMessageBanner();

        await UniTask.Yield();
    }

    public async UniTask RemoveItemFromInventory()
    {
        inventoryController.RemoveItem(_haveItem);
        await inventoryAnimation.RemoveItemFromInventory(_haveItem);
        await UniTask.Yield();
    }

    public async UniTask ChangeLocationSprite()
    {
        if (_location == null) return;
        await gameController.ChangeBG(_location);
        await UniTask.Yield();
    }

    private void StopAnim()
    {
        animL.enabled = false;
        animR.enabled = false;
    }
}



