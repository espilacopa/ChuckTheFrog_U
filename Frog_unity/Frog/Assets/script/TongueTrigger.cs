using UnityEngine;
using System.Collections;

public class TongueTrigger : MonoBehaviour {
	public bool pause = false;
	void OnCollisionEnter2D(Collider2D other){
		Debug.Log ("OnTriggerEnter2D " + pause);
		

	} 

	void OnTriggerExit2D(Collider2D other){  
		Debug.Log ("OnTriggerExit2D " + pause);
		if (!pause) {
			IDamageable component = (IDamageable) other.gameObject.GetComponent( typeof(IDamageable) );
			component.Hit ();
		}
	} 
	void OnTriggerStay(Collider2D other){ 
		
	} 

}