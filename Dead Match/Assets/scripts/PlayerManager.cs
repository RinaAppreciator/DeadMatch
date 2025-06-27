using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public PlayerInputManager playerInputManager;
    public GameObject menu;
    public int deadPlayers = 0;
    public int playerNum = 0;
    public bool player1SelectedCharacter;
    public bool player2SelectedCharacter;
    public PlayerSpawner playerSpawner;
    public GameObject characterSelectCamera;
    public GameObject level;
    public bool hasSpawned = false;
    public GameObject UImanager;
    public GameObject trueControllerHandler;
    public bool hasBlackedOut;

    public List<Transform> players = new List<Transform>();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        player1SelectedCharacter = false;
        player2SelectedCharacter = false;
        hasBlackedOut = false;
        hasSpawned = false;
    }

    public void Update()
    {
        if (player1SelectedCharacter == true && player2SelectedCharacter == true && hasSpawned == false)
        {
            hasSpawned = true;
            
           
            UImanager.SetActive(false);
            trueControllerHandler.SetActive(true);
            
            level.SetActive(true);
            fadeToBlack();

            //start game or ask for confirmation
            playerSpawner.spawnPlayers();
            Debug.Log("starting game");

        }
    }

    public void fadeToBlack()
    {
        if (hasBlackedOut == false)
        {
            hasBlackedOut = true;
            UIManager.Instance.StartCoroutine(UIManager.Instance.FadeBlackScreen(true));
        }
        else
        {
            return;
        }
    }

    public void RegisterPlayer(Transform playerTransform)
    {
       
            players.Add(playerTransform);
            playerNum += 1;
        
    }

    public int getID()
    {
        if (playerNum == 1)
        {
            return 1;
        }

        else if ( playerNum == 2)
        {
            return 2;
        }

        else
        {
            return 0;
        }
    }

    public void confirmCharacter(int index, int playerID)
    {
        switch (playerID)
        {
            case 1:
                {
                    playerSpawner.setPlayer1Character(index);
                    player1SelectedCharacter = true;
                    break;
                }

            case 2:
                {
                    playerSpawner.setPlayer2Character(index);
                    player2SelectedCharacter = true;
                    break;
                }
        }
    }

    public void cancelCharacter(int playerID)
    {
        switch (playerID)
        {
            case 1:
                {
                    
                    player1SelectedCharacter = false;
                    break;
                }

            case 2:
                {
                    player2SelectedCharacter = false;
                    break;                     
                }
        }
    }


    public void UnregisterPlayer(Transform playerTransform)
    {
        if (players.Contains(playerTransform))
        {
            players.Remove(playerTransform);
        }
    }

    public List<Transform> GetPlayers()
    {
        return players;
    }

   
}