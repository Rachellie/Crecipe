using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    //list:dict:FoodObject
    //index for list is level requirement; 0 = fail or null
    //dictionary:  key(string of food's name ie. "sliced apples"):value(FoodObject)

    private List<Dictionary<string, FoodObject>> database { get; set; } //full database (all ingredients, given-recipes, hidden-recipes) -> immutable

    private List<Dictionary<string, FoodObject>> shopDatabase { get; set; } //all ingredients that can be bought from the shop -> immutable

    public List<Dictionary<string, FoodObject>> recipeDatabase { get; private set; } //all given-recipes -> mutable: hidden recipes can be added to this database when unlocked

    public List<Dictionary<string, FoodObject>> hiddenDatabase { get; private set; } //all hidden-recipes -> mutable: can be transfered to the recipeDatabase when unlocked

    //public List<Dictionary<string, FoodObject>> hiddenIngredientDatabase { get; } //all hidden-ingredients -> immutable (FUTURE UPDATE)

    public void transferRecipe(int index, string name)
    {
        recipeDatabase[index].Add(name, hiddenDatabase[index][name]); //adds new value into recipeDatabase from hiddenDatabase

        hiddenDatabase[index].Remove(name); //deletes old value from hiddenDatabase
    }

    public void BuildDatabase()
    {
        database = new List<Dictionary<string, FoodObject>>
        {
			// level 0
			new Dictionary<string, FoodObject>()
            {
                { "Fail", new FoodObject("Fail", "Sprites/fail", "Models/salmon", "NA", 0f, 1, -5) }
            },
			// level 1
			new Dictionary<string, FoodObject>()
            {
                //ingredients
				{ "Apple", new FoodObject("Apple", "Sprites/apple", "Models/apple", "Knife", 1f, 1, 0) },
                { "Orange", new FoodObject("Orange", "Sprites/orange", "Models/salmon", "Knife", 1f, 1, 0) },
                { "Pear", new FoodObject("Pear", "Sprites/pear", "Models/salmon", "Knife", 1f, 1, 0) },
                //recipes
                { "Sliced Apples", new FoodObject("Sliced Apples", "Sprites/sliced_apples", "Models/sliced_apples", "Knife", 1f, 1, 4,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Apple", "Sprites/apple", q: 2)
                    })
                },
                { "Sliced Oranges", new FoodObject("Sliced Oranges", "Sprites/sliced_oranges", "Models/sliced_oranges", "Knife", 1f, 1, 4,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Orange", "Sprites/orange", q: 2)
                    })
                },
                { "Sliced Apples", new FoodObject("Sliced Pears", "Sprites/salmon", "Models/salmon", "Knife", 1f, 1, 4,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Pear", "Sprites/pear", q: 2)
                    })
                },
            },
			// level 2
			new Dictionary<string, FoodObject>()
            {
                //ingredients
                { "Egg", new FoodObject("Egg", "Sprites/egg", "Models/salmon", "Pan", 1f, 1, -1) },
                { "Flour", new FoodObject("Flour", "Sprites/flour", "Models/salmon", "NA", 2f, 1, 0) },
                { "Wheat", new FoodObject("Wheat", "Sprites/wheat", "Models/salmon", "NA", 2f, 1, 0) },
                //recipes
                { "Bread", new FoodObject("Bread", "Sprites/bread", "Models/salmon", "Oven", 1f, 1, 8,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Wheat", "Sprites/wheat", q: 1),
                        new FoodObject("Flour", "Sprites/flour", q: 1)
                    })
                },
                { "Scrambled Egg", new FoodObject("Scrambled Egg", "Sprites/scrambled_egg", "Models/salmon", "Pan", 1f, 1, 2,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Egg", "Sprites/egg", q: 1)
                    })
                },
            },
            // level 3
			new Dictionary<string, FoodObject>()
            {
                //ingredients
                { "Pepper", new FoodObject("Pepper", "Sprites/pepper", "Models/salmon", "NA", 1f, 1, -1) },
                { "Raw Beef", new FoodObject("Raw Beef", "Sprites/raw_beef", "Models/salmon", "NA", 3f, 1, -5) },
                { "Raw Chicken", new FoodObject("Raw Chicken", "Sprites/raw_chicken", "Models/salmon", "NA", 2f, 1, -5) },
                { "Salt", new FoodObject("Salt", "Sprites/salt", "Models/salmon", "NA", 1f, 1, -1) },
                //recipes
                { "Steak", new FoodObject("Steak", "Sprites/steak", "Models/salmon", "Pan", 1f, 1, 10,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Raw Beef", "Sprites/raw_beef", q: 1),
                        new FoodObject("Salt", "Sprites/salt", q: 1),
                        new FoodObject("Pepper", "Sprites/pepper", q: 1)
                    })
                },
                { "Pan Seared Chicken", new FoodObject("Pan Seared Chicken", "Sprites/pan_seared_chicken", "Models/salmon", "Pan", 1f, 1, 10,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Raw Chicken", "Sprites/raw_chicken", q: 1),
                        new FoodObject("Salt", "Sprites/salt", q: 1),
                        new FoodObject("Pepper", "Sprites/pepper", q: 1)
                    })
                },
            },
            // level 4
			new Dictionary<string, FoodObject>()
            {
                //ingredients
                { "Lychee", new FoodObject("Lychee", "Sprites/lychee", "Models/salmon", "NA", 1f, 1, 0) },
                { "Mango", new FoodObject("Mango", "Sprites/mango", "Models/salmon", "NA", 1f, 1, 0) },
                { "Watermelon", new FoodObject("Watermelon", "Sprites/watermelon", "Models/salmon", "NA", 1f, 1, 0) },
                //recipes
                { "Diced Mangos", new FoodObject("Diced Mangos", "Sprites/diced_mangos", "Models/salmon", "Knife", 1f, 1, 4,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Mango", "Sprites/mango", q: 2)
                    })
                },
                { "Skinned Lychees", new FoodObject("Skinned Lychees", "Sprites/skinned_lychees", "Models/salmon", "Knife", 1f, 1, 4,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Lychee", "Sprites/lychee", q: 2)
                    })
                },
                { "Sliced Watermelon", new FoodObject("Sliced Watermelon", "Sprites/sliced_watermelon", "Models/salmon", "Knife", 1f, 1, 4,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Watermelon", "Sprites/watermelon", q: 2)
                    })
                },
            },
            //level 5
            new Dictionary<string, FoodObject>()
            {
                //ingredients
				{ "Baking Powder", new FoodObject("Baking Powder", "Sprites/baking_powder", "Models/salmon", "NA", 1f, 1, -1) },
                { "Butter", new FoodObject("Butter", "Sprites/butter", "Models/salmon", "NA", 2f, 1, 0) },
                { "Cheese", new FoodObject("Cheese", "Sprites/cheese", "Models/salmon", "NA", 1f, 1, 0) },
                { "Sugar", new FoodObject("Sugar", "Sprites/sugar", "Models/salmon", "NA", 1f, 1, 0) },
                { "Lettuce", new FoodObject("Lettuce", "Sprites/lettuce", "Models/salmon", "NA", 1f, 1, 0) },
                { "Milk", new FoodObject("Milk", "Sprites/milk", "Models/salmon", "NA", 1f, 1, 0) },
                { "Onion", new FoodObject("Onion", "Sprites/onion", "Models/salmon", "NA", 1f, 1, 0) },
                { "Tomato", new FoodObject("Tomato", "Sprites/tomato", "Models/salmon", "NA", 1f, 1, 0) },
                //recipes
                { "Cake", new FoodObject("Cake", "Sprites/cake", "Models/salmon", "Oven", 1f, 1, 20,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Baking Powder", "Sprites/baking_powder", q: 1),
                        new FoodObject("Butter", "Sprites/butter", q: 1),
                        new FoodObject("Egg", "Sprites/egg", q: 2),
                        new FoodObject("Flour", "Sprites/flour", q: 1),
                        new FoodObject("Milk", "Sprites/milk", q: 1),
                        new FoodObject("Sugar", "Sprites/sugar", q: 1),
                    })
                },
                { "Cheeseburger", new FoodObject("Cheeseburger", "Sprites/cheeseburger", "Models/salmon", "Hand", 1f, 1, 20,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Sliced Cheese", "Sprites/sliced_cheese", q: 1),
                        new FoodObject("Steak", "Sprites/steak", q: 1),
                        new FoodObject("Lettuce", "Sprites/lettuce", q: 1),
                        new FoodObject("Sliced Tomatoes", "Sprites/sliced_tomatoes", q: 1),
                        new FoodObject("Bread", "Sprites/bread", q: 2),
                    })
                },
                { "Chopped Lettuce", new FoodObject("Chopped Lettuce", "Sprites/chopped_lettuce", "Models/salmon", "Knife", 1f, 1, 20,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Lettuce", "Sprites/lettuce", q: 2),
                    })
                },
                { "Chopped Onions", new FoodObject("Chopped Onions", "Sprites/chopped_onions", "Models/salmon", "Knife", 1f, 1, 20,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Onion", "Sprites/onion", q: 2),
                    })
                },
                { "Sliced Cheese", new FoodObject("Sliced Cheese", "Sprites/sliced_cheese", "Models/salmon", "Knife", 1f, 1, 20,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Cheese", "Sprites/cheese", q: 2),
                    })
                },
                { "Sliced Tomatoes", new FoodObject("Sliced Tomatoes", "Sprites/sliced_tomatoes", "Models/salmon", "Knife", 1f, 1, 20,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Tomato", "Sprites/tomato", q: 2),
                    })
                },
            },
            // level 6
			new Dictionary<string, FoodObject>()
            {
                //ingredients
                { "Turkey", new FoodObject("Turkey", "Sprites/turkey", "Models/salmon", "NA", 1f, 1, -1) },
                { "Potato", new FoodObject("Potato", "Sprites/potato", "Models/salmon", "NA", 3f, 1, -5) },
                { "Red Bean", new FoodObject("Red Bean", "Sprites/red_bean", "Models/salmon", "NA", 1f, 1, -1) },
                //recipes
                { "Baked Turkey", new FoodObject("Baked Turkey", "Sprites/baked_turkey", "Models/salmon", "Oven", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Turkey", "Sprites/turkey", q: 1),
                        new FoodObject("Salt", "Sprites/salt", q: 1),
                        new FoodObject("Pepper", "Sprites/pepper", q: 1),
                    })
                },
                { "Baked Potatoes", new FoodObject("Baked Potatoes", "Sprites/baked_potatoes", "Models/salmon", "Oven", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Potato", "Sprites/potato", q: 3),
                    })
                },
                { "Red Bean Paste", new FoodObject("Red Bean Paste", "Sprites/red_bean_paste", "Models/salmon", "Oven", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Red Bean", "Sprites/red_bean", q: 5),
                        new FoodObject("Salt", "Sprites/salt", q: 1),
                        new FoodObject("Sugar", "Sprites/sugar", q: 1),
                        new FoodObject("Water", "Sprites/water", q: 1),
                    })
                },
            },
            // level 7
			new Dictionary<string, FoodObject>()
            {
                //ingredients
                { "Black Tea Leaves", new FoodObject("Black Tea Leaves", "Sprites/black_tea_leaves", "Models/salmon", "NA", 1f, 1, -5) },
                { "Earl Grey Leaves", new FoodObject("Earl Grey Leaves", "Sprites/earl_grey_leaves", "Models/salmon", "NA", 1f, 1, -5) },
                { "Green Tea Leaves", new FoodObject("Green Tea Leaves", "Sprites/green_tea_leaves", "Models/salmon", "NA", 1f, 1, -5) },
                { "Jasmine Tea Leaves", new FoodObject("Jasmine Tea Leaves", "Sprites/jasmine_tea_leaves", "Models/salmon", "NA", 1f, 1, -5) },
                { "Oolong Tea Leaves", new FoodObject("Oolong Tea Leaves", "Sprites/oolong_tea_leaves", "Models/salmon", "NA", 1f, 1, -5) },
                { "Thai Tea Leaves", new FoodObject("Thai Tea Leaves", "Sprites/thai_tea_leaves", "Models/salmon", "NA", 1f, 1, -5) },
                //recipes

                { "Black Tea", new FoodObject("Black Tea", "Sprites/black_tea", "Models/salmon", "Kettle", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Black Tea Leaves", "Sprites/black_tea_leaves", q: 5),
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                    })
                },
                { "Black Milk Tea", new FoodObject("Black Milk Tea", "Sprites/black_milk_tea", "Models/salmon", "Kettle", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Black Tea", "Sprites/black_tea", q: 1),
                        new FoodObject("Milk Tea", "Sprites/milk_tea", q: 1),
                    })
                },
                { "Earl Grey Tea", new FoodObject("Earl Grey Tea", "Sprites/earl_grey_tea", "Models/salmon", "Kettle", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Earl Gray Tea Leaves", "Sprites/earl_grey_tea_leaves", q: 5),
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                    })
                },
                { "Earl Grey Milk Tea", new FoodObject("Earl Grey Milk Tea", "Sprites/earl_grey_milk_tea", "Models/salmon", "Kettle", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Earl Grey Tea", "Sprites/earl_grey_tea", q: 1),
                        new FoodObject("Milk Tea", "Sprites/milk_tea", q: 1),
                    })
                },
                { "Green Tea", new FoodObject("Green Tea", "Sprites/green_tea", "Models/salmon", "Kettle", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Green Tea Leaves", "Sprites/green_tea_leaves", q: 5),
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                    })
                },
                { "Green Milk Tea", new FoodObject("Green Milk Tea", "Sprites/green_milk_tea", "Models/salmon", "Kettle", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Green Tea", "Sprites/green_tea", q: 1),
                        new FoodObject("Milk Tea", "Sprites/milk_tea", q: 1),
                    })
                },
                { "Jasmine Tea",  new FoodObject("Jasmine Tea", "Sprites/jasmine_tea", "Models/salmon", "Kettle", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Jasmine Tea Leaves", "Sprites/jasmine_tea_leaves", q: 5),
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                    })
                },
                { "Jasmine Milk Tea", new FoodObject("Jasmine Milk Tea", "Sprites/jasmine_milk_tea", "Models/salmon", "Kettle", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Jasmine Tea", "Sprites/jasmine_tea", q: 1),
                        new FoodObject("Milk Tea", "Sprites/milk_tea", q: 1),
                    })
                },
                { "Milk Tea", new FoodObject("Milk Tea", "Sprites/milk_tea", "Models/salmon", "Kettle", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                        new FoodObject("Milk", "Sprites/milk", q: 1),
                        new FoodObject("Sugar", "Sprites/sugar", q: 2),
                    })
                },
                { "Oolong Tea", new FoodObject("Oolong Tea", "Sprites/oolong_tea", "Models/salmon", "Kettle", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Oolong Tea Leaves", "Sprites/oolong_tea_leaves", q: 5),
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                    })
                },
                { "Oolong Milk Tea", new FoodObject("Oolong Milk Tea", "Sprites/oolong_milk_tea", "Models/salmon", "Kettle", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Oolong Tea", "Sprites/oolong_tea", q: 1),
                        new FoodObject("Milk Tea", "Sprites/milk_tea", q: 1),
                    })
                },
                { "Thai Tea", new FoodObject("Thai Tea", "Sprites/thai_tea", "Models/salmon", "Kettle", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Thai Tea Leaves", "Sprites/thai_tea_leaves", q: 5),
                        new FoodObject("Boiling Water", "Sprites/boiling_water", q: 2),
                    })
                },
                { "Thai Milk Tea", new FoodObject("Thai Milk Tea", "Sprites/thai_milk_tea", "Models/salmon", "Kettle", 1f, 1, 1,
                    new HashSet<FoodObject>()
                    {
                        new FoodObject("Thai Tea", "Sprites/thai_tea", q: 1),
                        new FoodObject("Milk Tea", "Sprites/milk_tea", q: 1),
                    })
                },
            },
        };
    }


    public void BuildShopDatabase()
    {
        //builds shop database
        shopDatabase = new List<Dictionary<string, FoodObject>>
        {
            // level 0
            new Dictionary<string, FoodObject>()
            {
                //null
            },
            // level 1
            new Dictionary<string, FoodObject>()
            {
                {"Apple", database[1]["Apple"]},
                {"Orange", database[1]["Orange"]},
                {"Pear", database[1]["Pear"]},
            },
            //level 2
            new Dictionary<string, FoodObject>()
            {
                {"Egg", database[2]["Egg"]},
                {"Flour", database[2]["Flour"]},
                {"Wheat", database[2]["Wheat"]},
            },
            //level 3
            new Dictionary<string, FoodObject>()
            {
                {"Pepper", database[3]["Pepper"]},
                {"Raw Beef", database[3]["Raw Beef"]},
                {"Raw Chicken", database[3]["Raw Chicken"]},
                {"Salt", database[3]["Salt"]},
            },
            //level 4
            new Dictionary<string, FoodObject>()
            {
                {"Lychee", database[4]["Lychee"]},
                {"Mango", database[4]["Mango"]},
                {"Watermelon", database[4]["Watermelon"]},
            },
            //level 5
            new Dictionary<string, FoodObject>()
            {
                {"Baking Powder", database[5]["Baking Powder"]},
                {"Butter", database[5]["Butter"]},
                {"Cheese", database[5]["Cheese"]},
                {"Sugar", database[5]["Sugar"]},
                {"Lettuce", database[5]["Lettuce"]},
                {"Milk", database[5]["Milk"]},
                {"Onion", database[5]["Onion"]},
                {"Tomato", database[5]["Tomato"]},
            },
            //level 6
            new Dictionary<string, FoodObject>()
            {
                {"Turkey", database[6]["Turkey"]},
                {"Potato", database[6]["Potato"]},
                {"Red Bean", database[6]["Red Bean"]},
            },
            //level 7
            new Dictionary<string, FoodObject>()
            {
                {"Black Tea Leaves", database[7]["Black Tea Leaves"]},
                {"Earl Grey Leaves", database[7]["Earl Grey Leaves"]},
                {"Green Tea Leaves", database[7]["Green Tea Leaves"]},
                {"Jasmine Tea Leaves", database[7]["Jasmine Tea Leaves"]},
                {"Oolong Tea Leaves", database[7]["Oolong Tea Leaves"]},
                {"Thai Tea Leaves", database[7]["Thai Tea Leaves"]},
            },
        };
    }

    public void BuildRecipeDatabase()
    {
        //builds recipe database
        recipeDatabase = new List<Dictionary<string, FoodObject>>
        {
            // level 0
            new Dictionary<string, FoodObject>()
            {
                //null
            },
            // level 1
            new Dictionary<string, FoodObject>()
            {
                {"Sliced Apples", database[1]["Sliced Apples"]},
                {"Sliced Oranges", database[1]["Sliced Oranges"]},
                {"Sliced Pears", database[1]["Sliced Pears"]},
            },
            //level 2
            new Dictionary<string, FoodObject>()
            {
                {"Bread", database[2]["Bread"]},
                {"Scrambled Egg", database[2]["Scrambled Egg"]},
            },
            //level 3
            new Dictionary<string, FoodObject>()
            {
                {"Steak", database[3]["Steak"]},
                {"Pan Seared Chicken", database[3]["Pan Seared Chicken"]},
            },
            //level 4
            new Dictionary<string, FoodObject>()
            {
                {"Diced Mangos", database[4]["Diced Mangos"]},
                {"Skinned Lychees", database[4]["Skinned Lychees"]},
                {"Sliced Watermelon", database[4]["Sliced Watermelon"]},
            },
            //level 5
            new Dictionary<string, FoodObject>()
            {
                {"Cake", database[5]["Cake"]},
                {"Cheeseburger", database[5]["Cheeseburger"]},
                {"Chopped Lettuce", database[5]["Chopped Lettuce"]},
                {"Chopped Onions", database[5]["Chopped Onions"]},
                {"Sliced Cheese", database[5]["Sliced Cheese"]},
                {"Sliced Tomatoes", database[5]["Sliced Tomatoes"]},
            },
            //level 6
            new Dictionary<string, FoodObject>()
            {
                {"Baked Turkey", database[6]["Baked Turkey"]},
                {"Baked Potato", database[6]["Baked Potato"]},
                {"Red Bean Paste", database[6]["Red Bean Paste"]},
            },
            //level 7
            new Dictionary<string, FoodObject>()
            {
                {"Black Tea", database[7]["Black Tea"]},
                {"Black Milk Tea", database[7]["Black Milk Tea"]},
                {"Earl Grey", database[7]["Earl Grey"]},
                {"Earl Grey Milk Tea", database[7]["Earl Grey Milk Tea"]},
                {"Green Tea", database[7]["Green Tea"]},
                {"Green Milk Tea", database[7]["Green Milk Tea"]},
                {"Jasmine Tea", database[7]["Jasmine Tea"]},
                {"Jasmine Milk Tea", database[7]["Jasmine Milk Tea"]},
                {"Milk Tea", database[7]["Milk Tea"]},
                {"Oolong Tea", database[7]["Oolong Tea"]},
                {"Oolong Milk Tea", database[7]["Oolong Milk Tea"]},
                {"Thai Milk Tea", database[7]["Thai Milk Tea"]},
            },
        };
    }

    public void BuildHiddenDatabase()
    {
        //builds hidden database
    }

    /*public void BuildHiddenIDatabase()
    {
        //FUTURE UPDATE
        //builds hidden ingredient database
    }*/
}
