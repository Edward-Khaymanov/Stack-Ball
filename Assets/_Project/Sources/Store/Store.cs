using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class Store : Screen
{
    [SerializeField] private AssetLabelReference _ballSkinsLabel;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _showButton;
    [SerializeField] private StoreItem _storeItemTemplate;
    [SerializeField] private Transform _container;

    private readonly List<StoreItem> _storeItems = new List<StoreItem>();

    private void Start()
    {
        Build();
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Hide);
        _showButton.onClick.AddListener(Show);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(Hide);
        _showButton.onClick.RemoveListener(Show);
    }

    public void Build()
    {
        var player = Player.Current;
        var provider = new BallSkinProvider();
        var skinLocations = provider.GetLocationsByLabel(_ballSkinsLabel);
        var skinsWithKey = provider.GetKeySkins(skinLocations).OrderBy(x => x.Value.StoreOrder);

        foreach (var skins in skinsWithKey)
        {
            var storeItem = Instantiate(_storeItemTemplate, _container);
            var isChoised = player.ChoicedSkinKey == skins.Key;
            storeItem.Init(skins.Value, skins.Key, isChoised);
            _storeItems.Add(storeItem);
        }
    }

    public bool TryChoiceSkin(BallSkin skin, string skinKey)
    {
        if (skin.IsUnlocked == false)
            return false;

        var player = Player.Current;
        player.ChoiceSkin(skin, skinKey);
        UpdateStore(skinKey);
        EventsHolder.UpdateBallSkin();
        return true;
    }

    private void UpdateStore(string choicedSkinKey)
    {
        foreach (var item in _storeItems)
        {
            item.UpdateItem(item.SkinKey == choicedSkinKey);
        }
    }
}
