
using UnityEngine;
using UnityEngine.UI;

public class PoderUnoController : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject Player;
    private new SpriteRenderer renderer;
    private SpriteRenderer dir;
    bool bullet = false;
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        Destroy(this.gameObject, 5f);
        dir = Player.GetComponent<SpriteRenderer>();
        bullet = dir.flipX;
    }

    // Update is called once per frame
    void Update()
    {
        if (bullet)
        {
            renderer.flipX = true;
            rb.velocity = new Vector2(-5, rb.velocity.y);
        }
        else
        {
            renderer.flipX = false;
            rb.velocity = new Vector2(5, rb.velocity.y);
        }
    }
}
