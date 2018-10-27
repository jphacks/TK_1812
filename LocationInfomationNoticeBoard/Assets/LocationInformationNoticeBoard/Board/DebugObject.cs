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
    public class DebugObject : MonoBehaviour
    {
        Action action;

        protected Firebase.Auth.FirebaseAuth auth;
        Firebase.Auth.FirebaseUser user;

        async void OnEnable () {
            var dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
            await Firebase.FirebaseApp.CheckAndFixDependenciesAsync ().ContinueWith (task => {
                dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available) {
                    var auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
                    GameData.Auth = auth;
                } else {
                    Debug.LogError (
                        "Could not resolve all Firebase dependencies: " + dependencyStatus);
                }
            });

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
            });
        }

        public void OnClick () {
            if (action != null) {
                action ();
            }
        }

        public void Set (Action act) {
            action = act;
        }
    }
}