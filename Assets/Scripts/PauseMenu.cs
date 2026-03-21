using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PausMenu;
    [SerializeField] private GameObject Controls;

    private bool isPaused = false;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction pauseAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pauseAction = playerInput.actions["Pause"];

        pauseAction.performed += OnPausePerformed;

    }
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        pauseAction = playerInput.actions["Pause"];

        pauseAction.performed += OnPausePerformed;

    }
    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        
        if (!PausMenu.activeSelf)
        {
            TogglePausMenu(true);
        }
        else
        {
            TogglePausMenu(false);
        }
    }
    
    public bool TogglePausMenu(bool status)
    {
        isPaused = status;
        PausMenu.SetActive(status);

        if (status && Controls.activeSelf)
        {
            Controls.SetActive(false);
        }

        if (!status)
        {
            Time.timeScale = 1f;

            if (playerInput != null)
            {
                playerInput.ActivateInput();
            }
        }
        else
        {
            Time.timeScale = 0f;

            if (playerInput != null)
            {
                playerInput.DeactivateInput();
            }
        }

        return status;
    }


    // kapparna
    public void QuitGame()
    {
        Application.Quit();
    }

    public void enablePause()
    {
        TogglePausMenu(true);
    }
    public void disablePause()
    {
        TogglePausMenu(false);
    }

    public void showControls()
    {
        bool isActive = Controls.activeSelf;
        Controls.SetActive(!isActive);
    }
    public void hideControls()
    {
        Controls.SetActive(false);
    }
}
