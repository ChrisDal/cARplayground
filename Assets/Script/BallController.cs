using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
 

public class BallController : MonoBehaviour
{
    public float m_speed = 2.0f; 
    public ParticleSystem fumee; 
    public float particulesScaleFactor = 0.05f; 
    public Vector3 direction; 
    public float distanceMax = 2.0f; // in meters 
    private Rigidbody targetRb;  
    public bool dirSet = false; 
    
    bool accumulate = true;
    float metersMade = 0.0f;  

    public int scoreValue = -2; 
    TextMeshPro valueText; 
    AudioSource audioData; 

    bool toDestroy = false; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>(); 
        valueText = this.transform.GetChild(0).GetComponent<TextMeshPro>(); 
        valueText.SetText("");
        valueText.color = Color.red; 
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (! GameVariables.isRunning ) { return; }
        
        if (toDestroy  && ! audioData.isPlaying)
        {
            Destroy(gameObject); 
        }
        
        // si on est trop loin 
        Transform gmo  = GameObject.FindGameObjectWithTag("Player").transform; 

        if (dirSet)
        {
            //transform.position += direction * m_speed * Time.deltaTime; 
            if (accumulate) 
            {
                Vector2 gmopos = new Vector2(gmo.transform.position.x, gmo.transform.position.z); 
                Vector2 ballpos = new Vector2(transform.position.x, transform.position.z); 
                float dist = Vector2.Distance(gmopos, ballpos); 
                if (dist < 0.5f) {
                    targetRb.useGravity = true; 
                    accumulate = false; 
                }
                
                metersMade += m_speed * Time.deltaTime; 
            }

            
            targetRb.MovePosition(transform.position + direction * m_speed * Time.deltaTime); 
        }

        
        if (Vector3.Distance(gmo.position, transform.position) > distanceMax)  {
            Destroy(gameObject); 
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (! GameVariables.isRunning ) { return; }
        
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit Player !"); 
            valueText.SetText(scoreValue.ToString());
            audioData.Play(0); 
            InitParticules(particulesScaleFactor); 
            toDestroy = true; 
            
        }
        else if (other.CompareTag("HorizontalSurface"))
        {
            InitParticules(particulesScaleFactor); 
            toDestroy = true; 
        }
        else if (metersMade > 0.2f && other.CompareTag("VerticalSurface"))
        {
            InitParticules(particulesScaleFactor); 
            toDestroy = true; 
        }

        
    }

    public void InitParticules(float scale)
    {
        // Particle System Instatiate and rescale it
        ParticleSystem ps = Instantiate(fumee, transform.position, transform.rotation); 
        ParticleSystem.MainModule mainps = ps.main; 
        mainps.scalingMode = ParticleSystemScalingMode.Local; 
        ps.transform.localScale = Vector3.one * particulesScaleFactor; 
    }

    public void setDirection(Vector3 dir)
    {
        direction = dir; 
        dirSet = true; 
    }
}
