using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;

public class PopUpWindow : EditorWindow
{
    public enum WindowType
    {
        CONFIRMATION,
        ERROR
    }

    static WindowType windowSetting;
    static PopUpWindow window;

    private bool isConfirmed = false;
    public bool IsConfirmed => isConfirmed;

    public static void OpenPopUpWindow(WindowType setting)
    {
        windowSetting = setting;
        window = (PopUpWindow)GetWindow(typeof(PopUpWindow));
        window.titleContent = new GUIContent(setting.ToString().Substring(0, 1) + setting.ToString().Substring(1).ToLower() + " Window"); ;
        window.minSize = new Vector2(200, 200);
        window.maxSize = new Vector2(200, 200);
        window.Show();
    }

    private void OnGUI()
    {
        switch (windowSetting)
        {
            case WindowType.CONFIRMATION:
                DrawConfirmationPopUp();
                break;
            case WindowType.ERROR:
                DrawErrorPopUp();
                break;
        }
    }

    private void DrawConfirmationPopUp()
    {
        EditorGUILayout.HelpBox("Are you sure you want to close without saving?", MessageType.Warning);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Confirm", GUILayout.Height(30)))
        {
            isConfirmed = true;
            window.Close();
        }
        else if (GUILayout.Button("Cancel", GUILayout.Height(30)))
        {
            isConfirmed = false;
            window.Close();
        }
        EditorGUILayout.EndHorizontal();
    }
    private void DrawErrorPopUp()
    {
        EditorGUILayout.HelpBox("Error", MessageType.Error);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Okay", GUILayout.Height(30)))
        {
            window.Close();
        }
        EditorGUILayout.EndHorizontal();
    }
}
