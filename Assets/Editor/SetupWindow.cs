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
        WARRIOR
    }

    static ClassType dataSetting;
    static SetupWindow window;

    PopUpWindow popUpWindow;

    private void OnEnable()
    {
        popUpWindow = CreateInstance<PopUpWindow>();
    }

    public static void OpenSetupWindow(ClassType setting)
    {
        dataSetting = setting;
        window = (SetupWindow)GetWindow(typeof(SetupWindow));
        window.titleContent = new GUIContent(setting.ToString().Substring(0, 1) + setting.ToString().Substring(1).ToLower() + " Setup"); ;
        window.minSize = new Vector2(250, 200);
        window.Show();
    }

    private void OnGUI()
    {
        switch (dataSetting)
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

        DrawConfirmationButtons(charData);
    }

    void DrawConfirmationButtons(CharacterData charData)
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

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save & Exit", GUILayout.Width(Screen.width / 3), GUILayout.Height(30)))
        {
            if (isSaveable)
            {
                SaveCharacterData();
                window.Close();
            }
        }
        else if (GUILayout.Button("Save", GUILayout.Width(Screen.width / 3), GUILayout.Height(30)))
        {
            if (isSaveable)
            {
                SaveCharacterData();
            }
        }

        if (GUILayout.Button("Exit", GUILayout.Width(Screen.width / 3), GUILayout.Height(30)))
        {
            PopUpWindow.OpenPopUpWindow(PopUpWindow.WindowType.CONFIRMATION);
        }
        else if (popUpWindow.IsConfirmed)
        {
            window.Close();
        }
        EditorGUILayout.EndHorizontal();
    }

    void SaveCharacterData()
    {
        string prefabPath; // path to the base prefab
        string newPrefabPath = "Assets/Prefabs/Characters/";
        string dataPath = "Assets/Resources/CharacterData/Data/";

        switch (dataSetting)
        {
            case ClassType.MAGE:

                //create the .asset file
                dataPath += "Mage/" + EnemyDesignerWindow.MageInfo._name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.MageInfo, dataPath);

                //create the .prefab file path
                newPrefabPath += "Mage/" + EnemyDesignerWindow.MageInfo.name + ".prefab";

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

                magePrefab.GetComponent<Mage>()._mageData = EnemyDesignerWindow.MageInfo;

                break;

            case ClassType.ROGUE:

                //create the .asset file
                dataPath += "Rogue/" + EnemyDesignerWindow.RogueInfo._name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.RogueInfo, dataPath);

                //create the .prefab file path
                newPrefabPath += "Rogue/" + EnemyDesignerWindow.RogueInfo.name + ".prefab";

                //get prefab path
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.RogueInfo._prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject roguePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));

                if (!roguePrefab.GetComponent<Rogue>())
                {
                    roguePrefab.AddComponent(typeof(Rogue));
                }

                roguePrefab.GetComponent<Rogue>()._rogueData = EnemyDesignerWindow.RogueInfo;

                break;
            case ClassType.WARRIOR:

                //create the .asset file
                dataPath += "Warrior/" + EnemyDesignerWindow.WarriorInfo._name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.WarriorInfo, dataPath);

                //create the .prefab file path
                newPrefabPath += "Warrior/" + EnemyDesignerWindow.WarriorInfo.name + ".prefab";

                //get prefab path
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.WarriorInfo._prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject warriorPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));

                if (!warriorPrefab.GetComponent<Warrior>())
                {
                    warriorPrefab.AddComponent(typeof(Warrior));
                }

                warriorPrefab.GetComponent<Warrior>()._warriorData = EnemyDesignerWindow.WarriorInfo;

                break;
        }
    }
}
