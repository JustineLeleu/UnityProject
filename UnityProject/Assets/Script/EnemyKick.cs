using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKick : MonoBehaviour
{
    // Make sure only one collider is trigger and use the player hit function
    private void OnTriggerEnter(Collider other)
    {
        Enemy Enemy = GetComponentInParent<Enemy>();

        if (!other.isTrigger && other.CompareTag("Player") && Enemy.IsAttacking && other.GetComponentInChildren<PlayerActions>().Health > 0)
        {
            PlayerActions Player = other.GetComponentInChildren<PlayerActions>();

            switch (gameObject.name)
            {
                case "mixamorig:LeftLeg":
                    if (Enemy.AttackType == 0)
                    {
                        Player.Hit(25, 0, Enemy);
                        Enemy.IsAttacking = false;
                    }
                    break;

                case "mixamorig:RightHand":
                    if (Enemy.AttackType == 1)
                    {
                        Player.Hit(25, 1, Enemy);
                        Enemy.IsAttacking = false;
                    }
                    break;

                case "mixamorig:LeftHand":
                    if (Enemy.AttackType == 2)
                    {
                        Player.Hit(25, 0, Enemy);
                        Enemy.IsAttacking = false;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
