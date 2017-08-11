using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	private Transform BlackOut;
	private Animator BlackOutAnim;
	private float BlackOutAnimTime = 0.14f;


	private Transform MainMenu;


	private Transform PartMenu;

	// Use this for initialization
	void Start () {
		BlackOut = transform.parent.FindChild("BlackOut");
		BlackOutAnim = BlackOut.GetComponent<Animator> ();

		MainMenu = transform.parent.FindChild("MainMenu");
		PartMenu = transform.parent.FindChild("PartMenu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MainMenuPartMenuTransition(){//переход после главного меню в меню выбора части
		StartBlackOutObj ();//включается изображение, которое используется для затемнения
		StartBlackOutAnim ();//сначала включается анимация затемнения

		Invoke ("StartBlackOutAnim", BlackOutAnimTime);//выключить анимацию затемнения в переходе при его завершении
		Invoke ("EndBlackOutObj", BlackOutAnimTime);//выключить изображение, которое используется для затемнения при окончании анимации

		Invoke ("MainMenuToPartMenuActivate", BlackOutAnimTime / 2);//переключаются экраны с меню на выбор части

	}


	private void SetActivateBlackOut(bool state){//  вкючается/выключается изображение для затемнения
		BlackOut.gameObject.SetActive (state);
	}


	private void StartBlackOutAnim(){//включается анимация затемнения
		BlackOutAnim.SetBool ("isPlayBlackOutAnim", true);
	}


	private void EndBlackOutAnim(){//выключается анимация затемнения
		BlackOutAnim.SetBool ("isPlayBlackOutAnim", false);
	}


	private void StartBlackOutObj(){//включается изображение, которое используется для затемнения
		SetActivateBlackOut(true);
	}
	
	private void EndBlackOutObj(){//выключается изображение, которое используется для затемнения
		SetActivateBlackOut (false);
	}


	private void SetActiveMainMenu(bool state){//включается/выключается главное меню
		MainMenu.gameObject.SetActive (state);
	}

	private void SetActivatePartMenu(bool state){//включается/выключается меню выбора части
		PartMenu.gameObject.SetActive (state);
	}

	private void MainMenuToPartMenuActivate(){//включение/выключение панелей при переходе от экрана главного меню к экрану выбора части
		MainMenu.gameObject.SetActive (false);
		PartMenu.gameObject.SetActive (true);
	}

}
