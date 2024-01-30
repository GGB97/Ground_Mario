using System.Collections;
using System.Linq;
using UnityEngine;

public class TimeScheduler
{
    private readonly WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();
    
    private float undergroundTime = 60f; //낮보단 길게
    private float groundTime = 45f;
    private float middleTime = 15f;
    
    public float CurrentTime;

    public IEnumerator Timer()
    {
        while (true)
        {
            CurrentTime = 0f;
            GameManager.Instance.CallStateChangeEvent(GameState.Ground);
            while (CurrentTime < groundTime)
            {
                yield return _fixedUpdate;
                CurrentTime += Time.fixedDeltaTime;
            }
            
           
            CurrentTime = 0f;
            GameManager.Instance.CallStateChangeEvent(GameState.MiddleTime);
            while (CurrentTime < middleTime) //중간 대기 시간 짧게 (지하로 이동할 시간)
            {
                yield return _fixedUpdate;
                CurrentTime += Time.fixedDeltaTime;
            }
            
            CurrentTime = 0f;
            GameManager.Instance.CallStateChangeEvent(GameState.Underground);
            while (CurrentTime < undergroundTime)
            {
                yield return _fixedUpdate;
                CurrentTime += Time.fixedDeltaTime;
            }
            GameManager.Instance.Waveup();
        }
    }
}
