using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class AudioBackGroundMgr : MonoBehaviour
{
    public static AudioBackGroundMgr Instance;

    private AudioSource m_AudioSource;
    private AudioClip m_PreAudioClip;
    private string m_AudioName;
    private float m_MaxVolume = 0.3f;
    private float m_FadeOutTime = 1;
    private float m_FadeInTime = 1;
    private float m_Delay = 0;

    private void Awake()
    {
        Instance = this;

        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.volume = 0;
        m_AudioSource.loop = true;

        DontDestroyOnLoad(gameObject);
    }

    public void Play(string name)
    {
        m_AudioName = name;

        LoaderMgr.Instance.LoadOrDownload<AudioClip>(string.Format("Download/Audio/BackGround/{0}", name), name, (AudioClip clip) =>
        {
            if (m_AudioSource.isPlaying && m_AudioSource.clip == clip)
            {
                return;
            }
            StartCoroutine(DoPlayCoroutine(clip));
        }, 2);

        
    }

    private IEnumerator DoPlayCoroutine(AudioClip clip)
    {
        float time1 = Time.time;
        if(m_PreAudioClip != null)
        {
            yield return StartCoroutine(FadeOutCoroutine());
        }
        m_AudioSource.Stop();
        m_PreAudioClip = clip;
        m_AudioSource.clip = clip;
        float time2 = Time.time - time1;
        if(m_Delay > time2)
        {
            yield return new WaitForSeconds(m_Delay - time2);
        }

        yield return StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float time = 0;
        while(time <= m_FadeOutTime)
        {
            time += Time.deltaTime;
            m_AudioSource.volume = Mathf.Lerp(m_MaxVolume, 0, time);
            yield return null;
        }
        m_AudioSource.volume = 0;
    }

    private IEnumerator FadeInCoroutine()
    {
        m_AudioSource.Play();
        float time = 0;
        while (time <= m_FadeInTime)
        {
            time += Time.deltaTime;
            m_AudioSource.volume = Mathf.Lerp(0, m_MaxVolume, time);
            yield return null;
        }
        m_AudioSource.volume = m_MaxVolume;
    }
}
