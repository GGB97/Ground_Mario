using System.Collections;
using UnityEngine;

public class TimeScheduler
{
    private readonly WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();
    
    private float undergroundTime = 30f; //낮보단 길게
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
            //TODO : 이쯤에다 웨이브 증가를 넣어야되지싶은데, 게임매니저에 웨이브 만들고 여기서 증가?
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
