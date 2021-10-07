using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player2COntro : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private new SpriteRenderer renderer;
    public float jumpSpeed = 1600.0f;
    float speed = 0.0f;
    int estado = 0;
    private int salto = 2;
    
    public GameObject Disparo1;
    public GameObject Disparo2;
    public Transform InicioDisparo;
    public float contadorSegundos = 0;
    public float contadorSegundos2 = 0;
    
    private Text vidaTxt;
    private Text TimeT;
    
    private int vida;
    private float TimeIn;
    private const string ENEMY = "Enemigo";
    private bool timeC = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        
        vidaTxt = GameObject.Find("NumV").GetComponent<Text>();
        vida = int.Parse(vidaTxt.text);
        
        TimeT = GameObject.Find("Time").GetComponent<Text>();
        TimeIn = int.Parse(TimeT.text);
    }
    

    // Update is called once per frame
    void Update()
    {
        estado = 0;
        animator.SetInteger("Estado", estado);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            estado = 1;
            renderer.flipX = false;
            animator.SetInteger("Estado", estado);
            speed = 7.0f;
            rb.velocity = new Vector2(speed, rb.velocity.y); 
            transform.eulerAngles = new Vector3(0, 0, 0);

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            estado = 1;
            renderer.flipX = true;
            speed = 7.0f;
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
            animator.SetInteger("Estado", estado);

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            renderer.flipX = true;
            estado = 3;
            animator.SetInteger("Estado", estado);
        }
        if (salto <= 1)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                estado = 2;
                animator.SetInteger("Estado", estado);
                rb.AddForce(Vector2.up * jumpSpeed);
                salto++;
                jumpSpeed = 800.0f;
            }
        }
        if (Input.GetKey(KeyCode.X))
        {
            estado = 4;
            animator.SetInteger("Estado",estado);
            contadorSegundos += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            animator.SetInteger("Estado", 0);

            contadorSegundos += Time.deltaTime;
            if (contadorSegundos <= 0.5)
            {
                Instantiate(Disparo1, InicioDisparo.position, Quaternion.Euler(0f, 0f, 0));
                contadorSegundos = 0;
            }

            if (contadorSegundos >= 0.5 && contadorSegundos <= 5.5)
            {
                Instantiate(Disparo2, InicioDisparo.position, Quaternion.Euler(0f, 0f, 0));
                contadorSegundos = 0;
            }
        }

        if (timeC)
        {
            var tim = contadorSegundos2 += Time.deltaTime;
            TimeT.text = tim.ToString(CultureInfo.InvariantCulture);
            if (tim > 15.0)
            {
                SceneManager.LoadScene("SampleScene2");
                timeC = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Suelo1"))
        {
            Debug.Log("Suelo");
            salto = 0;
            jumpSpeed = 1600.0f;
            estado = 0;
        }
        if (collision.gameObject.CompareTag("Llave"))
        {
            SceneManager.LoadScene("JuegoFin");
        }
        if (collision.gameObject.CompareTag(ENEMY))
        {
            rb.AddForce(Vector2.up * 400);
            vida -= 1;
            vidaTxt.text = vida.ToString();
            if (vida <= 0)
            {
                SceneManager.LoadScene("SampleScene2");
                vidaTxt.text = "0";
                Destroy(this.gameObject, 0.1f);
            }

            var flip = renderer.flipX;
            if (flip)
            {
                rb.AddForce(Vector2.right * 800);
            }
            else
            {
                Debug.Log("false");
                rb.AddForce(Vector2.left * 800);
            }
        }
    }
}
