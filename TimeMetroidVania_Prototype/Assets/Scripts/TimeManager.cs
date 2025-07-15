using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("Custom Time Scales")]
    [Range(0f, 1f)] public float playerTimeScale = 1f;
    [Range(0f, 1f)] public float worldTimeScale = 1f;

    [Header("Settings")]
    [SerializeField] private float targetWorldSlowTimeScale = 0.2f;
    [SerializeField] private float slowDuration = 3f;
    [SerializeField] private float targetPlayerFastTimeScale = 1.2f;
    [SerializeField] private float fastDuration;
    private float fastTimer;
    private float slowTimer;
    private bool isWorldTimeSlowed;
    private bool isPlayerTimeFast;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (isWorldTimeSlowed)
        {
            slowTimer -= Time.unscaledDeltaTime;
            if (slowTimer <= 0f)
            {
                ResetWorldTime();
            }
        }

        if (isPlayerTimeFast)
        {
            fastTimer -= Time.unscaledDeltaTime;
            if(fastTimer <= 0f)
            {
                ResetPlayerTime();
            }
        }
    }

    public void IncreasePlayerTime()
    {
        if (isWorldTimeSlowed || isPlayerTimeFast )     
            return;

        Debug.Log("increasing Player time");
        playerTimeScale = targetPlayerFastTimeScale;
        fastTimer = fastDuration;
        isPlayerTimeFast = true;

    }

    public void ResetPlayerTime()
    {
        playerTimeScale = 1;
        isPlayerTimeFast = false;
    }

    public void SlowWorldTime()
    {
        if (isWorldTimeSlowed || isPlayerTimeFast) return;

        worldTimeScale = targetWorldSlowTimeScale;
        slowTimer = slowDuration;
        isWorldTimeSlowed = true;
    }

    public void ResetWorldTime()
    {
        worldTimeScale = 1f;
        isWorldTimeSlowed = false;
    }

    public float PlayerDeltaTime => Time.unscaledDeltaTime * playerTimeScale;
    public float WorldDeltaTime => Time.unscaledDeltaTime * worldTimeScale;

    public bool IsWorldTimeSlowed() => isWorldTimeSlowed;
    public bool IsPlayerTimeFast() => isPlayerTimeFast;
}
