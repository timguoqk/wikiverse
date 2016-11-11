// First
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using SimpleJSON;
using System.Diagnostics;

public class Node : MonoBehaviour {
	public float T = 1;
	public string title, text;
	public List<string> links = new List<string> ();
	public List<string> images = new List<string> ();
	public int generation = 1;

	private List<LineRenderer> lrs = new List<LineRenderer> ();

	//distances between two nodes
	private float lineDrawSpeed = 15f;
	private float counter;
	private int index = 0;

	TextMesh titleText;
	TextMesh bodyText;
	private Global g;
	private bool shouldDrawLine;

	// list of Vector3 for each child node
	List<Vector3> vecPos = new List<Vector3>();
	private float distance;

	private int textBool;

	// Use this for initialization
	void Start () {
		g = GameObject.Find("logic").GetComponent<Global>();
//		gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
//		foreach (MeshRenderer r in GetComponentsInChildren<MeshRenderer>())
//			r.enabled = false;
        counter = 0;
        shouldDrawLine = false;
		textBool = PlayerPrefs.GetInt ("textBool");
	}

	IEnumerator generate() {
		float stime = T + Random.value * generation;
		yield return new WaitForSeconds (stime);

        shouldDrawLine = true;
		foreach (string link in links) {
			if (g.pageLocations.Count == 0) {
				yield break;
			}
			GameObject obj;
			if (!g.createdPages.ContainsKey (link)) {
				// Create new page
				g.createdPages.Add (link, null);
				Tuple<Vector3, GameObject> t = g.pageLocations [0];
				Tuple<Vector3, GameObject>  candidate = t;
				float minDist = Vector3.Distance (candidate.First, transform.position);
				foreach (Tuple<Vector3, GameObject> tt in g.pageLocations) {
					float dist = Vector3.Distance (tt.First, transform.position);
					if (dist < minDist) {
						minDist = dist;
						candidate = tt;
					}
				}
				g.pageLocations.Remove (candidate);
				obj = (GameObject)Instantiate (candidate.Second, candidate.First, Quaternion.LookRotation (candidate.First));
				g.createdPages.Remove (link);
				g.createdPages.Add (link, obj);
				Node n = obj.GetComponent<Node> ();
				n.generation = generation + 1;
				n.setTitle (link);

			} else {
				obj = g.createdPages [link];
			}
				
			vecPos.Add (obj.transform.position);
			GameObject go = (GameObject)Instantiate (GameObject.Find ("LR"), transform);
			LineRenderer lr = go.GetComponent<LineRenderer>();
			lr.SetPosition (0, transform.position); 
			lr.SetWidth (0.01f, 0.01f);
			lr.SetColors (Color.red, Color.yellow);
			lrs.Add (lr);
		}
	}

	IEnumerator GetData() {
		//UnityWebRequest www = UnityWebRequest.Get("localhost:8080/wiki/" + title);
		UnityWebRequest www = UnityWebRequest.Get("http://timguoqk.me:8080/wiki/" + title);
        UnityEngine.Debug.Log("Getting data! Be informed!");
		yield return www.Send();

		if (www.isError) {
			UnityEngine.Debug.Log(www.error);
		} else {
			// Show results as text
			JSONNode data = JSON.Parse (www.downloadHandler.text);
			UnityEngine.Debug.Log(data);
			//string title = data ["title"];
			string body = data["text"];

			bodyText = gameObject.transform.FindChild("BodyText").GetComponent<TextMesh> ();

			if (textBool == 1) {
				setText (bodyText, body);
			}

			for (int i = 0; i < data["links"].Count; i++) {
				links.Add (data["links"][i]);
			}
		}

        StartCoroutine("generate");
	}

	void setText(TextMesh mesh, string txt) {
		if (txt == null) {
			txt = "";
		}

		mesh.text = "";
		int rowLimit = 40; //find the sweet spot
		int currentCount = 0;
		string[] parts = txt.Split(' ');

		for (int i = 0; i < parts.Length; i++) {
			if (currentCount >= rowLimit) {
				mesh.text += System.Environment.NewLine;
				currentCount = 0;
			}
			mesh.text += parts[i] + " ";
			currentCount += parts [i].Length;
		}
	}

	public void setTitle(string t) {
		title = t;
		titleText = gameObject.transform.FindChild("Title").GetComponent<TextMesh> ();
		setText (titleText, title);
		StartCoroutine ("GetData");
	}

	// Update is called once per frame
	void Update () {
        if (shouldDrawLine)
        {
            int i = 0;
            counter += .1f / lineDrawSpeed;
            foreach (Vector3 destination in vecPos)
            {
                LineRenderer lr = lrs[i];
                float distance = Vector3.Distance(destination, transform.position);
                if (counter < distance)
                {
                    float x = Mathf.Lerp(0, distance, counter);

                    Vector3 ptA = transform.position;
                    Vector3 ptB = destination;

                    Vector3 pointLine = x * Vector3.Normalize(ptB - ptA) + ptA;

                    lr.SetPosition(1, pointLine);
                }
                i++;
            }
        }
		
	}
}
