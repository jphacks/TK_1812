using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Board
{
	public class UICanvas : MonoBehaviour {
		[SerializeField] Text post;
		[SerializeField] Text title;
		[SerializeField] GameObject[] panels;
		[SerializeField] Text[] names;
		[SerializeField] Text[] contents;

		Action action;
		Action backAction;

        public string placeId;
        public string placeName;
        public string icon;

		public Text Post { get { return post; } }
		public Text Title { get { return title; } }
		public GameObject[] Panel { get { return panels; } }
		public Text[] Name { get { return names; } }
		public Text[] Content { get { return contents; } }

		public void SetActive(bool value){
			gameObject.SetActive(value);
		}

		public void setParam(string place_id, string place_name, string i_con){
			placeId = place_id;
			placeName = place_name;
			icon = i_con;
			BoardManager.Touch = true;
		}

		public void OnClick () {
			if(action != null){
				action();
			}
		}

		public void OnBack () {
			Debug.Log("OnBack");
			BoardManager.Touch = false;
		}

		public void Set(Action act){
			action = act;
		}
	}
}