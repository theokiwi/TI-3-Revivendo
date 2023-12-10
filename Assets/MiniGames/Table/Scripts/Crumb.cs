using UnityEngine;
using UnityEngine.UI;

public class Crumb : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    Rigidbody2D rb;
    private void Awake()
    {
        Image img = GetComponent<Image>();
        img.sprite = sprites[Random.Range(0,2)];
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pano"))
        {
            rb.AddForce((transform.position - collision.transform.position) * 500);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Canvas"))
        {
            Destroy(gameObject);
            CrumbMiniGame.instance.MinusCrumb();
        }
    }
}
