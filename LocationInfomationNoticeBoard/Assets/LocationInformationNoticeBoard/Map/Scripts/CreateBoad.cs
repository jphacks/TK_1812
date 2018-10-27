using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CreateBoad : MonoBehaviour
    {
        [SerializeField] GameObject boadPrefab;

        public void InstantiateBoad(float latitude,float longitude)
        {
            var map = MapSceneManager.MainMap;

            var location = new Mapbox.Utils.Vector2d(latitude, longitude);
            var localPotision = map.GeoToWorldPosition(location) + boadPrefab.transform.position;

            Instantiate(boadPrefab, localPotision, boadPrefab.transform.rotation,this.transform);
        }
    }
}