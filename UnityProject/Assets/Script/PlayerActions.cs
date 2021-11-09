using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    public bool IsAttacking = false;
    public bool CanHit = false;
    public float NextCombo = 0;
    public float Combo = 0;
    public float Health = 100f;
    public float Score = 0;
    public bool IsDead = false;

    [SerializeField] private float RotateSpeed = 720f;

    private Animator Animator;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Block the movements if input disable
        if (GameObject.FindGameObjectWithTag("GameMode").GetComponent<GameMode>().IsInputEnable)
        {
            // Deal with the chara rotation to avoid a camera rotation
            PlayerControlls Player = GetComponentInParent<PlayerControlls>();
            Vector3 MovementDirection = Player.MovementDirection;

            if (!IsAttacking)
            {
                if (MovementDirection != Vector3.zero)
                {
                    Animator.SetBool("Left", MovementDirection.x < 0);
                    Quaternion ToRotation = new Quaternion(0, -MovementDirection.x, 0, 1);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, ToRotation, RotateSpeed * Time.deltaTime);
                }
            }

            // Attack
            if (Input.GetKeyDown(KeyCode.E))
            {
                Animator.SetBool("Attack", true);

                if (IsAttacking && Combo < 2)
                {
                    if (NextCombo == Combo) NextCombo += 1;
                }
                else
                {
                    if (NextCombo == Combo) NextCombo = 0;
                }
            }
        }
            // UI update
            GameObject.Find("HealthBar").GetComponent<Slider>().value = Health / 100;
            GameObject.Find("Score").GetComponent<Text>().text = Score.ToString("000");
    }
    
    // Damage function for both player and enemy
    public void Kick(Enemy enemy)
    {
        if (enemy.CompareTag("Boss")) enemy.Hit(25, gameObject);
        else enemy.Hit(50, gameObject);
    }

    public void Hit (float Damage, float Reaction, Enemy Enemy)
    {
        Health -= Damage;

        if (Health == 0)
        {
            IsDead = true;
            Animator.Play("Death");
            Enemy.PlayerDetected = false;
        }
        else
        {
            Animator.SetFloat("Reaction", Reaction);
            Animator.Play("Reaction");
        }
    }
}
