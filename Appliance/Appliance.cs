using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObjectComparator : IEqualityComparer<FoodObject>
{
	public bool Equals(FoodObject lhs, FoodObject rhs)
	{
		return ((lhs.getName() == rhs.getName()) && (lhs.getQuantity() == rhs.getQuantity()));
	}
	
	public int GetHashCode(FoodObject food)
	{
		return food.getName().GetHashCode();
	}
}

public class Appliance : MonoBehaviour
{
	public Transform player;
	public float range = 2f;
	public Bag bag;
	
    private bool inUse = false;
    private string appName;
    private string icon;
    private string prevIng;
    private HashSet<FoodObject> ingredientSet = new HashSet<FoodObject>(new FoodObjectComparator());

    //GETTER METHODS
    public bool getUsage()
    {
        return inUse;
    }

    public string getName()
    {
        return appName;
    }

    public string getIcon()
    {
        return icon;
    }

    private string getPrevIng()
    {
        return prevIng;
    }

    public HashSet<FoodObject> getIngSet()
    {
        return ingredientSet;
    }

   

    //SETTER METHODS
    public void setUsage(bool newUse)
    {
        inUse = newUse;
    }

    public void setName(string newName)
    {
        appName = newName;
    }

    public void setIcon(string newIcon)
    {
        icon = newIcon;
    }

    public void setPrevIng(string newFoodName)
    {
        prevIng = newFoodName;
    }

    public void setIngSet(HashSet<FoodObject> newSet)
    {
        ingredientSet = newSet;
    }


    //OTHER METHODS

    //switch usage true -> false, false -> true
    public void switchUsage()
    {
        if(inUse)
        {
            inUse = false;
            return;
        }
        inUse = true;
        return;
    }

    //adds and ingredient into the appliance
    public void addIngToApp(FoodObject food, HashSet<FoodObject> foodSet)
    {
		food.subOneQ();
		PlayerData.player.SetCurrentFood(food);
		
		food = new FoodObject(food);
		food.setQuantity(1);
		
        foreach(FoodObject ing in foodSet)
        {
            if(ing.getName() == food.getName())
            {
				Debug.Log("match");
                ing.addOneQ();
				prevIng = food.getName();
				return;
            }
        }
		foodSet.Add(food);
        prevIng = food.getName();
    }

    //removes the most recent added ingredient from appliance
    public FoodObject removeIngFromApp()
    {
        FoodObject food = new FoodObject();
        foreach (FoodObject ing in ingredientSet)
        {
            if(ing.getName() == prevIng)
            {
                //decrease quantity by one
                ing.subOneQ();
                food = new FoodObject(ing);
                food.setQuantity(1);
                if(ing.getQuantity() <= 0)
                {
                    //remove from set
                    ingredientSet.Remove(ing);
                }
                return food;
            }
        }
        return food;
    }

    //produces FoodObject product
    public FoodObject execute()
    {
        //TEMPORARY
        FoodObject food = new FoodObject();
        //TEMPORARY
        
        //find if this recipe exists
        List<FoodObject> foodList = PlayerData.player.GetTable()[ingredientSet.Count];
        
        //reset the set and prevIng and usage
        ingredientSet.Clear();
        prevIng = "";
        inUse = false;

        return food;
    }
	
	public void MouseDown(HashSet<FoodObject> foodSet, string appName)
	{
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		
		if(distance <= range)
		{
			if(PlayerData.player.GetCurrentFood() != null)
			{
				if(PlayerData.player.GetCurrentFood().getName() == "Plate")
				{
					FoodObject food = null;
					FoodObject recipe;
					List<List<FoodObject>> recipeTable = PlayerData.player.GetRecipeTable();
					bool fail = true;
					
					for(int i = 0; i < recipeTable.Count; ++i)
					{
						for(int j = 0; j < recipeTable[i].Count; ++j)
						{
							recipe = recipeTable[i][j];
							
							if(recipe.getAppliance() == appName && foodSet.SetEquals(recipe.getIngNeeded()))
							{
								fail = false;
								Debug.Log("matching recipe!");
								food = new FoodObject(recipe);
								food.setPlate(true);
								break;
							}
						}
					}
					
					if(fail)
					{
						food = new FoodObject("FAIL", "Sprites/temp2", "Models/salmon", plate: true);
					}
					
					foodSet.Clear();
					PlayerData.player.SetCurrentFood(food);
					bag.Refresh();
				}
				else
				{
					addIngToApp(PlayerData.player.GetCurrentFood(), foodSet);
					if(PlayerData.player.GetCurrentFood().getQuantity() <= 0)
					{
						PlayerData.player.SetCurrentFood(null);
					}
					
					bag.Refresh();
					
					// debug
					foreach(FoodObject ing in foodSet)
					{
						Debug.Log(ing.getName() + " x" + ing.getQuantity());
					}
				}
			}
		}
	}
}
