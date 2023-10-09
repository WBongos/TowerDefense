using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScript : MonoBehaviour
{
    [SerializeField]
    private float m_Daño;
    public float Daño {
        get { return m_Daño; }
        set { m_Daño = value; }
    }
}
