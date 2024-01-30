using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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

    [SerializeField] private string _player = "Player";

    [SerializeField] private List<CharStats> statsModifiers;
    private CharStatsHandler _statsPlayer;
    private CharStatsHandler _statsBase;

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
        _statsPlayer = GameManager.Instance.player.GetComponent<CharStatsHandler>();
        _statsBase = GameManager.Instance.playerBase.GetComponent<CharStatsHandler>();
    }


    public void UpgradeBaseAttack()
    {
        _statsPlayer.AddStatModifier(statsModifiers[0]);
        _statsBase.AddStatModifier(statsModifiers[0]);

        var item = upgradeDictionary["Attack"];
        attackUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
    }

    public void UpgradeBaseHp()
    {
        _statsPlayer.AddStatModifier(statsModifiers[1]);
        _statsBase.AddStatModifier(statsModifiers[1]);

        var item = upgradeDictionary["HP"];
        hpUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
    }

    public void UpgradeBaseSpeed()
    {
        _statsPlayer.AddStatModifier(statsModifiers[2]);
        _statsBase.AddStatModifier(statsModifiers[2]);

        var item = upgradeDictionary["Speed"];
        speedUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
    }

    public void UpgradeBaseAttackspeed()
    {
        _statsPlayer.AddStatModifier(statsModifiers[3]);
        _statsBase.AddStatModifier(statsModifiers[3]);

        var item = upgradeDictionary["Attackspeed"];
        attackspeedUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
    }

    public void UpgradeBaseDash()
    {
        BaseMovement.Instance.dashDelay -= 1f;
        var item = upgradeDictionary["Dash"];
        dashUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
    }

    public void UpgradeBaseDrone()
    {
        BaseTurretMove.instance._turret.SetActive(true);
        BaseTurretMove.instance.CallBaseMoveEvent();
        turretBtn.SetActive(true);
        var item = upgradeDictionary["Drone"];
        droneUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
    }


}
