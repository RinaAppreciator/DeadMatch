using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public RoundManager roundManager;

    private fight playerScript;


    public Image blackScreen;
    public float fadeDuration = 0.5f;


    public Image healthBar1;
    public Image healthBar2;
    public Image MeterBar1;
    public Image MeterBar2;
    public fight player1;
    public fight player2;
    public TextMeshProUGUI name1;
    public TextMeshProUGUI name2;

    public GameObject subtitleBox;
    public TextMeshProUGUI subtitles;

    public int player1selectedcharacter;
    public int player2selectedcharacter;

    public GameObject pauseBox;

    public bool isCutscene;

 

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame

    private void Awake()
    {
        Instance = this;
    }

    public void SetPlayer(fight playerRef, int SlotNumber, int selectedChar)
    {
        switch (SlotNumber)
        {
            case 1:
                Debug.Log("registered the health of player 1 ");
                player1 = playerRef;
                player1selectedcharacter = selectedChar;
                //SetName();
                break;
            case 2:
                Debug.Log("registered the health of player 2 ");
                player2 = playerRef;
                player2selectedcharacter = selectedChar;
                //SetName();
                break;


        }
    }


    public void Start()
    {
        subtitleBox.SetActive(false);
        isCutscene = false;
        pauseBox.SetActive(false);
    }

    public void SetName()
    {
        name1.text = player1.FighterName;
        name2.text = player2.FighterName;
    }
    void Update()
    {
        ChangeHealth();

    

    }

    public void ChangeHealth()
    {
        
        healthBar1.fillAmount = player1.hp / player1.maxHP;
        MeterBar1.fillAmount = player1.meter / player1.maxMeter;

        healthBar2.fillAmount = player2.hp / player2.maxHP;
        MeterBar2.fillAmount = player2.meter / player2.maxMeter;
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

    public IEnumerator PlayCutscene(string firstLine, string secondLine, string thirdLine)
    {

        Debug.Log("playing cutscene");
        isCutscene = true;
        
        subtitleBox.SetActive(true);

        subtitles.text = firstLine;
        yield return StartCoroutine(waitSubtitles());
        subtitles.text = secondLine;
        yield return StartCoroutine(waitSubtitles());
        subtitles.text = thirdLine;
        yield return StartCoroutine(waitSubtitles());

        subtitleBox.SetActive(false);

        isCutscene = false;
        roundManager.endCutscene();
    }

   

    public IEnumerator waitSubtitles()
    {
        Debug.Log("changing dialogue");
        yield return new WaitForSeconds(2f);

    }

   
}