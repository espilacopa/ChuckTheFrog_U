using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuControler : MonoBehaviour {
	public Button button;
	public Transform list;
	// Use this for initialization
	void Start () {
		setBtnsLevel ();
	
	}
	// Update is called once per frame
	void Update () {
		

	}

	public void setBtnsLevel(){
		for (int j = 0; j < Levels.levelTab.length; j++) 
		{
			Button	btnLevel = Instantiate (button) as Button;
			int local_i = (Levels.levelTab.levels [j] as Level).id;
			btnLevel.onClick.AddListener(() => launchGame(local_i));
			btnLevel.transform.SetParent(list, false);
			Sprite spr = Resources.Load<Sprite> ((Levels.levelTab.levels[j] as Level).img);
			btnLevel.GetComponents<LevelBtn>()[0].illustration.GetComponents<Image>()[0].sprite = spr;

		}

	}

	public void launchGame(int levelId){
		Debug.Log ("launchGame " + levelId);
		Levels.curentLevel = levelId;
		SceneManager.LoadScene ("Main");
	}
}
