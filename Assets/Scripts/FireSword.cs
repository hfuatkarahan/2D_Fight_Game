using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireSword : MonoBehaviour
{
    public int fireSpeed = 10;
    public string enemyTag;


    void Update()
    {
        if (enemyTag == "Player1")
        {
            if (GameObject.FindGameObjectWithTag(enemyTag).GetComponent<Ajan>().onLeft)
            {
                transform.Translate(Vector3.left * Time.deltaTime * fireSpeed);
            }
            else if (GameObject.FindGameObjectWithTag(enemyTag).GetComponent<Ajan>().onRight)
            {
                transform.Translate(Vector3.right * Time.deltaTime * fireSpeed);
            }
            
        }
        else if (enemyTag == "Player2")
        {
            if (GameObject.FindGameObjectWithTag(enemyTag).GetComponent<Ajan>().onLeft)
            {
                transform.Translate(Vector3.left * Time.deltaTime * fireSpeed);
            }
            else if (GameObject.FindGameObjectWithTag(enemyTag).GetComponent<Ajan>().onRight)
            {
                transform.Translate(Vector3.right * Time.deltaTime * fireSpeed);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.CompareTag(enemyTag))
        {
            if (collision.gameObject.GetComponent<Ajan>().health > 0)
            {
                if (collision.gameObject.GetComponent<Ajan>().onBlock)
                {
                    collision.gameObject.GetComponent<Ajan>().health -= 2;
                }
                else
                {
                    collision.gameObject.GetComponent<Ajan>().health -= 15;
                    collision.GetComponent<Animator>().SetTrigger("Hurt");
                }
                if (collision.gameObject.GetComponent<Ajan>().health <= 0)
                {
                    collision.GetComponent<Animator>().SetBool("Death", true);
                    this.GameObject().SetActive(false);
                }
            }
        }

        if (collision.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

}
