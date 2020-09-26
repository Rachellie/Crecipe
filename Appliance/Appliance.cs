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
	public Transform appIng;
	
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
		Transform onHead = player.GetChild(2);
		onHead.SetParent(null, true);
		StartCoroutine(Slerp(onHead));
		
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
	
	IEnumerator Slerp(Transform food)
	{
		float timeElapsed = 0f;
		
		Vector3 start = food.position;
		Vector3 end = transform.position; // self
		
		while(timeElapsed < 0.25f)
		{
			food.position = Vector3.Slerp(start, end, timeElapsed/0.25f);
			food.Rotate(0, 2, 0);
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		
		Destroy(food.gameObject);
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
	
	public void MouseDown(ref HashSet<FoodObject> foodSet, string appName, string sound)
	{
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		
		if(distance <= range)
		{
			if(PlayerData.player.GetCurrentFood() != null)
			{
				if(PlayerData.player.GetCurrentFood().getName() == "Plate")
				{
					FoodObject food = null;
					bool fail = true;

                    for(int i = 0; i < PlayerData.player.GetLevel() && i < PlayerData.player.GetRecipeDB().Count; i++)
                    {
                        foreach(FoodObject match in PlayerData.player.GetRecipeDB()[i].Values)
                        {
                            if(match.getAppliance() == appName && foodSet.SetEquals(match.getIngNeeded()))
                            {
                                fail = false;
                                Debug.Log("Matching recipe!");
                                food = new FoodObject(match);
                                break;
                            }
                        }
                    }
					
					if(fail)
					{
						Debug.Log("searching hidden db");
						for(int i = 0; i < PlayerData.player.GetLevel() && i < PlayerData.player.GetHiddenDB().Count; i++)
						{
							foreach(FoodObject match in PlayerData.player.GetHiddenDB()[i].Values)
							{
								if(match.getAppliance() == appName && foodSet.SetEquals(match.getIngNeeded()))
								{
									fail = false;
									Debug.Log("Matching HIDDEN recipe!");
									food = new FoodObject(match);
                                    PlayerData.database.transferRecipe(i, match.getName());
									break;
								}
							}
						}
					}
					
					if(fail)
					{
						food = PlayerData.player.GetDB()[0]["Fail"];
					}
					
					foodSet.Clear();
					PlayerData.player.SetCurrentFood(food);
					bag.Refresh();
					
					ClientSend.UpdateAppliance(appName, "Plate");
				}
				else
				{
					ClientSend.UpdateAppliance(appName, PlayerData.player.GetCurrentFood().getName()); //
					addIngToApp(PlayerData.player.GetCurrentFood(), foodSet);
                    FindObjectOfType<AudioManager>().Play(sound);
					
					bag.Refresh();
					
					// debug
					foreach(FoodObject ing in foodSet)
					{
						Debug.Log(ing.getName() + " x" + ing.getQuantity());
					}
				}
			}
			else // open popup to show set of ingredients inside
			{
				if(appIng.gameObject.activeInHierarchy)
				{
					appIng.gameObject.SetActive(false);
				}
				else
				{
					appIng.gameObject.SetActive(true);
				}
				
				AppIng appIngScript = appIng.GetComponent<AppIng>();
				appIngScript.SetContents(ref foodSet);
			}
		}
	}
}
