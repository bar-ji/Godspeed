using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BF_TerrainEditor : Editor
{
    [CustomEditor(typeof(BF_Terrain))]
    class DecalMeshHelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            BF_Terrain myTarget = (BF_Terrain)target;
            myTarget.terrainToCopy = EditorGUILayout.ObjectField("terrainToCopy", myTarget.terrainToCopy, typeof(Terrain), true) as Terrain;

            if (GUILayout.Button("Sync Terrain Data"))
            {
                myTarget.CopyTerrainData();
                myTarget.MoveTerrainSync();
            }
        }
    }
}
