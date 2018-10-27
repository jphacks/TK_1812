using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Top
{
	public class TopManager : SingletonMonoBehaviour<TopManager>
	{
		[SerializeField] UICanvas uiCanvas;
		public static UICanvas UI { get { return Instance.uiCanvas; } }

		protected override void OnSingltonAwake(){
		}
	}
}