using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    public Vector3 MovementDirection;

    [SerializeField] private float WalkSpeed = 2f;
    [SerializeField] private float RunSpeed = 4f;
    [SerializeField] private float JumpForce = 4f;

    private bool CanJump = false;
    private float Speed;
    private Animator Animator;

    void Start()
    {
        Animator = GetComponentInChildren<Animator>();

        Speed = WalkSpeed;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(GetComponent<Collider>().bounds.center, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f);
    }

    void Update()
    {
        // Block movements if input disable
        if (GameObject.FindGameObjectWithTag("GameMode").GetComponent<GameMode>().IsInputEnable)
        {
            PlayerActions PlayerChara = GetComponentInChildren<PlayerActions>();

            float HorizontalInput = Input.GetAxis("Horizontal");

            MovementDirection = new Vector3(HorizontalInput, 0, 0);
            MovementDirection.Normalize();

            if (!PlayerChara.IsAttacking)
            {
                transform.Translate(MovementDirection * Speed * Time.deltaTime, Space.World);

                // Chara movements
                if (MovementDirection != Vector3.zero)
                {
                    if (Input.GetKey(KeyCode.LeftShift)) Speed = RunSpeed;
                    else Speed = WalkSpeed;
                }

                Animator.SetFloat("Speed", Mathf.Abs(MovementDirection.x) * Speed);

                // Jump
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (IsGrounded())
                    {
                        CanJump = true;
                    }
                }

                Animator.SetBool("Jump", CanJump);

            }
        }
    }

    private void FixedUpdate()
    {
        if (CanJump)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * JumpForce, ForceMode.Impulse);
            CanJump = false;
        }
    }
}
