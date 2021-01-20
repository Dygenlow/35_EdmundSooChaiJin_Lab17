using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private float jumpForce = 6.0f;
    private float gravityModifier = 2.5f;

    public int healthCount;
    public int coinCount;

    bool onGround = true;

    private Rigidbody2D playerRb;
    private Animator animator;
    public Text health;
    public Text coin;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;

        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LeftKey();
        RightKey();
        JumpKey();

        health.GetComponent<Text>().text = "Health: " + healthCount;
        coin.GetComponent<Text>().text = "Coin: " + coinCount;
    }

    private void LeftKey()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * -moveSpeed);
            transform.rotation = Quaternion.Euler(0, 180, 0);

            animator.SetBool("isMoving", true);
        }

        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void RightKey()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Euler(0, 0, 0);

            animator.SetBool("isMoving", true);
        }

        if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void JumpKey()
    {
        if(onGround && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

            animator.SetTrigger("JumpTrigger");

            onGround = false;
        }
    }

    private void OnCollisionEnter2D (Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }

        if(collision2D.gameObject.CompareTag("Mace"))
        {
            healthCount -= 10;
        }

        if(collision2D.gameObject.CompareTag("Coin"))
        {
            coinCount += 1;

            Destroy(collision2D.gameObject);
        }
    }
}
