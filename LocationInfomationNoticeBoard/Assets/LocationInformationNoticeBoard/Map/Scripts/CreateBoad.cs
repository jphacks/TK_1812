using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CreateBoad : MonoBehaviour
    {
        [SerializeField] GameObject boadPrefab;

        public void InstantiateBoad(Result result)
        {
            double latitude = result.geometry.location.lat;
            double longitude = result.geometry.location.lng;

            var map = MapSceneManager.MainMap;

            var location = new Mapbox.Utils.Vector2d(latitude, longitude);
            var localPotision = map.GeoToWorldPosition(location) + boadPrefab.transform.position;

            var obj = Instantiate(boadPrefab, localPotision, boadPrefab.transform.rotation,this.transform);
            var boadSet = obj.GetComponent<BoadSet>();
            boadSet.SetUp(result.place_id,result.name,result.icon);
        }
    }
}