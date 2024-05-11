using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playbutton;
    [SerializeField] private Button quitbutton;

    private void Awake()
    {
        playbutton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Kitchenchaos2);
        });

        playbutton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Time.timeScale = 1f;
    }
    
}
