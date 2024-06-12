using UnityEngine;
using System.Collections;

public class EngineSimulator: MonoBehaviour 
{
    public AudioSource engineStartSound; 
    public AudioSource engineRunningSound; 
    public float repairPercentage = 100f; 
    public float startDelay = 2f;
    private bool engineRunning = false;

    
    float GetModifiedChance(float baseChance) 
    {
        return baseChance * (repairPercentage / 100f);
    }
    
    IEnumerator TryStartEngineWithDelay() 
    {
        engineStartSound.Play(); 
        yield return new WaitForSeconds(startDelay); 
        float startChance = GetModifiedChance(0.7f); 

        if(Random.Range(0f, 1f) < startChance) 
        {
            engineRunning = true;
            engineRunningSound.Play();
            Debug.Log("Engine is running!");
        } 
        else 
        {
            Debug.Log("Fail");
        }
    }
    void RunEngine() 
    {
        
        float stopChance = GetModifiedChance(0.1f); 

        if(Random.Range(0f, 1f) < stopChance)
        {
            engineRunning = false;
            engineRunningSound.Stop();
        }
    }
}
