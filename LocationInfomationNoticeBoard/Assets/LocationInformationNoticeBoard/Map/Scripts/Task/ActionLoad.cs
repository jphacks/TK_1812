using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Task
{
    public class ActionLoad : MapActionTask
    {
        protected override string info
        {
            get { return "Load"; }
        }

        protected override void OnExecute()
        {
            MapSceneManager.UI.SetUp();
        }

        protected override void OnUpdate()
        {
            if(MapSceneManager.Getter.CanGetLonLat())
            {
                if (MapSceneManager.State.IsLoadStartLoacation)
                {
                    EndAction();
                }
            }
            else
            {
                EndAction();
            }
        }
    }
}