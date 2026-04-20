using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typper : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float timeBetweenLetters = 0.05f; // Velocidade de digitação

    public string textToType; // Texto a ser digitado

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }


    // Update is called once per frame
    private void Awake() 
    {
        textMesh.text = ""; // Limpa o texto no início    
    }

    [NaughtyAttributes.Button("Start Typing")]
    public void StartTyping()
    {
        StartCoroutine(type(textToType));
    }

    IEnumerator type(string s)
    {
        // Implementation for typing effect
        textMesh.text = ""; // Limpa o texto antes de começar a digitar
        foreach (char letter in s.ToCharArray())
        {
            textMesh.text += letter; // Adiciona a letra atual ao texto
            yield return new WaitForSeconds(timeBetweenLetters); // Espera um pouco antes de adicionar a próxima letra
        }

    }
}
