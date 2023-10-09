using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private float vida = 100;
    [SerializeField]
    private GameEvent<int> enemigoMuerto; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") {
            vida -= collision.GetComponent<EnemigoScript>().Daño;
            Destroy(collision.gameObject);
            enemigoMuerto.Raise(collision.GetComponent<EnemigoScript>().Valor);
            if (vida <= 0) {
                GameManager.Instance.ChangeScene(GameManager.GameOverScene);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
