using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public int HPCount;
    public int IndexCheckPoint;
    public List<string> Items; // Путь к спрайтам
    public string Location;
    public string ScreenShot;

    // Статичный метод для сохранения данных
    public static void Save(Data data)
    {
        // Сериализация объекта в JSON
        string jsonData = JsonUtility.ToJson(data);

        // Сохранение в PlayerPrefs
        PlayerPrefs.SetString("PlayerData", jsonData);
        PlayerPrefs.Save();
    }

    // Статичный метод для загрузки данных
    public static Data Load()
    {
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            // Получение данных из PlayerPrefs
            string jsonData = PlayerPrefs.GetString("PlayerData");

            // Десериализация данных
            Data loadedData = JsonUtility.FromJson<Data>(jsonData);

            return loadedData;
        }

        return null;
    }

    public static string SaveSprite(Sprite sprite)
    {
        if (sprite != null)
        {
            return sprite.name;
        }
        return null;
    }

    public static Sprite LoadSprite(string spriteName)
    {
        if (!string.IsNullOrEmpty(spriteName))
        {
            return Resources.Load<Sprite>(spriteName);
        }
        return null;
    }
}
