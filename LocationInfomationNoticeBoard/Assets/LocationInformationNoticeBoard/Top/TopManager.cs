using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.StateMachines;

namespace Top
{

	public class TopManager : SingletonMonoBehaviour<TopManager>
	{
		[SerializeField] UICanvas uiCanvas;
		[SerializeField] FSMOwner owner;
		public static UICanvas UI { get { return Instance.uiCanvas; } }
		public static bool Initialized
        {
            get { return Instance.owner.blackboard.GetValue<bool>("initialized"); }
            set { Instance.owner.blackboard.SetValue("initialized", value); }
        }

		public static bool Created
        {
            get { return Instance.owner.blackboard.GetValue<bool>("created"); }
            set { Instance.owner.blackboard.SetValue("created", value); }
        }

		protected override void OnSingltonAwake(){
		}
	}
}