using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
   [SerializeField]
    private GameEvent m_SubirDificultad;
  


    private static GameManager m_Instance;
    public static GameManager Instance => m_Instance;

    public const string TowerScene = "TowerDefense";
    public const string MainScene = "MainMenu";
    public const string GameOverScene = "GameOverScene";


    public delegate void ActualizarPuntos(int puntos);
    public event ActualizarPuntos onActualizarPuntos;
    public delegate void ActualizarRonda(int ronda);
    public event ActualizarRonda onActualizarRonda;



    private int m_Dinero = 0;
    public int Dinero { 
        get { return m_Dinero; }
        set { m_Dinero = value; }
    }

    private int m_Ronda = 0;
    public int Ronda { 
        get { return m_Ronda; }
        set { m_Ronda = value; }
    }

    private int m_EnemigosMuertos = 0;
    public int m_EnemigosAMatar = 10;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        InitValues();
        ChangeScene(MainScene);

    }



    public void SubirDificultad(int Money) {
        m_Dinero += Money;
        onActualizarPuntos?.Invoke(Dinero);
        m_EnemigosMuertos++;
        if (m_EnemigosMuertos == m_EnemigosAMatar) {
            m_Ronda++;
            m_EnemigosMuertos = 0;
            m_EnemigosAMatar = m_EnemigosAMatar + 5;
            m_SubirDificultad.Raise();
            onActualizarRonda?.Invoke(Ronda);
        }
    }

    private void InitValues() {
        m_Ronda = 1;
        m_Dinero = 100;
        m_EnemigosMuertos = 0;
        m_EnemigosAMatar = 10;
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    
}
