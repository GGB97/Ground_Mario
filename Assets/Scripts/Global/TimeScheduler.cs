using System.Collections;
using UnityEngine;

public class TimeScheduler
{
    private readonly WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();
    
    private float undergroundTime = 5f; //낮보단 길게
    private float groundTime = 5f;
    private float middleTime = 5f;
    
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
            
            GameManager.Instance.Waveup();
            CurrentTime = 0f;
            GameManager.Instance.gameState = GameState.MiddleTime;
            while (CurrentTime < middleTime) //중간 대기 시간 짧게 (지하로 이동할 시간)
            {
                yield return _fixedUpdate;
                CurrentTime += Time.fixedDeltaTime;
            }
            //TODO : 전환 시 대기시간 enum 저기서 끌어다 쓰면 어떨까
            
            CurrentTime = 0f;
            GameManager.Instance.gameState = GameState.Underground;
            while (CurrentTime < undergroundTime)
            {
                yield return _fixedUpdate;
                CurrentTime += Time.fixedDeltaTime;
                //TODO : 조건하나 달아서 시간 얼마안남았을 때 준비하라고하기?
            }
        }
    }
}
