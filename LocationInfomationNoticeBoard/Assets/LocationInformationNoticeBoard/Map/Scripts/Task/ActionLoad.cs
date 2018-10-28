using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

namespace Map.Task
{
    public class ActionLoad : MapActionTask
    {
        IEnumerator enumerator;
        protected override string info
        {
            get { return "Load"; }
        }

        protected override void OnExecute()
        {
            MapSceneManager.UI.SetUp();

            if(MapSceneManager.IsDebug)
            {
                enumerator = LoadDebugMapData();
            }
            else
            {
                enumerator = LoadMapData();
            }
        }

        protected override void OnUpdate()
        {
            if (MapSceneManager.Getter.CanGetLonLat())
            {
                if (MapSceneManager.State.IsLoadStartLoacation)
                {
                    if (enumerator != null)
                    {
                        if (!enumerator.MoveNext())
                        {
                            enumerator = null;
                        }
                    }
                    else
                    {
                        EndAction();
                    }

                }
            }
            else
            {
                if (enumerator != null)
                {
                    if (!enumerator.MoveNext())
                    {
                        enumerator = null;
                    }
                }
                else
                {
                    EndAction();
                }

            }


        }

        IEnumerator LoadMapData()
        {
            var trans = MapSceneManager.Getter.Latitude.ToString() + "," + MapSceneManager.Getter.Longitude;

            UriBuilder builder = new UriBuilder("https://maps.googleapis.com/maps/api/place/nearbysearch/json");
            builder.Query += "radius=6000";
            //builder.Query += "&key=" + token;
            builder.Query += "&location=" + trans;
            //builder.Query += "&location=35.7134029,139.7611094";
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
                    foreach(var d in data.results)
                    {
                        MapSceneManager.Data.spotList.Add(d);
                    }
                    //Debug.Log(data.results[0].geometry.location.lat);
                }
            }
        }


        IEnumerator LoadDebugMapData()
        {

            UriBuilder builder = new UriBuilder("https://maps.googleapis.com/maps/api/place/nearbysearch/json");
            builder.Query += "radius=6000";
            //builder.Query += "&key=" + token;
            builder.Query += "&key="+"AIzaSyCwijzoYm8HTkmgM6Em7aLG_tGLX7QqPcg";
            builder.Query += "&location=35.7134029,139.7611094";
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
                        MapSceneManager.Data.spotList.Add(d);
                    }
                    //Debug.Log(data.results[0].geometry.location.lat);
                }
            }
        }

    }
}