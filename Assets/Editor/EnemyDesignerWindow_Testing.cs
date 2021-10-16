using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;

public class EnemyDesignerWindow_Testing : EditorWindow
{
    Texture2D _headerSectionTexture;
    Texture2D _mageSectionTexture;
    Texture2D _rogueSectionTexture;
    Texture2D _warriorSectionTexture;

    Texture2D[] _textures;

    Color _headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    Rect _headerSection;
    Rect _mageSection;
    Rect _rogueSection;
    Rect _warriorSection;

    Rect[] _sections;

    static MageData _mageData_Testing;
    static RogueData _rogueData_Testing;
    static WarriorData _warriorData_Testing;

    public static MageData MageInfo_Testing { get { return _mageData_Testing; } }
    public static RogueData RogueInfo_Testing { get { return _rogueData_Testing; } }
    public static WarriorData WarriorInfo_Testing { get { return _warriorData_Testing; } }


    [MenuItem("Window/Enemy Designer Testing")]
    static void OpenWindow()
    {
        EnemyDesignerWindow_Testing window = (EnemyDesignerWindow_Testing)GetWindow(typeof(EnemyDesignerWindow_Testing));
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
    private static void InitData()
    {
        _mageData_Testing = (MageData)ScriptableObject.CreateInstance(typeof(MageData));
        _rogueData_Testing = (RogueData)ScriptableObject.CreateInstance(typeof(RogueData));
        _warriorData_Testing = (WarriorData)ScriptableObject.CreateInstance(typeof(WarriorData));
    }

    /// <summary>
    /// Initialize 2D visuals
    /// </summary>

    void InitSectionVisuals()
    {
        _headerSectionTexture = new Texture2D(1, 1);
        _headerSectionTexture.SetPixel(0, 0, _headerSectionColor);
        _headerSectionTexture.Apply();

        _mageSectionTexture = Resources.Load<Texture2D>("icons/blue");
        _rogueSectionTexture = Resources.Load<Texture2D>("icons/redOrange");
        _warriorSectionTexture = Resources.Load<Texture2D>("icons/purple");

        _textures = new Texture2D[] { _headerSectionTexture, _mageSectionTexture, _rogueSectionTexture, _warriorSectionTexture };
        _sections = new Rect[] { _headerSection, _mageSection, _rogueSection, _warriorSection };
    }

    /// <summary>
    /// Similar to any update function
    /// not called once per frame. called 1 or more times per interaction
    /// </summary>

    private void OnGUI()
    {
        DrawSections();
        DrawHeader();
        DrawMageSettings();
        DrawWarriorSettings();
        DrawRogueSettings();
    }

    /// <summary>
    /// 
    /// AUTOMATIC
    /// 
    /// Ddefines rect values and paints textures based on recks
    /// </summary>
    /// 

    void DrawSections()
    {
        _sections[0].x = 0;
        _sections[0].y = 0;
        _sections[0].width = Screen.width;
        _sections[0].height = 50;

        for (int i = 1; i < _sections.Length; i++)
        {
            _sections[i].x = (i - 1) * Screen.width / (_sections.Length - 1);
            _sections[i].y = _sections[0].height;
            _sections[i].width = Screen.width / (_sections.Length - 1);
            _sections[i].height = Screen.height - _sections[0].height;

            for (int j = 0; j <= i; j++)
            {
                GUI.DrawTexture(_sections[j], _textures[j]);
            }
        }

        _headerSection = _sections[0];
        _mageSection = _sections[1];
        _rogueSection = _sections[2];
        _warriorSection = _sections[3];
    }

    /// <summary>
    ///  Draw contents of header
    /// </summary>

    void DrawHeader()
    {
        GUILayout.BeginArea(_headerSection);
        // {
        GUILayout.Label("Enemy Designer Testing");
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
        _mageData_Testing._damageType = (MageDamageType) EditorGUILayout.EnumPopup(_mageData_Testing._damageType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon");
        _mageData_Testing._weaponType = (MageWeaponType)EditorGUILayout.EnumPopup(_mageData_Testing._weaponType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40))) 
        {
            GeneralSettings_Testing.OpenWindow(GeneralSettings_Testing.SettingsType_Testing.MAGE);
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
        _warriorData_Testing._classType = (WarriorClassType)EditorGUILayout.EnumPopup(_warriorData_Testing._classType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon");
        _warriorData_Testing._weaponType = (WarriorWeaponType)EditorGUILayout.EnumPopup(_warriorData_Testing._weaponType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings_Testing.OpenWindow(GeneralSettings_Testing.SettingsType_Testing.ROGUE);
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
        _rogueData_Testing._weaponType = (RogueWeaponType)EditorGUILayout.EnumPopup(_rogueData_Testing._weaponType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Strategy");
        _rogueData_Testing._strategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(_rogueData_Testing._strategyType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings_Testing.OpenWindow(GeneralSettings_Testing.SettingsType_Testing.WARRIOR);
        }

        // }
        GUILayout.EndArea();
    }
}

public class GeneralSettings_Testing: EditorWindow
{
    public enum SettingsType_Testing
    {
        MAGE,
        ROGUE,
        WARRIOR
    }

    static SettingsType_Testing dataSetting;
    static GeneralSettings_Testing window;

    public static void OpenWindow(SettingsType_Testing setting)
    {
        dataSetting = setting;
        window = (GeneralSettings_Testing)GetWindow(typeof(GeneralSettings_Testing));
        window.titleContent = new GUIContent(setting.ToString().Substring(0, 1) + setting.ToString().Substring(1).ToLower() + " Setup"); ;
        window.minSize = new Vector2(250, 200);
        window.Show();
    }

    private void OnGUI()
    {
        switch (dataSetting)
        {
            case SettingsType_Testing.MAGE:
                DrawSettings((CharacterData)EnemyDesignerWindow_Testing.MageInfo_Testing);
                break;
            case SettingsType_Testing.ROGUE:
                DrawSettings((CharacterData)EnemyDesignerWindow_Testing.RogueInfo_Testing);
                break;
            case SettingsType_Testing.WARRIOR:
                DrawSettings((CharacterData)EnemyDesignerWindow_Testing.WarriorInfo_Testing);
                break;
        }
    }
    
    void DrawSettings(CharacterData charData)
    {
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
    }
}
