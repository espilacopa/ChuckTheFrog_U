using UnityEngine;
using System.Collections;

public interface IFlyable
{

	int id
	{
		get;
	}
	string type {
		set;
		get;
	}
	void SetControler(GameObject _controler);
	void SetId(int _id);
	void Freeze ();
	void UnFreeze ();
	void Init (Vector4 zone,float _velocidadMax,float _maxAngle);
	void StopFly ();
	void DestroyE();
}
