using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class Timer : MonoBehaviour {


	// TIMER counting DOWN
	public float timer = 0.0f;
	public float timerMax = 3.0f;
	private bool startTimer=true;
	public bool finish = false;

	void Start()
	{


	}
	void Init(){
		timer = timerMax ;
		GetComponent<Slider> ().maxValue = timerMax;
		GetComponent<Slider> ().value = timerMax;
	}
	void Update()
	{
		if (startTimer) {

			timer -= Time.deltaTime;
			GetComponent<Slider>().value = timer;
			if (timer < 0) {
				timer =0;
				finish = true;
			}
		}
	}
	public void addBonus(float bonus){
		timer += bonus;
		if (timer > timerMax) {
			timer = timerMax;
		}
	}
	public void StartTimer(){
		startTimer = true;
	}
	public void PauseTimer(){
		startTimer = false;
	}
	public void ResetTimer (){
		timer = timerMax;
		finish = false;
	}
	public void SetTimer(int _timerMax){
		timerMax = _timerMax;
		Init();
	}

}
