using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;

public class EnemyDesignerWindow : EditorWindow
{
    Texture2D _headerSectionTexture;
    Texture2D _mageSectionTexture;
    Texture2D _rogueSectionTexture;
    Texture2D _warriorSectionTexture;

    Color _headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    Rect _headerSection;
    Rect _mageSection;
    Rect _rogueSection;
    Rect _warriorSection;

    static MageData _mageData;
    static RogueData _rogueData;
    static WarriorData _warriorData;

    public static MageData MageInfo { get { return _mageData; } }
    public static RogueData RogueInfo { get { return _rogueData; } }
    public static WarriorData WarriorInfo { get { return _warriorData; } }


    [MenuItem("Window/Enemy Designer")]
    static void OpenWindow()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    /// <summary>
    /// Similar to Start() or Awake()
    /// </summary>

    private void OnEnable()
    {
        InitSectionVisuals();
        InitData();
    }
    public static void InitData()
    {
        _mageData = (MageData)ScriptableObject.CreateInstance(typeof(MageData));
        _rogueData = (RogueData)ScriptableObject.CreateInstance(typeof(RogueData));
        _warriorData = (WarriorData)ScriptableObject.CreateInstance(typeof(WarriorData));
    }

    /// <summary>
    /// Initialize 2D visuals
    /// </summary>

    void InitSectionVisuals()
    {
        _headerSectionTexture = new Texture2D(1, 1);
        _headerSectionTexture.SetPixel(0, 0, _headerSectionColor);
        _headerSectionTexture.Apply();

        _mageSectionTexture = Resources.Load<Texture2D>("icons/purple");
        _rogueSectionTexture = Resources.Load<Texture2D>("icons/orange");
        _warriorSectionTexture = Resources.Load<Texture2D>("icons/aqua");
    }

    /// <summary>
    /// Similar to any update function
    /// not called once per frame. called 1 or more times per interaction
    /// </summary>

    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMageSettings();
        DrawWarriorSettings();
        DrawRogueSettings();
    }

    /// <summary>
    /// 
    /// MANUAL
    /// 
    /// Ddefines rect values and paints textures based on recks
    /// </summary>

    void DrawLayouts()
    {
        _headerSection.x = 0;
        _headerSection.y = 0;
        _headerSection.width = Screen.width;
        _headerSection.height = 50;

        GUI.DrawTexture(_headerSection, _headerSectionTexture);

        _mageSection.x = 0;
        _mageSection.y = 50;
        _mageSection.width = Screen.width / 3;
        _mageSection.height = Screen.height - 50;

        GUI.DrawTexture(_mageSection, _mageSectionTexture);

        _rogueSection.x = Screen.width / 3;
        _rogueSection.y = 50;
        _rogueSection.width = Screen.width / 3;
        _rogueSection.height = Screen.height - 50;

        GUI.DrawTexture(_rogueSection, _rogueSectionTexture);

        _warriorSection.x = 2 * Screen.width / 3;
        _warriorSection.y = 50;
        _warriorSection.width = Screen.width / 3;
        _warriorSection.height = Screen.height - 50;

        GUI.DrawTexture(_warriorSection, _warriorSectionTexture);
    }

    /// <summary>
    ///  Draw contents of header
    /// </summary>

    void DrawHeader()
    {
        GUILayout.BeginArea(_headerSection);
        // {
        GUILayout.Label("Enemy Designer");
        // }
        GUILayout.EndArea();
    }

    /// <summary>
    /// Draws Mage info
    /// </summary>

    void DrawMageSettings()
    {
        GUILayout.BeginArea(_mageSection);
        // {

        GUILayout.Label("Mage");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage");
        _mageData._damageType = (MageDamageType) EditorGUILayout.EnumPopup(_mageData._damageType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon");
        _mageData._weaponType = (MageWeaponType)EditorGUILayout.EnumPopup(_mageData._weaponType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40))) 
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.MAGE);
        }

        // }
        GUILayout.EndArea();
    }

    /// <summary>
    /// Draws Rogue info
    /// </summary>

    void DrawRogueSettings()
    {
        GUILayout.BeginArea(_rogueSection);
        // {

        GUILayout.Label("Rogue");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Class");
        _warriorData._classType = (WarriorClassType)EditorGUILayout.EnumPopup(_warriorData._classType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon");
        _warriorData._weaponType = (WarriorWeaponType)EditorGUILayout.EnumPopup(_warriorData._weaponType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ROGUE);
        }

        // }
        GUILayout.EndArea();
    }

    /// <summary>
    /// Draws Warrior info
    /// </summary>

    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(_warriorSection);
        // {

        GUILayout.Label("Warrior");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon");
        _rogueData._weaponType = (RogueWeaponType)EditorGUILayout.EnumPopup(_rogueData._weaponType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Strategy");
        _rogueData._strategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(_rogueData._strategyType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARRIOR);
        }

        // }
        GUILayout.EndArea();
    }
}

public class GeneralSettings: EditorWindow
{
    public enum SettingsType
    {
        MAGE,
        ROGUE,
        WARRIOR
    }

    static SettingsType dataSetting;
    static GeneralSettings window;

    public static void OpenWindow(SettingsType setting)
    {
        dataSetting = setting;
        window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        window.titleContent = new GUIContent(setting.ToString().Substring(0, 1) + setting.ToString().Substring(1).ToLower() + " Setup"); ;
        window.minSize = new Vector2(250, 200);
        window.Show();
    }

    private void OnGUI()
    {
        switch (dataSetting)
        {
            case SettingsType.MAGE:
                DrawSettings((CharacterData)EnemyDesignerWindow.MageInfo);
                break;
            case SettingsType.ROGUE:
                DrawSettings((CharacterData)EnemyDesignerWindow.RogueInfo);
                break;
            case SettingsType.WARRIOR:
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
        charData._maxEnergy= EditorGUILayout.FloatField(charData._maxEnergy);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Power");
        charData._power = EditorGUILayout.Slider(charData._power, 0, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("% Crit Chance");
        charData._critChance = EditorGUILayout.Slider(charData._critChance, 0, charData._power);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Enemy Name");
        charData._name = EditorGUILayout.TextField(charData._name);
        EditorGUILayout.EndHorizontal();

        if (charData._prefab == null)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Prefab] before it can be created.", MessageType.Warning);
        }

        else if (GUILayout.Button("Finish & Save", GUILayout.Height(30)))
        {
            SaveCharacterData();
            window.Close();
        }
    }

    void SaveCharacterData()
    {

    }
}
