using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class Waterfall2D : MonoBehaviour {

    private static int waterFallTimeShaderPropertyID = Shader.PropertyToID("_Waterfall_Time");

#if !UNITY_EDITOR
    private static int waterFallTimeShaderLastFrameUpdate = -1;
#endif

    [SerializeField] private float _width = 5f;
    [SerializeField] private float _height = 5f;

    [SerializeField] private int _sortingLayerID;
    [SerializeField] private int _sortingOrder;

    private void Start()
    {
        RecomputeMesh();
        CheckMaterialAndSortingLayer();
    }

    private void RecomputeMesh()
    {
        var meshFilter = GetComponent<MeshFilter>();
        var mesh = meshFilter.sharedMesh;

        if(mesh == null)
        {
            mesh = new Mesh();
            meshFilter.sharedMesh = mesh;
        }

        var vertices = new Vector3[4];
        var triangles = new int[6];
        var uvs = new Vector2[4];

        vertices[0] = new Vector3(-0.5f * _width, 0.5f * _height);
        vertices[1] = new Vector3(0.5f * _width, 0.5f * _height);
        vertices[2] = new Vector3(0.5f * _width, -0.5f * _height);
        vertices[3] = new Vector3(-0.5f * _width, -0.5f * _height);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        uvs[0] = new Vector2(0f, 1f);
        uvs[1] = new Vector2(1f, 1f);
        uvs[2] = new Vector2(1f, 0f);
        uvs[3] = new Vector2(0f, 0f);

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
    }

    private void CheckMaterialAndSortingLayer()
    {
        var meshRenderer = GetComponent<MeshRenderer>();

        var material = meshRenderer.sharedMaterial;
        if(material == null)
        {
            material = new Material(Shader.Find("Game2DWaterKit/Waterfall Unlit"));
            meshRenderer.sharedMaterial = material;
        }

        meshRenderer.sortingLayerID = _sortingLayerID;
        meshRenderer.sortingOrder = _sortingOrder;
    }

#if !UNITY_EDITOR
    private void LateUpdate()
    {
        if(Time.frameCount != waterFallTimeShaderLastFrameUpdate)
        {
            float t = (float)Time.time;
            Vector4 time = new Vector4(t / 20f, t, t * 2f, t * 3f);
            Shader.SetGlobalVector(waterFallTimeShaderPropertyID, time);

            waterFallTimeShaderLastFrameUpdate = Time.frameCount;
        }
    }
#endif

#if UNITY_EDITOR

    private void OnValidate()
    {
        RecomputeMesh();
        CheckMaterialAndSortingLayer();

        EditorApplication.update -= UpdateShaderTime;
        EditorApplication.update += UpdateShaderTime;
    }

    private static void UpdateShaderTime()
    {
        float t = (float) EditorApplication.timeSinceStartup;
        Vector4 time = new Vector4(t / 20f, t, t * 2f, t * 3f);
        Shader.SetGlobalVector(waterFallTimeShaderPropertyID, time);
    }

    // Add menu item to create Game2D Water GameObject.
    [MenuItem("GameObject/2D Object/Game2D Water Kit/Waterfall Object", false, 10)]
    private static void CreateWaterObject(MenuCommand menuCommand)
    {
        GameObject go = new GameObject("Waterfall", typeof(Waterfall2D));
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }

#endif

}
