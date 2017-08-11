using UnityEngine;
using System.Collections;

public class Hornet: MonoBehaviour,IFlyable,IDamageable{

	public float xMax;
	public float yMax;
	public float xMin;
	public float yMin;
	public float velocidadMax;
	public float maxAngle;
	public GameObject controler;
	private int _id;
	private string _type;
	private bool dead=false;
	private bool moving=true;
	private Vector3 _initPos;
	private Vector3 _pos;
	private float _speed = 20;
	private float _journeyLength;
	private float _startTime;
	private float _previousX;
	private float  _a=1 ;
	private float  _b=2;
	private float  _x;
	
	private float  _y;
	private int  _alpha;
	private float  _X;
	private float  _Y;
	private Vector3 _nextPos;
	private Vector3 _oldPos;
	private int _step=0;
	private int _rotSpeed;
	private bool _dead =false ;
	// Use this for initialization
	void Start () {
		_a=Random.Range(1,3) ;
		_b=Random.Range(1,3) ;
		_rotSpeed = Random.Range(10,15) ;
		foreach (Transform trans in gameObject.GetComponentsInChildren<Transform>())
		{

			if (trans != trans.root) //bcos root object just contains collider and control scripts
			{
				trans.GetComponent<SpriteRenderer>().sortingOrder += (id*10);
				//Debug.Log(trans.name + "->" + trans.GetComponent<SpriteRenderer>().sortingOrder);
			}
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (moving) {

			switch (_step) {

			case 0:
				
				_journeyLength = Vector3.Distance (_oldPos, _nextPos);
				float distCovered = (Time.time - _startTime) * _speed;
				float fracJourney = distCovered / _journeyLength;
				gameObject.transform.position = Vector3.Lerp (_oldPos, _nextPos, fracJourney);
				if (gameObject.transform.position == _nextPos && !_dead) {
					_step = 1;
				}
				break;
			case 1:
				_X = _x + (_a * Mathf.Cos (_alpha * 0.005f));
				_Y = _y + (_b * Mathf.Sin (_alpha * 0.005f));
				gameObject.transform.position = new Vector3 (_X, _Y, 0);
				_alpha += _rotSpeed;
				break;					
			case 2:
				if (gameObject.transform.position == _nextPos) {
					_startTime = Time.time;
					_nextPos = new Vector3 (_initPos.x + 2, _initPos.y, 0);
					_oldPos = gameObject.transform.position;
					_journeyLength = Vector3.Distance (_oldPos, _nextPos);
					_step += 1;
				}
				break;

			}
			bool move = true;
			if (_previousX < gameObject.transform.position.x) {
				move = false;
			}

			gameObject.GetComponent<Animator> ().SetBool ("moveForward", move);

			_previousX = gameObject.transform.position.x;
		}
	}
	public void Freeze(){
		moving = false;
		
	}
	public void UnFreeze(){
		moving = true;
		
	}
	public void SetControler(GameObject _controller){
		controler = _controller;
	}
	public void SetId(int id){
		_id = id;
	}
	public void Init(Vector4 zone,float _velocidadMax,float _maxAngle){
		_initPos = gameObject.transform.position;
		xMax = zone.x;
		yMax = zone.y;
		xMin = zone.z;
		yMin = zone.w;
		velocidadMax = _velocidadMax;
		maxAngle = _maxAngle;
		_oldPos = new Vector3 (xMax+4,_initPos.y+1,0);
		_y = _initPos.y;
		_x = _initPos.x;
		gameObject.transform.position =_oldPos;
		_nextPos = new Vector3(_initPos.x+1,_initPos.y,0);
		_oldPos = gameObject.transform.position;
		_startTime = Time.time+Random.Range(0,3);
	}
	public void StopFly(){
		if (!dead) {
			moving = false;
			GetComponent<Animator> ().SetBool ("dead", true);
			dead = true;
		}
		
	}
	public void DestroyE(){
		controler.GetComponent<GameController>().DestroyMe(id);
	}
	
	public void Hit(){
		_dead = true;
		_startTime = Time.time;
		_nextPos = new Vector3(xMax+5,_initPos.y,0);
		_oldPos = gameObject.transform.position;
		_step = 0;
	}
	public void Fire(){
		//gameObject.GetComponentInChildren<>

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
