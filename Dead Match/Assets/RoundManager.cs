using System.Collections;
using Unity.Properties;
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

    public AudioClip fight;
    public AudioClip koClip;

    public AudioSource audioSource;

    public static RoundManager Instance;

    public GameObject player1WonImage;
    public GameObject player2WonImage;
    public GameObject Round1Image;
    public GameObject Round2Image;
    public GameObject Round3Image;
    public GameObject FightImage;
    public GameObject KoImage1;
    public GameObject KoImage2;

    public bool isCutscene;


    public GameObject currentRoundImage;

    private bool gameEnded;
    private bool matchEnded;
    public bool roundRestarted;

    public int player1character;
    public int player2character;

    string firstLine;
    string secondLine;
    string thirdLine;

   

    private void Awake()
    {
        Instance = this;
    }

    public void SetPlayer(fight playerRef, int SlotNumber)
    {
        
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

        currentRoundNumber = 1;
        matchEnded = false;
        gameEnded = false;
        RoundStart(currentRoundNumber);
        this.player1character = UIManager.Instance.player1selectedcharacter;
        this.player2character = UIManager.Instance.player2selectedcharacter;
    }

    public void RoundStart(int currentRoundNumber)
    {
        gameEnded = false;
        roundWinner = 0;
        switch (currentRoundNumber)
        {
            case 1:
                
                Round1Image.SetActive(true);
                StartCoroutine(RoundStartTimer());
                break;

            case 2:
                Round2Image.SetActive(true);
                StartCoroutine(RoundStartTimer());
                break;

            case 3:
                Round3Image.SetActive(true);
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


        playSound(fight);
        FightImage.SetActive(true);
        StartCoroutine(FightStartTimer());
    }

    IEnumerator FightStartTimer()
    {
        yield return new WaitForSeconds(1f);
        roundRestarted = false;
        FightImage.SetActive(false);
    }

    public void RoundEnd(int roundWinner)
    {
        SlowdownGame(roundWinner);
       
    }

    public void SlowdownGame(int roundwinner)
    {
        if (gameEnded == false )
        {
            gameEnded = true;
            if (roundwinner == 1)
            {
                player1Wins += 1;
               
            }

            else if (roundwinner == 2)
            {
                player2Wins += 1;
                
            }

            if (player1Wins == 2 || player2Wins == 2)
            {
                matchEnded = true;
            }
                
           
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            
            StartCoroutine(SpeedupGame(roundWinner));
        }
 
       
    }

    IEnumerator SpeedupGame(int roundwinner)
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
        KoImage1.SetActive(true);
        playSound(koClip);
     
        yield return new WaitForSeconds(3f);
        KoImage1.SetActive(false);

        startCutscene();      
      
    }


    public void StartRoundRestart()
    {
        StartCoroutine(RoundRestartCoroutine());
    }

    public IEnumerator RoundRestartCoroutine()
    {
        Debug.Log("restarting round");
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

        player1.transform.eulerAngles = new Vector3(0, 180, 0);
        player2.transform.eulerAngles = new Vector3(0, 0, 0);

        player1.moves.Play("Idle");
        player1.moves.SetBool("Alive", true);
        player2.moves.Play("Idle");
        player2.moves.SetBool("Alive", true);

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
                Debug.Log("player 1 won round");
                roundWinner = 1;

            }

            if (player2.hp > 0)
            {
                Debug.Log("player 2 won round");
                roundWinner = 2;
 
            }

            
           

            SlowdownGame(roundWinner);

           
        }
    }

    public void restartRound()
    {
      
      
       if (matchEnded)
            {        
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        else
        {
            if (!roundRestarted)
            {
                roundRestarted = true;
                StartRoundRestart();
            }
        
        }
        
    }

    public void startCutscene()
    {
        Debug.Log("trying to start cutscene");
        isCutscene = true;


            
            int winnerCharacter = -1;
            int loserCharacter = -1;
            float winnerCharacterHealth = 0;

            if (roundWinner == 1)
            {
                winnerCharacter = player1character;
                winnerCharacterHealth = UIManager.Instance.healthBar1.fillAmount;
                loserCharacter = player2character;
            }
            else if (roundWinner == 2)
            {
                winnerCharacter = player2character;
                winnerCharacterHealth = UIManager.Instance.healthBar2.fillAmount;
                loserCharacter = player1character;
            }

            if (winnerCharacter != -1 && loserCharacter != -1)
            {
                PlayCutsceneForRound(currentRoundNumber, winnerCharacter, loserCharacter, winnerCharacterHealth);
            }
        


    }

    private void PlayCutsceneForRound(int roundNumber, int winner, int loser, float winnerHealth)
    {
        Debug.Log("winner has a health of" + winnerHealth);

        // Example:
        if (roundNumber == 1)
        {
            if (winner == 0 && loser == 0)
            {
                if (winnerHealth == 1)
                {
                    firstLine = "i won";
                    secondLine = "wait until i lock in";
                    thirdLine = "alright";
                    Debug.Log("Character 0 won against character 0 in round 1 in perfect health.");
                }

                if (winnerHealth < 1 && winnerHealth > 0.6)
                {
                    Debug.Log("Character 0 won against character 0 in round 1 with good health.");
                }

                if (winnerHealth < 0.6 && winnerHealth > 0.1)
                {
                    Debug.Log("Character 0 won against character 0 in round 1 in bad health.");
                }


            }
            else if (winner == 0 && loser == 1)
            {


                if (winnerHealth == 100)
                {
                    Debug.Log("Character 0 won against character 1 in round 1 in perfect health.");
                }

                if (winnerHealth < 100 && winnerHealth > 60)
                {
                    Debug.Log("Character 0 won against character 1 in round 1 with good health.");
                }

                if (winnerHealth < 60 && winnerHealth > 11)
                {
                    Debug.Log("Character 0 won against character 1 in round 1 in bad health.");
                }
                
            }

            // ... continue as needed for each unique (winner, loser) pair
        }

        if (roundNumber == 2)
        {

            if (matchEnded)
            {
                if (winner == 0 && loser == 0)
                {
                    Debug.Log("Character 0 won against character 0 decisively in round 2.");
                }
                else if (winner == 0 && loser == 1)
                {
                    Debug.Log("Character 0 won against character 1 in round 2.");
                }
            }

            else
            {
                if (winner == 0 && loser == 0)
                {
                    Debug.Log("Character 0 won against character 0 in a comeback in round 2.");
                }
                else if (winner == 0 && loser == 1)
                {
                    Debug.Log("Character 0 won against character 1 in round 2.");
                }
            }

         
            // ... continue as needed for each unique (winner, loser) pair
        }

        if (roundNumber == 3)
        {
            if (winner == 0 && loser == 0)
            {
                Debug.Log("Character 0 won against character 0 in round 3.");
            }
            else if (winner == 0 && loser == 1)
            {
                Debug.Log("Character 0 won against character 1 in round 3.");
            }
            // ... continue as needed for each unique (winner, loser) pair
        }
        // Add conditions for other rounds (2, 3) as needed

        StartCoroutine(UIManager.Instance.PlayCutscene(firstLine, secondLine, thirdLine));

    }


    public void endCutscene()
    {
        isCutscene = false;
        restartRound();
    }

    public void skipCutscene()
    {
        restartRound();
    }

    public void playSound(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}
