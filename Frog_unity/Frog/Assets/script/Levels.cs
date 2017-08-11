using UnityEngine;
using System.Collections;
using System.Xml;

public static class Levels  {
	public static int curentLevel;
	public static LevelList levelTab;
	public static Level getLevel(int id){
		if (levelTab.levels.Count == 0) {
			init ();
		}
		return (levelTab.getLevel(id) as Level);
	
	}
	public static void init(XmlDocument defGame = default(XmlDocument)){
		if (defGame == null) {
			defGame = new XmlDocument ();
			defGame.LoadXml (System.IO.File.ReadAllText (Application.dataPath + "/Resources/GameLevelDef"));
		
		}
		levelTab = new LevelList();
		foreach(XmlElement node in defGame.SelectNodes("Game/Level"))
		{
			Level level = new Level();
			levelTab.AddLevel(level);
			level.id = int.Parse(node.GetAttribute("id"));
			level.timer = int.Parse(node.GetAttribute("timer"));
			level.definition =  node.SelectSingleNode("Def").InnerText;
			level.img = node.GetAttribute("img");
			foreach(XmlElement flyNode in node.SelectNodes("Flies/FlyDef")){
				FlyDef fly = new FlyDef();
				fly.velocidadMax = float.Parse(flyNode.GetAttribute("velo"));
				fly.maxAngle = float.Parse(flyNode.GetAttribute("angl"));
				fly.bonus = float.Parse(flyNode.GetAttribute("bonus"));
				fly.type = flyNode.GetAttribute("type");
				fly.nbFly = int.Parse(flyNode.GetAttribute("nbFly"));
				fly.CompoDef = new ArrayList();
				foreach(XmlElement compoDef in flyNode.SelectNodes("GetComponent/CompoDef")){
					CompoDef compD = new CompoDef();
					compD.name = compoDef.GetAttribute("id");
					compD.type = compoDef.GetAttribute("param");
					compD.val = bool.Parse(compoDef.GetAttribute("val"));
					fly.CompoDef.Add(compD);
					
				}
				level.flyDefList.Add(fly);
			}
			level.waveList = new ArrayList();

			foreach(XmlElement waveNode in node.SelectNodes("Waves/Wave")){
				Wave wave = new Wave();
				wave.id = int.Parse(waveNode.GetAttribute("id"));
				wave.waveTime = int.Parse(waveNode.GetAttribute("time"));
				wave.enemieList = new ArrayList();
				foreach(XmlElement enemieNode in waveNode.SelectNodes("Enemies/Enemy")){
					EnemieD enemie = new EnemieD();
					enemie.nb = int.Parse(enemieNode.GetAttribute("nbEnemie"));
					enemie.type = enemieNode.GetAttribute("type");
					wave.enemieList.Add(enemie);
					
				}
				level.AddWave(wave);
			}
			level.goal = new Goal();
			level.goal.Def =  node.SelectSingleNode("Goal/Def").InnerText;
			foreach(XmlElement goalNode in node.SelectNodes("Goal/Star")){
				Debug.Log(goalNode.GetAttribute("id")+"  "+goalNode.GetAttribute("time"));
				level.goal.goals.Add(float.Parse(goalNode.GetAttribute("time")));
			}
			getLevel (0).locked = false;
		}

	}
}
