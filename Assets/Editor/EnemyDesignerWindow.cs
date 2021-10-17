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

    Texture2D[] _textures;

    Color _headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    Rect _headerSection;
    Rect _mageSection;
    Rect _rogueSection;
    Rect _warriorSection;

    Rect[] _sections;

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
    private static void InitData()
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
        _mageData._damageType = (MageDamageType) EditorGUILayout.EnumPopup(_mageData._damageType);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon");
        _mageData._weaponType = (MageWeaponType)EditorGUILayout.EnumPopup(_mageData._weaponType);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        if (GUILayout.Button("Create!", GUILayout.Height(40))) 
        {
            SetupWindow.OpenSetupWindow(SetupWindow.ClassType.MAGE);
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

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon");
        _warriorData._weaponType = (WarriorWeaponType)EditorGUILayout.EnumPopup(_warriorData._weaponType);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            SetupWindow.OpenSetupWindow(SetupWindow.ClassType.ROGUE);
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

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Strategy");
        _rogueData._strategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(_rogueData._strategyType);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            SetupWindow.OpenSetupWindow(SetupWindow.ClassType.WARRIOR);
        }

        // }
        GUILayout.EndArea();
    }
}
