using UnityEngine;
using System.Collections;

public class ScorePanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;
		gameObject.GetComponent<CanvasGroup> ().interactable = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Init(){
		gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;
		gameObject.GetComponent<CanvasGroup> ().interactable = false;

		gameObject.transform.FindChild ("star_1_g").GetComponent<Stars> ().Init ();
		gameObject.transform.FindChild ("star_2_g").GetComponent<Stars> ().Init ();
		gameObject.transform.FindChild ("star_3_g").GetComponent<Stars> ().Init ();
	}
	public void Show(ArrayList tab ){
		gameObject.GetComponent<CanvasGroup> ().interactable = true;
		gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
		Debug.Log (tab [0]+" "+tab [1]+" "+tab [2]);
		if ((bool)tab [0]) {
			gameObject.transform.FindChild ("star_1_g").GetComponent<Stars> ().Appear ();
		}
		if ((bool)tab [1]) {
			gameObject.transform.FindChild ("star_2_g").GetComponent<Stars> ().Appear ();
		}
		if ((bool)tab [2]) {
			gameObject.transform.FindChild ("star_3_g").GetComponent<Stars> ().Appear ();
		}

	}
	public void Hide(){
		
		gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;
		gameObject.GetComponent<CanvasGroup> ().interactable = false;
	}
}
