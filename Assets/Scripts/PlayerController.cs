using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
//movement
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed =3.0f;

//health
    public int maxHealth =5;
    int currentHealth;
    public int health { get {return currentHealth; }}

//temporary invincibility
    public float timeInvicible =2.0f;
    bool isInvencible;
    float damageCooldown;

    Animator animator;
    Vector2 moveDirection = new Vector2(1,0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();


        currentHealth = maxHealth;


        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)){
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);


       if (isInvencible){
        damageCooldown -= Time.deltaTime;
        if (damageCooldown <0){
            isInvencible=false;
        }
       }

    }

    void FixedUpdate(){
        Vector2 position = (Vector2)rigidbody2d.position + move* 3.0f * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }


    public void ChangeHealth (int amount){
       

        if (amount <0){
            if (isInvencible){
                animator.SetTrigger("Hit");
                return;
                
            }
            isInvencible =true;
            damageCooldown = timeInvicible;
        }
         currentHealth =Mathf.Clamp(currentHealth + amount, 0, maxHealth);
         UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }
}
