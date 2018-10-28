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
        [SerializeField] Text positionText;
        [SerializeField] LoacationGetter loacationGetter;
        [SerializeField] RawImage back;
        [SerializeField] Image loading;

        public void SetActiveLoad(bool value)
        {
            back.gameObject.SetActive(value);
            loading.gameObject.SetActive(value);
        }

        // Use this for initialization
        public void SetUp()
        {
            locationText.text = "0,0";
        }

        public void SetText(string text)
        {
            locationText.text = text;
        }

        public void SetPositionText(string text)
        {
            positionText.text = text;
        }

    }
}
