using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Enemigos;
    [SerializeField]
    private Transform[] m_WayPoints;
    [SerializeField]
    private Transform m_SpawnPoint;
    private int m_EnemigosASpawnear = GameManager.Instance.m_EnemigosAMatar;
    private int Contador = 0;


    void Start()
    {
        InitRonda();
    }

    private void InitRonda() {
        StartCoroutine(Spawn());
        Debug.Log(m_EnemigosASpawnear.ToString());
    }
    public void EndRonda() {
        StartCoroutine(EndRondaa());
    }
    private IEnumerator EndRondaa() {
        StopCoroutine(Spawn());
        Contador = 0;
        yield return new WaitForSeconds(7f);
        m_EnemigosASpawnear = GameManager.Instance.m_EnemigosAMatar;
        InitRonda();
    }



    private IEnumerator Spawn() { 
        while (Contador != m_EnemigosASpawnear)
        {
            Contador++;
            GameObject enemigo = Instantiate(m_Enemigos[Random.Range(0, m_Enemigos.Length)], m_SpawnPoint);
            enemigo.GetComponent<EnemigoScript>().InitValues(m_WayPoints);
            yield return new WaitForSeconds(1f);
        }
    }

}
