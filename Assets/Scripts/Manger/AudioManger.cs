using UnityEngine;

public class AudioManger : BaseManger
{
    private const string Sound_Path_Prefix = "Sounds/";
    public const string Sound_Alert = "Alert";
    public const string Sound_ArrowShoot = "ArrowShot";
    public const string Sound_Bg_Fast = "Bg(fast)";
    public const string Sound_Bg_Moderate = "Bg(moderate)";
    public const string Sound_ButtonClick = "ButtonClick";
    public const string Sound_Miss = "Miss";
    public const string Sound_ShootPerson = "ShootPerson";
    public const string Sound_Timer = "Timer";

    private AudioSource bgAudioSource;
    private AudioSource norAudioSource;

    public AudioManger(GameFacade facade) : base(facade)
    {
    }

    public override void OnInit()
    {
        GameObject go = new GameObject("AudioManger");

        bgAudioSource = go.AddComponent<AudioSource>();
        norAudioSource = go.AddComponent<AudioSource>();

        PlayBgSounde(Sound_Bg_Moderate);
    }

    //播放声音
    private void PlaySound(AudioSource audioSource, AudioClip audioClip, float volueme, bool isLoop = false)
    {
        audioSource.clip = audioClip;
        audioSource.loop = isLoop;
        audioSource.volume = volueme;
        audioSource.Play();
    }

    //加载声音
    private AudioClip LoadSound(string soundName)
    {
        return Resources.Load<AudioClip>(Sound_Path_Prefix + soundName);
    }

    //播放背景音乐
    public void PlayBgSounde(string soundName)
    {
        PlaySound(bgAudioSource, LoadSound(soundName), 0.1f, true);
    }
    
    //播放声音（除背景音乐之外的）
    public void PlayNorSound(string soundName)
    {
        PlaySound(norAudioSource, LoadSound(soundName), 0.6f);
    }
}