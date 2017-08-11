using UnityEngine;
using System.Collections;
using System.Xml;
using UnityEngine.SceneManagement;

public class WelcomeControler : MonoBehaviour 
{
	
	private XmlDocument _xmlDoc;
	private string _path;
	private string _fileInfo;
	private TextAsset _textXml;
	private string _fileName;

	void Awake()
		
	{
		_fileName = "GameLevelDef";
	}
	
	void Start ()
	{
		loadXMLFromAssest();
		Levels.init (_xmlDoc);
	}
	public void StartGame(){
		
		//SceneManager.LoadScene ("Main");
		SceneManager.LoadScene ("Menu");
	}
	private void loadXMLFromAssest()
	{
		_xmlDoc = new XmlDocument();
		if(System.IO.File.Exists(getPath()))
		{
			_xmlDoc.LoadXml(System.IO.File.ReadAllText(getPath()));
		}
		else
		{
			_textXml = (TextAsset)Resources.Load(_fileName, typeof(TextAsset));
			_xmlDoc.LoadXml(_textXml.text);
		}

	}

	private string getPath(){
		#if UNITY_EDITOR
		return Application.dataPath +"/Resources/"+_fileName;
		#elif UNITY_ANDROID
		return Application.persistentDataPath+_fileName;
		#elif UNITY_IPHONE
		return GetiPhoneDocumentsPath()+"/"+_fileName;
		#else
		return Application.dataPath +"/"+ _fileName;
		#endif
	}
	private string GetiPhoneDocumentsPath()
	{
		// Strip "/Data" from path 
		string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
		// Strip application name 
		path = path.Substring(0, path.LastIndexOf('/'));
		return path + "/Documents";
	}
}
