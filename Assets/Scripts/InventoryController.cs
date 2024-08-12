using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private List<Image> inventory;
    private Transform _target, _itemTransform;

    public Transform Target { get => _target; set => _target = value; }

    // Проверка, есть ли предмет
    public bool CheckHaveItem(CardData cardData)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].sprite == cardData.requiredItemSprite)
            {
                _itemTransform = inventory[i].transform;
                return true;
            }
        }
        return false;
    }

    public Transform GetTransformItem()
    {
        return _itemTransform;
    }

    // Добавление предмета в инвентарь
    public void AddedItem(Sprite sprite)
    {
        if (!sprite) return;

        foreach (Image image in inventory)
        {
            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = sprite;
                return;
            }
        }
    }

    public Transform GetTarget()
    {
        Transform target = null;

        foreach (Image image in inventory)
        {
            if (!image.enabled) return image.transform;
        }
        return target;
    }

    // Удаление предмета из инвентаря
    public void RemoveItem(Sprite sprite)
    {
        // Находим индекс первого совпадения
        int indexToRemove = -1;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].enabled && inventory[i].sprite == sprite)
            {
                indexToRemove = i;
                inventory[i].sprite = null;
                inventory[i].enabled = false;
                break;
            }
        }

        // Если предмет найден, удаляем его и сортируем инвентарь
        if (indexToRemove != -1)
        {
            inventory[indexToRemove].enabled = false;
            inventory[indexToRemove].sprite = null;

            // Сортируем оставшиеся элементы
            for (int i = indexToRemove; i < inventory.Count - 1; i++)
            {
                inventory[i].sprite = inventory[i + 1].sprite;
                inventory[i].enabled = inventory[i + 1].enabled;
            }

            // Отключаем последнюю ячейку
            inventory[inventory.Count - 1].enabled = false;
            inventory[inventory.Count - 1].sprite = null;
        }
        else
        {
            Debug.LogWarning("Предмет не найден в инвентаре.");
        }
    }
}
