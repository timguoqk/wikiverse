using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class requestWiki : MonoBehaviour {



	// Use this for initialization
	void Start () {
		StartCoroutine("GetData");
	}



	
	// Update is called once per frame
	void Update () {
	
	}
}
