using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Timer : Condition
{
    [SerializeField] private float counter = 10.0f;
    [SerializeField] private TextMeshProUGUI tMesh;
    
    private float remainingTime;
    private bool isRunning = true;

    public delegate void TimerEvent();
    public TimerEvent OnTimeRanOut;
    
    public UnityEvent TimeRanOut;

    private void OnEnable()
    {
        Debug.Log("Timer: timer is enabled");
        isRunning = true;
        remainingTime = counter;
    }

    private void Update()
    {
        if (isRunning)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                if (counter < 60)
                {
                    DisplayTimeInSeconds(remainingTime);
                }
                else
                {
                    DisplayTimeInMinutes(remainingTime);
                }
            }
            else
            {
                remainingTime = 0;
                isRunning = false;
                condition.FulfillCondition();
                OnTimeRanOut?.Invoke();
                TimeRanOut?.Invoke();
                this.enabled = false;
            }
        }
    }

    public void DisplayTimeInMinutes(float timeToDisplay)
    {
        timeToDisplay += 1;
        
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if(tMesh != null)
            tMesh.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void DisplayTimeInSeconds(float timeToDisplay)
    {
        timeToDisplay += 1;

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if (tMesh != null)
            tMesh.text = seconds.ToString();
    }

    //private void OnDisable()
    //{
    //    OnTimeRanOut?.Invoke();
    //    TimeRanOut?.Invoke();
    //}
}
