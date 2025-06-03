using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    private fight playerScript;


    public Image blackScreen;
    public float fadeDuration = 0.5f;


    public Image healthBar1;
    public Image healthBar2;
    public fight player1;
    public fight player2;
    public TextMeshProUGUI name1;
    public TextMeshProUGUI name2;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame

    private void Awake()
    {
        Instance = this;
    }

    public void SetPlayer(fight playerRef, int SlotNumber)
    {
        switch (SlotNumber)
        {
            case 1:
                Debug.Log("registered the health of player 1 ");
                player1 = playerRef;
                SetName();
                break;
            case 2:
                Debug.Log("registered the health of player 2 ");
                player2 = playerRef;
                SetName();
                break;


        }
    }


    public void Start()
    {
       
    }

    public void SetName()
    {
        name1.text = player1.FighterName;
        name2.text = player2.FighterName;
    }
    void Update()
    {
        ChangeHealth();
        //Debug.Log(healthBar.fillAmount);
    }

    public void ChangeHealth()
    {
        
        healthBar1.fillAmount = player1.hp / player1.maxHP;

        healthBar2.fillAmount = player2.hp / player2.maxHP;

    }

    public IEnumerator FadeBlackScreen(bool fadeIn)
    {
        float startAlpha = fadeIn ? 0 : 1;
        float endAlpha = fadeIn ? 1 : 0;
        float timer = 0f;

        Color color = blackScreen.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            blackScreen.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        blackScreen.color = new Color(color.r, color.g, color.b, endAlpha);
    }




}