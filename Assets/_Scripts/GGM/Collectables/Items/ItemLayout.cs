using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GGM.Item
{
    public class ItemLayout : MonoBehaviour
    {
        public Image UiIcon;
        public TextMeshProUGUI uiValue;

        private ItemSetup _currentSetup;
        public void Load(ItemSetup setup)
        {
            _currentSetup = setup;
            UpdateUi();
        }

        private void UpdateUi()
        {
            UiIcon.sprite = _currentSetup.UiIcon;
        }

        private void Update()
        {
            uiValue.text = _currentSetup.soInt.value.ToString();
        }
    }

}