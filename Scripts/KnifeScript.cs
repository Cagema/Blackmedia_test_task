using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    [SerializeField]
    private Vector2 throwForce;

    private bool isActive = true;

    private Rigidbody2D rb;
    private BoxCollider2D knifeCollider;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isActive && Input.GetMouseButtonDown(0))
        {
            rb.AddForce(throwForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;
            GameController.Instance.GameUI.DecrementDisplayedKnifeCount();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameController.Instance.appleSound.Play();
        GameController.Instance.GameUI.AddMoney();
        if (Random.Range(0, 1) == 0)
        {
            GameController.Instance.lives++;

        }
        Destroy(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive)
        {
            return;
        }

        isActive = false;

        if (collision.collider.tag == "Log")
        {
            GetComponent<ParticleSystem>().Play();
            GameController.Instance.hitSound.Play();

            rb.velocity = new Vector2(0, 0);
            rb.bodyType = RigidbodyType2D.Kinematic;
            this.transform.SetParent(collision.collider.transform);

            knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.35f);
            knifeCollider.size = new Vector2(knifeCollider.size.x, 0.7f);

            GameController.Instance.OnSuccessfulKnifeHit();
        }
        else if (collision.collider.tag == "Knife")
        {
            rb.velocity = new Vector2(rb.velocity.x, -2f);
            knifeCollider.enabled = false;

            GameController.Instance.gameOverSound.Play();
            if (GameController.Instance.lives > 0)
            {
                GameController.Instance.lives--;
                GameController.Instance.OnSuccessfulKnifeHit();
                return;
            }
            PlayerPrefs.SetInt("Score", 0);
            GameController.Instance.StartGameOverSequence(false);
        }
    }
}
