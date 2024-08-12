using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] private Transform leftCard, rightCard, reverseCard;
    [SerializeField] private float duration;

    private Vector3 _normalPosLeft, _normalPosRight, _normalPosReverse;

    private void Start()
    {
        _normalPosLeft = leftCard.position;
        _normalPosRight = rightCard.position;
        _normalPosReverse = reverseCard.position;

        HideCard();
        leftCard.gameObject.SetActive(true);
        rightCard.gameObject.SetActive(true);
    }

    public async UniTask ShowCards()
    {
        // Используем UniTaskCompletionSource для завершения таска после анимации
        var taskCompletionSource = new UniTaskCompletionSource();

        leftCard.DOMove(_normalPosLeft, duration, false).SetEase(Ease.OutBack);
        rightCard.DOMove(_normalPosRight, duration, false).SetEase(Ease.OutBack);
        leftCard.DOScale(0, duration).From();
        rightCard.DOScale(0, duration).From().OnComplete(() =>
        {
            taskCompletionSource.TrySetResult(); // Завершаем таск
        });
        await taskCompletionSource.Task; // Ожидаем завершения анимации
    }

    public void HideCard()
    {
        reverseCard.position = _normalPosReverse;
        leftCard.position = reverseCard.position;
        rightCard.position = reverseCard.position;
    }

    public void FlipCard(CardType cardType)
    {
        Transform currentCard = null;
        Transform holdCard = null;

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

        holdCard.DOMove(_normalPosReverse, duration, false).SetEase(Ease.InBack);
        holdCard.DOScale(0.001f, duration);
        currentCard.DOMoveX(0, duration, false).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            currentCard.DOScaleX(0.001f, 0.4f).OnComplete(() =>
            {
                reverseCard.position = currentCard.position;
                reverseCard.DOScaleX(0.001f, 0.4f).From();
                holdCard.localScale = Vector3.one;
                currentCard.position = _normalPosReverse;
                currentCard.localScale = Vector3.one;
            });
        });
    }

    public async UniTask HoldReverseCard()
    {
        // Используем UniTaskCompletionSource для завершения таска после анимации
        var taskCompletionSource = new UniTaskCompletionSource();

        reverseCard.DOMove(_normalPosReverse, duration, false).SetEase(Ease.InBack).OnComplete(() =>
        {
            taskCompletionSource.TrySetResult(); // Завершаем таск
        });
        await taskCompletionSource.Task; // Ожидаем завершения анимации
    }
}
