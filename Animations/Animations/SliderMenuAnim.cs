using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMenuAnim : MonoBehaviour
{
    public GameObject PanelMenu;
	public Transform ScrollViewContent;
	public Text FoodNameLabel;
	public GameObject buttonToCreate;
	public GameObject ingInfo;
	public Image FoodImage;
    public Button ButtonNext;
    public Button ButtonBack;
    public Button ButtonNextPage;
    public Button ButtonBackPage;
    public Button ButtonToDisable;
	public GameObject objectBlocker;
	
	private List<Button> buttonList = new List<Button>();
	private List<GameObject> ingredientList = new List<GameObject>();
	private int currentSection = 1;
	private int currentTab = 1;
    private int maxTab;
    private int currentPage = 1;
	private int maxPage;
	
	private List<FoodObject> levelList = new List<FoodObject>();
	
	// left 310, 110
	// 240, 240
	
	void Start()
	{
		maxTab = PlayerData.player.GetRecipeDB().Count-1;
        levelList.AddRange(PlayerData.player.GetRecipeDB()[1].Values); // start level 1
        RefreshSection(currentSection, false);
		RefreshPage(currentPage);
		SetMaxPage();
		buttonList[0].interactable = false; // page 1 shows at start
		
	}
	
	private void RefreshSection(int sectionNum, bool show)
	{
		Debug.Log("section #" + sectionNum);
		
		foreach(Button b in buttonList)
		{
			Destroy(b.gameObject);
		}
		buttonList.Clear();
		
		for(int i = 1+((sectionNum-1)*5); i <= 5+((sectionNum-1)*5); ++i)
		{
			if(i <= maxTab)
			{
				GameObject newButton = Instantiate(buttonToCreate, PanelMenu.transform);
				
				int mod = i%5;
				if(mod == 0){mod = 5;}

				newButton.transform.localPosition = new Vector3(90, (mod - 1)* -102 + 337, 0);
				
				buttonList.Add(newButton.GetComponent<Button>());
				
				ButtonTab buttonTab = newButton.GetComponent<ButtonTab>();
				buttonTab.Setup(this, i);
				buttonTab.gameObject.SetActive(show);
			}
		}
		
		if(currentSection == 1)
        {
            ButtonBack.interactable = false;
        }
        else
        {
            ButtonBack.interactable = true;
        }
		
		if(currentSection * 5 >= maxTab)
		{
			ButtonNext.interactable = false;
		}
		else
		{
			ButtonNext.interactable = true;
		}
	}
	
	private void RefreshPage(int pageNum)
	{
		Debug.Log("Currently on page #" + pageNum);
		
		FoodObject food = levelList[currentPage-1];
		
		FoodNameLabel.text = food.getName();
		FoodImage.sprite = Resources.Load<Sprite>(food.getIcon());
		
		foreach(GameObject obj in ingredientList)
		{
			Destroy(obj);
		}
		ingredientList.Clear();
		
		foreach(FoodObject ing in food.getIngNeeded())
		{
			GameObject newObj = Instantiate(ingInfo, ScrollViewContent);
			
			ingredientList.Add(newObj);
			
			IngredientInfo ingredientInfo = newObj.GetComponent<IngredientInfo>();
			ingredientInfo.Setup(ing);
		}
		
		if(currentPage == 1)
        {
            ButtonBackPage.interactable = false;
        }
        else
        {
            ButtonBackPage.interactable = true;
        }
		
		if(currentPage == maxPage)
		{
			ButtonNextPage.interactable = false;
		}
		else
		{
			ButtonNextPage.interactable = true;
		}
	}

    public void ShowHideMenu()
    {
        if (PanelMenu != null)
        {
            Animator animator = PanelMenu.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("show");
                Debug.Log(isOpen);
                animator.SetBool("show", !isOpen);
				
				foreach(Button button in buttonList)
				{
					button.gameObject.SetActive(isOpen);
				}
                ButtonNext.gameObject.SetActive(isOpen);
                ButtonBack.gameObject.SetActive(isOpen);
                ButtonToDisable.gameObject.SetActive(!isOpen);

                objectBlocker.gameObject.SetActive(isOpen);
            }
        }
    }
	
	public void ClickTabButton(Button currentButton, int num)
	{
		currentTab = num;
		currentPage = 1;
		
		foreach(Button button in buttonList)
		{
			button.interactable = true;
		}
		
		currentButton.interactable = false;
		
		levelList.Clear();
		levelList.AddRange(PlayerData.player.GetRecipeDB()[num].Values);
		
		SetMaxPage();
		RefreshPage(currentPage);
	}
	
	private void SetMaxPage()
	{
		maxPage = PlayerData.player.GetRecipeDB()[currentTab].Count;
	}
	
	public void prevSection()
	{
		--currentSection;
		RefreshSection(currentSection, true);
	}
	
	public void nextSection()
	{
		++currentSection;
		RefreshSection(currentSection, true);
	}

    public void prevPage()
    {
        --currentPage;
        RefreshPage(currentPage);
    }

    public void nextPage()
    {
        ++currentPage;
        RefreshPage(currentPage);
    }
}
