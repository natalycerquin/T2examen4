using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eenmo2Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    // private SpriteRenderer renderer;
    private SpriteRenderer flip;
    float velocityX = 5.0f;
   
    private int contador1 = 0;
    private const string Poder1 = "Poder1";
    private const string Poder2 = "Poder2";
    
    private Text puntajeTxt;
    private int puntaje;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        flip = GetComponent<SpriteRenderer>();
        
       puntajeTxt = GameObject.Find("NumE").GetComponent<Text>();
       puntaje = int.Parse(puntajeTxt.text);
    }
    
    void Update()
    {
        rb.velocity = new Vector2(velocityX , rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Tope1"))
        {
            velocityX *= -1;
            flip.flipX = !flip.flipX;
        }
        
        if (other.gameObject.CompareTag(Poder1))
        {
            Destroy(other.gameObject);
            contador1 += 1;
            if (contador1 == 4)
            {
                puntaje -= 1;
                puntajeTxt.text = puntaje.ToString();
                Destroy(this.gameObject, 0.1f);
            }
        }
        if (other.gameObject.CompareTag(Poder2))
        {
            Destroy(other.gameObject);
            contador1 += 2;
            Destroy(this.gameObject, 0.1f);
            if (contador1 == 6)
            {
                puntaje -= 1;
                puntajeTxt.text = puntaje.ToString();
                Destroy(this.gameObject, 0.1f);
            }
        }
    }
}
