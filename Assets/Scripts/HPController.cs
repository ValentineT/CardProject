using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    [SerializeField] private HPAnimation hPAnimation;
    [SerializeField] private List<Image> hpImages;

    public void ChangeCountHP(int count)
    {
        int currentHp = 0;

        // ������������ ������� ���������� ���������� ��������
        foreach (var image in hpImages)
        {
            if (image.enabled)
            {
                currentHp++;
            }
        }

        int newHp = Mathf.Clamp(currentHp + count, 0, hpImages.Count); // ������������ ��������

        if (newHp < currentHp)
        {
            // ��������� �������� � ����� ������
            for (int i = currentHp - 1; i >= newHp; i--)
            {
                hPAnimation.RemoveLife(hpImages[i]);
            }
        }
        else if (newHp > currentHp)
        {
            // �������� �������� � ������ ������
            for (int i = currentHp; i < newHp; i++)
            {
                hPAnimation.AddLife(hpImages[i]);
            }
        }
    }

    // ����� ��� ��������� ������� ���������� ������
    public void SetHP(int targetHp)
    {
        targetHp = Mathf.Clamp(targetHp, 0, hpImages.Count); // ������������ ��������

        int currentHp = GetCurrentHP(); // �������� ������� ���������� ������

        if (targetHp < currentHp)
        {
            // ��������� �������� � ����� ������
            for (int i = currentHp - 1; i >= targetHp; i--)
            {
                hPAnimation.RemoveLife(hpImages[i]);
            }
        }
        else if (targetHp > currentHp)
        {
            // �������� �������� � ������ ������
            for (int i = currentHp; i < targetHp; i++)
            {
                hPAnimation.AddLife(hpImages[i]);
            }
        }
    }

    public int GetCurrentHP()
    {
        int currentHp = 0;

        // ������������ ���������� ���������� ��������
        foreach (var image in hpImages)
        {
            if (image.enabled)
            {
                currentHp++;
            }
        }

        return currentHp;
    }
}
