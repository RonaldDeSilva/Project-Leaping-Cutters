using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WizardExplosion : MonoBehaviour
{
    #region Parameters

    private bool Activated;
    private bool CooldownPeriod;
    private GameObject Can;

    public float Cooldown;
    private int PlayerNum;
    private string SpecialButton;

    public GameObject AudioPlayer;
    public AudioClip CooldownSound;
    public AudioClip ShootSound;

    private int childNum;
    public Transform[] positions;
    public GameObject proj;

    #endregion

    #region Initialization
    void Start()
    {
        StopAllCoroutines();
        Activated = false;
        CooldownPeriod = false;
        PlayerNum = GetComponent<MovementBase>().playerNum;
        Can = GameObject.Find("Canvas");

        if (PlayerNum == 1)
        {
            SpecialButton = "Special";
            childNum = 0;
        }
        else if (PlayerNum == 2)
        {
            SpecialButton = "Special2";
            childNum = 1;
        }
        else if (PlayerNum == 3)
        {
            SpecialButton = "Special3";
            childNum = 2;
        }
        else if (PlayerNum == 4)
        {
            SpecialButton = "Special4";
            childNum = 3;
        }
        Can.transform.GetChild(childNum).GetChild(3).gameObject.GetComponent<Image>().color = Color.white;
    }
    #endregion

    #region Input and Activation
    void FixedUpdate()
    {
        if (Input.GetAxis(SpecialButton) > 0 && !Activated && !CooldownPeriod)
        {
            StartCoroutine("Shooter");
        }

        if (Activated)
        {
            StartCoroutine("SpecialCooldown");
            CooldownPeriod = true; 
            Activated = false;
        }

    }

    #endregion

    #region Coroutines
    IEnumerator Shooter()
    {
        var sound2 = Instantiate(AudioPlayer);
        sound2.GetComponent<SoundPlayer>().Awaken(ShootSound, 1f);
        Activated = true;
        Can.transform.GetChild(childNum).GetChild(3).gameObject.GetComponent<Image>().color = Color.black;
        for (int i = positions.Length - 1; i >= 0; i--)
        {
            var dir = new Vector2(positions[i].position.x - this.transform.position.x, positions[i].position.y - this.transform.position.y);
            var bullet = Instantiate(proj, positions[i].position, positions[i].rotation);
            bullet.GetComponent<Projectile>().Awaken(dir, this.transform.GetChild(0).gameObject);
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator SpecialCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        Can.transform.GetChild(childNum).GetChild(3).gameObject.GetComponent<Image>().color = Color.white;
        var sound = Instantiate(AudioPlayer);
        sound.GetComponent<SoundPlayer>().Awaken(CooldownSound, 1f);
        CooldownPeriod = false;
    }

    #endregion
}
