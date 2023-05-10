using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public Toggle toggle;
    public Sprite play;
    public Sprite pause;
    public GameObject background;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            toggle.isOn = Time.timeScale == 0;
        }
    }

    public void onToggle()
    {
        if (toggle.isOn)
        {
            Time.timeScale = 1;
            background.GetComponent<Image>().sprite = pause;
        }
        else
        {
            Time.timeScale = 0;
            background.GetComponent<Image>().sprite = play;
        }
    }
}
