using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmInput(InputAction.CallbackContext context)
    {


        if (context.started)
        {
            Debug.Log("loading scene");
            SceneManager.LoadScene("tet", LoadSceneMode.Single);
        }
    }
}
