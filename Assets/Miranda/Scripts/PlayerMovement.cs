using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8; //velocidade do jogador
    private float jumpingPower = 16f; //altura do pulo
    private bool isFacingRight = true; //verdadeiro caso o jogador esteja olhando para direita

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask gorundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && NoChao()) //pulo alto
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);

        }

        Flip();

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }
    private bool NoChao()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, gorundLayer); // V� se o jogador est� tocando o ch�o 

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
}
