using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_ScoreText;
    [SerializeField]
    private TextMeshProUGUI m_RondaText;
    [SerializeField]
    private GameObject[] torresUI;
    [SerializeField]
    private GameObject torreUI;
    private GameObject torre;
    [SerializeField]
    private GameObject[] torretas;
    [SerializeField]
    private Tilemap m_tilemap;
    private Camera m_Camera;
    [SerializeField]
    private InputActionAsset m_InputAsset;
    private InputActionAsset m_Input;
    [SerializeField]
    private Tile m_towerzone;
    [SerializeField]
    private GameObject bala;
    [SerializeField]
    private List<GameObject> torres;
    [SerializeField]
    private Button bTorre;
    [SerializeField]
    private Button bBallesta;
    [SerializeField]
    private Button bCañon;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        m_ScoreText.text = gameManager.Dinero.ToString();
        m_RondaText.text = "Ronda: " + gameManager.Ronda.ToString();
        gameManager.onActualizarPuntos += actualizarPuntosGui;
        gameManager.onActualizarRonda += ActualizarDatosRonda;
    }

    private void actualizarPuntosGui(int puntos)
    {
        m_ScoreText.text =  puntos.ToString();
    }
    public void ActualizarDatosRonda(int ronda)
    {
        m_RondaText.text = "Ronda: " + ronda.ToString();
    }
    void Start()
    {
        bTorre.onClick.AddListener(GetTorreta);
        bBallesta.onClick.AddListener(GetBallesta);
        bCañon.onClick.AddListener(GetCañon);
        torreUI = Instantiate(torresUI[1]);
        torre = torretas[1];
        torres = new List<GameObject>();
        m_Input = Instantiate(m_InputAsset);
        m_Input.FindActionMap("Inputs").FindAction("PlaceTower").started += PlaceTower;
        m_Input.FindActionMap("Inputs").FindAction("RemoveTower").started += RemoveTower;
        m_Camera = Camera.main;
        StartCoroutine(TowerFollowMouse());
        m_Input.FindActionMap("Inputs").Enable();

    }

    private void GetBallesta() {
        Destroy(torreUI.gameObject);
        torreUI = Instantiate(torresUI[0]);
        torre = torretas[0];
        
    }
    private void GetTorreta() {
        Destroy(torreUI.gameObject);
        torreUI = Instantiate(torresUI[1]);
        torre = torretas[1];
    }
    private void GetCañon() {
        Destroy(torreUI.gameObject);
        torreUI = Instantiate(torresUI[2]);
        torre = torretas[2];
    }
 
    private void PlaceTower(InputAction.CallbackContext context)
    {
        Vector3Int coordenadasTileMap = m_tilemap.WorldToCell(m_Camera.ScreenToWorldPoint(Input.mousePosition));

        if (m_tilemap.GetTile(coordenadasTileMap) == m_towerzone && torres.Count.Equals(0))
        {
                GameObject torreta = Instantiate(torre);
                if (torreta.gameObject.GetComponent<TorretaScript>().m_Valor <= gameManager.Dinero)
                {
                gameManager.Dinero -= torreta.gameObject.GetComponent<TorretaScript>().m_Valor;
                actualizarPuntosGui(gameManager.Dinero);
                torreta.GetComponent<TorretaScript>().Bala = bala;
                    torreta.transform.position = m_tilemap.GetCellCenterWorld(coordenadasTileMap);
                    torres.Add(torreta);
                }
                else {
                Destroy(torreta.gameObject);
                }

        }
        else if(m_tilemap.GetTile(coordenadasTileMap) == m_towerzone)
        { 
            foreach (GameObject t in torres)
            {
                if (m_tilemap.GetCellCenterWorld(coordenadasTileMap) == t.transform.position)
                    return;
            }
            GameObject torreta = Instantiate(torre);
            if (torreta.gameObject.GetComponent<TorretaScript>().m_Valor <= gameManager.Dinero)
            {
               gameManager.Dinero -= torreta.gameObject.GetComponent<TorretaScript>().m_Valor;
                actualizarPuntosGui(gameManager.Dinero);
                torreta.GetComponent<TorretaScript>().Bala = bala;
                torreta.transform.position = m_tilemap.GetCellCenterWorld(coordenadasTileMap);
                torres.Add(torreta);
            }
            else
            {
                Destroy(torreta.gameObject);
            }
        }


    }
    private void RemoveTower(InputAction.CallbackContext context) {
        Vector3Int coordenadasTileMap = m_tilemap.WorldToCell(m_Camera.ScreenToWorldPoint(Input.mousePosition));
        if (m_tilemap.GetTile(coordenadasTileMap) == m_towerzone){
            foreach (GameObject t in torres)
            {
                if (m_tilemap.GetCellCenterWorld(coordenadasTileMap) == t.transform.position) {
                    torres.Remove(t);
                    Destroy(t.gameObject);
                  
                }
                
                    
            }
        }
    }
    private void Update()
    {
      
    }
    private IEnumerator TowerFollowMouse()
    {
        while (true)
        {
            
            Vector3 point = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            torreUI.transform.position = new Vector2(point.x, point.y); 
            yield return new WaitForFixedUpdate();
        }
    }
    private void OnDestroy()
    {
        m_Input.FindActionMap("Inputs").FindAction("PlaceTower").started -= PlaceTower;
        m_Input.FindActionMap("Inputs").FindAction("RemoveTower").started -= RemoveTower;
        gameManager.onActualizarPuntos -= actualizarPuntosGui;
        gameManager.onActualizarRonda -= ActualizarDatosRonda;
    }
}
