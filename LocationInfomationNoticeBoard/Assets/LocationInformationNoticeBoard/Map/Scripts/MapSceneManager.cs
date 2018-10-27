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

        public static MapFSM State { get { return Instance.state; } }
        public static LoacationGetter Getter{ get { return Instance.getter; }}
        public static UICanvas UI{ get { return Instance.ui; }}
        public static PlayerController Player { get { return instance.player; }}

        override protected void OnSingltonAwake()
        { }
    }
}
