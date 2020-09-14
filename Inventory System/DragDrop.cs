using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public bool droppedOnSlot;
	public Text quantityText;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 prevPos;
	
	private Canvas canvas;
	public Image image;
	private Bag bagScript;
	private int index;
    private string model;
	private FoodObject food;
	//false is bag, true is inventory
	public bool tag;
	
	private Transform currentParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        prevPos = GetComponent<RectTransform>().anchoredPosition;
    }
	
	public void Setup(FoodObject currentFood, Canvas newCanvas, Bag bag, int indx)
	{
		food = currentFood;
		
		image.sprite = Resources.Load<Sprite>(food.getIcon());
		quantityText.text = food.getQuantity().ToString();
		canvas = newCanvas;
		bagScript = bag;
		index = indx;
        model = food.getModel();
	}

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        droppedOnSlot = false;
        prevPos = GetComponent<RectTransform>().anchoredPosition;
		
		currentParent = gameObject.transform.parent;
		gameObject.transform.SetParent(canvas.transform, true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
		gameObject.transform.SetParent(currentParent);
        StartCoroutine("RevertPos");
		bagScript.Refresh();
    }

    public void OnDrop(PointerEventData eventData)
    {
		if(eventData.pointerDrag.name != "Scroll View")
		{
			DragDrop script = eventData.pointerDrag.GetComponent<DragDrop>();
			
			Debug.Log("from " + script.tag + " to dragdrop " + tag);
			
			if(!script.tag && !tag) // BAG TO BAG
			{
				Debug.Log("OnDrop " + index);
				
				PlayerData.player.SwapBagItems(script.index, index);
				PlayerData.player.SetCurrentFood(PlayerData.player.GetBag()[15]);
				
			}
			else if(script.tag && tag) // INVENTORY TO INVENTORY
			{
				PlayerData.player.SwapInventoryItems(script.index, index);
			}
			else if(!script.tag && tag) // BAG TO INVENTORY
			{
				PlayerData.player.BagInventorySwap(script.GetIndex(), index);
			}
			else if(script.tag && !tag) // INVENTORY TO BAG
			{
				PlayerData.player.InventoryBagSwap(script.GetIndex(), index);
			}
			
			bagScript.Refresh();
			if(bagScript != null && bagScript != script.GetBagScript())
			{
				bagScript.Refresh();
			}
		}
    }

    IEnumerator RevertPos()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("Dropped on slot: " + droppedOnSlot);
        if(!droppedOnSlot)
        {
            rectTransform.anchoredPosition = prevPos;
            Debug.Log("Position: " + rectTransform.anchoredPosition);
        }
    }
	
	public int GetIndex()
	{
		return index;
	}
	
	public void Refresh()
	{
		bagScript.Refresh();
	}
	
	public Bag GetBagScript()
	{
		return bagScript;
	}
}
