using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8; //velocidade do jogador
    private float jumpingPower = 12f; //altura do pulo
    private bool isFacingRight = true; //verdadeiro caso o jogador esteja olhando para direita

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && NoChao())//pula se o jogador estiver tocando o chão com o layer 'Ground'
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);

        }

        Flip();

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y); // velocidade de movimento do jogador
    }
    private bool NoChao()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); // Vê se o jogador está tocando o chão 

    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || isFacingRight && horizontal > 0f) // Gira o sprite para o direção que o jogador estiver olhando
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        
        }

    }
}
