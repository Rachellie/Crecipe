using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// "SampleButton"
public class ShopButton : MonoBehaviour
{
	public Button button;
	public Text nameLabel;
	public Text priceLabel;
	public Text quantityLabel;
	public Image iconImage;
	
	private FoodObject food;
	private ShopScrollList scrollList;
	
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(HandleClick);
    }
	
	public void Setup(FoodObject currentFood, ShopScrollList currentScrollList)
	{
		food = currentFood;
		nameLabel.text = food.getName();
		priceLabel.text = food.getPrice().ToString();
		iconImage.sprite = Resources.Load<Sprite>(food.getIcon());
		
		scrollList = currentScrollList;
		if(scrollList.isPlayer)
		{
			quantityLabel.text = "x" + food.getQuantity().ToString();
		}
		else
		{
			quantityLabel.text = ""; // blank
		}
	}
	
	public void HandleClick()
	{
		scrollList.TryTransferFoodItemToOtherShop(food);
	}
}
