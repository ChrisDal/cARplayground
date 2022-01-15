using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public GameObject Child; // Reticle 

    // Start is called before the first frame update
    private void Start()
    {
        // get Reticle Model 
        Child = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (! GameVariables.isRunning)
        {
            Child.SetActive(false);
            return; 
        }
        
        // Conduct a ray cast from viewport (center) to plane 
        Ray rayscreenCenter = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(rayscreenCenter.origin, rayscreenCenter.direction, Color.blue); 

        // Test Hit 
        RaycastHit hit; 
        if (Physics.Raycast(rayscreenCenter, out hit)){
            
            if (hit.transform.gameObject.tag.Equals("HorizontalSurface"))
            {
                transform.position = hit.point; 
            }

            Child.SetActive(true);
        }
        
    }
}
