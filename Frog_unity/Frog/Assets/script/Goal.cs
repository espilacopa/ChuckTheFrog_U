using UnityEngine;
using System.Collections;

public class Goal
{
	public string Def;
	public ArrayList goals;
	
	public Goal( )
	{
		goals = new ArrayList ();
	}
	public ArrayList accomplished(int _remainFly, float _time){
		ArrayList temp = new ArrayList ();
		if (_remainFly == 0 && _time > (float)goals[0]) {
			temp.Add(true);
			temp.Add(true);
			temp.Add(true);
		} else if (_remainFly == 0 && _time >(float)goals[1]) {
			temp.Add(true);
			temp.Add(true);
			temp.Add(false);
		} else if (_remainFly == 0 && _time >= (float)goals[2]) {
			temp.Add(true);
			temp.Add(false);
			temp.Add(false);
		} else {
			temp.Add(false);
			temp.Add(false);
			temp.Add(false);
		}
		return temp;
		
	}
}