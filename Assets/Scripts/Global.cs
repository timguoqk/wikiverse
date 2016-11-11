using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Tuple<T1, T2>
{
	public T1 First { get; private set; }
	public T2 Second { get; private set; }
	internal Tuple(T1 first, T2 second)
	{
		First = first;
		Second = second;
	}
}

public static class Tuple
{
	public static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second)
	{
		var tuple = new Tuple<T1, T2>(first, second);
		return tuple;
	}
}

public class Global : MonoBehaviour {
	public float alpha = 10 * Mathf.PI / 180;
	public string keyword;
	public float r = 5;
	public GameObject seed1;
	public GameObject seed2;
	public float currentlyShowing = 1;

	public Dictionary<string, GameObject> createdPages;
	public List<Tuple<Vector3, GameObject>> pageLocations;


	// Use this for initialization
	void Start () {
		//keyword = PlayerPrefs.GetString("keyword");
		createdPages = new Dictionary<string, GameObject> ();
		generate ();
		Tuple<Vector3, GameObject> t = pageLocations[0];
		pageLocations.Remove (t);
		GameObject obj = (GameObject) Instantiate (t.Second, t.First, Quaternion.LookRotation (t.First));
		Debug.Log (keyword);
		createdPages.Add (keyword, obj);
		obj.GetComponent<Node> ().setTitle (keyword);
	}

	void generate() {
		List<Tuple<Vector3, GameObject>> temp = new List<Tuple<Vector3, GameObject>> ();
		for (int i = 1; i <= 2; i++) {
			GameObject seed = (i == 1) ? seed1 : seed2;
			for (float theta = 0.2f * i; theta < 2 * Mathf.PI - 0.2f; theta += alpha) {
				for (float phi = 0.2f * i; phi < 2 * Mathf.PI - 0.2f; phi += alpha / Mathf.Abs (Mathf.Sin (theta))) {
					Tuple<Vector3, GameObject> pos = new Tuple<Vector3, GameObject>(sphericalToCartesian (r + Random.value, theta + Random.value / Mathf.PI, phi + Random.value / Mathf.PI), seed);
					temp.Add (pos);
				}
			}
		}
		pageLocations = temp.Distinct ().ToList ();
	}
	
	// Update is called once per frame
	void Update () {
		currentlyShowing = Mathf.Min (1, currentlyShowing + 0.006f);
	}

	public Vector3 sphericalToCartesian(float r, float theta, float phi) {
		Vector3 cart = new Vector3 ();
		cart.x = r * Mathf.Sin (theta) * Mathf.Cos (phi);
		cart.y = r * Mathf.Sin (theta) * Mathf.Sin (phi);
		cart.z = r * Mathf.Cos (theta);
		return cart;
	}
}
