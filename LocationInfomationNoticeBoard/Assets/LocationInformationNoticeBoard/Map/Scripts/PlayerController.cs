using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] Mapbox.Unity.Map.AbstractMap maps;

        [SerializeField] LoacationGetter locationGetter;

        private Vector3 offset;
        // Use this for initialization
        public void Setup()
        {
            //maps.Options.locationOptions.latitudeLongitude = string.Format("{0},{1}", locationGetter.Latitude, locationGetter.Latitude);
            //var lo = string.Format("{0},{1}", 139.7621, 35.71447);
            //Debug.Log("tes:" + lo);
            //maps.Options.locationOptions.latitudeLongitude = lo;
            Mapbox.Utils.Vector2d location;
            if(locationGetter.CanGetLonLat())
            {
                Debug.Log("マップ生成");
                location = new Mapbox.Utils.Vector2d(locationGetter.Latitude, locationGetter.Longitude);
            }
            else
            {
                location = new Mapbox.Utils.Vector2d(0, 0);
            }

            maps.Initialize(location,15);
            maps.UpdateMap(location);
           
            //Mapbox.Utils.Vector2d location = new Mapbox.Utils.Vector2d(locationGetter.Latitude, locationGetter.Longitude);
            var tes = maps.GeoToWorldPosition(location);

            offset = transform.localPosition;
        }

        // Update is called once per frame
        public void PlayerTransUpdate()
        {
            var map = MapSceneManager.MainMap;
            var location = new Mapbox.Utils.Vector2d(locationGetter.Latitude, locationGetter.Longitude);
            transform.localPosition = map.GeoToWorldPosition(location) + offset;

            string positionTextTemplate = "x: {0}\ny: {1}";
            MapSceneManager.UI.SetPositionText(string.Format(positionTextTemplate, transform.position.x, transform.position.y));
        }
    }
}