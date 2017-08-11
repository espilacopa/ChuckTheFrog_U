using UnityEngine;
using System.Collections;

public class Stars : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<CanvasGroup> ().alpha = 0.0f;
	}
	public void Appear(){
		gameObject.GetComponent<CanvasGroup> ().alpha = 1.0f;

	}
	public void Init(){
		gameObject.GetComponent<CanvasGroup> ().alpha = 0.0f;

	}
}
