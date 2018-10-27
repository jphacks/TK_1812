using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CameraTrans : MonoBehaviour
    {
        [SerializeField] GameObject player;

        private Vector3 offset;

        // Use this for initialization
        void Start()
        {
            offset = transform.position - player.transform.position;
        }

        void LateUpdate()
        {
            transform.position = player.transform.position + offset;
        }
    }
}