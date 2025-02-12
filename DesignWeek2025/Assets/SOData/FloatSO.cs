using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Created with the help of this tutorial: https://www.youtube.com/watch?v=MBM_4zrQHao

[CreateAssetMenu]
public class FloatSO : ScriptableObject
{
	[SerializeField]
	private float _value;

	public float Value
	{
		get { return _value; }
		set { _value = value; }
	}

}
