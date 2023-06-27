using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : BasePickup
{
    [SerializeField] private int coinAmount = 1;

    protected override void Pickup(GameObject subject)
    {
        CoinManager.Instance.AddCoin(coinAmount);
    }
}
