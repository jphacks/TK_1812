using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class LoacationGetter : MonoBehaviour
    {
        /// <summary>経緯度取得間隔（秒）</summary>
        private const float IntervalSeconds = 0.1f;

        /// <summary>ロケーションサービスのステータス</summary>
        private LocationServiceStatus locationServiceStatus;

        /// <summary>経度</summary>
        public double Longitude { get; private set; }

        /// <summary>経度</summary>
        public double Latitude { get; private set; }

        //private double beforeLat;
        //private double beforeLng;


        /// <summary>緯度経度情報が取得可能か</summary>
        /// <returns>可能ならtrue、不可能ならfalse</returns>
        public bool CanGetLonLat()
        {
            return Input.location.isEnabledByUser;
        }

        /// <summary>経緯度取得処理</summary>
        /// <returns>一定期間毎に非同期実行するための戻り値</returns>
        private IEnumerator Start()
        {
            while (true)
            {
                locationServiceStatus = Input.location.status;
                if (Input.location.isEnabledByUser)
                {
                    switch (locationServiceStatus)
                    {
                        case LocationServiceStatus.Stopped:
                            Input.location.Start();
                            break;
                        case LocationServiceStatus.Running:
                            //beforeLat = Latitude;
                            //beforeLng = Longitude;
                            Longitude = Input.location.lastData.longitude;
                            Latitude = Input.location.lastData.latitude;
                            MapSceneManager.State.IsLoadStartLoacation = true;
                            break;
                        default:
                            break;
                    }
                }
                yield return null;
                //if(beforeLat != Latitude || beforeLng != Longitude)
                //{
                //    MapSceneManager.Player.OnRunMotion();
                //}
                //else
                //{
                //    MapSceneManager.Player.OffRunMotion();
                //}
                //yield return new WaitForSeconds(IntervalSeconds);
            }
        }
    }
}
