using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float  ghostDelay;
    private float ghostDelaySeconds;
    public GameObject ghost;
    public bool makeGhost = false;

    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    private void FixedUpdate()
    {
        Ghosting();
    }
   
    private void Ghosting()
    {
        if (makeGhost)
        {
            Debug.Log("ghosting");
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(ghost,transform.position,transform.rotation);
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 1f);
            }
        }
    }
}
