using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class toggleText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Toggle> ().onValueChanged.AddListener ((currentValue) => {toggle();});
		PlayerPrefs.SetInt("textBool", 1);
	}

	void toggle() {
		if (PlayerPrefs.GetInt ("textBool") == 0) {
			PlayerPrefs.SetInt ("textBool", 1);	// 1 = true

		} else {
			PlayerPrefs.SetInt ("textBool", 0);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
