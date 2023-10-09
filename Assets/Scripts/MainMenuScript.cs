using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void OnChangePlay()
    {
        GameManager.Instance.ChangeScene(GameManager.TowerScene);
    }

}
