using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentCard : MonoBehaviour
{
    [SerializeField] private Image portraitSprite, illustrationSprite_L, illustrationSprite_R, reverseSprite, locationSprite;
    [SerializeField] private TMP_Text bannerText, reverseTopText, reverseDownText, cartText_L, cardText_R;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private MapController mapController;
    [SerializeField] private InventoryAnimation inventoryAnimation;
    [SerializeField] private HPController hpController;
    [SerializeField] private BannerAnimation bannerAnimation;
    [SerializeField] private GameController gameController;

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
        illustrationSprite_L.sprite = cardSetScriptableObject?.leftCard.illustrationSprite;
        illustrationSprite_R.sprite = cardSetScriptableObject?.rightCard.illustrationSprite;

        bannerText.text = cardSetScriptableObject?.bannerText;
        cartText_L.text = cardSetScriptableObject?.leftCard.messageText;
        cardText_R.text = cardSetScriptableObject?.rightCard.messageText;
        mapController.ActivateCheckPoint(cardSetScriptableObject.indexCheckPoint);
    }

    // Проверка, требуется ли предмет
    public void CheckRequestItem(CardType cardType)
    {
        CardData cardData = null;

        if (cardType == CardType.Left) cardData = CardSetScriptableObject.leftCard;
        else if (cardType == CardType.Right) cardData = CardSetScriptableObject.rightCard;

        reverseSprite.enabled = true;
        _changeBanner = cardData.changeBanner;

        if (cardData.locationSprite != null) _location = cardData.locationSprite;
        else _location = null;

        if (cardData.requiredItemSprite) // Проверка, требуется ли предмет
        {
            if (inventoryController.CheckHaveItem(cardData)) // Есть предмет в инвентаре
            {
                NextCardSetScriptableObject = cardData.nextSetIfItem;
                _changeLifeCount = cardData.changeLifePointsIfItem;
                _haveItem = cardData.requiredItemSprite;
                // Проверка, нужно удалить предмет из инветоаря
                RemoveItem = cardData.removeItemSprite;

                Debug.Log("Есть предмет");
            }
            else                                             // Нет предмет в инвентаре
            {
                NextCardSetScriptableObject = cardData.nextSetIfNoItem;
                _changeLifeCount = cardData.changeLifePointsIfNoItem;
                Debug.Log("Нет предмета");
            }

            reverseSprite.sprite = cardData.requiredItemSprite;
        }
        else
        {
            NextCardSetScriptableObject = cardData.nextSetIfItem;
            _changeLifeCount = cardData.changeLifePointsIfItem;
            reverseTopText.text = cardData.reverseTopTextIfItem;
            reverseDownText.text = cardData.reverseBottomTextIfItem;

            if (cardData.itemSprite) // Проверка, дается ли предмет
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
            await inventoryAnimation.MoveItemToInventory(_giveItem, reverseSprite.transform, inventoryController.GetTarget());
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
}



