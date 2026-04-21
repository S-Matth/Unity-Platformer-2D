using UnityEngine;

public class Timer: MonoBehaviour
{
    [Header("Réglages")]
    public float stopTimeBeforeDeath = 3f;
    public float speedThreshold = 0.1f;
    public float noMaskDeathDelay = 0.1f;
    public float deathCooldown = 1f; // empêche la boucle après respawn

    private Rigidbody2D rb;
    private CPlayerLife playerLife;
    private PlayerMask playerMask;

    private bool isReallyInWater = false;
    private bool isDying = false;
    private float timer;
    private float cooldownTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerLife = GetComponent<CPlayerLife>();
        playerMask = GetComponent<PlayerMask>();

        ForceResetState();
    }

    private void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }

        if (isDying)
            return;

        if (!isReallyInWater)
        {
            timer = stopTimeBeforeDeath;
            return;
        }

        if (!playerMask.hasWaterMask)
        {
            KillNow();
            return;
        }

        float speed = rb.linearVelocity.magnitude;

        if (speed <= speedThreshold)
        {
            timer -= Time.deltaTime;
            Debug.Log("Timer restant: " + timer);

            if (timer <= 0f)
            {
                KillNow();
            }
        }
        else
        {
            timer = stopTimeBeforeDeath;
        }
    }

    public void SetInWater(bool value)
    {
        if (isDying) return;
        if (cooldownTimer > 0f) return;

        isReallyInWater = value;

        if (!value)
            timer = stopTimeBeforeDeath;
    }

    public void ForceResetState()
    {
        isReallyInWater = false;
        isDying = false;
        timer = stopTimeBeforeDeath;
        cooldownTimer = deathCooldown;
        CancelInvoke();
    }

    private void KillNow()
    {
        if (isDying) return;

        isDying = true;
        isReallyInWater = false;
        timer = stopTimeBeforeDeath;
        cooldownTimer = deathCooldown;
        CancelInvoke();

        playerLife.Die();

        isDying = false;
    }
}