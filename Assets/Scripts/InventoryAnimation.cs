using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAnimation : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private Transform courier, bag;
    private InventoryController _inventoryController;

    private Image _image;
    private Color _color;

    private void Start()
    {
        _inventoryController = GetComponent<InventoryController>();
        _image = courier.GetComponent<Image>();
        _color = _image.color;
        _image.enabled = false;
    }

    public async UniTask MoveItemToInventory(Sprite sprite, Transform start, Transform end)
    {
        courier.position = start.position;
        _image.sprite = sprite;
        _image.enabled = true;

        var taskCompletionSource = new UniTaskCompletionSource();

        courier.DOMove(end.position, duration, false).SetEase(Ease.InBack, 0.6f).OnComplete(() =>
        {
            bag.DOScale(bag.localScale * 1.1f, 0.2f).From().OnComplete(() =>
            {
                _image.enabled = false;
                _inventoryController.AddedItem(sprite);

                taskCompletionSource.TrySetResult();
            });
        });

        await taskCompletionSource.Task;
    }

    public async UniTask RemoveItemFromInventory(Sprite sprite)
    {
        var taskCompletionSource = new UniTaskCompletionSource();

        courier.position = _inventoryController.GetTransformItem().position;
        _image.sprite = sprite;
        _image.enabled = true;

        courier.DOScale(courier.localScale * 2, 0.5f);
        _image.DOFade(0, 0.5f).OnComplete(() =>
        {
            {
                _image.enabled = false;
                _image.color = _color;
                courier.localScale = Vector3.one;

                taskCompletionSource.TrySetResult();
            }
        });

        await taskCompletionSource.Task;
    }
}
