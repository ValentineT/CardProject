using System.Collections;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource musAudioSource, sfxAudioSource;
    [SerializeField] private AudioClip gameMus, mapMus, menuMus, flipClip, tapClip;
    [SerializeField] private float transitionDuration; // ����� ��������

    private void Start()
    {
        musAudioSource.PlayOneShot(menuMus);
    }
    public void PlayFlip()
    {
        sfxAudioSource.PlayOneShot(flipClip);
    }

    public void PlayClick()
    {
        sfxAudioSource.PlayOneShot(tapClip);
    }

    public void PlayMenuMusic()
    {
        StartCoroutine(FadeToNewMusic(menuMus));
    }

    public void PlayGameMusic()
    {
        StartCoroutine(FadeToNewMusic(gameMus));
    }

    public void PlayMapMusic()
    {
        StartCoroutine(FadeToNewMusic(mapMus));
    }

    // ������� ��� �������� ������������ ������
    private IEnumerator FadeToNewMusic(AudioClip newClip)
    {
        if (musAudioSource.isPlaying)
        {
            // ������ ��������� ��������� �������� �����
            yield return StartCoroutine(FadeOut());
        }

        // ������ ����
        musAudioSource.clip = newClip;
        musAudioSource.Play();

        // ������ ����������� ��������� ������ �����
        yield return StartCoroutine(FadeIn());
    }

    // ����� ��� �������� ���������� ���������
    private IEnumerator FadeOut()
    {
        float startVolume = musAudioSource.volume;

        while (musAudioSource.volume > 0)
        {
            musAudioSource.volume -= startVolume * Time.deltaTime / transitionDuration;
            yield return null;
        }

        musAudioSource.Stop();
        musAudioSource.volume = startVolume; // ���������� ��������� � ��������� ��������
    }

    // ����� ��� �������� ���������� ���������
    private IEnumerator FadeIn()
    {
        musAudioSource.volume = 0f;
        float targetVolume = 0.5f; // ��� ������ �������� ��������� �� ���������

        while (musAudioSource.volume < targetVolume)
        {
            musAudioSource.volume += Time.deltaTime / transitionDuration;
            yield return null;
        }
    }
}
