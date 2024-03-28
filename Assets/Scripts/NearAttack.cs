using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NearAttack : MonoBehaviour
{
    public bool enemyNear;
    public bool player1, player2;

    public GameObject enemy;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (player1 && collision.CompareTag("Player2"))
        {
            enemyNear = true;
        }
        else if (player2 && collision.CompareTag("Player1"))
        {
            enemyNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (player1 && collision.CompareTag("Player2"))
        {
            enemyNear = false;
        }
        else if (player2 && collision.CompareTag("Player1"))
        {
            enemyNear = false;
        }
    }
}
