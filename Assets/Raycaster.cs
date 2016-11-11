using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Raycaster : MonoBehaviour {
	//private Dictionary<GameObject, int> time;
	private GameObject focusedObject;
	private int timeout = 150;
	private int currentTime = 0;
	// Use this for initialization
	void Start () {
		//time = new Dictionary<GameObject, int> ();
		focusedObject = null;
	}

	void FixedUpdate () {
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		RaycastHit hitInfo;
		Debug.DrawRay(transform.position, fwd, Color.green);
		if (Physics.Raycast (transform.position, fwd, out hitInfo)) {
			GameObject obj = hitInfo.collider.gameObject;
			if (focusedObject && focusedObject != obj) {
				notTheObject ();
			} else if (obj.GetComponent<Node> () != null) {
				if (obj != focusedObject) {
					iTween.ScaleBy (obj, iTween.Hash ("amount", new Vector3 (3f, 3f, 3f)));
					focusedObject = obj;
					currentTime = 0;
				}
			}
		} else if (focusedObject) {
			notTheObject ();
		}
	}

	void notTheObject() {
		currentTime += 1;
		if (currentTime >= timeout) {
			iTween.ScaleBy (focusedObject, iTween.Hash ("amount", new Vector3 (1 / 3f, 1 / 3f, 1 / 3f)));
			focusedObject = null;
		}
	}

	void checkObj(GameObject obj) {
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		RaycastHit hitInfo;
		if (Physics.Raycast (transform.position, fwd, out hitInfo) && hitInfo.collider.gameObject == obj) {
			iTween.ScaleBy (obj, iTween.Hash ("amount", new Vector3 (1.0f, 1.0f, 1.0f), "oncomplete", "checkObj", "oncompletetarget", gameObject, "oncompleteparams", obj));
		} else {
			iTween.ScaleBy (obj, iTween.Hash ("amount", new Vector3 (1/1.5f, 1/1.5f, 1/1.5f), "oncomplete", "checkObj", "oncompletetarget", gameObject, "oncompleteparams", obj));
		}
	}

	IEnumerator resetGazed(Animator a) {
		yield return new WaitForSeconds (1.2f);
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		RaycastHit hitInfo;
		//Debug.DrawRay(transform.position, fwd, Color.green);
		if (Physics.Raycast (transform.position, fwd, out hitInfo)) {
			GameObject obj = hitInfo.collider.gameObject;
			if (obj.GetComponent<Animator> () != a) {
				a.SetBool ("gazed", false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
