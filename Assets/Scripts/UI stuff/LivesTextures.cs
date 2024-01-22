using UnityEngine;
using UnityEngine.UI;

public class LivesTextures : MonoBehaviour
{
    public Sprite Lives3;
    public Sprite Lives2;
    public Sprite Lives1;
    public Sprite Lives0;

    public Sprite DashCooldown0;
    public Sprite DashCooldown1;
    public Sprite DashCooldown2;
    public Sprite DashCooldown3;
    public Sprite DashCooldown4;
    public Sprite DashCooldown5;

    public Sprite ReloadCooldown0;
    public Sprite ReloadCooldown1;
    public Sprite ReloadCooldown2;
    public Sprite ReloadCooldown3;
    public Sprite ReloadCooldown4;
    public Sprite ReloadCooldown5;

    public Sprite SpecialCooldown0;
    public Sprite SpecialCooldown1;
    public Sprite SpecialCooldown2;
    public Sprite SpecialCooldown3;
    public Sprite SpecialCooldown4;
    public Sprite SpecialCooldown5;

    private void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Lives3;
        transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Lives3;
        transform.GetChild(2).gameObject.GetComponent<Image>().sprite = Lives3;
        transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Lives3;


        transform.GetChild(0).GetChild(1).gameObject.GetComponent<Image>().sprite = DashCooldown5;
        transform.GetChild(1).GetChild(1).gameObject.GetComponent<Image>().sprite = DashCooldown5;
        transform.GetChild(2).GetChild(1).gameObject.GetComponent<Image>().sprite = DashCooldown5;
        transform.GetChild(3).GetChild(1).gameObject.GetComponent<Image>().sprite = DashCooldown5;

        transform.GetChild(0).GetChild(2).gameObject.GetComponent<Image>().sprite = ReloadCooldown5;
        transform.GetChild(1).GetChild(2).gameObject.GetComponent<Image>().sprite = ReloadCooldown5;
        transform.GetChild(2).GetChild(2).gameObject.GetComponent<Image>().sprite = ReloadCooldown5;
        transform.GetChild(3).GetChild(2).gameObject.GetComponent<Image>().sprite = ReloadCooldown5;

        transform.GetChild(0).GetChild(3).gameObject.GetComponent<Image>().sprite = SpecialCooldown5;
        transform.GetChild(1).GetChild(3).gameObject.GetComponent<Image>().sprite = SpecialCooldown5;
        transform.GetChild(2).GetChild(3).gameObject.GetComponent<Image>().sprite = SpecialCooldown5;
        transform.GetChild(3).GetChild(3).gameObject.GetComponent<Image>().sprite = SpecialCooldown5;
    }
}
