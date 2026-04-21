using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Header("Sons")]
    [SerializeField] private AudioClip soundFootstep;
    [SerializeField] private AudioClip soundJump;
    [SerializeField] private AudioClip soundDoubleJump;
    [SerializeField] private AudioClip soundWallJump;
    [SerializeField] private AudioClip soundDash;
    [SerializeField] private AudioClip soundLand;
    [SerializeField] private AudioClip soundDeath;

    [Header("Paramètres")]
    [SerializeField] private float footstepInterval = 0.35f;

    private AudioSource audioSource;
    private float footstepTimer = 0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(soundJump);
        Debug.Log("PlayJump appelé !");
        audioSource.PlayOneShot(soundJump);
    }
    public void PlayDoubleJump() => audioSource.PlayOneShot(soundDoubleJump);
    public void PlayWallJump() => audioSource.PlayOneShot(soundWallJump);
    public void PlayDash() => audioSource.PlayOneShot(soundDash);
    public void PlayLand() => audioSource.PlayOneShot(soundLand);
    public void PlayDeath() => audioSource.PlayOneShot(soundDeath);

    // Appelé chaque frame depuis playerController quand le joueur court
    public void HandleFootstep(bool isGrounded, float inputX)
    {
        if (isGrounded && Mathf.Abs(inputX) > 0.1f)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                audioSource.PlayOneShot(soundFootstep);
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }
}