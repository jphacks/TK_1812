using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Json;

namespace Map
{
    public class MapSceneData
    {
        public List<Result> spotList = new List<Result>();

        public void Clear()
        {
            spotList.Clear();
        }
    }
}