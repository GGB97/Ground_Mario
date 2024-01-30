using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Transform Player { get; private set; }
    [SerializeField] private string playerTag = "Player";

    [SerializeField] private TMP_Text waveText;
    [SerializeField] private Slider hpGaugeSlider;
    [SerializeField] private Slider fuelGaugeSlider;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private GameObject upgradeUI;

    private HealthSystem playerHealthSystem;

    private int waveCount;
    private int coinCount;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;

        playerHealthSystem = Player.GetComponent<HealthSystem>();
        playerHealthSystem.OnDamage += UpdateHPUI;
        playerHealthSystem.OnHeal += UpdateHPUI;
    }

    private void UpdateHPUI()
    {
        hpGaugeSlider.value = playerHealthSystem.CurrentHealth / playerHealthSystem.MaxHealth;
    }

    private void UpdateFuelUI()
    {
        fuelGaugeSlider.value = playerHealthSystem.CurrentHealth / playerHealthSystem.MaxHealth;
    }

    private void UpdateWaveUI()
    {
        waveText.text = waveCount.ToString();
    }

    private void UpdateCoinUI()
    {
        coinText.text = coinCount.ToString();
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
}
