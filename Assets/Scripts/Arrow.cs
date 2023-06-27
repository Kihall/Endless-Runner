using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float moveSpeed = 10f;

    private float timeToDie = 10f;

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        timeToDie -= Time.deltaTime;
        if (timeToDie < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}
