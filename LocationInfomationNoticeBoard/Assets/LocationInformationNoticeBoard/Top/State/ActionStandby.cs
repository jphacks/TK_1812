using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

namespace Top.Task
{
    public class ActionStandby : TopActionTask
    {
        protected override void OnExecute()
        {
            var dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync ().ContinueWith (task => {
				dependencyStatus = task.Result;
				if (dependencyStatus == Firebase.DependencyStatus.Available) {
					var auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
                    GameData.Auth = auth;
				} else {
					Debug.LogError (
						"Could not resolve all Firebase dependencies: " + dependencyStatus);
				}
                TopManager.Initialized = true;
			});
        }

        protected override void OnClick(){

        }
        protected override void OnBack(){}

    }
}
