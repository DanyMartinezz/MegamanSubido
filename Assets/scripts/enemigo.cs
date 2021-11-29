using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;



public class enemigo : MonoBehaviour


{

    AIPath micamino_ia;
    bool buscarM = true;
    [SerializeField] GameObject megaman;
    // Start is called before the first frame update
    void Start()
    {
        micamino_ia = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buscarM)
        {
            micamino_ia.enabled = Physics2D.OverlapCircle(transform.position, 4, LayerMask.GetMask("player"));
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 4);
    }



}
