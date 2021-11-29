using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mecanicadisp_diagonal : MonoBehaviour

    
{
    [SerializeField] GameObject bala;
    private float timer = 0;
    private float timerwait = 2;
    bool estadentro = false;
    Animator Myanimator;
    public GameObject sonidomorirm;
    public GameObject sonidomuerteMIO;
    [SerializeField] Transform disparador2;
    [SerializeField] Transform disparador3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    Myanimator = GetComponent<Animator>();

    } 

    void disparar()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 8);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                if (collider.gameObject.tag.Equals("jugador"))
                {
                    
                        GameObject mybullet = Instantiate(bala, transform.position, transform.rotation);
                        bool dirDisparo = transform.localScale.x != 1 ? false : true;
                        mybullet.GetComponent<disparo_enm_3>().shoot(dirDisparo);
                        
                    
                   
                }
            }
          
        }

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.Equals("jugador"))
     {
            if (!estadentro)
            Myanimator.SetBool("CERCA",false);
            {
                 GameObject mybullet = Instantiate(bala,disparador2.position, disparador2.rotation);
                GameObject mybullet2 = Instantiate(bala,disparador3.position, disparador3.rotation);
                bool dirDisparo = transform.localScale.x != 1 ? false : true;
                bool dirDisparo2 = transform.localScale.x != 1 ? true: false;
               mybullet.GetComponent<disparo_enm_3>().shoot(dirDisparo);
                mybullet2.GetComponent<disparo_enm_3>().shoot(dirDisparo2);
                estadentro = true;

            }
             if (collision.gameObject.tag == "Player")
     {
            if(estadentro == false)
            {
              disparar();
              Myanimator.SetBool("CERCA", true);
              estadentro = true;
              Myanimator.SetBool("DESTRUIR", false);
                }
            }
        
        }
    }

     private void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.gameObject.tag == "Player")
        {
          estadentro = false;
          Myanimator.SetBool("DESTRUIR", false);
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

        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }
void OnDestroy()
    {
       Instantiate(sonidomuerteMIO);
    }
}

