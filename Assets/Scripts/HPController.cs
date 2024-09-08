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

        // Подсчитываем текущее количество включенных сердечек
        foreach (var image in hpImages)
        {
            if (image.enabled)
            {
                currentHp++;
            }
        }

        int newHp = Mathf.Clamp(currentHp + count, 0, hpImages.Count); // ограничиваем диапазон

        if (newHp < currentHp)
        {
            // Выключаем сердечки с конца списка
            for (int i = currentHp - 1; i >= newHp; i--)
            {
                hPAnimation.RemoveLife(hpImages[i]);
            }
        }
        else if (newHp > currentHp)
        {
            // Включаем сердечки с начала списка
            for (int i = currentHp; i < newHp; i++)
            {
                hPAnimation.AddLife(hpImages[i]);
            }
        }
    }

    public int GetCurrentHP()
    {
        int currentHp = 0;

        // Подсчитываем количество включенных сердечек
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
