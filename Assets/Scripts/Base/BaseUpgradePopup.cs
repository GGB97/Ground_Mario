using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseUpgradePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text PopupText;
    [SerializeField] private TMP_Text PopupFailText;

    [SerializeField] private Button btnAttack;
    [SerializeField] private Button btnHp;
    [SerializeField] private Button btnSpeed;
    [SerializeField] private Button btnAttackspeed;
    [SerializeField] private Button btnDash;
    [SerializeField] private Button btnDrone;

    [SerializeField] private Button btnYes;
    [SerializeField] private Button btnNo;
    [SerializeField] private Button btnOk;

    [SerializeField] private GameObject _UpgradeCheckPopup;
    [SerializeField] private GameObject _UpgradeFailPopup;

    public delegate void PopupConfirmFunc();
    PopupConfirmFunc popupConfirmFunc;

    int gold = 5000; // 나중에 지우기

    

    private void Start()
    {
        btnAttack.onClick.AddListener(() => UpgradePopupMsg("Upgrade Attack?", "Attack", BaseUpgrade.Instance.UpgradeBaseAttack));
        btnHp.onClick.AddListener(() => UpgradePopupMsg("Upgrade HP?", "HP", BaseUpgrade.Instance.UpgradeBaseHp));
        btnSpeed.onClick.AddListener(() => UpgradePopupMsg("Upgrade Speed?", "Speed", BaseUpgrade.Instance.UpgradeBaseSpeed));
        btnAttackspeed.onClick.AddListener(() => UpgradePopupMsg("Upgrade Attackspeed?", "Attackspeed", BaseUpgrade.Instance.UpgradeBaseAttackspeed));
        btnDash.onClick.AddListener(() => UpgradePopupMsg("Upgrade Dash?", "Dash", BaseUpgrade.Instance.UpgradeBaseDash));
        btnDrone.onClick.AddListener(() => UpgradePopupMsg("Upgrade Drone?", "Drone", BaseUpgrade.Instance.UpgradeBaseDrone));

        btnYes.onClick.AddListener(() => ConfirmPopup());
        btnNo.onClick.AddListener(() => OffPopup());
        btnOk.onClick.AddListener(() => FailUpgrade());
    }

    private void UpgradePopupMsg(string msg, string option, PopupConfirmFunc popupConfirmFunc)
    {
        var item = BaseUpgrade.Instance.upgradeDictionary[option];
        if (item.upgradeCurrent == item.upgradeMax)
        {
            _UpgradeFailPopup.gameObject.SetActive(true);
            PopupFailText.text = "Max Upgrade";
        }
        else if (item.price > gold)
        {
            _UpgradeFailPopup.gameObject.SetActive(true);
            PopupFailText.text = "No Money";
        }
        else
        {
            this.popupConfirmFunc = popupConfirmFunc;
            _UpgradeCheckPopup.gameObject.SetActive(true);
            PopupText.text = msg;
        }
    }

    private void ConfirmPopup()
    {
        popupConfirmFunc();
        _UpgradeCheckPopup.gameObject.SetActive(false);
    }

    private void OffPopup()
    {
        _UpgradeCheckPopup.gameObject.SetActive(false);
    }

    private void FailUpgrade()
    {
        _UpgradeFailPopup.gameObject.SetActive(false);
    }

}
