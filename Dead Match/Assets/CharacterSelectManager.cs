using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class CharacterSelectManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static CharacterSelectManager Instance;

    public List<Button> characterButtons;

    //blocks

    public GameObject leftEdith;
    public GameObject leftVanelson;
    public GameObject leftTetsu;

    public GameObject rightEdith;
    public GameObject rightVanelson;
    public GameObject rightTetsu;

    public GameObject rightBackgrounds;
    public GameObject leftBackgrounds;



    private void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        
    }

    public Button GetButtonAtIndex(int index)
    {
        return characterButtons[index];
    }

    public int ButtonCount => characterButtons.Count;

    public void showCharacterInfo(int index, int playerID)
    {
        
        if (playerID == 1)
        {
            rightBackgrounds.SetActive(false);
            switch (index)
            {
                case 0:
                    {
                        leftEdith.SetActive(true);
                        leftVanelson.SetActive(false);
                        leftTetsu.SetActive(false);
                        break;
                    }
                case 1:
                    {
                        leftVanelson.SetActive(true);
                        leftEdith.SetActive(false);
                        leftTetsu.SetActive(false);
                        break;
                    }
                case 2:
                    {
                        leftTetsu.SetActive(true);
                        leftEdith.SetActive(false);
                        leftVanelson.SetActive(false);
                        break;
                    }
            }
        }

        if ( playerID == 2)
        {
            leftBackgrounds.SetActive(false);
            switch (index)
            {
                case 0:
                    {
                        rightEdith.SetActive(true);
                        rightVanelson.SetActive(false);
                        rightTetsu.SetActive(false);
                        break;
                    }
                case 1:
                    {
                        rightVanelson.SetActive(true);
                        rightEdith.SetActive(false);
                        rightTetsu.SetActive(false);
                        break;
                    }
                case 2:
                    {
                        rightTetsu.SetActive(true);
                        rightVanelson.SetActive(false);
                        rightEdith.SetActive(false);
                        break;
                    }
            }
        }

    }
}
