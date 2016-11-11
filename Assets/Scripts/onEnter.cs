using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class onEnter : MonoBehaviour {
	private InputField field;

	void LockInput(InputField input) {
		
		if (input.text.Length > 0) {
			PlayerPrefs.SetString("keyword", field.text);
			PlayerPrefs.Save ();
			Debug.Log (PlayerPrefs.GetString("keyword"));
		} else if (input.text.Length == 0) {
			Debug.Log("Main Input Empty");
		}
	}

	// Use this for initialization
	void Start () {
		field = GetComponent<InputField> ();
		field.onEndEdit.AddListener (delegate{LockInput(field);});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
