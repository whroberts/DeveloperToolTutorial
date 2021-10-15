using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyDesignerWindow : EditorWindow
{
    Texture2D _headerSectionTexture;
    Texture2D _mageSectionTexture;
    Texture2D _warriorSectionTexture;
    Texture2D _rogueSectionTexture;

    Color _headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    Rect _headerSection;
    Rect _mageSection;
    Rect _warriorSection;
    Rect _rogueSection;

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
        InitTextures();
    }

    /// <summary>
    /// Initialize Texture 2D Values
    /// </summary>

    void InitTextures()
    {
        _headerSectionTexture = new Texture2D(1, 1);
        _headerSectionTexture.SetPixel(0, 0, _headerSectionColor);
        _headerSectionTexture.Apply();
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
    /// Ddefines rect values and paints textures based on recks
    /// </summary>

    void DrawLayouts()
    {
        _headerSection.x = 0;
        _headerSection.y = 0;
        _headerSection.width = Screen.width;
        _headerSection.height = 50;

        GUI.DrawTexture(_headerSection, _headerSectionTexture);
    }

    /// <summary>
    ///  Draw contents of header
    /// </summary>

    void DrawHeader()
    {

    }

    /// <summary>
    /// Defines Rect values and paints textures based on Rects
    /// </summary>

    void DrawMageSettings()
    {

    }

    /// <summary>
    /// Defines Rect values and paints textures based on Rects
    /// </summary>

    void DrawWarriorSettings()
    {

    }

    /// <summary>
    /// Defines Rect values and paints textures based on Rects
    /// </summary>

    void DrawRogueSettings()
    {

    }
}
