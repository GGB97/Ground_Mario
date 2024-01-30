using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Transform Player { get; private set; }

    [SerializeField] private TMP_Text waveText;
    [SerializeField] private Slider hpGaugeSlider;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject gamOverObj;

    private Resource_Data resource_Data;
    private HealthSystem playerHealthSystem;

    private void Awake()
    {
        instance = this;
        gamOverObj.SetActive(false);
    }

    private void Start()
    {
        var playerObj = GameManager.Instance.playerBase;
        resource_Data = GameManager.Instance.player.GetComponent<PlayerResourceController>()._data;
        playerHealthSystem = playerObj.GetComponent<HealthSystem>();
        playerHealthSystem.OnDamage += UpdateHPUI;
        playerHealthSystem.OnHeal += UpdateHPUI;

        UpdateCoinUI();
        UpdateWaveUI();

        GameManager.Instance.playerBase.GetComponent<HealthSystem>().OnDeath += OnDeathUI;
    }

    private void UpdateHPUI()
    {
        hpGaugeSlider.value = playerHealthSystem.CurrentHealth / playerHealthSystem.MaxHealth;
    }

    public void UpdateWaveUI()
    {
        waveText.text = GameManager.Instance.WaveCnt.ToString();
    }

    public void UpdateCoinUI()
    {
        coinText.text = resource_Data.coin.ToString();
    }

    public void OnClickUpgradeBtn()
    {
        if (upgradeUI.activeSelf)
        {
            upgradeUI.SetActive(false);
        }
        else
        {
            upgradeUI.SetActive(true);
        }
    }

    void OnDeathUI()
    {
        gamOverObj.SetActive(true);
    }

    public void ReTryBtn()
    {
        SceneManager.LoadScene("GameOver"); // 시작 씬으로 변경 예정
    }
}
