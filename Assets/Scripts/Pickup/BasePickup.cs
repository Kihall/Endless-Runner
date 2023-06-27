using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other.gameObject);
        }

        Destroy(gameObject);
    }

    protected abstract void Pickup(GameObject subject);
}
