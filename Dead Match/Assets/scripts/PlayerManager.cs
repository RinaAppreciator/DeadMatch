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
        deadPlayers = 0;
        playerInputManager.EnableJoining();
    }

    public void Update()
    {
        switch (deadPlayers)
        {
            case 0:
                playerInputManager.EnableJoining();
                break;
            case 1:
                playerInputManager.DisableJoining();
                StartCoroutine(SpawnCoolDown());
                break;
            case 2:
                playerInputManager.DisableJoining();
                break;
        }
    }

    public void RegisterPlayer(Transform playerTransform)
    {
        if (!players.Contains(playerTransform))
        {
            players.Add(playerTransform);
            menu.SetActive(false);
        }
    }

    IEnumerator SpawnCoolDown()
    {
        yield return new WaitForSeconds(5f);
        deadPlayers = 0;
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