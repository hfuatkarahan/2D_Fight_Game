using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerOnePos, playerTwoPos;


    void Update()
    {
        if (playerOnePos.position.x < playerTwoPos.position.x)
        {
            playerOnePos.localScale = new Vector2(3, 3);
            playerOnePos.gameObject.GetComponent<Ajan>().onLeft = true;
            playerOnePos.gameObject.GetComponent<Ajan>().onRight = false;

            playerTwoPos.localScale = new Vector2(2, 2);
            playerTwoPos.gameObject.GetComponent<Ajan>().onLeft = false;
            playerTwoPos.gameObject.GetComponent<Ajan>().onRight = true;
        }
        else
        {
            playerOnePos.localScale = new Vector2(-3, 3);
            playerOnePos.gameObject.GetComponent<Ajan>().onLeft = false;
            playerOnePos.gameObject.GetComponent<Ajan>().onRight = true;

            playerTwoPos.localScale = new Vector2(-2, 2);
            playerTwoPos.gameObject.GetComponent<Ajan>().onLeft = true;
            playerTwoPos.gameObject.GetComponent<Ajan>().onRight = false;
        }
    }
}
