using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;


namespace Screens
{
    public enum ScreenType
    {
        Panel,
        InfoPanel,
        Shop,
        MainMenu,
        Game,
        Pause,
        Inventory,
        GameOver
    }


    public class ScreenBase : MonoBehaviour
    {
        public ScreenType screenType;

        public List<Transform> listOfObjects;
        public List<Typper> listOfPhrases;

        public bool isActive = false;

        [Header("Animation Settings")]
        public float animationDuration = 0.4f;
        public float delayBetweenObjects = 0.2f;
        public Ease animationEase = Ease.OutBack;


        void Start()
        {
            if(!isActive) HideObjects();
        }

        [Button("Show Screen")]     // Botão para mostrar a tela no editor gerado pelo NaughtyAttributes
        public virtual void Show()
        {
            ShowObjects();
           // isActive = true;
            //gameObject.SetActive(true);
            Debug.Log("Showing screen: " + screenType.ToString());
        }

        [Button("Hide Screen")]
        public virtual void Hide()
        {
            HideObjects();
            //gameObject.SetActive(false);
            Debug.Log("Hiding screen: " + screenType.ToString());
        }

        private void HideObjects()
        {
            listOfObjects.ForEach(obj => obj.gameObject.SetActive(false));
           // isActive = false;
        }

        private void ShowObjects()
        {
            for(int i = 0; i < listOfObjects.Count ; i++)
            {
                var obj = listOfObjects[i];

                obj.gameObject.SetActive(true);
                obj.DOScale(0, animationDuration).From().SetDelay(i * delayBetweenObjects).SetEase(animationEase);
            }

            Invoke(nameof(StartType), listOfObjects.Count * delayBetweenObjects + animationDuration); // Inicia a digitação após a animação de entrada terminar
        }

        private void StartType()
        {
            for(int i = 0; i < listOfPhrases.Count ; i++)
            {
                listOfPhrases[i].StartTyping();
            }
        }   

        private void ForceShowObjects()
        {
            listOfObjects.ForEach(obj => obj.gameObject.SetActive(true));
        }
    }
}