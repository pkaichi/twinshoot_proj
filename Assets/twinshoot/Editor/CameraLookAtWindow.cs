using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class CameraLookAtWindow : EditorWindow
{



    [MenuItem("Window/RM-Pro/CameraLookAtWindow")]
    static void OpenWindow()
    {
        EditorWindow.GetWindow<CameraLookAtWindow>();
    }

    List<Camera> sceneCameraList;
    string[] sceneCameraPopupList = new string[0];
    int popupIndex = 0;

    Vector3 targetPos;

    void OnEnable()
    {
        sceneCameraList = GameObject.FindObjectsOfType<Camera>().ToList();

        sceneCameraPopupList = sceneCameraList.Select((c, i) => $"{i}:{c.name}").ToArray();

        Selection.selectionChanged += OnSelectionObjectChange;
    }

    void OnDisable()
    {
        Selection.selectionChanged -= OnSelectionObjectChange;
    }

    void OnSelectionObjectChange()
    {

        if (Selection.activeGameObject != null)
        {
            targetPos = Selection.activeGameObject.transform.position;
        }
        Repaint();
    }

    void OnGUI()
    {
        var pi = EditorGUILayout.Popup("Camera", popupIndex, sceneCameraPopupList);
        if (pi != popupIndex)
        {
            popupIndex = pi;
        }

        targetPos = EditorGUILayout.Vector3Field("lookAt", targetPos);


        EditorGUILayout.Space();
        if (GUILayout.Button("LookAt Apply!"))
        {
            var cam = sceneCameraList[popupIndex];

            cam.transform.LookAt(targetPos);
        }
    }
}
