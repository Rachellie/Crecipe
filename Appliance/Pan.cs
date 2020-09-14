using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Appliance
{
	private static HashSet<FoodObject> ingredientSet = new HashSet<FoodObject>(new FoodObjectComparator());
	
	void OnMouseDown()
	{
		MouseDown(ingredientSet, "Pan");
	}
}
