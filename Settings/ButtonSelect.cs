using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    public Button button;
    public List<Button> otherButtons;

    public void SelectButton()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = Color.white;
        button.colors = colors;

        foreach(Button _otherButton in otherButtons)
        {
            ColorBlock color = _otherButton.colors;
            color.normalColor = Color.gray;
            _otherButton.colors = color;
        }
    }
}
