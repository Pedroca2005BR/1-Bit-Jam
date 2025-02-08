using UnityEngine;

public class TorchSound : MonoBehaviour
{
    private AudioSource torchAudio;
    [SerializeField] private string soundName = "tocha"; // Nome da SFX no AudioManager
    [SerializeField] private float maxVolume = 1f;
    [SerializeField] private float fadeSpeed = 1f;
    private bool playerNearby = false;

    private void Start()
    {
        torchAudio = AudioManager.Instance.GetSFXAudioSource(soundName);
        if (torchAudio != null)
        {
            torchAudio.loop = true; // Mantém o som da tocha tocando continuamente
            torchAudio.Play();
        }
    }

    private void Update()
    {
        if (torchAudio == null) return;

        if (playerNearby)
        {
            torchAudio.volume = Mathf.MoveTowards(torchAudio.volume, maxVolume, fadeSpeed * Time.deltaTime);
        }
        else
        {
            torchAudio.volume = Mathf.MoveTowards(torchAudio.volume, 0, fadeSpeed * Time.deltaTime);
            if (torchAudio.volume == 0)
            {
                torchAudio.Stop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (!torchAudio.isPlaying)
            {
                torchAudio.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}