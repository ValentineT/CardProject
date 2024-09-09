using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private List<Image> inventory;
    private Transform _target, _itemTransform;

    public Transform Target { get => _target; set => _target = value; }

    // ��������, ���� �� �������
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

    // ���������� �������� � ���������
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

    // �������� �������� �� ���������
    public void RemoveItem(Sprite sprite)
    {
        // ������� ������ ������� ����������
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

        // ���� ������� ������, ������� ��� � ��������� ���������
        if (indexToRemove != -1)
        {
            inventory[indexToRemove].enabled = false;
            inventory[indexToRemove].sprite = null;

            // ��������� ���������� ��������
            for (int i = indexToRemove; i < inventory.Count - 1; i++)
            {
                inventory[i].sprite = inventory[i + 1].sprite;
                inventory[i].enabled = inventory[i + 1].enabled;
            }

            // ��������� ��������� ������
            inventory[inventory.Count - 1].enabled = false;
            inventory[inventory.Count - 1].sprite = null;
        }
        else
        {
            Debug.LogWarning("������� �� ������ � ���������.");
        }
    }

    // �������� ������ ��������� ��� ����������
    public List<Sprite> GetInventorySprites()
    {
        List<Sprite> sprites = new List<Sprite>();

        foreach (Image image in inventory)
        {
            if (image.enabled && image.sprite != null)
            {
                sprites.Add(image.sprite);
            }
        }

        return sprites;
    }

    // ��������� �������� � ���������
    public void LoadInventorySprites(List<Sprite> sprites)
    {
        // ���������� ���������
        foreach (Image image in inventory)
        {
            image.enabled = false;
            image.sprite = null;
        }

        // ��������� ���������� �������
        for (int i = 0; i < sprites.Count; i++)
        {
            if (i < inventory.Count)
            {
                inventory[i].sprite = sprites[i];
                inventory[i].enabled = true;
            }
        }
    }

}
