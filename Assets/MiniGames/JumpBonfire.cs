using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBonfire : MonoBehaviour
{
    Rigidbody2D rb;
    public int altura;
    public bool isJumping = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isJumping == false)
        {
            Vector2 v = new Vector2(0, altura);
            isJumping = true;
            rb.AddForce(v);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            isJumping = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fogueira"))
        {
            BonfireMiniGame.Instance.EndMinigame();
        }
        else
        {
            BonfireMiniGame.Instance.BonfireJumped();
        }
    }
}
