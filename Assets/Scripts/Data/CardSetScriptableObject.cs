using UnityEngine;

[CreateAssetMenu(fileName = "NewCardSet", menuName = "Card Game/Card Set")]
public class CardSetScriptableObject : ScriptableObject
{
    public Sprite portrait; // ������� ��� ����
    [TextArea(3, 3)] public string bannerText; // ����� �� �������
    public bool checkPoint; // ������ ��������� ��������

    public CardData leftCard; // ����� �����
    public CardData rightCard; // ������ �����
}
