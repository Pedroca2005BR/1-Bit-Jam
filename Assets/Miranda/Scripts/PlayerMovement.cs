using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IRespawnable
{
    private float horizontal;
    [SerializeField] public float speed = 8f; // Velocidade normal do jogador
    [SerializeField] private float crouchSpeed = 4f; // Velocidade reduzida ao agachar
    private float jumpingPower = 12f; // Altura do pulo
    private bool isFacingRight = true;
    private bool isCrouching = false; // Controle do estado de agachamento
    private AudioSource walkSound;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private BoxCollider2D playerCollider; // Collider para modificar ao agachar

    private Transform respawnPoint;
    private bool isDisabled = false;
    public Calor script;

    // Animation variables
    private Animator animator;
    private string currentAnimation = "";

    private void Start()
    {
        animator = GetComponent<Animator>(); // Pega referência do animator
        walkSound = AudioManager.Instance.CreateLoopingSFX("playerWalk");
    }

    void Update()
    {
        if (isDisabled) return;

        horizontal = Input.GetAxisRaw("Horizontal");

        // Toca o som de movimento apenas se estiver no chão e se movendo
        // nao estava combinando com o jogo
        /*
        if (horizontal != 0 && NoChao())
        {
            if (!walkSound.isPlaying)
            {
            walkSound.Play();
            }
        }
        else
        {
            if (walkSound.isPlaying)
            {
            walkSound.Stop();
            }
        }
        */
        // Pular
        if (Input.GetButtonDown("Jump") && NoChao() && !isCrouching) // Não pode pular agachado
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            ChangeAnimation("jump_start_rap_ani");
        }

        // Agachar
        if (Input.GetKeyDown(KeyCode.LeftControl) && NoChao()) // Se pressionar LeftControl e estiver no chão
        {
            isCrouching = true;
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y / 2); // Reduz altura do collider
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl)) // Se soltar LeftControl, levanta
        {
            isCrouching = false;
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y * 2); // Restaura o tamanho do collider
        }

        if (script.calor <= 0) StartCoroutine(Respawn());

        Flip();
        CheckAnimation();
    }

    private void FixedUpdate()
    {
        if (isDisabled) return;

        float moveSpeed = isCrouching ? crouchSpeed : speed; // Se estiver agachado, usa velocidade reduzida
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    private bool NoChao()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void CheckAnimation()
    {
        if (currentAnimation == "jump_start_rap_ani" || currentAnimation == "jump_end_rap_ani") return;

        if (currentAnimation == "jump_up_rap_ani")
        {
            if (rb.linearVelocity.y < 0.2)
                ChangeAnimation("jump_down_rap_ani");
            return;
        }

        if (currentAnimation == "jump_down_rap_ani")
        {
            if (NoChao())
                ChangeAnimation("jump_end_rap_ani");
            return;
        }

        if (isCrouching)
        {
            if (rb.linearVelocity.x > 0.5f || rb.linearVelocity.x < -0.5f)
                ChangeAnimation("walk_agachado_ani");

            else ChangeAnimation("idle_agachado_rap_ani");// Ativa animação de agachar
        }
        else if (rb.linearVelocity.x > 0.5f || rb.linearVelocity.x < -0.5f)
        {
            ChangeAnimation("walk_rap_ani", 0f);
        }
        else
        {
            if (currentAnimation != "idle_rap_ani" && NoChao())
            {
                ChangeAnimation("sit_down_rap_ani");
            }
        }


    }

    public void ChangeAnimation(string animation, float crosfade = 0.1f, float time = 0)
    {
        if (time > 0) StartCoroutine(Wait());
        else Validate();

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(time - crosfade);

            if (animation == "idle_rap_ani")
            {
                if (rb.linearVelocity.y == 0f)
                    Validate();
            }
            else
                Validate();
        }


        void Validate()
        {
            if (currentAnimation != animation)
            {
                currentAnimation = animation;
                if (currentAnimation == "")
                    CheckAnimation();
                else
                    animator.CrossFade(animation, crosfade);
            }
        }
    }

    private void Flip()
    {
        if (!isFacingRight && horizontal < 0f || isFacingRight && horizontal > 0f)
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
            AudioManager.Instance.PlaySFX("playerDamage");
            StartCoroutine(Respawn());
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioManager.Instance.PlaySFX("playerDamage");
            StartCoroutine(Respawn());
        }
    }

    public void SetSpawnPoint(Transform transform)
    {
        respawnPoint = transform;
    }

    public IEnumerator Respawn()
    {
        script.calor = 100;
        isDisabled = true;

        yield return new WaitForSeconds(0.1f);

        transform.position = respawnPoint.position;

        yield return new WaitForSeconds(0.1f);
        isDisabled = false;
    }
}