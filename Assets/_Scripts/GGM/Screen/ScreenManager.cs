using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;

namespace Screens
{
    public class ScreenManager : Singleton<ScreenManager>
    {
        public List<ScreenBase> screens;

        public ScreenType startScreen;  // = ScreenType.MainMenu;

        private ScreenBase _currentScreen;

        // void Awake()
        // {
        // }

        void Start()
        {
            hideAllScreens();
            showByType(startScreen);
            
        }

        public void showByType(ScreenType type)
        {
            // screens.ForEach(screen =>
            // {
            //     if (screen.screenType == type)
            //         screen.Show();
            //     else
            //         screen.Hide();
            // });

            if(_currentScreen != null) _currentScreen.Hide();

            var nextScreen = screens.Find(screen => screen.screenType == type);
            if (nextScreen != null && nextScreen != _currentScreen)
            {
                _currentScreen?.Hide(); // Esconde a tela atual, se houver
                nextScreen.Show();
                _currentScreen = nextScreen;
            }
            //    screens.ForEach(screen =>
            //     {
            //         if (screen.screenType != type)
            //             screen.Hide();
            //     });
            // }
            else
            {
                Debug.LogWarning("Screen of type " + type.ToString() + " not found!");
            }
        }

        public void hideAllScreens()
        {
            screens.ForEach(screen => screen.Hide());
        }
    }
}
