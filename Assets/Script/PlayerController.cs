using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private new SpriteRenderer renderer;
    public GameObject _Reset;
    public GameObject Disparo1;
    public GameObject Disparo2;
    public Transform InicioDisparo;
    public float contadorSegundos = 0;
    int estado = 0;
    float speed = 0.0f;
    public float jumpSpeed = 1600.0f;
    int salto = 2;
    
    private Text vidaTxt;
    private int vida;
    private const string ENEMY = "Enemigo";
    void Start()
    {
        _Reset.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        var transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        
        vidaTxt = GameObject.Find("NumV").GetComponent<Text>();
        vida = int.Parse(vidaTxt.text);
    }

    // Update is called once per frame
    void Update()
    {
        estado = 0;
        animator.SetInteger("Estado", estado);
        speed = 0.0f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            estado = 1;
            renderer.flipX = false;
            speed = 7.0f;
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
            animator.SetInteger("Estado", estado);
            
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
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo1"))
        {
            salto = 0;
            jumpSpeed = 1600.0f;
            estado = 0;
        }
        if (collision.gameObject.CompareTag("Llave"))
        {
            SceneManager.LoadScene("SampleScene2");
        }

        if (collision.gameObject.CompareTag(ENEMY))
        {
            rb.AddForce(Vector2.up * 400);
            vida -= 1;
            vidaTxt.text = vida.ToString();
            if (vida <= 0)
            {
                vidaTxt.text = "0";
                Destroy(this.gameObject, 0.1f);
                _Reset.gameObject.SetActive(true);
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
