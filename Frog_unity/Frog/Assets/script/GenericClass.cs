using UnityEngine;
using System.Collections;

public class GenericClass 
{

	public T GenericMethod<T>(T param){
		return param;
	}
}
