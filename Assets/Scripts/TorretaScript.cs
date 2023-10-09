using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TorretaScript : MonoBehaviour
{

    private GameObject m_bala;
    public GameObject Bala { 
        get { return m_bala; }
        set { m_bala = value; }
    }
    private List<Collider2D> enemigos;
    [SerializeField]
    private TorretaSO m_Valores;

    public int m_Lvl;
    public float m_Vida;
    public float m_Velocidad;
    public float m_Rango;
    public float m_Daño;
    public int m_Valor;

    private int m_EnemigosAMatar = 20;
    private int m_EnemigosMuertos = 0;

    private void Awake()
    {  
        m_Lvl = m_Valores.Lvl;
        m_Vida = m_Valores.Vida;
        m_Velocidad = m_Valores.Velocidad;
        m_Rango = m_Valores.Rango;
        m_Daño = m_Valores.Daño;
        m_Valor = m_Valores.Valor;
        this.GetComponent<CircleCollider2D>().radius = m_Rango;
        enemigos = new List<Collider2D>();
        StartCoroutine(Shoot());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            enemigos.Add(collision); 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemigos.Contains(collision))
        {
            enemigos.Remove(collision);
        }
    }

    public void SubirDeNivel() {
        m_EnemigosMuertos++;
        if (m_EnemigosMuertos == m_EnemigosAMatar) { 
            m_EnemigosMuertos = 0;
            m_EnemigosAMatar += 5;
            m_Lvl++;
            m_Vida += 5;
            m_Velocidad += 0.1f;
            m_Daño += 0.4f;
          
        }
    }
    IEnumerator Shoot() {
        while (true)
        {
            if (!enemigos.Count.Equals(0)) {
                Vector2 direction =  (enemigos[0].transform.position - transform.position).normalized;
                GameObject bala = Instantiate(m_bala);
                bala.GetComponent<BalaScript>().Daño = m_Daño;
                bala.transform.position = transform.position;
                bala.transform.up = direction; 
                bala.GetComponent<Rigidbody2D>().velocity = bala.transform.up * 20;
            }
            yield return new WaitForSeconds(m_Velocidad);
        }
    }
}
