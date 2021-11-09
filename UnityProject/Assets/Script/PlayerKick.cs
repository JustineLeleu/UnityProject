using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    // Make sure only one collider is trigger and use the player kick function
    private void OnTriggerEnter(Collider other)
    {
        PlayerActions Player = GetComponentInParent<PlayerActions>();

        if (!other.isTrigger && (other.CompareTag("Enemy") || other.CompareTag("Boss")) && Player.CanHit && other.GetComponentInChildren<Enemy>().Health > 0)
        {
            switch (gameObject.name)
            {
                case "LeftLeg":
                    if (Player.Combo == 1)
                    {
                        Player.Kick(other.GetComponentInChildren<Enemy>());
                        Player.CanHit = false;
                    }
                    break;

                case "RightLeg":
                    if (Player.Combo != 1)
                    {
                        Player.Kick(other.GetComponentInChildren<Enemy>());
                        Player.CanHit = false;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
