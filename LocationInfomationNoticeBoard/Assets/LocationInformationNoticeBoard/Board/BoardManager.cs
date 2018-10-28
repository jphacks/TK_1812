using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.StateMachines;

namespace Board
{

	public class BoardManager : SingletonMonoBehaviour<BoardManager>
	{
		[SerializeField] UICanvas uiCanvas;
		[SerializeField] FSMOwner owner;
		public static UICanvas UI { get { return Instance.uiCanvas; } }

		public static bool Touch
        {
            get { return Instance.owner.blackboard.GetValue<bool>("touch"); }
            set { Instance.owner.blackboard.SetValue("touch", value); }
		}


		protected override void OnSingltonAwake(){
		}
	}
}