using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    private fight playerScript;
    public int SlotNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        ////player.transform.SetParent(transform);
        //playerScript = player.gameObject.GetComponent<fight>();
        
        switch (SlotNumber)
        {
          case 1:
                GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
                //player.transform.SetParent(transform);
                playerScript = player.gameObject.GetComponent<fight>();

                player.transform.localScale = new Vector3(1, 1, 1);

                player.transform.Rotate(0, 90, 0);

                print("player 1 rotated");

                UIManager.Instance.SetPlayer(playerScript, SlotNumber);

                StartCoroutine(playerRegister());
                break;

          case 2:
               StartCoroutine(playerSpawn());
              // print("player 2 rotated");
              //player.transform.localScale = new Vector3(1, 1, -1);

               break;


        }

        //player.transform.Rotate(0, 90, 0);


        //UIManager.Instance.SetPlayer(playerScript, SlotNumber);


        //StartCoroutine(playerRegister());


    }

    IEnumerator playerRegister()
    {
        yield return new WaitForSeconds(0.1f);


        if (RoundManager.Instance != null)
        {
            RoundManager.Instance.SetPlayer(playerScript, SlotNumber);
        }
        else
        {
            Debug.LogWarning("RoundManager.Instance is null! Cannot register player in spawner.");
        }
    }

    IEnumerator playerSpawn()
    {
        yield return new WaitForSeconds(0.01f);

        GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        //player.transform.SetParent(transform);
        playerScript = player.gameObject.GetComponent<fight>();

        player.transform.Rotate(0, 90, 0);

        print("player 2 rotated");

        UIManager.Instance.SetPlayer(playerScript, SlotNumber);

        StartCoroutine(playerRegister());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
