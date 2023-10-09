using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    [SerializeField]
    private EnemigoSO m_Valores;
    [SerializeField]
    private GameEvent<int> m_Muerto;
    private int m_Ronda = GameManager.Instance.Ronda;
    [SerializeField]
    private float m_aumentoPorRonda = 0.1f;
    [SerializeField]
    private Transform[] wayPoints; 
    public float Daño;
    private float Velocidad;
    private float Vida;
    public int Valor;
    private int wayPointIndex = 0;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayPointIndex].position, Time.deltaTime * Velocidad);
        if (transform.position == wayPoints[wayPointIndex].position) {
            wayPointIndex++;
        }    
        
        
    }

    public void InitValues(Transform[] waypoints) {
         m_Ronda = GameManager.Instance.Ronda;
        Daño = m_Valores.Daño * (1 + (m_Ronda - 1) * m_aumentoPorRonda);
        Velocidad = m_Valores.Velocidad;
        Vida = m_Valores.Vida * (1 + (m_Ronda - 1) * m_aumentoPorRonda);
        Valor = m_Valores.Valor;
        wayPoints = waypoints;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bala")
        {
        
            Vida -=  collision.gameObject.GetComponent<BalaScript>().Daño;
            Destroy(collision.gameObject);
            if (Vida <= 0)
            {
                m_Muerto.Raise(Valor);
                Destroy(this.gameObject);
            }
        }
    }

   
}
