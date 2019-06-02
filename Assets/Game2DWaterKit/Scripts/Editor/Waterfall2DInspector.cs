using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(Waterfall2D))]
public class Waterfall2DInspector : Editor {

    private static GUIStyle boxGroupStyle;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var widthProperty = serializedObject.FindProperty("_width");
        var heightProperty = serializedObject.FindProperty("_height");
        var sortingLayerIDProperty = serializedObject.FindProperty("_sortingLayerID");
        var sortingOrder = serializedObject.FindProperty("_sortingOrder");

        EditorGUILayout.LabelField("Mesh Properties", EditorStyles.boldLabel);
        BeginBoxGroup();
        EditorGUILayout.PropertyField(widthProperty);
        EditorGUILayout.PropertyField(heightProperty);
        EndBoxGroup();

        EditorGUILayout.LabelField("Rendering Properties", EditorStyles.boldLabel);
        BeginBoxGroup();
        DrawSortingLayerField(sortingLayerIDProperty, sortingOrder);
        EndBoxGroup();

        serializedObject.ApplyModifiedProperties();
    }

    private static void DrawSortingLayerField(SerializedProperty layerID, SerializedProperty orderInLayer)
    {
        MethodInfo methodInfo = typeof(EditorGUILayout).GetMethod("SortingLayerField", BindingFlags.Static | BindingFlags.NonPublic, null, new[] {
                typeof( GUIContent ),
                typeof( SerializedProperty ),
                typeof( GUIStyle ),
                typeof( GUIStyle )
            }, null);

        if (methodInfo != null)
        {
            object[] parameters = { new GUIContent("Sorting Layer"), layerID, EditorStyles.popup, EditorStyles.label };
            methodInfo.Invoke(null, parameters);
            EditorGUILayout.PropertyField(orderInLayer, new GUIContent("Order In Layer"));
        }
    }

    private void BeginBoxGroup()
    {
        if (boxGroupStyle == null)
            boxGroupStyle = new GUIStyle("HelpBox");

        EditorGUILayout.BeginVertical(boxGroupStyle);
    }

    private void EndBoxGroup()
    {
        EditorGUILayout.EndVertical();
    }
}
