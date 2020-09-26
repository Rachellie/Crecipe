﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pot : Appliance, IPointerDownHandler
{
    public static HashSet<FoodObject> ingredientSet = new HashSet<FoodObject>(new FoodObjectComparator());

    public void OnPointerDown(PointerEventData data)
    {
        MouseDown(ref ingredientSet, "Pot", "pot");
    }
}