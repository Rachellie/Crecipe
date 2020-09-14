using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInfo : MonoBehaviour
{
	public Text nameLabel;
	public Image iconImage;

    public void Setup(FoodObject currentFood)
	{
		nameLabel.text = currentFood.getName() + " x" + currentFood.getQuantity().ToString(); ;
		iconImage.sprite = Resources.Load<Sprite>(currentFood.getIcon());
	}
}
