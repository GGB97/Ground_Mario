using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BaseUpgrade : MonoBehaviour
{
    public static BaseUpgrade Instance;

    [SerializeField] private TMP_Text attackUpgradeCount;
    [SerializeField] private TMP_Text hpUpgradeCount;
    [SerializeField] private TMP_Text speedUpgradeCount;
    [SerializeField] private TMP_Text attackspeedUpgradeCount;
    [SerializeField] private TMP_Text dashUpgradeCount;
    [SerializeField] private TMP_Text droneUpgradeCount;
    [SerializeField] private GameObject turretBtn;

    [SerializeField] private GameObject _player;

    [SerializeField] private List<CharStats> statsModifiers;
    private CharStatsHandler _statsPlayer;
    private CharStatsHandler _statsBase;

    public PlayerResourceController resource_Data;

    public class UpgradeOption
    {
        public int upgradeMax;
        public int upgradeCurrent;
        public int price;

        public UpgradeOption(int _upgradeMax, int _upgradeCurrent, int _price)
        {
            upgradeMax = _upgradeMax;
            upgradeCurrent = _upgradeCurrent;
            price = _price;
        }
    }


    public Dictionary<string, UpgradeOption> upgradeDictionary = new Dictionary<string, UpgradeOption>()
    {
        {"Attack", new UpgradeOption(5, 0, 300)},
        {"HP", new  UpgradeOption(5, 0, 300)},
        {"Speed", new UpgradeOption(5, 0, 300)},
        {"Attackspeed", new UpgradeOption(5, 0, 300)},
        {"Dash", new UpgradeOption(3, 0, 500)},
        {"Drone", new UpgradeOption(1, 0, 1000)}
    };



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _player = GameManager.Instance.player;
        _statsPlayer = GameManager.Instance.player.GetComponent<CharStatsHandler>();
        _statsBase = GameManager.Instance.playerBase.GetComponent<CharStatsHandler>();
        resource_Data = _player.GetComponent<PlayerResourceController>();
    }


    public void UpgradeBaseAttack()
    {
        _statsPlayer.AddStatModifier(statsModifiers[0]);
        _statsBase.AddStatModifier(statsModifiers[0]);

        var item = upgradeDictionary["Attack"];
        resource_Data._data.coin -= item.price;
        attackUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
        UIManager.instance.UpdateCoinUI();
    }

    public void UpgradeBaseHp()
    {
        _statsPlayer.AddStatModifier(statsModifiers[1]);
        _statsBase.AddStatModifier(statsModifiers[1]);

        var item = upgradeDictionary["HP"];
        resource_Data._data.coin -= item.price;
        hpUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
        UIManager.instance.UpdateCoinUI();
    }

    public void UpgradeBaseSpeed()
    {
        _statsPlayer.AddStatModifier(statsModifiers[2]);
        _statsBase.AddStatModifier(statsModifiers[2]);

        var item = upgradeDictionary["Speed"];
        resource_Data._data.coin -= item.price;
        speedUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
        UIManager.instance.UpdateCoinUI();
    }

    public void UpgradeBaseAttackspeed()
    {
        _statsPlayer.AddStatModifier(statsModifiers[3]);
        _statsBase.AddStatModifier(statsModifiers[3]);

        var item = upgradeDictionary["Attackspeed"];
        resource_Data._data.coin -= item.price;
        attackspeedUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
        UIManager.instance.UpdateCoinUI();
    }

    public void UpgradeBaseDash()
    {
        BaseMovement.Instance.dashDelay -= 1f;
        var item = upgradeDictionary["Dash"];
        resource_Data._data.coin -= item.price;
        dashUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
        UIManager.instance.UpdateCoinUI();
    }

    public void UpgradeBaseDrone()
    {
        BaseTurretMove.instance._turret.SetActive(true);
        BaseTurretMove.instance.CallBaseMoveEvent();
        turretBtn.SetActive(true);
        var item = upgradeDictionary["Drone"];
        resource_Data._data.coin -= item.price;
        droneUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
        UIManager.instance.UpdateCoinUI();
    }


}
