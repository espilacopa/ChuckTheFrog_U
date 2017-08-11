using UnityEngine;
using System.Collections;

public class Frog_placement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	Camera cam = Camera.main;
	
	transform.position = -cam.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0));
}
	
	// Update is called once per frame
	void Update () {
	
	}
}
