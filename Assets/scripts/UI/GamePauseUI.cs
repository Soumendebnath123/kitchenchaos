using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button OptionsButton;

    private void Awake()
    {
        
        resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        MainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        OptionsButton.onClick.AddListener(() =>
        {

            OptionsUI.Instance.Show();
        });
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }
    private void Instance_O(object sender, System.EventArgs e)
    {

    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
