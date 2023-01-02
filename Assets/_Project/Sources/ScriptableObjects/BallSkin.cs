using UnityEngine;

[CreateAssetMenu(fileName = "New BallSkin", menuName = "Ball Skin", order = 52)]
public class BallSkin : ScriptableObject
{
    [SerializeField] private Mesh _skin;
    [SerializeField] private Material _material;
    [SerializeField] private Sprite _storeIcon;
    [SerializeField] private bool _useMaterialColor;
    [SerializeField] private bool _isUnlocked;
    [SerializeField] private int _storeOrder;

    public Mesh Skin => _skin;
    public Material Material => _material;
    public Sprite StoreIcon => _storeIcon;
    public bool UseMaterialColor => _useMaterialColor;
    public bool IsUnlocked => _isUnlocked;
    public int StoreOrder => _storeOrder;
}
