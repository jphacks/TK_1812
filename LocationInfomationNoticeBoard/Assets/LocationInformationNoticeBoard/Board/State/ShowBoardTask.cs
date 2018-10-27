using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

namespace Board.Task
{
    public class ShowBoardTask : BoardActionTask
    {
        protected override void OnExecute()
        {
            BoardManager.UI.Set(()=>OnClick());
        }

        protected override void OnClick(){
            AddPost();
        }
        protected override void OnBack(){}

        /// <summary>
        /// 投稿をする
        /// </summary>
        public void AddPost () {
            var post = BoardManager.UI.Post.text;
            Debug.Log(post);
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference ("posts");

            var key = reference.Push().Key;
            Dictionary<string, object> childUpdates = new Dictionary<string, object>();
            Dictionary<string, object> newPost = new Dictionary<string, object>();
            newPost["uid"] = GameData.Auth.CurrentUser.UserId;
            newPost["place_id"] = 100000;
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
        }
    }
}
