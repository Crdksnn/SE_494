using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private GameObject settingsMenuUI;
    [SerializeField] private GameObject creditsMenuUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private UIController instance;
    
    private void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
        }else{
            instance = this;
        }
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            TogglePause();
            pauseMenuUI.gameObject.SetActive(!pauseMenuUI.activeSelf);
            
            if(settingsMenuUI.activeSelf)
                settingsMenuUI.SetActive(false);
            
        }
            
    }

    public void PressedPlayButton()
    {
        sceneFader.FadeTo("Map Scene");
    }

    public void PressedSettingsButton()
    {
        settingsMenuUI.gameObject.SetActive(!settingsMenuUI.activeSelf);
    }

    public void PressedCreditsButton()
    {
        creditsMenuUI.gameObject.SetActive(!creditsMenuUI.activeSelf);
    }

    public void PressedResumeButton()
    {
        TogglePause();
        pauseMenuUI.gameObject.SetActive(false);
        
        if(settingsMenuUI.activeSelf)
            settingsMenuUI.SetActive(false);
    }

    public void PressedMainMenuButton()
    {
        TogglePause();
        sceneFader.FadeTo("UI Scene");
    }
    
    public void PressedQuitButton()
    {
        Application.Quit();
    }
    
    public void TogglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return;
        }
        Time.timeScale = 0f;
    }
    
}
