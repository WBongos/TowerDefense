using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_ScoreText;
    void Start()
    {
        m_ScoreText.text = "Has sobrevivido: " + GameManager.Instance.Ronda.ToString() + " Rondas";

    }

    public void Retry() {
        GameManager.Instance.ChangeScene(GameManager.MainScene);
    }
}
