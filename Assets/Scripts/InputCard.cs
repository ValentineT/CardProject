using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private CardAnimation cardAnimation;
    [SerializeField] private ContentCard contentCard;
    [SerializeField] private GameController gameController;
    [SerializeField] private CardType cardType;
    [SerializeField] private Animator _animator;

    private Transform _transform;
    private AnimationClip _animationClip;

    public AnimationClip AnimationClip { get => _animationClip; set => _animationClip = value; }

    private void Start()
    {
        _transform = transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _transform.DOScale(_transform.localScale * 1.1f, 0.3f);
        // �������� ��� ���������
        if (_animationClip != null && _animator != null)
        {
            _animator.enabled = true;
            _animator.Play(_animationClip.name);
            _animator.speed = 1f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _transform.DOScale(Vector3.one, 0.3f);
        // ���������� �������� � ��������� �� ������ ����
        if (_animationClip != null && _animator != null)
        {
            _animator.Play(_animationClip.name, 0, 0f); // ������� �� ������ ����
            _animator.speed = 0f; // ���������� ��������
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickCard();
    }

    private void ClickCard()
    {
        if (_animator) _animator.enabled = false;

        if (cardType != CardType.Reverse)
        {
            contentCard.CheckRequestItem(cardType);
            cardAnimation.FlipCard(cardType);
        }
        else
        {
            ClickReverse();
        }
    }

    // ���� �� ������� ����� � ����������� ���������������� ��������
    private async void ClickReverse()
    {
        contentCard.ChangeCountHP();

        if (contentCard.RemoveItem) await contentCard.RemoveItemFromInventory();

        await contentCard.SetItemToInventory();
        await cardAnimation.HoldReverseCard();
        await contentCard.HoldBanner();
        await contentCard.ChangeLocationSprite();
        contentCard.SetContent(contentCard.NextCardSetScriptableObject);
        if (gameController.IsGameOver) return;
        await UniTask.Delay(600);
        await contentCard.ShowBanner();
        await cardAnimation.ShowCards();
    }
}
