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

        _textures = new Texture2D[] { _headerSectionTexture, _mageSectionTexture, _rogueSectionTexture, _warriorSectionTexture };
        _sections = new Rect[] { _headerSection, _mageSection, _rogueSection, _warriorSection };
    }

    /// <summary>
    /// Similar to any update function
    /// not called once per frame. called 1 or more times per interaction
    /// </summary>

    private void OnGUI()
    {
        //DrawLayouts();
        DrawSections();
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
    /// 
    /// AUTOMATED
    /// 
    /// Defines rect values and paints textures based on recks
    /// </summary>

    void DrawSections()
    {
        _sections[0].x = 0;
        _sections[0].y = 0;
        _sections[0].width = Screen.width;
        _sections[0].height = 50;

        for (int i = 1; i < _sections.Length; i++)
        {
            _sections[i].x = (i-1) * Screen.width / (_sections.Length - 1);
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
        // }
        GUILayout.EndArea();
    }
}
