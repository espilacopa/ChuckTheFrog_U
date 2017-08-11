using UnityEngine;
using System.Collections;

public class Wave
{
	public ArrayList enemieList;
	public int id;
	public int waveTime;
	public Wave()
	{
	}
	public void AddEnemie(EnemieD _enemie){
		enemieList.Add (_enemie);
	}
}