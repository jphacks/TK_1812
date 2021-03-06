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
    public class LoadSceneAction : TopActionTask
    {
        protected override void OnExecute()
        {
            TopManager.UI.Set(() => OnClick());
            SceneManager.LoadSceneAsync("Board");
            SceneManager.LoadSceneAsync("Map", LoadSceneMode.Additive);
        }

        protected override void OnClick(){
        }
        protected override void OnBack(){}
    }
}
