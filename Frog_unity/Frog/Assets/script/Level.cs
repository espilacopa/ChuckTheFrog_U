using UnityEngine;
using System.Collections;

public class Level
{
	public ArrayList waveList;
	public int id;
	public string definition="toto";
	public int nbFly;
	public int nbFlyBonus;
	public int timer;
	public Goal goal;
	public float timeBonus;
	public ArrayList flyDefList;
	public bool locked=true;
	public string img;
	
	
	public Level()
	{
		flyDefList = new ArrayList ();
		waveList = new ArrayList ();
	}
	public void AddWave(Wave _wave){
		waveList.Add (_wave);
	}
}