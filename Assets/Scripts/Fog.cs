using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Fog : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float fogDeathTimerMax = 3f;
    [SerializeField] private TextMeshProUGUI fogDeathTimerText;
    [SerializeField] private TextMeshProUGUI fogDistanceText;

    private float fogDeathTimer;
    private bool inFog;
    private HealthSystem player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
        fogDeathTimer = fogDeathTimerMax;
        fogDeathTimerText.gameObject.SetActive(false);
    }

    private void Update()
    {
        FogMovingToRight();
        PlayerInFog();
        SetFogDistanceToPlayerText();
    }

    private void SetFogDistanceToPlayerText()
    {
        float fogDistanceToPlayer = Vector3.Distance(player.transform.position, transform.position) - 50;

        if (fogDistanceToPlayer > 50)
        {
            fogDistanceText.text = "Fog Is Coming " + String.Format("{0:0.0}", fogDistanceToPlayer);
        }
        else
        {
            fogDistanceText.text = "Behind You!";
        }
    }

    private void PlayerInFog()
    {
        if (inFog)
        {
            fogDeathTimer -= Time.deltaTime;
            fogDeathTimerText.text = String.Format("{0:0.0}", fogDeathTimer);
            if (fogDeathTimer <= 0)
            {
                player.Damage(10);
                fogDeathTimerText.gameObject.SetActive(false);
            }
        }
    }

    private void FogMovingToRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            inFog = true;
            fogDeathTimerText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            inFog = false;
            fogDeathTimer = fogDeathTimerMax;
            fogDeathTimerText.gameObject.SetActive(false);
        }
    }
}
