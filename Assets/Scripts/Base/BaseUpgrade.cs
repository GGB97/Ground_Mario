using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class BaseUpgrade : MonoBehaviour
{
    public static BaseUpgrade Instance;

    [SerializeField] private TMP_Text attackUpgradeCount;
    [SerializeField] private TMP_Text hpUpgradeCount;
    [SerializeField] private TMP_Text speedUpgradeCount;
    [SerializeField] private TMP_Text attackspeedUpgradeCount;
    [SerializeField] private TMP_Text dashUpgradeCount;
    [SerializeField] private TMP_Text droneUpgradeCount;

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


    public void UpgradeBaseAttack()
    {
        // todo
        var item = upgradeDictionary["Attack"];
        attackUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
    }

    public void UpgradeBaseHp()
    {
        // todo
        var item = upgradeDictionary["HP"];
        hpUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
    }

    public void UpgradeBaseSpeed()
    {
        BaseMovement.Instance.moveSpeed += 1f;
        var item = upgradeDictionary["Speed"];
        speedUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
    }

    public void UpgradeBaseAttackspeed()
    {
        // todo
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
        // todo
        var item = upgradeDictionary["Drone"];
        droneUpgradeCount.text = $"( {++item.upgradeCurrent} / {item.upgradeMax} )";
    }
}
