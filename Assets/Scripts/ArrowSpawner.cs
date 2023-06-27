using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private Transform arrowPrefab;
    [SerializeField] private float instantiateAtDistance = 20f;

    private GameObject player;
    private float shootTimerMax = 3f;
    private float shootTimer;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

        shootTimer = shootTimerMax;
    }

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;

        shootTimer = Mathf.Clamp(shootTimer, 0, shootTimerMax);

        if (DistanceToPlayer() < instantiateAtDistance && shootTimer <= 0)
        {
            Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            shootTimer = shootTimerMax;
        }
    }

    private float DistanceToPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer;
    }
}
