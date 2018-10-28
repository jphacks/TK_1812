using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CreateBoad : MonoBehaviour
    {
        [SerializeField] GameObject boadPrefab;

        Dictionary<Result,GameObject> generatedList = new Dictionary<Result, GameObject>();
        List<GameObject> poolObj = new List<GameObject>();

        private int maxPoolObjCount = 10;
        private int activeCount = 0;

        public void InstantiateBoad(Result result)
        {
            if (!generatedList.ContainsKey(result))
            {
                double latitude = result.geometry.location.lat;
                double longitude = result.geometry.location.lng;

                var map = MapSceneManager.MainMap;

                var location = new Mapbox.Utils.Vector2d(latitude, longitude);
                var localPotision = map.GeoToWorldPosition(location) + boadPrefab.transform.position;

                bool allActive = true;
                foreach(var obj in poolObj)
                {
                    if(!obj.active)
                    {
                        generatedList.Add(result, obj);
                        obj.transform.position = localPotision;
                        var boadSet = obj.GetComponent<BoadSet>();
                        boadSet.SetUp(result.place_id, result.name, result.icon);
                        obj.SetActive(true);
                        activeCount++;
                        allActive = false;
                        break;
                    }
                }
                if(allActive)
                {
                    var obj = Instantiate(boadPrefab, localPotision, boadPrefab.transform.rotation, this.transform);
                    generatedList.Add(result, obj);
                    poolObj.Add(obj);
                    activeCount++;

                    var boadSet = obj.GetComponent<BoadSet>();
                    boadSet.SetUp(result.place_id, result.name, result.icon);
                }
            
            }
        }

        public void DestroyBoad(Result result)
        {
            if(generatedList.ContainsKey(result))
            {
                if (activeCount >= maxPoolObjCount)
                {
                    Destroy(generatedList[result]);
                    poolObj.Remove(generatedList[result]);
                    activeCount--;
                    generatedList.Remove(result);
                }
                else
                {
                    generatedList[result].SetActive(false);
                    activeCount--;
                    generatedList.Remove(result);
                }

            }

        }
    }
}