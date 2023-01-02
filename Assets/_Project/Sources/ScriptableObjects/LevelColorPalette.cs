using UnityEngine;

[CreateAssetMenu(fileName = "New ColorPalette", menuName = "Color Palette", order = 51)]
public class LevelColorPalette : ScriptableObject
{
    [SerializeField] private Color32 _main;
    [SerializeField] private Gradient _platform;
    [SerializeField] private Gradient _background;

    public Color32 Main => _main;
    public Gradient Platform => _platform;
    public Gradient Background => _background;
}
