using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource Button_Click, Cycle, Scooter, Jet, Car, Crash, Cash, TimeUp, BG_;
    public AudioClip button_click, cycle, scooter, jet, car, crash, cash, timeup, bg;


    public static AudioManager instance;
    private void Awake()
    {
    }

    void Start()
    {
        Button_Click = gameObject.AddComponent<AudioSource>();
        Cycle = gameObject.AddComponent<AudioSource>();
        Scooter = gameObject.AddComponent<AudioSource>();
        Jet = gameObject.AddComponent<AudioSource>();
        Car = gameObject.AddComponent<AudioSource>();
        Crash = gameObject.AddComponent<AudioSource>();
        Cash = gameObject.AddComponent<AudioSource>();
        TimeUp = gameObject.AddComponent<AudioSource>();
        BG_ = gameObject.AddComponent<AudioSource>();

        Button_Click.clip = button_click;
        Cycle.clip = cycle;
        Scooter.clip = scooter;
        Jet.clip = jet;
        Car.clip = car;
        Crash.clip = crash;
        Cash.clip = cash;
        TimeUp.clip = timeup;
        BG_.clip = bg;

        Cycle.loop = true;
        Scooter.loop = true; 
        Jet.loop = true;
        Car.loop = true;
    }

    public void OnBG()
    {
        BG_.Play();
    }


    public void OnButtonClick()
    {
       
       // Button_Click.Play();
    }
    public void OnCycleMove()
    {
        if (!Cycle.isPlaying)
        {
            Cycle.Play();
        }
    }

    public void StopCycleMove()
    {
        if (Cycle.isPlaying)
        {
            Cycle.Stop();
        }
    }

    public void OnScooterMove()
    {
        if (!Scooter.isPlaying)
        {
            Scooter.Play();
        }
    }

    public void StopScooterMove()
    {
        if (Scooter.isPlaying)
        {
            Scooter.Stop();
        }
    }

    public void OnJetMove()
    {
        if (!Jet.isPlaying)
        {
            Jet.Play();
        }
    }

    public void StopJetMove()
    {
        if (Jet.isPlaying)
        {
            Jet.Stop();
        }
    }

    public void OnCarMove()
    {
        if (!Car.isPlaying)
        {
            Car.Play();
        }
    }

    public void StopCarMove()
    {
        if (Car.isPlaying)
        {
            Car.Stop();
        }
    }

    public void OnCrash()
    {
        Crash.Play();
    }

    public void OnCashCollect()
    {
        Cash.Play();
    }

    public void OnTimeUp()
    {
        TimeUp.Play();
    }




}
