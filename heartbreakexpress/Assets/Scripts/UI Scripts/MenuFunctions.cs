using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using Unity.VisualScripting;
using Debug = System.Diagnostics.Debug;

public class MenuFunctions : MonoBehaviour
{
    [Header("Tutorial")]
    [SerializeField]
    private VisualTreeAsset HowToMenu;
    private VisualElement Tutorial;
    
    [Header("UI Volume Control")]
    [SerializeField]
    private VisualTreeAsset VolumeMenu;
    private VisualElement VolumeStuff;

    [Header("Letters")] [SerializeField] 
    private VisualTreeAsset LettersMenu;

    private VisualElement LettersStuff;
    
    
    // Initializing Main Menu Buttons
    private UIDocument doc;
    public Button PlayButton;
    public Button OptionsButton;
    public Button QuitButton;
    public Button MenuButton;
    public Button HowToButton;
    public Button LettersButton;
    
    private VisualElement Container;
    private VisualElement LettersPanel;
    //private VisualElement VolumePanel;
    
        // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        Container = root.Q<VisualElement>("Container");
        //HowToWrapper = root.Q<VisualElement>("HowToWrapper");
        

        // Gathering Button Roots

        PlayButton = root.Q<Button>("PlayButton");
        OptionsButton = root.Q<Button>("OptionsButton");
        QuitButton = root.Q<Button>("QuitButton");
        MenuButton = root.Q<Button>("MenuButton");
        HowToButton = root.Q<Button>("HowToButton");
        LettersButton = root.Q<Button>("LettersButton");
        
        PlayButton.clicked += PlayGame;
        OptionsButton.clicked += OptionsMenu;
        //QuitButton.clicked += ExitGame;
        //BackButton.clicked += BackButtonClicked;
        //HowToButton.clicked += DisplayTutorial;
        //BackVolume.clicked += BackButtonClicked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void PlayGame()
    {
        //SFX_Source.Play();
        //SceneManager.LoadScene(SCENE NAME);
        UnityEngine.Debug.Log("we get to play the game lmao");
    }

    void OptionsMenu()
    {
        UnityEngine.Debug.Log("Options Opened");
    }
    
    /*void DisplayTutorial()
    {
        //SFX_Source.Play();
        Container.Clear();
        //Container.Add(Tutorial);
    }

    void BackButtonClicked()
    {
        SFX_Source.Play();
        Container.Clear();
        Container.Add(PlayButton);
        Container.Add(OptionsButton);
        Container.Add(HowToButton);
        Container.Add(QuitButton);
    }

    void VolumeSettings()
    {
        SFX_Source.Play();
        Container.Clear();
        Container.Add(VolumeStuff);

    }

    void ExitGame()
    {
        SFX_Source.Play();
        Application.Quit();
        UnityEngine.Debug.Log("Quit.");
    }*/
}
