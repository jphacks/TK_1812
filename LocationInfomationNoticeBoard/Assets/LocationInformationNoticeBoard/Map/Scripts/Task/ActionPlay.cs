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
            foreach(var data in MapSceneManager.Data.spotList)
            {
                var getter = MapSceneManager.Getter;
                var location = data.geometry.location;
                MapSceneManager.Boad.InstantiateBoad(data);
                //if(ConvertDistance.Distance(getter.Latitude,getter.Longitude, location.lat, location.lng, 'K') <= 3)
                //{
                //    MapSceneManager.Boad.InstantiateBoad(data);
                //}
            }
            //MapSceneManager.Boad.InstantiateBoad(0, 0);
            //MapSceneManager.Boad.InstantiateBoad(0.03f, 0);
            //var lat1 = 0;
            //var lon1 = 0;
            //var lat2 = 0.03f;
            //var lon2 = 0;

            //var tes = ConvertDistance.Distance(lat1,lon1,lat2,lon2,'K');
            //Debug.Log(tes);
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