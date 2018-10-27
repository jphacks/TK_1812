using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.StateMachines;

namespace Map
{
    [System.Serializable]
    public class MapFSM
    {
        [SerializeField] FSMOwner owner;

        public bool IsLoadStartLoacation
        {
            get { return owner.blackboard.GetValue<bool>("is_load_start_loacation"); }
            set { owner.blackboard.SetValue("is_load_start_loacation", value); }
        }

        public void Clear()
        {
            IsLoadStartLoacation = false;
        }
    }

    public class MapSceneManager : SingletonMonoBehaviour<MapSceneManager>
    {
        [SerializeField] MapFSM state;
        [SerializeField] LoacationGetter getter;
        [SerializeField] UICanvas ui;
        [SerializeField] PlayerController player;
        [SerializeField] Mapbox.Unity.Map.AbstractMap mainMap;
        [SerializeField] CreateBoad boad;

        MapSceneData data = new MapSceneData();

        public static MapFSM State { get { return Instance.state; } }
        public static LoacationGetter Getter{ get { return Instance.getter; }}
        public static UICanvas UI{ get { return Instance.ui; }}
        public static PlayerController Player { get { return Instance.player; }}
        public static Mapbox.Unity.Map.AbstractMap MainMap { get { return Instance.mainMap; }}
        public static CreateBoad Boad { get { return Instance.boad; }}
        public static MapSceneData Data { get { return Instance.data; }}

        override protected void OnSingltonAwake()
        { }
    }
}
