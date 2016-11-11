using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class onClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Button> ().onClick.AddListener (() => {switchScene();});
	}

	void switchScene() {
		SceneManager.LoadScene("Cubes");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
