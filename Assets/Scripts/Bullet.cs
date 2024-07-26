using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;

    public Rigidbody2D rb;
    public GameObject impactEffect;

    // add this to enemy weapon script
    public int damage = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        // TODO: check not player
        GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 0.45f);
        Destroy(gameObject);

        //TODO: copy this to Enemy's weapon script
        Damageable player = hitInfo.GetComponent<Damageable>();
        if (player != null)
        {
            player.Hit(damage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
