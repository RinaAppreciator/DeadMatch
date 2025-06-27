using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject edithPrefab;
    public GameObject vanelsonPrefab;
    public GameObject tetsuPrefab;
    private fight playerScript;
    public int player1SelectedCharacter;
    public int player2SelectedCharacter;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject UImanager;
    public bool matchStarted;
    public boundaryManager wallSetter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        startGame();
        UIManager.Instance.StartCoroutine(UIManager.Instance.FadeBlackScreen(false));
       


    }

    public void startGame()
    {
        spawnPlayer1();
        spawnPlayer2();
    }

    public void playerRegister(fight playerScript, int playerID)
    {
        //yield return new WaitForSeconds(0.1f);


        if (RoundManager.Instance != null)
        {
            RoundManager.Instance.SetPlayer(playerScript, playerID);
        }
        else
        {
            Debug.LogWarning("RoundManager.Instance is null! Cannot register player in spawner.");
        }
    }

    public void Update()
    {
    }

    public void spawnPlayer1()
    {
       
        switch (player1SelectedCharacter)
        {

            case 0:
                {
                    GameObject currentPlayer = Instantiate(edithPrefab, spawnPoint1.transform.position, Quaternion.identity);
                    currentPlayer.name = "Player #1";

                    //player.transform.SetParent(transform);
                    playerScript = currentPlayer.gameObject.GetComponent<fight>();

                    currentPlayer.transform.localScale = new Vector3(1, 1, 1);

                    currentPlayer.transform.Rotate(0, 180, 0);

                    print("player 1 rotated");

                    UIManager.Instance.SetPlayer(playerScript, 1, player1SelectedCharacter);

                    playerRegister(playerScript, 1);
                    break;
                }

            case 1:
                {
                    GameObject currentPlayer = Instantiate(vanelsonPrefab, spawnPoint1.transform.position, Quaternion.identity);
                    currentPlayer.name = "Player #1";
                    //player.transform.SetParent(transform);
                    playerScript = currentPlayer.gameObject.GetComponent<fight>();

                    currentPlayer.transform.localScale = new Vector3(1, 1, 1);

                    currentPlayer.transform.Rotate(0, 90, 0);

                    print("player 1 rotated");

                    UIManager.Instance.SetPlayer(playerScript, 1, player1SelectedCharacter);

                    //StartCoroutine(playerRegister(1));
                    break;
                }

            case 2:
                {
                    GameObject currentPlayer = Instantiate(tetsuPrefab, spawnPoint1.transform.position, Quaternion.identity);
                    currentPlayer.name = "Player #1";
                    //player.transform.SetParent(transform);
                    playerScript = currentPlayer.gameObject.GetComponent<fight>();

                    currentPlayer.transform.localScale = new Vector3(1, 1, -1);

                    currentPlayer.transform.Rotate(0, -90, 0);

                    print("player 1 rotated");

                    UIManager.Instance.SetPlayer(playerScript, 1, player1SelectedCharacter);

                    //StartCoroutine(playerRegister(1));
                    break;
                }
        }


    }

    public void spawnPlayer2()
    {
        
        switch (player2SelectedCharacter)
        {

            case 0:
                {
                    GameObject currentPlayer = Instantiate(edithPrefab, spawnPoint2.transform.position, Quaternion.identity);

                    currentPlayer.name = "Player #2";
                    //player.transform.SetParent(transform);
                    playerScript = currentPlayer.gameObject.GetComponent<fight>();

                    currentPlayer.transform.localScale = new Vector3(1, 1, -1);

                    currentPlayer.transform.Rotate(0, 0, 0);

                    

                    UIManager.Instance.SetPlayer(playerScript, 2, player2SelectedCharacter);

                    playerRegister(playerScript, 2);
                    break;
                }

            case 1:
                {
                    GameObject currentPlayer = Instantiate(vanelsonPrefab, spawnPoint2.transform.position, Quaternion.identity);
                    //player.transform.SetParent(transform);

                    currentPlayer.name = "Player #2";
                    playerScript = currentPlayer.gameObject.GetComponent<fight>();

                    currentPlayer.transform.localScale = new Vector3(1, 1, -1);

                    currentPlayer.transform.Rotate(0, 90, 0);

                    print("player 1 rotated");

                    UIManager.Instance.SetPlayer(playerScript, 2, player2SelectedCharacter);

                    //StartCoroutine(playerRegister(2));
                    break;
                }

            case 2:
                {
                    GameObject currentPlayer = Instantiate(tetsuPrefab, spawnPoint2.transform.position, Quaternion.identity);
                    //player.transform.SetParent(transform);
                    playerScript = currentPlayer.gameObject.GetComponent<fight>();

                    currentPlayer.name = "Player #2";

                    currentPlayer.transform.localScale = new Vector3(1, 1, 1);

                    currentPlayer.transform.Rotate(0, -90, 0);

                    print("player 1 rotated");

                    UIManager.Instance.SetPlayer(playerScript, 2, player2SelectedCharacter);

                    //StartCoroutine(playerRegister(2));
                    break;
                }
        }

    }

   
    public void spawnPlayers()
    {
        matchStarted = true;
        //spawnPlayer1();
        //spawnPlayer2();
        //StartCoroutine(player2SpawnDelay());
        
        
        //spawnPlayer2();
    }
    
  
    public void setPlayer1Character(int player1SelectedCharacter)
    {
        this.player1SelectedCharacter = player1SelectedCharacter;

    }

    public void setPlayer2Character(int player2SelectedCharacter)
    {
        this.player2SelectedCharacter = player2SelectedCharacter;

    }

}
