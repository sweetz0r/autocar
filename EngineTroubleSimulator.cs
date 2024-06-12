using UnityEngine;

public class EngineTroubleSimulator : MonoBehaviour
{
    public AudioSource engineSound;
    public AudioClip normalEngineSound;
    public AudioClip troubledEngineSound;
    public GameObject playerCamera;
    public float troubleThreshold = 0.5f;

    private bool isEngineTroubled = false;
    private bool isEngineRunning = false;
    private float engineRPM = 0.0f;
    private float engineStartFailureChance = 0.1f;
    private float engineStopChanceWhenTroubled = 0.1f;

    void Update()
    {
        CheckEngineState();
        if (isEngineTroubled)
        {
            ApplyEffectsWhenTroubled();
        }
        else
        {
            ChangeEngineSound(normalEngineSound);
        }
    }

    public void StartEngine()
    {
        if (ShouldEngineStart())
        {
            isEngineRunning = true;
            engineRPM = GetMinEngineRPM();
            engineSound.Play();
        }
    }

    public void StopEngine()
    {
        isEngineRunning = false;
        engineRPM = 0.0f;
        engineSound.Stop();
    }

    private void CheckEngineState()
    {
        isEngineTroubled = engineRPM < troubleThreshold;
    }

    private void ApplyEffectsWhenTroubled()
    {
        ApplyCameraShake();
        FluctuateEngineRPM();
        ChangeEngineSound(troubledEngineSound);
        TryEngineStop();
    }

    private void ApplyCameraShake()
    {
        playerCamera.transform.localPosition = GetRandomShakePosition();
    }

    private void FluctuateEngineRPM()
    {
        engineRPM = Mathf.Lerp(engineRPM, GetRandomRPM(), Time.deltaTime);
    }

    private void TryEngineStop()
    {
        if (isEngineRunning && ShouldEngineStop())
        {
            StopEngine();
        }
    }

    private void ChangeEngineSound(AudioClip clip)
    {
        if (engineSound.clip != clip)
        {
            engineSound.clip = clip;
            engineSound.Play();
        }
    }

    private bool ShouldEngineStart()
    {
        return Random.Range(0f, 1f) > engineStartFailureChance;
    }

    private bool ShouldEngineStop()
    {
        return Random.Range(0f, 1f) < engineStopChanceWhenTroubled;
    }

    private float GetMinEngineRPM()
    {
        return 1.0f;
    }

    private Vector3 GetRandomShakePosition()
    {
        return Random.insideUnitSphere * 0.1f;
    }

    private float GetRandomRPM()
    {
        return Random.Range(0.0f, troubleThreshold);
    }
}