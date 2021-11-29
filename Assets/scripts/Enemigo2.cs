using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2 : MonoBehaviour
{
    public Transform jugador_p;
    public Transform punto_instancia;
    public GameObject proyectil;
    private float tiempo;
    private Rigidbody2D rb;
    Animator Myanimator;
    bool estadentro = false;
    public float speed;
    public GameObject sonidomorirm;
    public GameObject sonidomuerteMIO;

    void Start()
    {
         Myanimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
     
       jugador_p=GameObject.Find("jugador").transform;
    }

    // Update is called once per frame
    void Update()
    {
    Posicion();
    rb.velocity = new Vector2(speed, rb.velocity.y);

    if(!estadentro)
      Myanimator.SetBool("CERCA", false);

    }

     void Posicion()
    {
        if (jugador_p.position.x > this.transform.position.x)
        {
            this.transform.localScale=new Vector2(-1, 1);
        }
        else 
        {
            this.transform.localScale=new Vector2(1, 1);
       
         }  
    }


    void disparar()
    {
         GameObject mybullet = Instantiate(proyectil,punto_instancia.position, Quaternion.identity);
        bool dirDisparo = transform.localScale.x != 1 ? true : false;

        mybullet.GetComponent<disparo_enm_3>().shoot(dirDisparo);
        tiempo=0;
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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

        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
     void OnDestroy()
    {
       Instantiate(sonidomuerteMIO);
    }
}
