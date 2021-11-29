using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo_seguir_jugador : MonoBehaviour

{

    public Transform posicio_jugador;
    CircleCollider2D mycollider;
    Animator Myanimator;
    public GameObject sonidomuerteMIO;
      public GameObject sonidomorirm;

    // Start is called before the first frame update
    void Start()
    {
        mycollider = GetComponent<CircleCollider2D>();
    Myanimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        
        {

            if (Physics2D.OverlapCircle(transform.position, 6, LayerMask.GetMask("Player")) != null)
            {

            }

            if (posicio_jugador.position.x > this.transform.position.x)
            {
                this.transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                this.transform.localScale = new Vector2(1, 1);
            }
            void OnDrawGizmos()
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, 6);

            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            GameObject objeto = collision.gameObject;
            string etiqueta = objeto.tag;
            if (etiqueta == "BulletTag")
            {
                 Myanimator.SetBool("DESTRUIR", true);
             StartCoroutine(Destruir());  
            }
             if (etiqueta == "Player")
             {
                 Instantiate(sonidomorirm);
               Application.LoadLevel(Application.loadedLevel);
             }
    }
      IEnumerator Destruir()
    {

        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }


    void OnDestroy()
    {
       Instantiate(sonidomuerteMIO);
    }
}