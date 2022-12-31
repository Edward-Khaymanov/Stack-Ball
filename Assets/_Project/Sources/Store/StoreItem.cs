using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StoreItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _icon;

    private BallSkin _skin;
    private Image _background;
    private Store _store;
    private bool _isChoised;
    private string _skinKey;

    public string SkinKey => _skinKey;

    public void Init(BallSkin skin, string skinKey, bool isChoised)
    {
        _skin = skin;
        _skinKey = skinKey;
        UpdateItem(isChoised);
    }

    private void Awake()
    {
        _background = GetComponent<Image>();
        _store = GetComponentInParent<Store>();
    }

    public void UpdateItem(bool choiced)
    {
        _isChoised = choiced;
        _icon.sprite = _skin.StoreIcon;

        if (choiced)
            _background.color = Color.green;
        else
            _background.color = Color.gray;

        if (_skin.IsUnlocked == false)
            _background.color = Color.red;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isChoised)
            return;

        var isChoised = _store.TryChoiceSkin(_skin, _skinKey);
        UpdateItem(isChoised);
    }
}
