using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float TimeLeft = 300;
    public bool TimerOn = false;
    public AudioClip air_horn;
    public AudioClip clock_beep;
    AudioSource audioSource;
    public TMPro.TextMeshProUGUI TimerTxt;
    void Start()
    {
        TimerOn = true;
        TimeLeft = 300;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if(TimerOn)
        {
            if (TimeLeft > 1)
            {
                TimeLeft -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(TimeLeft / 60);
                float seconds = Mathf.FloorToInt(TimeLeft % 60);
                TimerTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);
                // PlayBeep();
                // if (TimeLeft = 60) //start playing intensifying heartbeat or something
            }

            else
            {
                Debug.Log("Time is UP!");
                TimeLeft = 0;
                TimerOn = false;
                TimerTxt.text = "0:00";
                audioSource.PlayOneShot(air_horn, 0.8F);
            }
        }
    }
    /*void PlayBeep()
    {
        switch (TimeLeft)
        {
            case (10):
                {
                    audioSource.PlayOneShot(clock_beep, 0.3F);
                    break;
                }
            case (9):
                {
                    audioSource.PlayOneShot(clock_beep, 0.4F);
                    break;
                }
            case (8):
                {
                    audioSource.PlayOneShot(clock_beep, 0.5F);
                    break;
                }
            case (7):
                {
                    audioSource.PlayOneShot(clock_beep, 0.6F);
                    break;
                }
            case (6):
                {
                    audioSource.PlayOneShot(clock_beep, 0.7F);
                    break;
                }
            case (5):
                {
                    audioSource.PlayOneShot(clock_beep, 0.8F);
                    break;
                }
            case (4):
                {
                    audioSource.PlayOneShot(clock_beep, 0.9F);
                    break;
                }
            case (3):
                {
                    audioSource.PlayOneShot(clock_beep, 1F);
                    break;
                }
            case (2):
                {
                    audioSource.PlayOneShot(clock_beep, 1F);
                    break;
                }
            case (1):
                {
                    audioSource.PlayOneShot(clock_beep, 1F);
                    break;
                }
        }
    }*/
}
