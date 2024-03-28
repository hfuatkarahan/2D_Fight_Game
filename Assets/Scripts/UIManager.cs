using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerText, roundStartText, roundEndText;
    public int timer = 90, roundNo = 1;
    public GameObject levelEndPanel, playerOne, playerTwo;

    // Start is called before the first frame update
    void Start()
    {
        roundNo = PlayerPrefs.GetInt("RoundNumber",1);
        playerOne.GetComponent<Ajan>().enabled = false;
        playerTwo.GetComponent<Ajan>().enabled = false;
        levelEndPanel.SetActive(false);
        timerText.text = timer.ToString();
        StartCoroutine(RoundStart());
    }

    IEnumerator RoundStart()
    {
        roundStartText.text = "Round " + roundNo.ToString();
        roundStartText.gameObject.transform.DOScale(new Vector2(1.5f, 1.5f), 1);
        yield return new WaitForSeconds(1.5f);
        roundStartText.gameObject.transform.localScale = Vector2.one;
        roundStartText.gameObject.transform.DOScale(Vector2.one * 1.2f, 1f);
        roundStartText.text = "3";
        yield return new WaitForSeconds(1f);
        roundStartText.gameObject.transform.DOScale(Vector2.one * 1.2f, 1f);
        roundStartText.text = "2";
        roundStartText.gameObject.transform.DOScale(Vector2.one * 1.2f, 1f);
        yield return new WaitForSeconds(1f);
        roundStartText.text = "1";
        roundStartText.gameObject.transform.DOScale(Vector2.one * 1.2f, 1f);
        yield return new WaitForSeconds(1f);
        roundStartText.text = "Fight";
        roundStartText.gameObject.transform.DOScale(Vector2.one * 1.5f, 1f);
        yield return new WaitForSeconds(0.5f);
        roundStartText.text = "";
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        playerOne.GetComponent<Ajan>().enabled = true;
        playerTwo.GetComponent<Ajan>().enabled = true;
        yield return new WaitForSeconds(1);
        timer--;
        timerText.text = timer.ToString();
        if (timer < 4)
        {
            timerText.color = Color.red;
            timerText.gameObject.transform.DOScale(Vector2.one * 1.2f, 1f);
        }
        if (timer == 0)
        {
            timer = 0;
            levelEndPanel.SetActive(true);
            playerOne.GetComponent<Ajan>().enabled = false;
            playerTwo.GetComponent<Ajan>().enabled = false;
            StartCoroutine(RoundWinnerCheck());
        }
        else
        {
            StartCoroutine(Timer());
        }
    }

    IEnumerator RoundWinnerCheck()
    {
        yield return new WaitForSeconds(0);
        
        if (playerOne.GetComponent<Ajan>().health > playerTwo.GetComponent<Ajan>().health)
        {
            roundEndText.text = "Player 1 Wins!";
        }
        else if (playerOne.GetComponent<Ajan>().health == playerTwo.GetComponent<Ajan>().health)
        {
            roundEndText.text = "DRAW!";
        }
        else if (playerOne.GetComponent<Ajan>().health < playerTwo.GetComponent<Ajan>().health)
        {
            roundEndText.text = "Player 2 Wins!";
        }

        yield return new WaitForSeconds(2);
        
        if (roundNo == 2)
        {
            roundEndText.text = "GAME OVER";
            yield return new WaitForSeconds(1);
            PlayerPrefs.SetInt("RoundNumber", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (roundNo == 1)
        {
            roundNo++;
            PlayerPrefs.SetInt("RoundNumber", roundNo);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }   
}
