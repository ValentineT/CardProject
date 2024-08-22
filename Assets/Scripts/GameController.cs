using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform holdPanel, mapPanel, mainMenuPanel;
    [SerializeField] private Image bg;
    [SerializeField] private float duration, posUp;

    private Image _imageHold;
    private Color _normalColorHold;
    private Vector3 _positionUp;

    private void Start()
    {
        _imageHold = holdPanel.GetComponent<Image>();
        _normalColorHold = _imageHold.color;
        _positionUp = new Vector3(0, posUp, 0);
        holdPanel.position = _positionUp;
    }

    public async UniTask ShowHold()
    {
        var taskCompletionSource = new UniTaskCompletionSource();
        holdPanel.position = Vector3.zero;

        _imageHold.DOFade(0, duration).From().OnComplete(() =>
        {
            taskCompletionSource.TrySetResult();
        });

        await taskCompletionSource.Task;
    }

    public async UniTask HideHold()
    {
        var taskCompletionSource = new UniTaskCompletionSource();

        _imageHold.DOFade(0, duration).OnComplete(() =>
        {
            holdPanel.position = _positionUp;
            _imageHold.color = _normalColorHold;
            taskCompletionSource.TrySetResult();
        });

        await taskCompletionSource.Task;
    }

    public async void ShowMenu()
    {
        await ShowHold();
        mainMenuPanel.position = Vector3.zero;
        await HideHold();
    }

    public async void HideMenu()
    {
        await ShowHold();
        mainMenuPanel.position = _positionUp;
        await HideHold();
    }

    public async void ShowMap()
    {
        await ShowHold();
        mapPanel.position = Vector3.zero;
        await HideHold();
    }

    public async UniTask HideMap()
    {
        await ShowHold();
        mapPanel.position = _positionUp;
        mainMenuPanel.position = _positionUp;
        await HideHold();
        await UniTask.Yield();
    }

    public async UniTask ChangeBG(Sprite sprite)
    {
        await ShowHold();

        bg.sprite = sprite;
        await HideHold();
    }

    public void HideMapButton() => HideMap().Forget();
}
