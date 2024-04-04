using UnityEngine;

public class CopyRot : MonoBehaviour
{
    public Transform thing;
    public bool Off;
    public bool CopyPos;

    void Update()
    {
        if (!Off)
        {
            //transform.rotation = thing.transform.rotation;
            if (CopyPos)
            {
                transform.position = thing.transform.position;
            }
        }
    }

}
