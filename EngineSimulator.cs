using UnityEngine;
using System.Collections;

public class EngineSimulator: MonoBehaviour 
{
    public AudioSource engineStartSound; // Звук запуска двигателя
    public AudioSource engineRunningSound; // Звук работающего двигателя
    public float repairPercentage = 100f; // Процент ремонта двигателя от 0 до 100
    public float startDelay = 2f; // Задержка перед запуском двигателя в секундах
    private bool engineRunning = false;

    // Возвращает модифицированный шанс в зависимости от процента ремонта
    float GetModifiedChance(float baseChance) 
    {
        return baseChance * (repairPercentage / 100f);
    }
    
    IEnumerator TryStartEngineWithDelay() 
    {
        engineStartSound.Play(); // Воспроизводим звук запуска двигателя
        yield return new WaitForSeconds(startDelay); // Ждем указанное время
        float startChance = GetModifiedChance(0.7f); // Базовый шанс 70%

        if(Random.Range(0f, 1f) < startChance) 
        {
            engineRunning = true;
            engineRunningSound.Play();
            Debug.Log("Двигатель заведен!");
        } 
        else 
        {
            Debug.Log("Не удалось завести двигатель.");
        }
    }
    void RunEngine() 
    {
        // Шанс заглохнуть двигателя уменьшается с увеличением процента ремонта
        float stopChance = GetModifiedChance(0.1f); // Базовый шанс 10%

        if(Random.Range(0f, 1f) < stopChance)
        {
            engineRunning = false;
            engineRunningSound.Stop();
            Debug.Log("Двигатель заглох.");
        }
    }
}