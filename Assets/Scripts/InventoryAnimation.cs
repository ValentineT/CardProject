using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAnimation : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private Transform bulet, bag;
    private InventoryController _inventoryController;

    private Image _image;
    private Color _color;

    private void Start()
    {
        _inventoryController = GetComponent<InventoryController>();
        _image = bulet.GetComponent<Image>();
        _color = _image.color;
        _image.enabled = false;
    }

    public async UniTask MoveItemToInventory(Sprite sprite, Transform start, Transform end)
    {
        bulet.position = start.position;
        _image.sprite = sprite;
        _image.enabled = true;

        // Используем UniTaskCompletionSource для завершения таска после анимации
        var taskCompletionSource = new UniTaskCompletionSource();

        bulet.DOMove(end.position, duration, false).SetEase(Ease.InBack, 0.6f).OnComplete(() =>
        {
            bag.DOScale(bag.localScale * 1.1f, 0.2f).From().OnComplete(() =>
            {
                _image.enabled = false;
                _inventoryController.AddedItem(sprite);

                taskCompletionSource.TrySetResult(); // Завершаем таск
            });
        });

        await taskCompletionSource.Task; // Ожидаем завершения анимации
    }

    public async UniTask RemoveItemFromInventory(Sprite sprite)
    {
        var taskCompletionSource = new UniTaskCompletionSource();
        bulet.position = _inventoryController.GetTransformItem().position;
        _image.sprite = sprite;
        _image.enabled = true;
        bulet.DOScale(bulet.localScale * 2, 0.5f);
        _image.DOFade(0, 0.5f).OnComplete(() => { 
            {
                _image.enabled = false;
                _image.color = _color;
                bulet.localScale = Vector3.one;

                taskCompletionSource.TrySetResult();
            } });

        await taskCompletionSource.Task;
    }
}
