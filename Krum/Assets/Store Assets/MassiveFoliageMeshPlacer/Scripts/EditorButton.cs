using UnityEngine;
using UnityEditor;
//The script for the editor button for the MassiveFoliageMeshPlacer asset.
[CustomEditor(typeof(MassiveFoliageMeshPlacer))]
public class EditorButtonMFMP : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MassiveFoliageMeshPlacer mfmp = (MassiveFoliageMeshPlacer)target;

        if (GUILayout.Button("Add Details"))//The button for adding details
        {
            mfmp.AddDetails();
        }
        if (GUILayout.Button("Clean ALL Details"))//The button for clearing all the details
        {
            mfmp.CleanDetails(-1);
        }
    }
}