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
		public string post;

		protected Firebase.Auth.FirebaseAuth auth;
		Firebase.Auth.FirebaseUser user;
		Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;

		public void OnClick () {
			auth.SignInAnonymouslyAsync ().ContinueWith (task => {
				if (task.IsCanceled) {
					Debug.LogError ("SignInAnonymouslyAsync was canceled.");
					return;
				}
				if (task.IsFaulted) {
					Debug.LogError ("SignInAnonymouslyAsync encountered an error: " + task.Exception);
					return;
				}

				user = task.Result;

				Debug.LogFormat ("User signed in successfully: {0} ({1})", user.DisplayName, user.UserId);

				updateProfile ();
			});

			AddPost();
		}

		public virtual void Start () {
			Firebase.FirebaseApp.CheckAndFixDependenciesAsync ().ContinueWith (task => {
				dependencyStatus = task.Result;
				if (dependencyStatus == Firebase.DependencyStatus.Available) {
					auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
				} else {
					Debug.LogError (
						"Could not resolve all Firebase dependencies: " + dependencyStatus);
				}
				Debug.Log (auth);
			});
		}

		public void updateProfile () {
			var profile = new Firebase.Auth.UserProfile {
				DisplayName = "Jane Q. User"
			};

			var user = auth.CurrentUser;
			user.UpdateUserProfileAsync (profile).ContinueWith (task => {
				if (task.IsCanceled) {
					Debug.LogError ("UpdateUserProfileAsync was canceled.");
					return;
				}
				if (task.IsFaulted) {
					Debug.LogError ("UpdateUserProfileAsync encountered an error: " + task.Exception);
					return;
				}

				Debug.Log ("User profile updated successfully.");
			});
		}

		protected virtual void InitializeFirebase () {
			FirebaseApp app = FirebaseApp.DefaultInstance;
			app.SetEditorDatabaseUrl ("https://mapboard-5364b.firebaseio.com");
			if (app.Options.DatabaseUrl != null)
				app.SetEditorDatabaseUrl (app.Options.DatabaseUrl);
		}

		//投稿
		public void AddPost () {
			DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference ("posts");

			// Use a transaction to ensure that we do not encounter issues with
			// simultaneous updates that otherwise might create more than MaxScores top scores.

			var key = reference.Push().Key;
			Dictionary<string, object> childUpdates = new Dictionary<string, object>();
			Dictionary<string, object> newPost = new Dictionary<string, object>();
			newPost["uid"] = auth.CurrentUser.UserId;
			newPost["text"] = post;

    		childUpdates[key] = newPost;
			reference.UpdateChildrenAsync(childUpdates).ContinueWith (task => {
				if (task.IsCanceled) {
					Debug.LogError ("UpdateChildrenAsync was canceled.");
					return;
				}
				if (task.IsFaulted) {
					Debug.LogError ("UpdateChildrenAsync encountered an error: " + task.Exception);
					return;
				}

				Debug.Log ("Chilidren updated successfully.");
			});

			//reference.RunTransaction (AddScoreTransaction)
			//	.ContinueWith (task => {
			//		if (task.Exception != null) {
			//			Debug.Log (task.Exception.ToString ());
			//		} else if (task.IsCompleted) {
			//			Debug.Log ("Transaction complete.");
			//		}
			//	});
		}

		TransactionResult AddScoreTransaction (MutableData mutableData) {
			List<object> posts = mutableData.Value as List<object>;

			Dictionary<string, object> newPost = new Dictionary<string, object> ();
			newPost["uid"] = auth.CurrentUser.UserId;
			newPost["text"] = "test";
			posts.Add (newPost);

			// You must set the Value to indicate data at that location has changed.
			mutableData.Value = posts;
			return TransactionResult.Success (mutableData);
		}
	}
}