using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;

public class SetupWindow : EditorWindow
{
    public enum ClassType
    {
        MAGE,
        ROGUE,
        WARRIOR,
        NULL
    }

    private bool _saveDataSet = true;
    private bool _savePrefab = true;

    static ClassType _dataSetting;
    static SetupWindow _window;

    PopUpWindow _popUpWindow;

    private void OnEnable()
    {
        _popUpWindow = CreateInstance<PopUpWindow>();
    }

    public static void OpenSetupWindow(ClassType setting)
    {
        _dataSetting = setting;
        _window = (SetupWindow)GetWindow(typeof(SetupWindow));
        _window.titleContent = new GUIContent(setting.ToString().Substring(0, 1) + setting.ToString().Substring(1).ToLower() + " Setup");
        _window.minSize = new Vector2(300, 300);
        _window.Show();
    }

    private void OnGUI()
    {
        switch (_dataSetting)
        {
            case ClassType.MAGE:
                DrawSettings((CharacterData)EnemyDesignerWindow.MageInfo);
                break;
            case ClassType.ROGUE:
                DrawSettings((CharacterData)EnemyDesignerWindow.RogueInfo);
                break;
            case ClassType.WARRIOR:
                DrawSettings((CharacterData)EnemyDesignerWindow.WarriorInfo);
                break;
            case ClassType.NULL:
                break;
        }
    }

    void DrawSettings(CharacterData charData)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab");
        charData._prefab = EditorGUILayout.ObjectField(charData._prefab, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Health");
        charData._maxHealth = EditorGUILayout.FloatField(charData._maxHealth);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Energy");
        charData._maxEnergy = EditorGUILayout.FloatField(charData._maxEnergy);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        GUILayout.Label("Power");
        GUILayout.Label("% Crit Chance");
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        charData._power = EditorGUILayout.Slider(charData._power, 0, 100);
        charData._critChance = EditorGUILayout.Slider(charData._critChance, 0, charData._power);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Enemy Name");
        charData._name = EditorGUILayout.TextField(charData._name);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Save as new DataSet");
        _saveDataSet = EditorGUILayout.Toggle(_saveDataSet);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Save as new Prefab");
        _savePrefab = EditorGUILayout.Toggle(_savePrefab);
        EditorGUILayout.EndHorizontal();

        DrawButtons(charData);
    }

    void DrawButtons(CharacterData charData)
    {
        bool isSaveable = false;

        EditorGUILayout.BeginVertical();
        if (charData._prefab == null)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Prefab] before it can be created.", MessageType.Error);
            isSaveable = false;
        }
        if (charData._name == null)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Name] before it can be created.", MessageType.Error);
            isSaveable = false;
        }
        if (charData._prefab != null && charData._name != null)
        {
            isSaveable = true;
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save & Exit", GUILayout.Width(Screen.width / 3.11f), GUILayout.Height(30)))
        {
            if (isSaveable)
            {
                SaveCharacterData();
                _window.Close();
            }
        }
        else if (GUILayout.Button("Save", GUILayout.Width(Screen.width / 3.11f), GUILayout.Height(30)))
        {
            if (isSaveable)
            {
                SaveCharacterData();
            }
        }

        if (GUILayout.Button("Exit", GUILayout.Width(Screen.width / 3.11f), GUILayout.Height(30)))
        {
            PopUpWindow.OpenPopUpWindow(PopUpWindow.WindowType.CONFIRMATION);
        }
        else if (_popUpWindow.IsConfirmed)
        {
            _window.Close();
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create New", GUILayout.Height(30)))
        {
            
        }

        EditorGUILayout.EndVertical();
    }

    void SaveCharacterData()
    {
        string prefabPath; // path to the base prefab
        string newPrefabPath = "Assets/Prefabs/Characters/";
        string dataPath = "Assets/Resources/CharacterData/Data/";

        switch (_dataSetting)
        {
            case ClassType.MAGE:

                if (_saveDataSet)
                {
                    //create the .asset file
                    dataPath += "Mage/" + EnemyDesignerWindow.MageInfo._name + ".asset";
                    AssetDatabase.CreateAsset(EnemyDesignerWindow.MageInfo, dataPath);
                }

                if (_savePrefab)
                {
                    //create the .prefab file path
                    newPrefabPath += "Mage/" + EnemyDesignerWindow.MageInfo._name + ".prefab";

                    //get prefab path
                    prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo._prefab);
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);

                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject magePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));

                    if (!magePrefab.GetComponent<Mage>())
                    {
                        magePrefab.AddComponent(typeof(Mage));
                    }

                    //not really sure what this does
                    //magePrefab.GetComponent<Mage>()._mageData = EnemyDesignerWindow.MageInfo;
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                break;
        }
    }
}
