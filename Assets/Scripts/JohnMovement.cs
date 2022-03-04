using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    [HideInInspector]
    public ControllerType controller;
    public float Speed;
    public float JumpForce;
    public GameObject BulletPrefab;
    public GameManagment managment;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    public int Health = 5;

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Animator.SetBool("running", Horizontal != 0.0f);

        // Detectar Suelo
        // Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;

        // Salto
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        // Disparar
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }

        // Si cae pierde la vida
        if(Rigidbody2D.position.y < -2f)
        {
            managment.GameEnd();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    //Mov
    public void Move()
    {
        if(controller == ControllerType.PC)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
        }     

        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);

        Flip();
    }

    public void Flip()
    {
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void Jump()
    {
        if (Grounded)
        {
            Rigidbody2D.AddForce(Vector2.up * JumpForce);
        }      
    }

    public void Shoot()
    {
        if(Time.time > LastShoot + 0.25f)
        {
            Vector3 direction;
            if (transform.localScale.x == 1.0f) direction = Vector3.right;
            else direction = Vector3.left;

            GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
            bullet.GetComponent<BulletScript>().SetDirection(direction);

            LastShoot = Time.time;
        }
        
    }

    public void HorizontalMovement(int value)
    {
        Horizontal = (float) value;
    }

    public void Hit()
    {
        Health -= 1;
        if (Health == 0)
        {
            managment.GameEnd();
        }
    }
}