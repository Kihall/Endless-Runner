using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerHealthText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerHealthText;

    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
    }

    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;

        UpdateText();
    }

    private void UpdateText()
    {
        playerHealthText.text = " " + healthSystem.GetHealthAmount();
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        UpdateText();
    }

    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        UpdateText();
    }
}
