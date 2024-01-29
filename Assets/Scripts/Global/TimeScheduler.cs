using System.Collections;
using UnityEngine;

public class TimeScheduler
{
    private readonly WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();
    
    private float undergroundTime = 30f;
    private float groundTime = 30f;
    
    public float CurrentTime;

    public IEnumerator Timer()
    {
        while (true)
        {
            CurrentTime = 0f;
            GameManager.Instance.gameState = GameState.Ground;
            while (CurrentTime < groundTime)
            {
                yield return _fixedUpdate;
                CurrentTime += Time.fixedDeltaTime;
            }
            
            CurrentTime = 0f;
            GameManager.Instance.gameState = GameState.Underground;
            while (CurrentTime < undergroundTime)
            {
                yield return _fixedUpdate;
                CurrentTime += Time.fixedDeltaTime;
            }
        }
    }
}
