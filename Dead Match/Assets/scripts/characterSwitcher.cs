using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.Cinemachine;

public class characterSwitcher : MonoBehaviour
{

    int index = 0;
    [SerializeField] List<GameObject> fighters = new List<GameObject>();
    [SerializeField] CinemachineCamera GroupCamera;
    [SerializeField] CinemachineTargetGroup targetGroup;
    PlayerInputManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GetComponent<PlayerInputManager>();
        index = Random.Range(0, fighters.Count);
        manager.playerPrefab = fighters[index];

        UpdateCameraTargetGroup();
    }


    public void SwitchNextSpawnCharacter(PlayerInput input)
    {
        index = Random.Range(0, fighters.Count);
        manager.playerPrefab = fighters[index];

        UpdateCameraTargetGroup();
    }


    void UpdateCameraTargetGroup()
    {
        if (targetGroup != null)
        {
            foreach (GameObject fighter in fighters)
            {
                // Create a new target for each fighter and add it to the list
                targetGroup.AddMember(fighter.transform, 1f, 1f);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
