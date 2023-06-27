using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : BasePickup
{
    [SerializeField] private int healAmount = 1;

    protected override void Pickup(GameObject subject)
    {
        subject.GetComponent<HealthSystem>().Heal(healAmount);
    }
}
