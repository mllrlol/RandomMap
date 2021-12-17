using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomMap))]
[CanEditMultipleObjects]
public class RandomMapEditor : Editor
{
    SerializedProperty canCut;
    SerializedProperty wallPrefab;
    SerializedProperty passagewayPrefab;
    SerializedProperty manualSeed;
    SerializedProperty seed;
    SerializedProperty width;
    SerializedProperty height;
    SerializedProperty cellSize;
    SerializedProperty roomCount;
    SerializedProperty roomMinSize;
    SerializedProperty roomMaxSize;

    RandomMap randomMap;

    private void OnEnable()
    {
        canCut = serializedObject.FindProperty("canCut");
        wallPrefab = serializedObject.FindProperty("wallPrefab");
        passagewayPrefab = serializedObject.FindProperty("passagewayPrefab");
        manualSeed = serializedObject.FindProperty("manualSeed");
        seed = serializedObject.FindProperty("seed");
        width = serializedObject.FindProperty("width");
        height = serializedObject.FindProperty("height");
        cellSize = serializedObject.FindProperty("cellSize");
        roomCount = serializedObject.FindProperty("maxRoomCount");
        roomMinSize = serializedObject.FindProperty("roomMinSize");
        roomMaxSize = serializedObject.FindProperty("roomMaxSize");
    }

    public override void OnInspectorGUI()
    {
        randomMap = (RandomMap)target;

        EditorGUILayout.LabelField("General Settings", EditorStyles.boldLabel);
        randomMap.CanCut = EditorGUILayout.Toggle("Can Cut", randomMap.CanCut);
        randomMap.WallPrefab = EditorGUILayout.ObjectField("Wall Prefab", randomMap.WallPrefab, typeof(GameObject), false) as GameObject;
        randomMap.PassagewayPrefab = EditorGUILayout.ObjectField("Passageway Prefab", passagewayPrefab.objectReferenceValue, typeof(GameObject), false) as GameObject;
        randomMap.ManualSeed = EditorGUILayout.Toggle("Manual Seed", manualSeed.boolValue);
        EditorGUILayout.Space(10);

        if (manualSeed.boolValue)
        {
            EditorGUILayout.LabelField("Seed", EditorStyles.boldLabel);
            randomMap.Seed = EditorGUILayout.IntField(randomMap.Seed);
        }
        else
        {
            EditorGUILayout.LabelField("Generation options", EditorStyles.boldLabel);
            randomMap.Width = EditorGUILayout.IntField("Width", randomMap.Width);
            randomMap.Height = EditorGUILayout.IntField("Height", randomMap.Height);
            randomMap.CellSize = EditorGUILayout.FloatField("Cell Size", randomMap.CellSize);
            randomMap.MaxRoomCount = EditorGUILayout.IntField("Max Room Count", randomMap.MaxRoomCount);
            randomMap.RoomMinSize = EditorGUILayout.IntField("Room Min Size", randomMap.RoomMinSize);
            randomMap.RoomMaxSize = EditorGUILayout.IntField("Room Max Size", randomMap.RoomMaxSize);
        }

        EditorGUILayout.Space(10);

        if (GUILayout.Button("New Map"))
        {
            randomMap.NewMapWithRoomsAndPassagewaysEditor();
        }
    }
}