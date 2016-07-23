using UnityEngine;
using System.Collections;

public class Destructible : Basic 
{
	public int health;

	public void takeDamage(int dmg)
	{
		health -= dmg;
	}
}
