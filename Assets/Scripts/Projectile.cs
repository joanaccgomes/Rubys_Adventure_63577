using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D Rigidbody2d;
    

    void Awake()
    {
        Rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 100.0f){
            Destroy(gameObject);
        }
    }
    public void Launch(Vector2 direction, float force){
        Rigidbody2d.AddForce(direction * force);
    }

    void OnTriggerEnter2D(Collider2D other){

    EnemyController enemy = other.GetComponent<EnemyController>();

      if (enemy != null){
        enemy.Fix();
      }
        
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other){
        Destroy(gameObject);
    }
}
