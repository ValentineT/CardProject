using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private List<Sprite> cursorSprites; // Лист спрайтов для курсора
    [SerializeField] private Vector2 hotspot = Vector2.zero; // Точка отсчета спрайта (по умолчанию центр)
    [SerializeField] private Texture2D defaultCursorTexture; // Текстура для сброса на системный курсор, если необходимо

    private SpriteRenderer _cursorRenderer;

    private void Start()
    {
        // Скрываем системный курсор
        Cursor.visible = false;

        // Добавляем объект для отображения кастомного курсора
        GameObject cursorObject = new GameObject("CustomCursor");
        cursorObject.transform.SetParent(transform);

        _cursorRenderer = cursorObject.AddComponent<SpriteRenderer>();
        _cursorRenderer.sortingOrder = 100; // Устанавливаем порядок отрисовки, чтобы курсор был всегда сверху
        ChangeCursorSprite(0);
    }

    private void Update()
    {
        // Следим за положением мыши и обновляем позицию кастомного курсора
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0; // Ставим Z в 0, чтобы курсор был на уровне UI
        _cursorRenderer.transform.position = cursorPosition;
    }

    // Метод для смены спрайта курсора по индексу
    public void ChangeCursorSprite(int index)
    {
        if (index >= 0 && index < cursorSprites.Count)
        {
            _cursorRenderer.sprite = cursorSprites[index];
        }
        else
        {
            Debug.LogWarning("Index out of range of cursorSprites list.");
        }
    }

    // Метод для возврата системного курсора
    public void ResetToDefaultCursor()
    {
        Cursor.visible = true;
        _cursorRenderer.sprite = null;
    }
}
