using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Uicontroller : MonoBehaviour
{
    private Vector2 moveDirection;
    private int currentIndex = 0;
    public GameObject Player1Icon;
    public GameObject Player2Icon;
    public GameObject iconPrefab;
  
    private float moveCooldown = 0.5f;
    private float nextMoveTime = 0f;
    private int playerID;

    private bool confirmedChar = false;
    private int timesConfirmed = 0;

    public AudioClip characterSelect;
    public AudioClip characterSwitch;
    public AudioClip characterCancel;

    public AudioSource audioSource;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Obsolete]
    void Start()
    {
       
        playerID = PlayerManager.Instance.getID();
        Canvas canvas = (Canvas)FindFirstObjectByType(typeof(Canvas));
        if (playerID == 1)
        {
            iconPrefab = Player1Icon;
            iconPrefab = Instantiate(Player1Icon, canvas.transform);
           
        }
        if (playerID == 2)
        {
            iconPrefab = Player2Icon;
            iconPrefab = Instantiate(Player2Icon, canvas.transform);
            
        }

        //iconPrefab = Instantiate(iconPrefab, canvas.transform);
        HighlightCurrentButton();
        //Debug.Log("player ID " + playerID);

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextMoveTime && confirmedChar == false)
        {

            if (moveDirection.x == 0)
            {
                return;
            }

            if (moveDirection.x > 0)
            {
                currentIndex = (currentIndex + 1) % CharacterSelectManager.Instance.ButtonCount;
                HighlightCurrentButton();
                nextMoveTime = Time.time + moveCooldown;
            }

            if (moveDirection.x < 0)
            {
                currentIndex = (currentIndex - 1) % CharacterSelectManager.Instance.ButtonCount;

       
                HighlightCurrentButton();
                nextMoveTime = Time.time + moveCooldown;
            }

        }


        if (PlayerManager.Instance.hasSpawned == true)
        {
            Destroy(gameObject);
        }



    }

    public void DirectionInput(InputAction.CallbackContext context)                                   //verifica se o jogador está no chão ou no ar para
                                                                                                        //tocar o move correto
    {
       

            if (context.canceled)
            {
                moveDirection.x = 0;
                return;
            }

            else
            {
                moveDirection = new Vector2(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y).normalized;
            }

             


        

    }

    public void ConfirmInput(InputAction.CallbackContext context)                                  
    {
        timesConfirmed += 1;

        if (context.started )
        {
            if (timesConfirmed > 1)
            {
                confirmedChar = true;
                playSound(characterSelect);
                PlayerManager.Instance.confirmCharacter(currentIndex, playerID);
                CharacterSelectManager.Instance.showCharacterInfo(currentIndex, playerID);
            }

            if (confirmedChar)
            switch(playerID)
            {
                case 1:
                    {
                        switch (currentIndex)
                        {
                            case 0:
                                {
                                    Debug.Log("player 1 has selected Edith");
                                    break;
                                }
                            case 1:
                                {
                                    Debug.Log("player 1 has selected Vanelson");
                                    break;
                                }
                            case 2:
                                {
                                    Debug.Log("player 1 has selected Tetsu");
                                    break;
                                }
                            case 3:
                                {
                                    Debug.Log("player 1 has selected John Trabalho");
                                    break;
                                }
                        }
                        break;
                    }


                case 2:
                    {
                        switch (currentIndex)
                        {
                            case 0:
                                {
                                    Debug.Log("player 2 has selected Edith");
                                    break;
                                }
                            case 1:
                                {
                                    Debug.Log("player 2 has selected Vanelson");
                                    break;
                                }
                            case 2:
                                {
                                    Debug.Log("player 2 has selected Tetsu");
                                    break;
                                }
                            case 3:
                                {
                                    Debug.Log("player 2 has selected John Trabalho");
                                    break;

                                }
                        }
                        break;
                    }
            }
        }
    }

    public void CancelInput(InputAction.CallbackContext context)                                   
    {
        if (context.started)
        {
            if (confirmedChar == true)
            {
                confirmedChar = false;
                playSound(characterCancel);
                PlayerManager.Instance.cancelCharacter(playerID);
            }

            if ( confirmedChar == false)
            {
                //change scene and go back 
            }
        }

    }


    private void HighlightCurrentButton()
    {
        
        CharacterSelectManager.Instance.showCharacterInfo(currentIndex, playerID);
        Button button = CharacterSelectManager.Instance.GetButtonAtIndex(currentIndex);
        RectTransform buttonRect = button.GetComponent<RectTransform>();
        
        // Move the icon over the button
        iconPrefab.gameObject.transform.position = buttonRect.position;
        playSound(characterSwitch);
    }

    public void playSound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

}
