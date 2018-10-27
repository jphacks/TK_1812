using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Task
{
    public class ActionPlay : MapActionTask
    {
        IEnumerator enumerator;
        protected override string info
        {
            get { return "Play"; }
        }

        protected override void OnExecute()
        {
            MapSceneManager.Player.Setup();
        }

        protected override void OnUpdate()
        {
            if (MapSceneManager.Getter.CanGetLonLat())
            {
                string lonLatInfoTemplate = "緯度: {0}\n経度: {1}";
                MapSceneManager.UI.SetText(string.Format(lonLatInfoTemplate, (float)MapSceneManager.Getter.Latitude, (float)MapSceneManager.Getter.Longitude));
                MapSceneManager.Player.PlayerTransUpdate();
            }
        }
    }
}