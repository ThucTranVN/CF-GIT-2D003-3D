using UnityEngine;
using Unity.AI.Navigation;

public class AINavmeshLinkEnableDemo : MonoBehaviour
{
    private RaycastHit hit;
    public NavMeshLink[] linksUpArr;
    public NavMeshLink[] linksDownArr;

    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                for(int i = 0; i < linksUpArr.Length; i++)
                {
                    linksUpArr[i].activated = false;
                    linksDownArr[i].activated = true;
                }
            }

            if (hit.collider.CompareTag("Obstacle"))
            {
                for (int i = 0; i < linksUpArr.Length; i++)
                {
                    linksUpArr[i].activated = true;
                    linksDownArr[i].activated = false;
                }
            }
        }
    }
}
