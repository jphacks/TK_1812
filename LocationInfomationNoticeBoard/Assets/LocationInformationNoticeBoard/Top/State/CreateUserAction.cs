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
        protected override void OnExecute()
        {
            TopManager.UI.Set(() => OnClick());
        }

        protected override void OnClick(){
            GameData.Auth.SignInAnonymouslyAsync ().ContinueWith (task => {
				if (task.IsCanceled) {
					Debug.LogError ("SignInAnonymouslyAsync was canceled.");
					return;
				}
				if (task.IsFaulted) {
					Debug.LogError ("SignInAnonymouslyAsync encountered an error: " + task.Exception);
					return;
				}

				var user = task.Result;
				GameData.User = user;

				Debug.LogFormat ("User signed in successfully: {0} ({1})", user.DisplayName, user.UserId);
				updateProfile();
			});
        }
        protected override void OnBack(){}

        public async void updateProfile () {
            var name = TopManager.UI.Name.text;
            if(name == null){
                name = "no name";
            }

			var profile = new Firebase.Auth.UserProfile {
				DisplayName = name
			};

			var user = GameData.Auth.CurrentUser;
			await user.UpdateUserProfileAsync (profile).ContinueWith (task => {
				if (task.IsCanceled) {
					Debug.LogError ("UpdateUserProfileAsync was canceled.");
					return;
				}
				if (task.IsFaulted) {
					Debug.LogError ("UpdateUserProfileAsync encountered an error: " + task.Exception);
					return;
				}
				Debug.Log ("User profile updated successfully.");
				TopManager.Created = true;
			});
		}
    }
}
