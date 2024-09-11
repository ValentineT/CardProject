using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardSetScriptableObject))]
public class CardSetScriptableObjectEditor : Editor
{
    private GUIStyle _boldStyle;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // ��������� ������� �����
        if (_boldStyle == null)
        {
            _boldStyle = new GUIStyle(EditorStyles.boldLabel);
            _boldStyle.fontSize += 1;
        }

        // ���� ��� �����
        EditorGUILayout.LabelField("���������� � ����", _boldStyle);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("portrait"), new GUIContent("�������"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bannerText"), new GUIContent("����� �������"));

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("checkPoint"), new GUIContent("��������"));
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); // �������������� �����
        EditorGUILayout.Space();

        // ������ ��� ����� �����
        EditorGUILayout.LabelField("����� �����", _boldStyle);
        DrawCardFields("leftCard");

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); // �������������� �����
        EditorGUILayout.Space();

        // ������ ��� ������ �����
        EditorGUILayout.LabelField("������ �����", _boldStyle);
        DrawCardFields("rightCard");

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawCardFields(string cardPropertyPath)
    {
        // �������� ����
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.messageText"), new GUIContent("����� ���������"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.illustrationSprite"), new GUIContent("�����������"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.animationClip"), new GUIContent("��������"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.itemSprite"), new GUIContent("������ �������� �������"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.requiredItemSprite"), new GUIContent("������ ���������� ��������"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.removeItemSprite"), new GUIContent("������� ������ ��� �������������"));

        // ���� ��� ��������� ������� � �������
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.changeBanner"), new GUIContent("������� ������"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.locationSprite"), new GUIContent("������� ������� (������)"));
       

        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.nextSetIfItem"), new GUIContent("��������� ����� (���� �������)"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.nextSetIfNoItem"), new GUIContent("��������� ����� (��� ��������)"));

        EditorGUILayout.Space();

        // ������ ����� (���� ������� ����)
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.reverseSpriteIfItem"), new GUIContent("������ �������"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.reverseTopTextIfItem"), new GUIContent("������� ����� �������"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.reverseBottomTextIfItem"), new GUIContent("������ ����� �������"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.changeLifePointsIfItem"), new GUIContent("�������� ���� �����"));

        EditorGUILayout.Space();

        // ������ ����� (���� �������� ���)
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.reverseSpriteIfNoItem"), new GUIContent("������ �������"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.reverseTopTextIfNoItem"), new GUIContent("������� ����� �������"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.reverseBottomTextIfNoItem"), new GUIContent("������ ����� �������"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty($"{cardPropertyPath}.changeLifePointsIfNoItem"), new GUIContent("�������� ���� �����"));
    }
}
