using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthSystem player = other.GetComponent<HealthSystem>();
        if (player != null)
        {
            player.Damage(10);
            GameOverUI.Instance.Show();
            Time.timeScale = 0;
        }
    }
}
