using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public int HPCount;
    public int IndexCheckPoint;
    public List<string> Items; // ���� � ��������
    public string Location;
    public string ScreenShot;

    // ��������� ����� ��� ���������� ������
    public static void Save(Data data)
    {
        // ������������ ������� � JSON
        string jsonData = JsonUtility.ToJson(data);

        // ���������� � PlayerPrefs
        PlayerPrefs.SetString("PlayerData", jsonData);
        PlayerPrefs.Save();
    }

    // ��������� ����� ��� �������� ������
    public static Data Load()
    {
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            // ��������� ������ �� PlayerPrefs
            string jsonData = PlayerPrefs.GetString("PlayerData");

            // �������������� ������
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
