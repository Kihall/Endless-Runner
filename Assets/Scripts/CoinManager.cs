using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI coinText;

    private int currentCoinAmount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        coinText.text = " " + currentCoinAmount;
    }

    public void AddCoin(int coinAmount)
    {
        currentCoinAmount += coinAmount;
        UpdateCoinText();
    }

    public static int GetCoinAmount()
    {
        return Instance.currentCoinAmount;
    }
}
