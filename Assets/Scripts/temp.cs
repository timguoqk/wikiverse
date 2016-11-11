using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class temp : MonoBehaviour {
	void Start () {
		
	}

	void Update () {
		bool x = OVRInput.Get (OVRInput.Button.One);
		//sDebug.Log (x);
		//Vector2 vec = OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick);

		//Debug.Log (vec);
	}

}
