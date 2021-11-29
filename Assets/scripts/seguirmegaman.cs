using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seguirmegaman : MonoBehaviour
    
{
    [SerializeField] Transform jugador;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =  new Vector3(jugador.position.x, jugador.position.y, transform.position.z);
    }
}
