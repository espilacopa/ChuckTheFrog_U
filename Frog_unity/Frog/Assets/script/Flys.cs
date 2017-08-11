using UnityEngine;
using System.Collections;

public class Flys : MonoBehaviour, IFlyable, IDamageable {

	public float xMax;
	public float yMax;
	public float xMin;
	public float yMin;
	public float velocidadMax;
	public float maxAngle;
	public GameObject controler;
	private int _id;
	private float x;
	private float y;
	private float tiempo;
	private Vector2 vector;
	private float angle;
	private bool facingRight;
	private bool moving = true;
	private bool dead = false;
	private string _type;

	void Start () {
		facingRight = false;
		angle = Random.Range (-maxAngle, maxAngle);
		vector = new Vector2 (Random.Range (-velocidadMax, velocidadMax), Random.Range (-velocidadMax, velocidadMax));

	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
		
			bool move = false;
			tiempo += Time.deltaTime;
			if (tiempo > .1) {
				rotateV(Random.Range(-angle, angle));
				vector = new Vector2 (Random.Range (-velocidadMax, velocidadMax), Random.Range (-velocidadMax, velocidadMax));
				move = true;
			}
			if (tiempo > .7) {
				angle = Random.Range (-maxAngle, maxAngle);
				vector = new Vector2 (Random.Range (-velocidadMax, velocidadMax), Random.Range (-velocidadMax, velocidadMax));
				move = true;
			}
			Vector3 nextPos = new Vector3 (transform.localPosition.x + vector.x, transform.localPosition.y + vector.y, 0.0f);
			if (nextPos.x >= xMax || nextPos.x <= xMin || nextPos.y >= yMax || nextPos.y <= yMin ){
				rotateV( Random.Range (135,225));
			}
			if(move) tiempo = 0;
			if(!facingRight && vector.x>0 ||  facingRight && vector.x<0  ){
				facingRight = !facingRight;
				Vector3 facing =transform.localScale;
				facing = new Vector3 (facing.x *-1,facing.y,facing.z);
				transform.localScale = facing;
			}
			transform.localPosition = new Vector3 (transform.localPosition.x + vector.x, transform.localPosition.y + vector.y, 0.0f);
		}

	}
	public void SetControler(GameObject _controller){
		controler = _controller;
	}
	public void SetId(int id){
		_id = id;
	}
	public void Freeze(){
		moving = false;
	
	}
	public void UnFreeze(){
		moving = true;
		
	}
	private void rotateV(float angle)
	{
		float radians = angle * Mathf.Deg2Rad;
		float sin = Mathf.Sin(radians);
		float cos = Mathf.Cos(radians);
		
		float tx = vector.x;
		float ty = vector.y;

		vector = new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
	
	}
	public void Init(Vector4 zone, float _velocidadMax,float _maxAngle){
		xMax = zone.x;
		yMax = zone.y;
		xMin = zone.z;
		yMin = zone.w;
		velocidadMax = _velocidadMax;
		maxAngle = _maxAngle;
	}
	public void StopFly(){
		if (!dead) {
			moving = false;
			dead = true;
			GetComponent<Animator> ().SetBool ("dead", dead);
		}

	}
	public void Hit(){
		if (type == "Bonus") {
			controler.GetComponent<GameController> ().addBonus ();
		}
		StopFly ();
	}
	public void DestroyE(){
		controler.GetComponent<GameController>().DestroyMe(id);
	}
	public int id
	{    
		get    
		{    
			return _id;
		}  

	}
	public string type
	{    
		get    
		{    
			return _type;
		}  
		set    
		{    
			 _type = value;
		}  

	}
}