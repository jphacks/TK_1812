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
            BoardManager.UI.SetActive(true);
            BoardManager.UI.Title.text = BoardManager.UI.placeName;
            GetPost();
        }

        protected override void OnClick(){
            AddPost();
        }

        /// <summary>
        /// 投稿を取得
        /// </summary>
        async void GetPost() {
            var list = new List<string>();
            Debug.Log(BoardManager.UI.placeId);
            await FirebaseDatabase.DefaultInstance
            .GetReference("posts")
            .OrderByChild("place_id")
            .LimitToLast(5)
            .EqualTo(BoardManager.UI.placeId)
            .GetValueAsync()
            .ContinueWith(task => {
                if (task.IsFaulted) {
                    Debug.Log("Post Get Faild");
                }
                else if (task.IsCompleted) {
                    var snapshot = task.Result;
                    if(snapshot != null){
                        IEnumerator result = snapshot.Children.GetEnumerator();
                        while (result.MoveNext()) {
                            DataSnapshot data = result.Current as DataSnapshot;
                            string name = (string)data.Child("text").Value;
                            list.Add(name);
                        }
                    }
                }
            });

            for (int i = 0; i < list.Count; i++){
                BoardManager.UI.Content[i].text = list[i];
            }
        }

        /// <summary>
        /// 投稿をする
        /// </summary>
        void AddPost () {
            var post = BoardManager.UI.Post.text;
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference ("posts");

            var key = reference.Push().Key;
            Dictionary<string, object> childUpdates = new Dictionary<string, object>();
            Dictionary<string, object> newPost = new Dictionary<string, object>();
            newPost["uid"] = GameData.Auth.CurrentUser.UserId;
            newPost["place_id"] = BoardManager.UI.placeId;
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
