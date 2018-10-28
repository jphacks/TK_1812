using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Threading.Tasks;
using Top;
using UnityEngine.SceneManagement;

namespace Top.Task
{
    public class CreateUserAction : TopActionTask
    {
		Firebase.Auth.FirebaseUser user;

        protected override void OnExecute()
        {
            TopManager.UI.Set(() => OnClick());
        }

        protected async override void OnClick(){
            await GameData.Auth.SignInAnonymouslyAsync ().ContinueWith (task => {
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
			});

			var name = TopManager.UI.Name.text;
	           if(name == null){
	               name = "no name";
	           }

			var profile = new Firebase.Auth.UserProfile {
				DisplayName = name
			};

			//var user = GameData.Auth.CurrentUser;
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
			TopManager.Created = true;
        }
        protected override void OnBack(){}

        public void updateProfile () {
            var name = TopManager.UI.Name.text;
            if(name == null){
                name = "no name";
            }

			var profile = new Firebase.Auth.UserProfile {
				DisplayName = name
			};

			//var user = GameData.Auth.CurrentUser;
			var user = GameData.User;
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
			TopManager.Created = true;
		}
    }
}
