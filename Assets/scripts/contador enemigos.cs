using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contadorenemigos : MonoBehaviour
    
{
    [SerializeField] public float vida;
        private int contador1;
    [SerializeField] private GameObject efectomuerte;
    // Start is called before the first frame update
    public void tomardaño(float daño)
    {
        vida -= daño; 
         if (vida <=0)
        {
            muerte();
        }

    }

    // Update is called once per frame
   private void muerte()
    {
        Instantiate(efectomuerte, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
