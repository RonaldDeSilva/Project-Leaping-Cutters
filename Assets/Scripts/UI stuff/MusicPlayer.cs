using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip Menu;
    public AudioClip ThePitt;
    public AudioClip BabyBeardsShip;
    public AudioClip WizardMap;
    private AudioSource Music;
    private bool changed = false;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Music = GetComponent<AudioSource>();
        Music.clip = Menu;
        Music.Play();
    }

    public void PitMusic()
    {
        Music.Stop();
        Music.clip = ThePitt;
        Music.Play();
    }
    public void MenuMusic()
    {
        Music.Stop();
        Music.clip = Menu;
        Music.Play();
    }
    public void WizardMusic()
    {
        Music.Stop();
        Music.clip = WizardMap;
        Music.Play();
    }
    public void BabyBeardMusic()
    {
        Music.Stop();
        Music.clip = BabyBeardsShip;
        Music.Play();
    }
}
