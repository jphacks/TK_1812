using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Board;

namespace Map
{
    public class BoadSet : MonoBehaviour
    {
        private string placeId;
        private string place_name;
        private string icon;

        public void SetUp(string id, string name, string iconUrl)
        {
            placeId = id;
            place_name = name;
            icon = iconUrl;
        }

        public void OnClick()
        {
            BoardManager.UI.setParam(placeId, place_name, icon);
            Debug.Log(place_name + "がクリックされました");
        }
    }
}