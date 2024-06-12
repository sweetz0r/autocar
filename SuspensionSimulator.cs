using UnityEngine;

public class SuspensionSimulator : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioClip knockingSound;
    public WheelCollider[] wheelColliders;
    public bool[] wheelDamages;

    public float maxLoad = 1f;

    private void Update()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            var wheelCollider = wheelColliders[i];
            var audioSource = audioSources[i];
            var isDamaged = wheelDamages[i];

            bool isLoaded = IsSuspensionLoaded(wheelCollider);
            PlayKnockingSound(audioSource, isLoaded, isDamaged);
        }
    }

    private bool IsSuspensionLoaded(WheelCollider wheelCollider)
    {
        WheelHit hit;
        if (wheelCollider.GetGroundHit(out hit))
        {
            float load = hit.force;
            return load > maxLoad; // Порог загрузки подвески
        }
        return false;
    }

    private void PlayKnockingSound(AudioSource audioSource, bool isLoaded, bool isDamaged)
    {
        if (isDamaged && isLoaded && !audioSource.isPlaying)
        {
            audioSource.clip = knockingSound;
            audioSource.Play();
        }
        else if ((!isDamaged || !isLoaded) && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}