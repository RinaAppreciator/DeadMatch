using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Image healthBar;
    public TextMeshProUGUI scoreText;
    public fight player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame

    public void Start()
    {
        scoreText.text = player.score.ToString();
    }
    void Update()
    {
        ChangeHealth();
        ChangeScore();

        //Debug.Log(healthBar.fillAmount);
    }

    public void ChangeHealth()
    {
        
        healthBar.fillAmount = player.hp / player.maxHP;
        
    }

    public void ChangeScore()
    {

        scoreText.text = player.score.ToString();

    }

}