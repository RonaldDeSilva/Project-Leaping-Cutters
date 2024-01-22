using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Specialabilitybasewithnospecifics : MonoBehaviour
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
    public AudioClip ActivationSound;

    private int childNum;

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
        Can.transform.GetChild(childNum).GetChild(3).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().SpecialCooldown5;
    }
    #endregion

    #region Input and Activation
    void FixedUpdate()
    {
        if (Input.GetAxis(SpecialButton) > 0 && !Activated && !CooldownPeriod)
        {
            StartCoroutine("AbilityName");
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
    IEnumerator AbilityName()
    {
        var sound2 = Instantiate(AudioPlayer);
        sound2.GetComponent<SoundPlayer>().Awaken(ActivationSound, 1f);
        Activated = true;
        Can.transform.GetChild(childNum).GetChild(3).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().SpecialCooldown0;
        yield return new WaitForSeconds(1f);
    }

    IEnumerator SpecialCooldown()
    {
        yield return new WaitForSeconds(Cooldown / 5);
        Can.transform.GetChild(childNum).GetChild(3).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().SpecialCooldown1;
        yield return new WaitForSeconds(Cooldown / 5);
        Can.transform.GetChild(childNum).GetChild(3).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().SpecialCooldown2;
        yield return new WaitForSeconds(Cooldown / 5);
        Can.transform.GetChild(childNum).GetChild(3).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().SpecialCooldown3;
        yield return new WaitForSeconds(Cooldown / 5);
        Can.transform.GetChild(childNum).GetChild(3).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().SpecialCooldown4;
        yield return new WaitForSeconds(Cooldown / 5);
        Can.transform.GetChild(childNum).GetChild(3).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().SpecialCooldown5;
        var sound = Instantiate(AudioPlayer);
        sound.GetComponent<SoundPlayer>().Awaken(CooldownSound, 1f);
        CooldownPeriod = false;
    }

    #endregion
}
