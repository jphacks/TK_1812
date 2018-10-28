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
        [SerializeField] Animator animator;

        private Vector3 offset;
        //　キャラクターの速度
        private Vector3 velocity;
        //　前の速度
        private Vector3 oldVelocity;
        //　歩く速さ
        [SerializeField]
        private float moveSpeed = 2f;
        // Use this for initialization
        public void Setup()
        {
            Mapbox.Utils.Vector2d location;
            if(locationGetter.CanGetLonLat())
            {
                Debug.Log("マップ生成");
                location = new Mapbox.Utils.Vector2d(locationGetter.Latitude, locationGetter.Longitude);
            }
            else
            {
                location = new Mapbox.Utils.Vector2d(35.7134029f, 139.7611094);
            }

            maps.Initialize(location,15);
            maps.UpdateMap(location);
           
            //Mapbox.Utils.Vector2d location = new Mapbox.Utils.Vector2d(locationGetter.Latitude, locationGetter.Longitude);

            offset = transform.localPosition;
        }

        // Update is called once per frame
        public void PlayerTransUpdate()
        {
            velocity = Vector3.zero;

            var map = MapSceneManager.MainMap;
            var location = new Mapbox.Utils.Vector2d(locationGetter.Latitude, locationGetter.Longitude);
            var pos = map.GeoToWorldPosition(location) + offset;
            velocity = pos;
            oldVelocity = transform.localPosition;

            if (Vector3.Distance(transform.position, pos) > 0.05f)
            { 
                var moveDirection = (pos - transform.position).normalized;
                velocity = new Vector3(moveDirection.x * moveSpeed, transform.position.y, moveDirection.z * moveSpeed);

                transform.LookAt(transform.position + new Vector3(moveDirection.x, 0, moveDirection.z));
                OnRunMotion();
            }
            else
            {
                velocity = Vector3.zero;
                OffRunMotion();
            }


            transform.localPosition += velocity * Time.deltaTime;
            //cCon.Move(velocity * Time.deltaTime);

            //transform.localPosition = pos;

            string positionTextTemplate = "x: {0}\ny: {1}";
            MapSceneManager.UI.SetPositionText(string.Format(positionTextTemplate, transform.position.x, transform.position.y));
        }

        public void OnRunMotion()
        {
            animator.SetBool("isRun", true);
        }

        public void OffRunMotion()
        {
            animator.SetBool("isRun", false);
        }
    }
}