using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public ParticleSystem particles;

    public void OnClick()
    {
        if (particles != null)
        {
            particles.Play();
        }
        // Adicione aqui a lógica para iniciar o jogo, como carregar uma cena ou ativar um menu
        Debug.Log("Start Game Button Clicked!");
    }
}
