using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

namespace Board.Task
{
    public class ActionStandby : BoardActionTask
    {
        protected override void OnExecute()
        {
            //データベースをセット
            FirebaseApp app = FirebaseApp.DefaultInstance;
            app.SetEditorDatabaseUrl ("https://mapboard-5364b.firebaseio.com");
            if (app.Options.DatabaseUrl != null){
                app.SetEditorDatabaseUrl (app.Options.DatabaseUrl);
            }

            EndAction();
        }

        protected override void OnClick(){

        }
        protected override void OnBack(){}

    }
}
