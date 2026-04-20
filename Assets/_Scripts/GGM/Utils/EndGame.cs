using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public string tagToCompare = "Player";

    public GameObject endGameScreen; // UI a ser exibida ao encerrar o jogo


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagToCompare))
        {
            callEndGame();
        }
    }
    

    public void callEndGame()
    {
        // Lógica para encerrar o jogo, como mostrar uma tela de vitória ou carregar uma nova cena
        //Debug.Log("Jogo Encerrado!");
        if (endGameScreen != null)
        {
            endGameScreen.SetActive(true); // Ativa a tela de fim de jogo
            Time.timeScale = 0f;    // Pausa o TEMPO QUANDO O JOGO TERMINA

        }
    }
}
