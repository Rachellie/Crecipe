using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int slotNum;
    //false is bag, true is inventory
    public bool tag;
	private Bag bagScript;
	
	public void SetBagScript(Bag bag)
	{
		bagScript = bag;
	}
	
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop itemslot " + slotNum);
        if (eventData.pointerDrag != null)
        {
			if(eventData.pointerDrag.name != "Scroll View")
			{
				DragDrop script = eventData.pointerDrag.GetComponent<DragDrop>();
				script.droppedOnSlot = true;
				
				Debug.Log("from " + script.tag + " to itemslot " + tag);
				
				if(!script.tag && !tag) // BAG TO BAG
				{
					PlayerData.player.SwapBagItems(script.GetIndex(), slotNum);
					if(script.GetIndex() == 15 && slotNum != 15)
					{
						PlayerData.player.SetCurrentFood(null);
					}
					if(slotNum == 15)
					{
						PlayerData.player.SetCurrentFood(PlayerData.player.GetBag()[15]);
					}
				}
				else if(script.tag && tag) // INVENTORY TO INVENTORY
				{
					Debug.Log("inventory drop");
					PlayerData.player.SwapInventoryItems(script.GetIndex(), slotNum);
				}
				else if(!script.tag && tag) // BAG TO INVENTORY
				{
					PlayerData.player.BagInventorySwap(script.GetIndex(), slotNum);
				}
				else if(script.tag && !tag) // INVENTORY TO BAG
				{
					PlayerData.player.InventoryBagSwap(script.GetIndex(), slotNum);
				}
				
				script.Refresh();
				if(bagScript != null && bagScript != script.GetBagScript())
				{
					bagScript.Refresh();
				}
			}
        }
    }
}
