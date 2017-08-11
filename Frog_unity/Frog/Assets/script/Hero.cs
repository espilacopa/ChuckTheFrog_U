using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {
	public bool fire = false;
	public GameObject tongue;
	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		anim.SetBool ("open", false);

	}
}
