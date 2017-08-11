using UnityEngine;
using System.Collections;

public class LevelList
{
	public ArrayList levels;
	
	public LevelList()
	{
		levels = new ArrayList ();
	}
	public void AddLevel(Level _level){
		
		_level.id = levels.Count;
		levels.Add (_level);
	}
	public Level getLevel(int _id){
		return levels [_id] as Level;
		
	}
	public int length
	{
		get { return levels.Count; }
		//set { _myProperty = value; }
	}
}
