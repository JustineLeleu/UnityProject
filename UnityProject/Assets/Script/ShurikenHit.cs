using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenHit : MonoBehaviour
{
    // Use the player hit function when projectile hit the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetType() == typeof(CapsuleCollider))
        {
            PlayerActions Player = other.GetComponentInChildren<PlayerActions>();
            Player.Hit(10, 0, GameObject.FindGameObjectWithTag("Boss").GetComponentInChildren<Enemy>());

            Destroy(gameObject);
        }
    }
}
