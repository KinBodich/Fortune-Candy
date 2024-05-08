using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class InfoUI : MonoBehaviour
    {
        public void ShowPanel()
        {
            gameObject.SetActive(true);
        }

        public void HidePanel()
        {
            gameObject.SetActive(false);
        }
    }
}