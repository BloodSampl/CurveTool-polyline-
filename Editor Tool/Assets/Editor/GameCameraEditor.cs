using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameCamera))]
public class GameCameraEditor : Editor
{
    GameCamera mTarget;

    public override void OnInspectorGUI()
    {
        mTarget = (GameCamera)target;
        DrawDefaultInspector();
        DrawCameraHeightPreviewSlider();
    }
    void DrawCameraHeightPreviewSlider()
    {
        Vector3 cameraPosition = mTarget.transform.position;
        cameraPosition.y = EditorGUILayout.Slider("Camera Height", cameraPosition.y, mTarget.MinimumHeight, mTarget.MaximumHeight);
        if(cameraPosition.y != mTarget.transform.position.y)
        {
            Undo.RecordObject(mTarget, "Change Camera Height");
            mTarget.transform.position = cameraPosition;
        }
    }
}
