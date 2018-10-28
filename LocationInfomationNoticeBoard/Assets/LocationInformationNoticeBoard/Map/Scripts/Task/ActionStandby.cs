using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Task
{
    public class ActionStandby : MapActionTask
    {
        protected override string info
        {
            get { return "Standby"; }
        }

        protected override void OnExecute()
        {
            MapSceneManager.State.Clear();
            MapSceneManager.Data.Clear();
            EndAction();
        }

        protected override void OnUpdate()
        {
        }
    }
}