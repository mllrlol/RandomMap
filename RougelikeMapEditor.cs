using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RougelikeMap))]
[CanEditMultipleObjects]
public class RougelikeMapEditor : Editor
{
    RougelikeMap randomMap;

    public override void OnInspectorGUI()
    {
        randomMap = (RougelikeMap)target;
        float windowWidth = EditorGUIUtility.currentViewWidth;

        EditorGUILayout.LabelField("General Settings", EditorStyles.boldLabel);
        randomMap.WallPrefab = EditorGUILayout.ObjectField("Wall Prefab", randomMap.WallPrefab, typeof(GameObject), false) as GameObject;
        randomMap.PassagewayPrefab = EditorGUILayout.ObjectField("Passageway Prefab", randomMap.PassagewayPrefab, typeof(GameObject), false) as GameObject;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Manual seed", GUILayout.Width(windowWidth - 44f));
        randomMap.ManualSeed = EditorGUILayout.Toggle(randomMap.ManualSeed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Can Cut", GUILayout.Width(windowWidth - 44f));
        randomMap.CanCut = EditorGUILayout.Toggle(randomMap.CanCut);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Seed", EditorStyles.boldLabel);
        if (randomMap.ManualSeed)
        {
            randomMap.Seed = EditorGUILayout.IntField(randomMap.Seed);
        }
        else
        {
            GUI.enabled = false;
            EditorGUILayout.IntField(randomMap.Seed);
            GUI.enabled = true;
        }
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Generation Settings", EditorStyles.boldLabel);
        randomMap.Width = EditorGUILayout.IntField("Width", randomMap.Width);
        randomMap.Height = EditorGUILayout.IntField("Height", randomMap.Height);
        randomMap.CellSize = EditorGUILayout.FloatField("Cell Size", randomMap.CellSize);
        randomMap.MaxRoomCount = EditorGUILayout.IntField("Max Room Count", randomMap.MaxRoomCount);
        randomMap.RoomMinSize = EditorGUILayout.IntField("Room Min Size", randomMap.RoomMinSize);
        randomMap.RoomMaxSize = EditorGUILayout.IntField("Room Max Size", randomMap.RoomMaxSize);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Should Overlap", GUILayout.Width(windowWidth - 44f));
        randomMap.ShouldOverlap = EditorGUILayout.Toggle(randomMap.ShouldOverlap);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New Map"))
        {
            if (!randomMap.ManualSeed)
            {
                randomMap.Seed = (int)System.DateTime.Now.Ticks;
            }
            randomMap.NewMapWithRoomsAndPassagewaysEditor();
        }
        if (GUILayout.Button("Clear Map"))
        {
            randomMap.DestoryMapItemEditor();
        }
        GUILayout.EndHorizontal();

        EditorUtility.SetDirty(randomMap);
    }
}