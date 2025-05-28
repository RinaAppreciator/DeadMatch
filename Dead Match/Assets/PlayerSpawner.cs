using UnityEditor;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public int SlotNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        switch(SlotNumber)
        {
            case 1:
                player.transform.localScale = new Vector3(1, 1, 1);
            break;

            case 2:
                player.transform.localScale = new Vector3(1, 1, -1);
            break;


        }
        player.transform.Rotate(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
