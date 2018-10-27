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
		Action action;

		public Text Post { get { return post; } }

		public void OnClick () {
			if(action != null){
				action();
			}
		}

		public void Set(Action act){
			action = act;
		}
	}
}