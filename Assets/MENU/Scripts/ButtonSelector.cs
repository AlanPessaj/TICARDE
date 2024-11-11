using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSelector : MonoBehaviour
{
    public Button playAgain;
    public Button menu;
    Button selectedButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SelectButton(playAgain);
            GetComponent<AudioSource>().Play();
        }

        if (Input.GetButtonDown("A") && selectedButton != null)
        {
            selectedButton.onClick.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SelectButton(menu);
            GetComponent<AudioSource>().Play();
        }
    }

    private void SelectButton(Button button)
    {
        if (selectedButton != null)
        {
            DeselectButton(selectedButton);
        }

        selectedButton = button;
        var colors = button.colors;
        colors.normalColor = colors.normalColor * 0.7f;
        button.colors = colors;
    }

    private void DeselectButton(Button button)
    {
        var colors = button.colors;
        colors.normalColor = colors.normalColor / 0.7f;
        button.colors = colors;
    }

    private void Start()
    {
        SelectButton(playAgain);
    }
}
