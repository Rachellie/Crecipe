using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashTable
{
    private List<List<FoodObject>> table;
    private int row; //# of levels
	
	public void BuildDatabase()
	{
        //for the shop
		table = new List<List<FoodObject>>()
		{
			// str foodName, str icon, str model, str appliance, str tag, float price, int quantity, hashset ingNeeded
			// LEVEL 0
			new List<FoodObject>()
			{
				new FoodObject("FAIL", "Sprites/temp2", "Models/salmon", "Toaster", 0f),
			},
			// level 1
			new List<FoodObject>()
			{
                new FoodObject("Apple", "Sprites/apple", "Models/salmon", "Knife", 1f),
                new FoodObject("Orange", "Sprites/orange", "Models/salmon", "Knife", 1f),
                new FoodObject("Pear", "Sprites/pear", "Models/salmon", "Knife", 1f),
                
            },
			// level 2
			new List<FoodObject>()
			{
                new FoodObject("Egg", "Sprites/egg", "Models/salmon", "Toaster", 1f),
                new FoodObject("Flour", "Sprites/flour", "Models/salmon", "Toaster", 1f),
                new FoodObject("Wheat", "Sprites/wheat", "Models/salmon", "Toaster", 1f),
                
            },
			// level 3
			new List<FoodObject>()
			{
                new FoodObject("Pepper", "Sprites/pepper", "Models/salmon", "Toaster", 1f),
                new FoodObject("Raw Beef", "Sprites/raw_beef", "Models/salmon", "Toaster", 3f),
                new FoodObject("Raw Chicken", "Sprites/raw_chicken", "Models/salmon", "Toaster", 2f),
                new FoodObject("Salt", "Sprites/salt", "Models/salmon", "Toaster", 1f),
            },
            //level 4
            new  List<FoodObject>()
            {
                new FoodObject("Lychee", "Sprites/lychee", "Models/salmon", "Toaster", 1f),
                new FoodObject("Mango", "Sprites/mango", "Models/salmon", "Toaster", 1f),
                new FoodObject("Watermelon", "Sprites/watermelon", "Models/salmon", "Toaster", 1f),
            },
            //level 5
            new  List<FoodObject>()
            {
                new FoodObject("Baking Powder", "Sprites/baking_powder", "Models/salmon", "Toaster", 1f),
                new FoodObject("Butter", "Sprites/butter", "Models/salmon", "Toaster", 1f),
                new FoodObject("Cheese", "Sprites/cheese", "Models/salmon", "Toaster", 1f),
                new FoodObject("Sugar", "Sprites/sugar", "Models/salmon", "Toaster", 1f),
                new FoodObject("Lettuce", "Sprites/lettuce", "Models/salmon", "Toaster", 1f),
                new FoodObject("Milk", "Sprites/milk", "Models/salmon", "Toaster", 1f),
                new FoodObject("Onion", "Sprites/onion", "Models/salmon", "Toaster", 1f),
                new FoodObject("Tomato", "Sprites/tomato", "Models/salmon", "Toaster", 1f),
            },
            //level 6
            new  List<FoodObject>()
            {
                new FoodObject("Turkey", "Sprites/turkey", "Models/salmon", "Toaster", 1f),
                new FoodObject("Potato", "Sprites/potato", "Models/salmon", "Toaster", 1f),
                new FoodObject("Red Bean", "Sprites/red_bean", "Models/salmon", "Toaster", 1f),

            },
            //level 7,
            new  List<FoodObject>()
            {
                new FoodObject("Black Tea Leaves", "Sprites/black_tea_leaves", "Models/salmon", "Toaster", 1f),
                new FoodObject("Earl Grey Leaves", "Sprites/earl_grey_leaves", "Models/salmon", "Toaster", 1f),
                new FoodObject("Green Tea Leaves", "Sprites/green_tea_leaves", "Models/salmon", "Toaster", 1f),
                new FoodObject("Jasmine Tea Leaves", "Sprites/jasmine_tea_leaves", "Models/salmon", "Toaster", 1f),
                new FoodObject("Oolong Tea Leaves", "Sprites/oolong_tea_leaves", "Models/salmon", "Toaster", 1f),
                new FoodObject("Thai Tea Leaves", "Sprites/thai_tea_leaves", "Models/salmon", "Toaster", 1f),
            }
        };
	}

    public void BuildRecipeDatabase()
    {
        //all recipes
		table = new List<List<FoodObject>>()
		{
			// level 0
			new List<FoodObject>()
			{
				//
			},
			//level 1
			new List<FoodObject>()
			{
				new FoodObject("Sliced Apples", "Sprites/sliced_apples", "Models/sliced_apples", "Knife", 1f, 1,
					new HashSet<FoodObject>()
					{
                        new FoodObject("Apple", "Sprites/apple", q: 2)
                    }
				),
				new FoodObject("Sliced Oranges", "Sprites/sliced_oranges", "Models/sliced_oranges", "Knife", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Orange", "Sprites/orange", q: 2)
                    }
                ),
                new FoodObject("Sliced Pears", "Sprites/sliced_pears", "Models/salmon", "Knife", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Pear", "Sprites/pear", q: 2)
                    }
                ),
            },
			//level 2
			new List<FoodObject>()
			{
                new FoodObject("Bread", "Sprites/bread", "Models/salmon", "Oven", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Wheat", "Sprites/wheat", q: 1),
                        new FoodObject("Flour", "Sprites/flour", q: 1)
                    }
                ),
                new FoodObject("Scrambled Egg", "Sprites/scrambled_egg", "Models/salmon", "Pan", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Egg", "Sprites/egg", q: 1)
                    }
                ),
            },
            //level 3
            new List<FoodObject>()
            {
                new FoodObject("Steak", "Sprites/steak", "Models/salmon", "Pan", 3f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Raw Beef", "Sprites/raw_beef", q: 1),
                        new FoodObject("Salt", "Sprites/salt", q: 1),
                        new FoodObject("Pepper", "Sprites/pepper", q: 1)
                    }
                ),
                new FoodObject("Pan Seared Chicken", "Sprites/pan_seared_chicken", "Models/salmon", "Pan", 2f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Raw Chicken", "Sprites/raw_chicken", q: 1),
                        new FoodObject("Salt", "Sprites/salt", q: 1),
                        new FoodObject("Pepper", "Sprites/pepper", q: 1)
                    }
                ),
            },
            //level 4
            new List<FoodObject>()
            {
                new FoodObject("Diced Mangos", "Sprites/diced_mangos", "Models/salmon", "Knife", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Mango", "Sprites/mango", q: 2)
                    }
                ),
                new FoodObject("Skinned Lychees", "Sprites/skinned_lychees", "Models/salmon", "Knife", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Lychee", "Sprites/lychee", q: 2)
                    }
                ),
                new FoodObject("Sliced Watermelon", "Sprites/sliced_watermelon", "Models/salmon", "Knife", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Watermelon", "Sprites/watermelon", q: 1)
                    }
                ),
            },
            //level 5
            new List<FoodObject>()
            {
                new FoodObject("Cake", "Sprites/cake", "Models/salmon", "Oven", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Baking Powder", "Sprites/baking_powder", q: 1),
                        new FoodObject("Butter", "Sprites/butter", q: 1),
                        new FoodObject("Egg", "Sprites/egg", q: 2),
                        new FoodObject("Flour", "Sprites/flour", q: 1),
                        new FoodObject("Milk", "Sprites/milk", q: 1),
                        new FoodObject("Sugar", "Sprites/sugar", q: 1),
                    }
                ),
                new FoodObject("Cheeseburger", "Sprites/cheeseburger", "Models/salmon", "Hand", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Sliced Cheese", "Sprites/sliced_cheese", q: 1),
                        new FoodObject("Steak", "Sprites/steak", q: 1),
                        new FoodObject("Lettuce", "Sprites/lettuce", q: 1),
                        new FoodObject("Sliced Tomatoes", "Sprites/sliced_tomatoes", q: 1),
                        new FoodObject("Bread", "Sprites/bread", q: 2),
                    }
                ),
                new FoodObject("Chopped Lettuce", "Sprites/chopped_lettuce", "Models/salmon", "Knife", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Lettuce", "Sprites/lettuce", q: 2),
                    }
                ),
                new FoodObject("Chopped Onions", "Sprites/chopped_onions", "Models/salmon", "Knife", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Onion", "Sprites/onion", q: 2),
                    }
                ),
                new FoodObject("Sliced Cheese", "Sprites/sliced_cheese", "Models/salmon", "Knife", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Cheese", "Sprites/cheese", q: 2),
                    }
                ),
                new FoodObject("Sliced Tomatoes", "Sprites/sliced_tomatoes", "Models/salmon", "Knife", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Tomato", "Sprites/tomato", q: 2),
                    }
                ),
            },
            //level 6
            new List<FoodObject>()
            {
                new FoodObject("Baked Turkey", "Sprites/baked_turkey", "Models/salmon", "Oven", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Turkey", "Sprites/turkey", q: 1),
                        new FoodObject("Salt", "Sprites/salt", q: 1),
                        new FoodObject("Pepper", "Sprites/pepper", q: 1),
                    }
                ),
                new FoodObject("Baked Potatoes", "Sprites/baked_potatoes", "Models/salmon", "Oven", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Potato", "Sprites/potato", q: 3),
                    }
                ),
                new FoodObject("Red Bean Paste", "Sprites/red_bean_paste", "Models/salmon", "Oven", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Red Bean", "Sprites/red_bean", q: 5),
                        new FoodObject("Salt", "Sprites/salt", q: 1),
                        new FoodObject("Sugar", "Sprites/sugar", q: 1),
                        new FoodObject("Water", "Sprites/water", q: 1),
                    }
                ),
            },
            //level 7
            new List<FoodObject>()
            {
                new FoodObject("Black Tea", "Sprites/black_tea", "Models/salmon", "Kettle", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Black Tea Leaves", "Sprites/black_tea_leaves", q: 5),
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                    }
                ),
                new FoodObject("Black Milk Tea", "Sprites/black_milk_tea", "Models/salmon", "Kettle", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Black Tea", "Sprites/black_tea", q: 1),
                        new FoodObject("Milk Tea", "Sprites/milk_tea", q: 1),
                    }
                ),
                new FoodObject("Earl Grey Tea", "Sprites/earl_grey_tea", "Models/salmon", "Kettle", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Earl Gray Tea Leaves", "Sprites/earl_grey_tea_leaves", q: 5),
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                    }
                ),
                new FoodObject("Earl Grey Milk Tea", "Sprites/earl_grey_milk_tea", "Models/salmon", "Kettle", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Earl Grey Tea", "Sprites/earl_grey_tea", q: 1),
                        new FoodObject("Milk Tea", "Sprites/milk_tea", q: 1),
                    }
                ),
                new FoodObject("Green Tea", "Sprites/green_tea", "Models/salmon", "Kettle", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Green Tea Leaves", "Sprites/green_tea_leaves", q: 5),
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                    }
                ),
                new FoodObject("Green Milk Tea", "Sprites/green_milk_tea", "Models/salmon", "Kettle", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Green Tea", "Sprites/green_tea", q: 1),
                        new FoodObject("Milk Tea", "Sprites/milk_tea", q: 1),
                    }
                ),
                new FoodObject("Jasmine Tea", "Sprites/jasmine_tea", "Models/salmon", "Kettle", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Jasmine Tea Leaves", "Sprites/jasmine_tea_leaves", q: 5),
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                    }
                ),
                new FoodObject("Jasmine Milk Tea", "Sprites/jasmine_milk_tea", "Models/salmon", "Kettle", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Jasmine Tea", "Sprites/jasmine_tea", q: 1),
                        new FoodObject("Milk Tea", "Sprites/milk_tea", q: 1),
                    }
                ),
                new FoodObject("Milk Tea", "Sprites/milk_tea", "Models/salmon", "Kettle", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                        new FoodObject("Milk", "Sprites/milk", q: 1),
                        new FoodObject("Sugar", "Sprites/sugar", q: 2),
                    }
                ),
                new FoodObject("Oolong Tea", "Sprites/oolong_tea", "Models/salmon", "Kettle", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Oolong Tea Leaves", "Sprites/oolong_tea_leaves", q: 5),
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                    }
                ),
                new FoodObject("Oolong Milk Tea", "Sprites/oolong_milk_tea", "Models/salmon", "Kettle", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Oolong Tea", "Sprites/oolong_tea", q: 1),
                        new FoodObject("Milk Tea", "Sprites/milk_tea", q: 1),
                    }
                ),
                new FoodObject("Thai Tea", "Sprites/thai_tea", "Models/salmon", "Kettle", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Thai Tea Leaves", "Sprites/thai_tea_leaves", q: 5),
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                    }
                ),
                new FoodObject("Thai Milk Tea", "Sprites/thai_milk_tea", "Models/salmon", "Kettle", 1f, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Thai Tea", "Sprites/thai_tea", q: 1),
                        new FoodObject("Milk Tea", "Sprites/milk_tea", q: 1),
                    }
                ),
            }
		};
    }
	
	public List<List<FoodObject>> GetTable()
	{
		return table;
	}

    //gets the index(level) of the foodObject
    private int process(FoodObject food)
    {
        return food.getIngNeeded().Count;
    }

    public void addFood(FoodObject food)
    {
        int index = process(food);
        //no duplicates
        if(!table[index].Contains(food))
        {
            table[index].Add(food);
        }
    }

    public void removeFood(FoodObject food)
    {
        int index = process(food);
        table[index].Remove(food);
    }

    public int getLevel(FoodObject food)
    {
        return process(food);
    }

    public void matchRecipe(List<FoodObject> ingList)
    {
        int index = ingList.Count;
        foreach(FoodObject food in table[index])
        {
            //check to see which one matches the ingList given (order doesn't matter, should it be a set?)
        }
    }

    //FOR DEBUGGING
    public void displayName() //display the names of each FoodObject in HashTable
    {
        for(int i = 0; i < table.Count; i++)
        {
            for(int j = 0; j < table[i].Count; j++)
            {
                Debug.Log(table[i][j].getName());
            }
        }
    }
}
