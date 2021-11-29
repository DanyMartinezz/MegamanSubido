using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class megaman : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float velocidadsalto;
    [SerializeField] BoxCollider2D misPies;
    public Transform FirePoint;
    public GameObject Bala;


    public GameObject sonidosalto;
    public GameObject sonidocaida;
    public GameObject sonidodash;
    public GameObject sonidodisparo;
    public GameObject sonidomorirm;
    
    Animator Myanimator;
    Rigidbody2D MyBody;
    BoxCollider2D MyCollider;
    float FireRate = 0;
    float FireTime = 0;
    [SerializeField] Text contadorenemigostext;
    bool sepuedemover = true;
    bool doblesalto = false;
    bool disparando = false;
    public GameObject ganaste;
    public bool Dash;
    public float Tiempo_Dash;
    public float Velocidad_Dash;
    float dir;
    private int  contadorenemigos = 0;
    void Start()
    {
        Myanimator = GetComponent<Animator>();
        MyBody = GetComponent<Rigidbody2D>();
        MyCollider = GetComponent<BoxCollider2D>();
        Dash = GetComponent<Animator>();
        
    }


    void Update()
    {
        Runing();
        Jumping();
        Falling();
        Disparar();
        Disparador();
        dir = transform.localScale.x;
        Dash_mov();
        detectarenemigos();

    }

    void Disparar()
    {
        if (!disparando)
        {
            
            if (Input.GetKey(KeyCode.Z))
            {
                Instantiate(sonidodisparo);
                disparando = true;
                Myanimator.SetLayerWeight(1, 1);
                StartCoroutine(PausaDisparo());
            }
        }

    }

    IEnumerator PausaDisparo()
    {
        yield return new WaitForSeconds(1f);
        disparando = false;
        Myanimator.SetLayerWeight(1, 0);
    }

    void Disparador()
    {

        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= FireTime)
        {
            Instantiate(Bala, FirePoint.position, Quaternion.identity);
            FireTime = Time.time + FireRate;
        }
    }
    void Falling()
    {
        if (MyBody.velocity.y < 0)
        {
            
            Myanimator.SetBool("Caer", true);
            doblesalto = false;
        }
    }


    void Jumping()
    {



        if (EnSuelo())
        {

            doblesalto = true;
            Myanimator.SetBool("Caer", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(sonidosalto);
                Debug.Log("Salto");
                MyBody.AddForce(new Vector2(1, 500));
                Myanimator.SetTrigger("Saltar");
            }
        }
        else if (doblesalto == true)
        {

            if (Input.GetKeyDown(KeyCode.Space))

            {
                 
                doblesalto = false;
                MyBody.velocity = new Vector2(0, 0);
                Debug.Log("Salto");
                MyBody.AddForce(new Vector2(1, 500));
                Myanimator.SetTrigger("Saltar");
            }
        }
    }
    bool EnSuelo()
    {
        bool estaTocando = misPies.IsTouchingLayers(LayerMask.GetMask("Ground"));

        return estaTocando;
    }
    void Runing()
    {
        if (sepuedemover)
        {
            float mov = Input.GetAxis("Horizontal");

            transform.Translate(new Vector2(mov * Time.deltaTime * speed, 0));

            if (mov != 0)

            {
                Myanimator.SetBool("estar corriendo", true);
                if (mov < 0)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                else
                {
                    transform.localScale = new Vector2(1, 1);
                }


            }

            else
            {
                Myanimator.SetBool("estar corriendo", false);

            }
        }
    }
    void Dash_mov()
    {
        if (EnSuelo())
        {
          
            if (Input.GetKey(KeyCode.X))
            {
               
                sepuedemover = false;
                Tiempo_Dash = +1 * Time.deltaTime;

                
                

                if (Tiempo_Dash < 0.40f)
                {
                    Dash = true;
                    Myanimator.SetBool("dash", true);
                    transform.Translate(dir * Vector3.right * Tiempo_Dash * 1000 * Time.fixedDeltaTime);
                }

 
                if (dir != 0)
                {
                    Myanimator.SetBool("dash", true);
                      Instantiate(sonidodash);

                    if (dir < 0)
                    {
                        if (sepuedemover)
                        {
                          
                            transform.localScale = new Vector2(-1, 1);
                        }

                    }
                    else
                    {
                        if (sepuedemover)
                        {
                            transform.localScale = new Vector2(1, 1);
                        }
                    }
                }
            }

            else
            {
                Dash = false;
                Myanimator.SetBool("dash", false);
                Tiempo_Dash = 0;
                sepuedemover = true;
            }
        }

    }

    private void detectarenemigos() {
        contadorenemigos = 0;
        var detectarenemigos = FindObjectsOfType<enemigoCONTADOR>();
        contadorenemigos = detectarenemigos.Length;
        Debug.Log(contadorenemigos);
        if (contadorenemigos <= 0 )
        {
            ganaste.SetActive(true);
        }
        contadorenemigostext.text = contadorenemigos.ToString();
    }

}









