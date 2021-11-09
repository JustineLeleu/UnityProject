using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float Health = 100f;
    public bool IsAttacking = false;
    public float AttackType = 0f;
    public GameObject[] Shuriken;
    public bool IsDead = false;
    public bool PlayerDetected;

    [SerializeField] private int NbrAttack;

    private Animator Animator;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Detect when player enter the fight zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PlayerDetected)
        {
            PlayerDetected = true;
            if (gameObject.CompareTag("Enemy")) Animator.SetBool("EnnemyDetected", true);
            other.GetComponentInChildren<Animator>().SetBool("EnnemyDetected", true);

            StartCoroutine("Delay");

            if (gameObject.CompareTag("Boss"))
            {
                GameObject.FindGameObjectWithTag("GameMode").GetComponent<GameMode>().ShowBossBar();
            }
        }
    }

    // Detect when player leave the fight zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && PlayerDetected)
        {
            PlayerDetected = false;
            if (gameObject.CompareTag("Enemy")) Animator.SetBool("EnnemyDetected", false);
            other.GetComponentInChildren<Animator>().SetBool("EnnemyDetected", false);

            if (gameObject.CompareTag("Boss"))
            {
                GameObject.FindGameObjectWithTag("GameMode").GetComponent<GameMode>().HideBossBar();
            }
        }
    }

    // Attack function with delay between each attack
    void Attack ()
    {
        AttackType = Random.Range(0, NbrAttack);
        IsAttacking = true;
        Animator.SetBool("CanAttack", true);
        Animator.SetFloat("Attack", AttackType);
        StartCoroutine("Delay");
    }

    IEnumerator Delay ()
    {
        yield return new WaitForEndOfFrame();
        Animator.SetBool("CanAttack", false);
        if (IsAttacking)
        {
            yield return new WaitForSeconds(Animator.GetCurrentAnimatorClipInfo(0).Length);
            IsAttacking = false;
        }
        yield return new WaitForSeconds(Random.Range(1,2));
        if (PlayerDetected && !IsDead) Attack();
    }

    // Hit function deal with the enemy damages
    public void Hit(float Damage, GameObject Player)
    {
        Health -= Damage;
        if (gameObject.CompareTag("Boss")) GameObject.Find("BossBar").GetComponent<Slider>().value = Health / 100;

        if (Health == 0) 
        {
            IsDead = true;
            if (gameObject.CompareTag("Boss")) Player.GetComponent<PlayerActions>().Score += 50;
            else Player.GetComponent<PlayerActions>().Score += 10;
            Animator.Play("Death");
            Physics.IgnoreCollision(GetComponentInParent<CapsuleCollider>(), Player.GetComponentInParent<CapsuleCollider>(), true);
            Destroy(transform.parent.gameObject, 4);
        }
        else Animator.Play("Reaction");
    }

    // function used only by the boss for his attacks as events in the animations
    private void ShurikenThrow ()
    {
        GameObject Projectile = Instantiate(Shuriken[((int)AttackType)], GetComponentInParent<CapsuleCollider>().bounds.center, Quaternion.Euler(0,90,0));
        Destroy(Projectile, 5);
    }
}
