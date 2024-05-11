using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    private void Start()
    {
        player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, player.OnSelectedCounterChangedEventArgs e)
    {
       if(e.selectedCounter == baseCounter)
        {
            Show();
        }
       else
        {
            Hide();
        }
    }
    private void Show()
    {
        foreach (GameObject visualgameObject in visualGameObjectArray)
        {
            visualgameObject.SetActive(true);
        }
    }
    private void Hide()
    
    {
        foreach (GameObject visualgameObject in visualGameObjectArray)
        {
            visualgameObject.SetActive(false);
        }
    }
}