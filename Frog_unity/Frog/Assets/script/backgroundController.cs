using UnityEngine;
using System.Collections;

public class backgroundController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera cam = Camera.main;
		Vector3 pos = cam.ScreenToWorldPoint(new Vector3(Screen.width,0,0));
		pos.z = 0;
		(transform.Find("first_plan")).position = pos;
		pos = cam.ScreenToWorldPoint(new Vector3(0,0,0));
		pos.z = 0;

		//(transform.Find("first_plan")).position = new Vector3(Screen.width,Screen.height,0);
		(transform.Find ("second_plan")).position = pos;
		(transform.Find ("third_plan")).position = pos;
		(transform.Find ("forth_plan")).localScale = new Vector3 (1.5f,1,1);
		//(transform.Find ("fifth_plan")).localScale = new Vector3 (1.5f,1,1);
		(transform.Find ("sixth_plan")).localScale = new Vector3 (1.5f,1,1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
