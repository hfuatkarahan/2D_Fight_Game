using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Ajan : MonoBehaviour
{
    public Animator animator;
    public int runSpeed = 10, jumpForce = 350;
    public Rigidbody2D rb;
    public GameObject sampleFireSword;
    public bool player1, player2, onBlock, onGround, onLeft, onRight;
    public int health = 100;
    public Image healthBar;

    void Start()
    {
        if (this.gameObject.CompareTag("Player1"))
        {
            player1 = true;
            transform.GetChild(0).GetComponent<NearAttack>().player1 = true;
            transform.GetChild(0).GetComponent<NearAttack>().enemy = GameObject.FindGameObjectWithTag("Player2");
        }

        if (this.gameObject.CompareTag("Player2"))
        {
            player2 = true;
            transform.GetChild(0).GetComponent<NearAttack>().player2 = true;
            transform.GetChild(0).GetComponent<NearAttack>().enemy = GameObject.FindGameObjectWithTag("Player1");
        }
    }

    void AnimationReset(string animName)
    {
        animator.SetBool("Run", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Crouch", false);
        animator.SetBool("Block", false);
        animator.SetBool(animName, true);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)health / 100.0f;
        #region Player1Controller

        if (player1)
        { 
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                AnimationReset("Run");
                Player1NormalCollider();
                onBlock = false;
            }

            if (Input.GetKeyDown(KeyCode.W) && onGround)
            {
                animator.SetTrigger("Jump");
                rb.AddForce(Vector2.up * jumpForce);
                animator.SetBool("Crouch", false);
                Player1NormalCollider();
                onBlock = false;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                AnimationReset("Crouch");
                Player1CrouchCollider();
                onBlock = false;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Attack");
                Player1NormalCollider();
                onBlock = false;
                Invoke(nameof(Attack), 0.25f);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                animator.SetTrigger("DashAttack");
                Player1NormalCollider();
                onBlock = false;
                if (transform.GetChild(0).GetComponent<NearAttack>().enemyNear)
                {
                    transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Ajan>().health -= 20;
                    if (transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Ajan>().health <= 0)
                    {
                        transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Animator>().SetBool("Death", true);
                    }
                    else
                    {
                        transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Animator>().SetTrigger("Hurt");
                    }
                }
            }

            if (Input.anyKey == false)
            {
                AnimationReset("Idle");
                Player1NormalCollider();
                onBlock = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                AnimationReset("Block");
                Player1NormalCollider();
                onBlock = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime * runSpeed);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime * runSpeed);
            }
        }
        #endregion

        #region Player2Controller

        if (player2)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                AnimationReset("Run");
                Player2NormalCollider();
                onBlock = false;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && onGround)
            {
                animator.SetTrigger("Jump");
                rb.AddForce(Vector2.up * jumpForce);
                animator.SetBool("Crouch", false);
                Player2NormalCollider();
                onBlock = false;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                AnimationReset("Crouch");
                Player2CrouchCollider();
                onBlock = false;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                animator.SetTrigger("Attack");
                Player2NormalCollider();
                onBlock = false;
                Invoke(nameof(Attack), 0.25f);
            }

            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                animator.SetTrigger("DashAttack");
                Player2NormalCollider();
                onBlock = false;
                if (transform.GetChild(0).GetComponent<NearAttack>().enemyNear)
                {
                    transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Ajan>().health -= 20;
                    if (transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Ajan>().health <= 0)
                    {
                        transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Animator>().SetBool("Death",true);
                    }
                    else
                    {
                        transform.GetChild(0).GetComponent<NearAttack>().enemy.GetComponent<Animator>().SetTrigger("Hurt");
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                AnimationReset("Block");
                Player2NormalCollider();
                onBlock = true;
            }

            if (Input.anyKey == false)
            {
                AnimationReset("Idle");
                Player2NormalCollider();
                onBlock = false;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * Time.deltaTime * runSpeed);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * Time.deltaTime * runSpeed);
            }
        }
        #endregion
    }

    void Attack()
    {
        GameObject newFireSword = Instantiate(sampleFireSword, transform.position, Quaternion.identity);
        newFireSword.SetActive(true);
        if (player1)
        {
            newFireSword.GetComponent<FireSword>().enemyTag = "Player2";
        }
        else if (player2)
        {
            newFireSword.GetComponent<FireSword>().enemyTag = "Player1";
        }
    }

    public void Player1CrouchCollider()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(0.36f,0.64f);
        GetComponent<BoxCollider2D>().offset = new Vector2(0.03f, -0.23f);
    }

    public void Player1NormalCollider()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(0.36f, 0.9f);
        GetComponent<BoxCollider2D>().offset = new Vector2(0.03f, -0.09f);
    }
    public void Player2CrouchCollider()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(0.78f, 0.8f);
        GetComponent<BoxCollider2D>().offset = new Vector2(0.08f, 0.46f);
    }

    public void Player2NormalCollider()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(0.78f, 1.22f);
        GetComponent<BoxCollider2D>().offset = new Vector2(0.08f, 0.66f);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
