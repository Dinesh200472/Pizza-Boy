using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----Audio Source-----")]
    [SerializeField] AudioSource BackGround;
    [SerializeField] AudioSource Sfx;
    [SerializeField] AudioSource Acceleration;


    public AudioClip background,buttonclick, cycle, scooter, auto, jet, crash, cash, timeup, finish, Sdelivery,LevelFailed;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Update()
    {
        if(Game_Manager.instance.isPlayScene)
        {
            BackGround.volume = 1;
        }
        else
        {
            BackGround.volume = 0.1f;
        }

        if(ui_manager.instance.isCrashed)
        {
            Acceleration.Stop();
        }

    }

    private void Start()
    {
        BackGround.clip = background;
        BackGround.Play();
    }

    public void OnClickButton()
    { 
        Sfx.PlayOneShot(buttonclick);
    }

    public void PlaySound(AudioClip clip)
    {
        Sfx.PlayOneShot(clip);
    }

    public void PlayAcc(AudioClip clip)
    {
        Acceleration.clip = clip;
        Acceleration.loop = true;
        Acceleration.Play();
    }

    public void LevelFail(AudioClip clip)
    {
        Acceleration.PlayOneShot(clip);
    }

    public void PlayOff()
    {
        Acceleration.loop = false;
        Acceleration.Stop();
    }

}
