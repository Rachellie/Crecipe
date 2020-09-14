using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FoodObject
{
    private string foodName;
    private string icon;
	private string model;
    private string appliance;
    private float price = 1f;
    private int quantity = 1;
    private HashSet<FoodObject> ingredientsNeeded;
	private bool onPlate = false;

    //default constructor
    public FoodObject()
    {
        foodName = "FAIL";
        icon = "Sprites/fail";
		model = "Models/salmon";
		appliance = "Pan";
        price = 1f;
        quantity = 1;
        ingredientsNeeded = null;
		onPlate = false;
    }

    public FoodObject(string name = "FAIL", string i = "Sprites/fail", string m = "Models/salmon", string app = "Pan", float p = 1f, int q = 1, HashSet<FoodObject> need = null, bool plate = false)
    {
        foodName = name;
        icon = i;
		model = m;
        appliance = app;
        price = p;
        quantity = q;
        ingredientsNeeded = need;
		onPlate = plate;
    }

    // copy constructor
    public FoodObject(FoodObject other)
    {
        foodName = other.foodName;
        icon = other.icon;
		model = other.model;
        appliance = other.appliance;
        price = other.price;
        quantity = other.quantity;
        ingredientsNeeded = other.ingredientsNeeded;
		onPlate = other.onPlate;
    }


    //GETTER METHODS
    public string getName()
    {
        return foodName;
    }

    public string getIcon()
    {
        return icon;
    }
	
	public string getModel()
    {
        return model;
    }

    public string getAppliance()
    {
        return appliance;
    }

    public float getPrice()
    {
        return price;
    }

    public int getQuantity()
    {
        return quantity;
    }

    public HashSet<FoodObject> getIngNeeded()
    {
        return ingredientsNeeded;
    }
	
	public bool getPlate()
	{
		return onPlate;
	}

    //SETTER METHODS
    public void setName(string newName)
    {
        foodName = newName;
    }

    public void setIcon(string newIcon)
    {
        icon = newIcon;
    }
	
	public void setModel(string newModel)
    {
        model = newModel;
    }

    public void setAppliance(string newAppliance)
    {
        appliance = newAppliance;
    }

    public void setPrice(float newPrice)
    {
        price = newPrice;
    }

    public void setQuantity(int newQuantity)
    {
        quantity = newQuantity;
    }

    public void setIngNeeded(HashSet<FoodObject> newSet)
    {
        ingredientsNeeded = newSet;
    }
	
	public void setPlate(bool newPlate)
	{
		onPlate = newPlate;
	}

    //OTHER METHODS HERE

    //add one to quantity
    public void addOneQ()
    {
        quantity += 1;
    }

    //subtract one to quantity
    public void subOneQ()
    {
        quantity -= 1;
    }

}
