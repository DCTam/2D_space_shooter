using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Types{Basic, TopLevel, TopLevelNoCol, BottomLevel, Fighter}

public abstract class Basic : MonoBehaviour 
{
	public Controller controller;
	public Team team;
	private List<Types> types;

	// Use this for initialization
	protected void super () 
	{
		types = new List<Types>();
		addType(Types.Basic);
	}

	public void addType(Types type)
	{
		if(!isType(type))
			types.Add (type);
	}
	
	public bool isType(Types type)
	{
		if (types == null)
			return false;
		for (int i = 0; i < types.Count; i++) 
			if (types [i] == type)
				return true;
		return false;
	}
}
