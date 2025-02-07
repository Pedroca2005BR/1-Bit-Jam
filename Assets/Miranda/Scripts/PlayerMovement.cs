using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IRespawnable
{
    private float horizontal;
    private float speed = 8; //velocidade do jogador
    private float jumpingPower = 12f; //altura do pulo
    private bool isFacingRight = true; //verdadeiro caso o jogador esteja olhando para direita

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Transform respawnPoint; // controla o ponto de respawn
    private bool isDisabled = false;    // isDisabled controla se o Update pode ser chamado ou n�o. Importante para Respawn
    public Calor script;

    //animation variables
    private Animator animator;
    private string currentAnimation = "";

    private void Start()
    {
        animator = GetComponent<Animator>(); // pega  referencia animator
    }


  
    //fun��o que  muda as anima��es
    private void ChangeAnimation(string animation, float crosfade  = 0.2f)
    {
        if(currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(animation, crosfade);

        }
    }

    void Update()
    {
        if (isDisabled) return;

        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && NoChao())//pula se o jogador estiver tocando o ch�o com o layer 'Ground'
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);

        }

        if(script.calor <= 0 ) StartCoroutine(Respawn()); 
        Flip();


        CheckAnimation(); // chama fun��o de checar anima��es
    }

   


    private void FixedUpdate()
    {
        if (isDisabled) return;

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y); // velocidade de movimento do jogador
    }
    private bool NoChao()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); // V� se o jogador est� tocando o ch�o 

    }

    private void CheckAnimation()
    {
        if(rb.linearVelocityX >0.05f || rb.linearVelocityX < -0.05f)
        {
            ChangeAnimation("walk_rap_ani");
        }
        else
        {
            ChangeAnimation("Idle_rap_ani");
        }


    }



    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || isFacingRight && horizontal > 0f) // Gira o sprite para o dire��o que o jogador estiver olhando
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerKiller"))
        {
            StartCoroutine(Respawn());
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Respawn());
        }
    }

    public void SetSpawnPoint(Transform transform)
    {
        respawnPoint = transform;
        
        // TO DO: Rodar anima��o de dormir
    }

    public IEnumerator Respawn()
    {
        script.calor = 100;
        isDisabled = true;

        // TO DO: Colocar anima��o e trocar o tempo de WaitForSeconds para o tempo de anima��o

        yield return new WaitForSeconds(0.1f);

        transform.position = respawnPoint.position;

        yield return new WaitForSeconds(0.1f);
        isDisabled = false;
    }
}
