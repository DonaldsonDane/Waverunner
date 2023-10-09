using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    public float timer;
    private bool isTimerRunning = false;

  

    private void Update()
    {
        // Update the timer text if the timer is running.
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerText();
        }
    }

    public void StartTimer()
    {
        // Start the timer.
        isTimerRunning = true;
        timer = 0f;
    }

    public void EndTimer()
    {
        // Stop the timer.
        isTimerRunning = false;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        // Update the TMP_Text component with the current timer value.
        if (timerText != null)
        {
            timerText.text = "Time: " + timer.ToString("F2"); // Format the timer value as desired.
        }
    }
}
