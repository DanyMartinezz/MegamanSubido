using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparo_enm_3 : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D body;
    GameObject playerG;
    Animator Myanimator;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // Myanimator = GetComponent<Animator>();
    }

    public void shoot (bool dir)
    {
         body = GetComponent<Rigidbody2D>();

        if (dir)
        {
            body.velocity = new Vector2(speed , 6);

            this.transform.localScale = new Vector2(this.transform.localScale.x * 1, this.transform.localScale.y);
            
        }
        else
        {
            body.velocity = new Vector2(-speed, 6);

             this.transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("jugador"))
        {
            //Myanimator.SetTrigger("chocar");
            body.velocity = Vector2.zero;
            Application.LoadLevel(Application.loadedLevel);
        }
      

    }
  
    void die ()
    {
        Destroy(gameObject);
    }

}
