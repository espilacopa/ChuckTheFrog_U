using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public Camera cam;
	private float maxX;
	private float maxY;
	private float minX;
	private float minY;	
	public GameObject tongue;
	public GameObject frog;
	private ArrayList flyTab;
	private ArrayList enemieTab;
	private Vector4 screenSize;
	public GameObject endPanel;
	public GameObject timer;
	private bool finish;
	private bool pause =false;
	private Level curentLevel;
	private int currentWaveId = 0;
	//private int currentLevelId = 0;
	private Wave currentWave;
	private Vector4 ScreenSize;
	private int countMax=3;
	private int countDown;
	private bool noMoreWave = false;


	public static GameController control;
	void Awake(){
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		
		} else if (control != this) {
			Destroy (gameObject);
		
		}
	
	}
	void initGame(){
		endPanel.GetComponent<ScorePanel> ().Init ();
		timer.GetComponent<Timer> ().SetTimer (curentLevel.timer);
		SpawnFly ();
	}
	void Start ()
	{


		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 MaxScreen = cam.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0)) ;
		Vector3 MinScreen = cam.ScreenToWorldPoint (new Vector3 (0, 0, 0)) ;

		screenSize = new Vector4 (MaxScreen.x,MaxScreen.y,MinScreen.x,MinScreen.y);

		Vector3 posFrog = new Vector3 (screenSize.z, screenSize.w, 0);//-cam.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		posFrog.z = 0;
		frog.transform.position =posFrog ;
		flyTab = new ArrayList ();
		enemieTab = new ArrayList ();
		curentLevel = Levels.getLevel(Levels.curentLevel);
		if (curentLevel.waveList.Count>0) {
			noMoreWave = false;
			currentWave = curentLevel.waveList [currentWaveId] as Wave;
		} else {
			noMoreWave = true;
		}
		initGame ();
		StartCoroutine ("GameStart");
	}
	IEnumerator GameStart(){
		Freeze ();

		GameObject.FindWithTag("Panel_CountDown").GetComponent<CanvasGroup>().alpha = 1;
		for (countDown = countMax; countDown>0;countDown--)
		{
			GameObject.FindWithTag("CountDown").GetComponent<Text> ().text = countDown.ToString();
			yield return new WaitForSeconds(1);

		}
		GameObject.FindWithTag("Panel_CountDown").GetComponent<CanvasGroup>().alpha = 0;
		
		unFreeze ();
		
	}
	void SpawnFly ()
	{
		Vector4 flyZone = new Vector4 ();
		flyZone.x = screenSize.x - (screenSize.x -screenSize.z)*0.03f;
		flyZone.y = screenSize.y - (screenSize.y -screenSize.w)*0.2f;
		flyZone.z = screenSize.z + (screenSize.x -screenSize.z)*0.2f;		
		flyZone.w = screenSize.w + (screenSize.y - screenSize.w) * 0.2f;
		for (int j = 0; j < curentLevel.flyDefList.Count ; j++) {
			for (int i=0;i<(curentLevel.flyDefList[j] as FlyDef).nbFly;i++){
				Vector3 spawnPosition = new Vector3 (Random.Range (flyZone.z,flyZone.x), Random.Range (flyZone.w,flyZone.y), 0.0f);
				Quaternion spawnRotation = Quaternion.identity;
				GameObject temp = Instantiate (Resources.Load((curentLevel.flyDefList[j] as FlyDef).type), spawnPosition, spawnRotation) as GameObject;
				temp.name = "Fly"+flyTab.Count;
				IFlyable component = (IFlyable) temp.GetComponent( typeof(IFlyable) );
				component.SetId(flyTab.Count);
				component.Init(flyZone, (curentLevel.flyDefList[j] as FlyDef).velocidadMax , (curentLevel.flyDefList[j] as FlyDef).maxAngle);
				component.SetControler(gameObject);
				if((curentLevel.flyDefList[j] as FlyDef).CompoDef.Count >0){
					for (int k=0;k<(curentLevel.flyDefList[j] as FlyDef).CompoDef.Count;k++){
						CompoDef compD = (curentLevel.flyDefList[j] as FlyDef).CompoDef[k] as CompoDef;
						switch(compD.type){
							case "Animator":
							Debug.Log(compD.name.ToString()+"  "+compD.val.ToString());
								temp.GetComponent<Animator>().SetBool((compD.name),compD.val);
							break;

						}
					}

				}
				flyTab.Add (temp);
			}

		}
	}

	void addEnemies ()
	{
		Vector4 flyZoneE = new Vector4 ();
		flyZoneE.x = screenSize.x - (screenSize.x -screenSize.z)*0.03f;
		flyZoneE.y = screenSize.y - (screenSize.y -screenSize.w)*0.2f;
		flyZoneE.z = screenSize.x - (screenSize.x -screenSize.z)*0.3f;		
		flyZoneE.w = screenSize.w + (screenSize.y - screenSize.w) * 0.2f;
		Quaternion spawnRotation = Quaternion.identity;
		EnemieD tempEnemie;
		for (int k = 0; k < currentWave.enemieList.Count; k++) {
			tempEnemie = currentWave.enemieList[k] as EnemieD;
			for (int j = 0; j < tempEnemie.nb; j++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (flyZoneE.z,flyZoneE.x), Random.Range (flyZoneE.w,flyZoneE.y), 0.0f);
				GameObject temp = Instantiate (Resources.Load(tempEnemie.type),spawnPosition,spawnRotation ) as GameObject;
				IFlyable component = (IFlyable) temp.GetComponent( typeof(IFlyable) );
				component.SetId(j);
				component.SetControler(gameObject);
				component.Init(flyZoneE, 0 , 0);
				component.type = "Enemie";
				enemieTab.Add (temp);
			}

		}	
		if (curentLevel.waveList.Count > (currentWaveId+1)) {
			currentWaveId += 1;
			Debug.Log (curentLevel.waveList.Count+"  "+ currentWaveId);
			currentWave = curentLevel.waveList [currentWaveId] as Wave;
		} else {
			noMoreWave = true;
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{

		Debug.Log (countDown + "  " + timer.GetComponent<Timer> ().finish);
		if (countDown == 0) {
			if (timer.GetComponent<Timer> ().finish && !finish) {
				
				End ();
			}else if (pause) {
				Freeze ();
			} else if (!noMoreWave) {
				if (Mathf.Floor (timer.GetComponent<Timer> ().timer) == currentWave.waveTime) {
					addEnemies ();
				}
			}
		}



	}
	void End(){
		tongue.GetComponent<Tongue> ().Reset ();
		finish = true;
		Freeze();
		endPanel.GetComponent<ScorePanel> ().Show((curentLevel.goal.accomplished(flyTab.Count,timer.GetComponent<Timer> ().timer)));
	}
	void Freeze(){
		GameObject tempFly;
		for (int j = 0; j < flyTab.Count; j++) {
			tempFly = flyTab[j] as GameObject;
			IFlyable component = (IFlyable) tempFly.GetComponent( typeof(IFlyable) );
			component.Freeze();
		}
		for (int k = 0; k < enemieTab.Count; k++) {
			tempFly = enemieTab[k] as GameObject;
			IFlyable component = (IFlyable) tempFly.GetComponent( typeof(IFlyable) );
			component.Freeze();
		}

		timer.GetComponent<Timer> ().PauseTimer();
		tongue.GetComponent<Tongue>().Pause();

	
	}
	void unFreeze(){
		endPanel.GetComponent<ScorePanel> ().Init ();
		GameObject tempFly;
		for (int j = 0; j < flyTab.Count; j++) {
			tempFly = flyTab[j] as GameObject;
			IFlyable component = (IFlyable) tempFly.GetComponent( typeof(IFlyable) );
			component.UnFreeze();
		}

		for (int k = 0; k < enemieTab.Count; k++) {
			tempFly = enemieTab[k] as GameObject;
			IFlyable component = (IFlyable) tempFly.GetComponent( typeof(IFlyable) );
			component.UnFreeze();
		}
		timer.GetComponent<Timer> ().StartTimer();
		tongue.GetComponent<Tongue>().UnPause();
		pause = false;
	}
	public void DestroyMe(int id){
		IFlyable component;
		for (var i=0; i< flyTab.Count; i++) {
			component = (IFlyable) (flyTab[i]as GameObject).GetComponent( typeof(IFlyable) );
			if(component.id == id){
				Destroy (flyTab [i] as GameObject);
				flyTab.RemoveAt (i);
			}
		}
		if (flyTab.Count == 0) {
			End ();
		}
	}
	public void addBonus(){
		timer.GetComponent<Timer>().addBonus(curentLevel.timeBonus);
	}

	public void nextLevel(){
		Levels.curentLevel ++;
		Start ();

	}


	public void Menu(){

		SceneManager.LoadScene ("Menu");
	
	}
	public void Again(){
		finish = false;
		noMoreWave = false;
		timer.GetComponent<Timer> ().ResetTimer();
		while (flyTab.Count>0) {
			Destroy (flyTab [flyTab.Count-1] as GameObject);
			flyTab.RemoveAt (flyTab.Count-1);
		}
		while (enemieTab.Count>0) {
			Destroy (enemieTab [enemieTab.Count-1] as GameObject);
			enemieTab.RemoveAt (enemieTab.Count-1);
		}
		unFreeze ();
		SpawnFly ();

	}
	public void Pause(){
		pause = !pause;
		if (pause)
			Freeze ();
		else
			unFreeze ();
	}

}


