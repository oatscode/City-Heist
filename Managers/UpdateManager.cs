﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour {
	[Header("Set Dynamically")]
	// Singleton
	private static UpdateManager _S;
	public static UpdateManager S { get { return _S; } set { _S = value; } }

	public delegate void 	LoopDelegate();
	public static event 	LoopDelegate updateDelegate;

	public delegate void 	FixedLoopDelegate();
	public static event 	FixedLoopDelegate fixedUpdateDelegate;

	// Selected GameObject
	//GameObject lastselect;

	// Screen Resolution
	Vector2 resolution;

	void Awake () {
		S = this;

		resolution = new Vector2(Screen.width, Screen.height);
	}

	void Update () {
		if (updateDelegate != null) {
			updateDelegate ();
		}
	}

	void FixedUpdate(){
		if (fixedUpdateDelegate != null) {
            if (!GameManager.S.paused) {
				fixedUpdateDelegate();

				// If screen resolution changes...
				if (resolution.x != Screen.width || resolution.y != Screen.height) {
					// ... change screen width & height to update position/size of UI 
					resolution.x = Screen.width;
					resolution.y = Screen.height;
				}

				//// Prevent left mouse clicks from deselecting currently selected game object]
				//if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null) {
				//	UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(lastselect);
				//} else {
				//	lastselect = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
				//}
			}
		}
	}
}