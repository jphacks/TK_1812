using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class UICanvas : MonoBehaviour
    {
        private const string lonLatInfoTemplate = "緯度: {0}\n経度: {1}";

        [SerializeField] Text locationText;

        [SerializeField] LoacationGetter loacationGetter;

        // Use this for initialization
        public void SetUp()
        {
            locationText.text = "0,0";
        }

        public void SetText(string text)
        {
            locationText.text = text;
        }
    }
}
