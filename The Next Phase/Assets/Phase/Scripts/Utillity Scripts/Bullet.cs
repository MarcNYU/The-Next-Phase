using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public int damage;
    public float lifeSpan;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        //move in a direction fast
        transform.position += direction * Time.deltaTime * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log ("Bullet Hit");
        //get rid of enemies
        // check if its an enemy
        if (col.collider.tag == "Enemy")
        {
            col.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(1, transform.position);
        }
        Destroy(gameObject);

    }
}
