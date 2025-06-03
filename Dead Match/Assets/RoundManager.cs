using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{

    public fight player1;
    public fight player2;

    public GameObject player1SpawnPoint;
    public GameObject player2SpawnPoint;

    public int roundWinner;

    public bool player1Won;

    public bool player2Won;

    public int currentRoundNumber;

    public int player1Wins;
    public int player2Wins;

    public static RoundManager Instance;

    public GameObject player1WonImage;
    public GameObject player2WonImage;
    public GameObject Round1Image;
    public GameObject Round2Image;
    public GameObject Round3Image;
    public GameObject FightImage;
    public GameObject KoImage1;
    public GameObject KoImage2;

    public GameObject currentRoundImage;
   

    private void Awake()
    {
        Instance = this;
    }

    public void SetPlayer(fight playerRef, int SlotNumber)
    {
        Debug.Log("function called");
        switch (SlotNumber)
        {
            case 1:
                Debug.Log("registered player 1");
                player1 = playerRef;
                
                break;
            case 2:
                player2 = playerRef;
                Debug.Log("registered player 2");

                break;


        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RoundStart(1);
     
    }

    public void RoundStart(int currentRoundNumber)
    {
        roundWinner = 0;
        switch (currentRoundNumber)
        {
            case 1:
                Round1Image.SetActive(true);
                StartCoroutine(RoundStartTimer());
                break;

            case 2:
                Round1Image.SetActive(true);
                StartCoroutine(RoundStartTimer());
                break;

            case 3:
                Round1Image.SetActive(true);
                StartCoroutine(RoundStartTimer());
                break;



        }
    }

    IEnumerator RoundStartTimer()
    {
        yield return new WaitForSeconds(2f);
        Round1Image.SetActive(false);
        Round2Image.SetActive(false);
        Round3Image.SetActive(false);


        FightImage.SetActive(true);
        StartCoroutine(FightStartTimer());
    }

    IEnumerator FightStartTimer()
    {
        yield return new WaitForSeconds(3f);
        FightImage.SetActive(false);
    }

    public void RoundEnd(int roundWinner)
    {
        SlowdownGame(roundWinner);
       
    }

    public void SlowdownGame(int roundwinner)
    {
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        StartCoroutine(SpeedupGame(roundWinner));
    }

    IEnumerator SpeedupGame(int roundwinner)
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
        KoImage1.SetActive(true);
     
        yield return new WaitForSeconds(3f);
        KoImage1.SetActive(false);
        KoImage2.SetActive(false);

        // start conversation around here

        if (player1Wins == 1 && player2Wins == 0)
        {
            StartRoundRestart();
        }

        if (player1Wins == 1 && player2Wins == 1)
        {
            StartRoundRestart();
        }

        if (player1Wins == 0 && player2Wins == 1)
        {
            StartRoundRestart();
        }

        if (player1Wins == 2 || player2Wins == 2 )
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
      
    }


    public void StartRoundRestart()
    {
        StartCoroutine(RoundRestartCoroutine());
    }

    public IEnumerator RoundRestartCoroutine()
    {
        currentRoundNumber += 1;

        yield return UIManager.Instance.FadeBlackScreen(true);

        player1.hp = player1.maxHP;
        player2.hp = player2.maxHP;
        
        player1.transform.position = player1SpawnPoint.transform.position;
        player1.transform.rotation = player1SpawnPoint.transform.rotation;
        player1.transform.localScale = new Vector3(1, 1, 1); // Face right

        player2.transform.position = player2SpawnPoint.transform.position;
        player2.transform.rotation = player2SpawnPoint.transform.rotation;
        player2.transform.localScale = new Vector3(1, 1, -1); // Face left

        player1.transform.Rotate(0, 90, 0);
        player2.transform.Rotate(0, 90, 0);

        player1.moves.Play("Idle");
        player2.moves.Play("Idle");

        yield return UIManager.Instance.FadeBlackScreen(false);

        RoundStart(currentRoundNumber);


    }


    // Update is called once per frame
    void Update()
    {

   


        if (player1.hp <= 0 || player2.hp <= 0)
        {

            Debug.Log("round is over");

            if(player1.hp > 0)
            {
                roundWinner = 1;
                player1Wins += 1;
                


            }

            if (player2.hp > 0)
            {
                roundWinner = 2;
                player2Won = true;
                player2Wins += 1;

            }
            Debug.Log("round is over");

            SlowdownGame(roundWinner);

           
        }
    }
}
