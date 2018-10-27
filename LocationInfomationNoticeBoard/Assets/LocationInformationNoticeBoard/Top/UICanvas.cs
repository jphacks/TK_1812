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

namespace Top {
	public class UICanvas : MonoBehaviour {
		[SerializeField] Text name;
		Action action;
		public string post;

		protected Firebase.Auth.FirebaseAuth auth;
		Firebase.Auth.FirebaseUser user;

		public Text Name { get { return name; } }

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