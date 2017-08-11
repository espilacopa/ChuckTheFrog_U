using UnityEngine;
using System.Collections;

public class Tongue : MonoBehaviour {

	//destination point
	private Vector3 endPoint;
	//vertical position of the gameobject
	private float initWith;
	private Vector3 initPos;
	public Vector3 endMarker;
	private float speed = 30;
	private float startTime;
	private float journeyLength;

	public GameObject impact;
	private bool pause = false;
	void Awake(){
		initWith = GetComponent<Renderer> ().bounds.size.x;
		transform.localScale = new Vector3 (0.2f, 1, 1);
		transform.position = new Vector3 (transform.position.x,transform.position.y,0);
		endMarker = new Vector3 (0.2f, 1, 1);
		startTime = Time.time;
		impact= GameObject.FindGameObjectWithTag("Sphere"); 
		initPos = impact.transform.position;
	}


	void Start () {


	}

	void FixedUpdate ()
	{
		if (!pause) {
			if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))) {

				//declare a variable of RaycastHit struct
				RaycastHit hit;
				//Create a Ray on the tapped / clicked position
				Ray ray;
				//for unity editor
				#if UNITY_EDITOR
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				//for touch device
				#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
				ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				#endif

				if (Physics.Raycast (ray, out hit)) {
					//Debug.Log ("hit");
					endPoint = hit.point;
					endPoint.z = 0;
					startTime = Time.time;
					float endX = Mathf.Round ((Vector3.Distance (gameObject.transform.position, endPoint) / initWith) * 1000f) / 1000f;
					endMarker = new Vector3 (endX, 1, 1);
					journeyLength = Vector3.Distance (gameObject.transform.localScale, endMarker);
					gameObject.transform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (endPoint.y - transform.position.y, endPoint.x - transform.position.x) * 180 / Mathf.PI);

					GetComponentInParent<Animator> ().SetBool ("open", true);

				}
			
			} else if (gameObject.transform.localScale.x != (endMarker.x)) {
				float distCovered = (Time.time - startTime) * speed;
				float fracJourney = distCovered / journeyLength;

				transform.localScale = Vector3.Lerp (transform.localScale, endMarker, fracJourney);
				
				distCovered = (Time.time - startTime) * (speed*2);
				fracJourney = distCovered / journeyLength;
				impact.transform.position = Vector3.Lerp (initPos, endPoint, fracJourney);
			} else {
				endPoint = initPos;
				endMarker = new Vector3 (0.2f,1, 1);
				GetComponentInParent<Animator> ().SetBool ("open", false);

			}
		}

	}
	public void Reset(){
		
		impact.transform.position = initPos;
	}
	public void Pause(){
		pause = true;
		impact.GetComponent<TongueTrigger> ().pause = pause;
	}
	public void UnPause(){
		pause = false;
		impact.GetComponent<TongueTrigger> ().pause = pause;
	}
}
