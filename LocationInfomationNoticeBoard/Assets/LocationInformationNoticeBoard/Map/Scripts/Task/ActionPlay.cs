using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

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

            var getter = MapSceneManager.Getter;
            foreach(var data in MapSceneManager.Data.spotList)
            {
                var location = data.geometry.location;
                if(MapSceneManager.IsDebug)
                {
                    if (ConvertDistance.Distance(35.7134029f, 139.7611094f, location.lat, location.lng, 'K') <= 3)
                    {
                        MapSceneManager.Boad.InstantiateBoad(data);
                    }
                }
                else
                {
                    if (ConvertDistance.Distance(getter.Latitude, getter.Longitude, location.lat, location.lng, 'K') <= 3)
                    {
                        MapSceneManager.Boad.InstantiateBoad(data);
                    } 
                }
            }

            if (MapSceneManager.IsDebug)
            {
                MapSceneManager.Data.beforeLat = 35.7134029f;
                MapSceneManager.Data.beforeLng = 139.7611094f;
            }
            else
            {
                MapSceneManager.Data.beforeLat = getter.Latitude;
                MapSceneManager.Data.beforeLng = getter.Longitude;
            }

            MapSceneManager.UI.SetActiveLoad(false);
        }

        protected override void OnUpdate()
        {
            if(MapSceneManager.IsDebug)
            {
                var data = MapSceneManager.Data;
                var location = MapSceneManager.MainMap.WorldToGeoPosition(MapSceneManager.Player.gameObject.transform.position);
                if (ConvertDistance.Distance(location.x, location.y, data.beforeLat, data.beforeLng, 'K') >= 3)
                {
                    data.beforeLat = location.x;
                    data.beforeLng = location.y;
                    enumerator = LoadMapData(location);
                }
            }
            if (MapSceneManager.Getter.CanGetLonLat())
            {
                string lonLatInfoTemplate = "緯度: {0}\n経度: {1}";
                MapSceneManager.UI.SetText(string.Format(lonLatInfoTemplate, (float)MapSceneManager.Getter.Latitude, (float)MapSceneManager.Getter.Longitude));
                MapSceneManager.Player.PlayerTransUpdate();

                var getter = MapSceneManager.Getter;
                var data = MapSceneManager.Data;
                //前に更新した時より3km以上離れていたら更新する
                if (ConvertDistance.Distance(getter.Latitude, getter.Longitude, data.beforeLat, data.beforeLng, 'K') >= 3)
                {
                    data.beforeLat = getter.Latitude;
                    data.beforeLng = getter.Longitude;
                    var vec = new Mapbox.Utils.Vector2d(getter.Latitude, getter.Longitude);
                    enumerator = LoadMapData(vec);
                }
            }

            if(enumerator != null)
            {
                if(!enumerator.MoveNext())
                {
                    enumerator = null;
                }
            }
        }

        IEnumerator LoadMapData(Mapbox.Utils.Vector2d vec)
        {
            var trans = vec.x.ToString() + "," + vec.y;

            UriBuilder builder = new UriBuilder("https://maps.googleapis.com/maps/api/place/nearbysearch/json");
            builder.Query += "radius=6000";
            //builder.Query += "&key=" + token;
            builder.Query += "&location=" + trans;
            builder.Query += "&type=restaurant";
            var uri = builder.Uri;
            Debug.Log(uri);

            using (UnityWebRequest www = UnityWebRequest.Get(uri))
            {
                var sendRequest = www.SendWebRequest();
                while (!sendRequest.isDone) { yield return null; }

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    // Show results as text
                    //Debug.Log(www.downloadHandler.text);

                    var resultsString = www.downloadHandler.text;

                    var data = JsonUtility.FromJson<Response>(resultsString);
                    foreach (var d in data.results)
                    {
                        if(!MapSceneManager.Data.spotList.Contains(d))
                        {
                            MapSceneManager.Data.spotList.Add(d);
                        }
                    }

                    //spotlistに更新をかける(現在地より6km以上離れた要素は削除する)
                    UpdateSpotList(vec);
                }
            }
        }


        public void UpdateSpotList(Mapbox.Utils.Vector2d vec)
        {
            var spots = MapSceneManager.Data.spotList;
            var removeList = new List<Result>();
            foreach(var s in spots)
            {
                var distance = ConvertDistance.Distance(vec.x, vec.y, s.geometry.location.lat, s.geometry.location.lng, 'K');
                if(distance > 6)
                {
                    removeList.Add(s);
                }
                if(distance <= 3)
                {
                    MapSceneManager.Boad.InstantiateBoad(s);
                }
                if(distance > 3)
                {
                    MapSceneManager.Boad.DestroyBoad(s);
                }
            }

            //最後にリストから消去
            foreach(var r in removeList)
            {
                spots.Remove(r);
            }
        }
    }
}