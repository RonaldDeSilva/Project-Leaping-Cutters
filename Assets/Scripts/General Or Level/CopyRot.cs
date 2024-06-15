using UnityEngine;

public class CopyRot : MonoBehaviour
{
    public Transform thing;
    public Transform thing2;
    public bool Off;
    public bool CopyPos;

    void Update()
    {
        if (!Off)
        {
            transform.rotation = thing.transform.rotation;
            //transform.rotation = new Quaternion(0, 0, thing2.transform.rotation.z, 0);
            if (CopyPos)
            {
                transform.position = thing.transform.position;
            }
        }
    }

}
