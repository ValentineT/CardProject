using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] private Transform leftCard, rightCard, reverseCard;
    [SerializeField] private GameController gameController;
    [SerializeField] private float duration;

    private Vector3 _normalPosLeft, _normalPosRight, _normalPosReverse;
    private InputCard _inputCard_L, _inputCard_R, _inputCard_Rev;

    private void Start()
    {
        _normalPosLeft = leftCard.position;
        _normalPosRight = rightCard.position;
        _normalPosReverse = reverseCard.position;

        _inputCard_L = leftCard.GetComponent<InputCard>();
        _inputCard_R = rightCard.GetComponent<InputCard>();
        _inputCard_Rev = reverseCard.GetComponent<InputCard>();

        InputCardActivate(false);
        HideCard();
        leftCard.gameObject.SetActive(true);
        rightCard.gameObject.SetActive(true);
    }

    private void InputCardActivate(bool active)
    {
        _inputCard_L.enabled = active;
        _inputCard_R.enabled = active;
        _inputCard_Rev.enabled = active;
    }

    public async UniTask ShowCards()
    {
        var taskCompletionSource = new UniTaskCompletionSource();

        InputCardActivate(false);

        leftCard.DOMove(_normalPosLeft, duration, false).SetEase(Ease.OutBack);
        rightCard.DOMove(_normalPosRight, duration, false).SetEase(Ease.OutBack);
        leftCard.DOScale(0, duration).From();
        rightCard.DOScale(0, duration).From().OnComplete(() =>
        {
            InputCardActivate(true);
            taskCompletionSource.TrySetResult(); // ��������� ����
        });
        await taskCompletionSource.Task; // ������� ���������� ��������
    }

    public void HideCard()
    {
        InputCardActivate(false);
        reverseCard.position = _normalPosReverse;
        leftCard.position = reverseCard.position;
        rightCard.position = reverseCard.position;
    }

    public void FlipCard(CardType cardType)
    {
        Transform currentCard = null;
        Transform holdCard = null;
        
       // rightCard.localScale = Vector3.one;

        switch (cardType)
        {
            case CardType.Left:
                currentCard = leftCard;
                holdCard = rightCard;
                break;
            case CardType.Right:
                currentCard = rightCard;
                holdCard = leftCard;
                break;
        }

        InputCardActivate(false);

        holdCard.DOMove(_normalPosReverse, duration, false).SetEase(Ease.InBack);
        holdCard.DOScale(0.001f, duration);
        currentCard.DOScale(Vector3.one, 0.4f).OnComplete(() => 
        {
            currentCard.DOMoveX(0, duration, false).SetEase(Ease.InOutBack).OnComplete(() =>
            {
                currentCard.DOScaleX(0.001f, 0.4f).OnComplete(() =>
                {
                    reverseCard.localScale = Vector3.one;
                    reverseCard.position = currentCard.position;
                    reverseCard.DOScaleX(0.001f, 0.4f).From().OnComplete(() =>
                    {
                        holdCard.localScale = Vector3.one;
                        currentCard.position = _normalPosReverse;
                        currentCard.localScale = Vector3.one;
                        InputCardActivate(true);
                    });
                });
            });
        });
    }

    public async UniTask HoldReverseCard()
    {
        InputCardActivate(false);
        var taskCompletionSource = new UniTaskCompletionSource();
        reverseCard.DOMove(_normalPosReverse, duration, false).SetEase(Ease.InBack).OnComplete(() =>
        {
            taskCompletionSource.TrySetResult(); // ��������� ����
        });
        await taskCompletionSource.Task; // ������� ���������� ��������
    }
}
