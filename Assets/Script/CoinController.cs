using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 


public class CoinController : MonoBehaviour
{
    const float k_CollectionDuration = 0.5f;
    const float k_RotationSpeed = 0.5f;

    float m_CurrentRotation = 0.0f;

    bool m_Collected = false;
    float m_CollectionTimer = 0.0f;
    Vector3 m_StartScale = Vector3.one;
    Vector3 m_CollectedScale = Vector3.one;
    public Ray m_ray; 
    float speed = 5.0f; 
    bool setOnFloor; 
    public bool good; 
    public int scoreValue; 

    AudioSource audioData;
    TextMeshPro valueText; 
    public ParticleSystem explosionParticle; 
    public float particulesScaleFactor = 0.1f; 
    private Transform player; 

    private void Start()
    {
        m_ray = new Ray(transform.position, Vector3.down); 
        setOnFloor = false; 
        audioData = GetComponent<AudioSource>();
        

        valueText = this.transform.GetChild(0).GetComponent<TextMeshPro>(); 
        valueText.SetText(""); 

        player = GameObject.FindGameObjectWithTag("Player").transform; 

    }

    void Update()
    {
        if ( ! GameVariables.isRunning)
        {
            return; 
        }
        
        bool underPlayer = transform.position.y < player.position.y; 

        if (transform.position.y < -1.0f){
            setOnFloor = true; 
            speed = 0.0f; 
            if ( ! audioData.isPlaying ){
                Destroy(gameObject); 
                GameVariables.ncoins -= 1; 
            }
            
            return; 
        }

        // Sound is over
        if ( m_Collected && (! audioData.isPlaying) ){
            Destroy(gameObject); 
            GameVariables.ncoins -= 1; 
            return; 
        }
        
        // Ray Cast
        Debug.DrawRay(transform.position, 2.0f*Vector3.down, Color.red); 
        m_ray = new Ray(transform.position, Vector3.down); 
        RaycastHit hit; 
        if (Physics.Raycast(m_ray, out hit)){
            
            if ((hit.distance < 0.15f && hit.transform.gameObject.tag.Equals("HorizontalSurface")) || underPlayer) { 
                speed = 0.0f;
                setOnFloor = true; 
            }
        }
        // falling 
        if (! setOnFloor)
        {
            transform.position += speed * Vector3.down * Time.deltaTime;    
        }
        

        // If not collected, just rotate slowly
        if (!m_Collected)
        {
            m_CurrentRotation += Time.deltaTime * k_RotationSpeed * 180.0f;
            transform.localRotation = Quaternion.Euler(0.0f, m_CurrentRotation, 0.0f);
            
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (m_Collected)
            return;

        if (other.CompareTag("Player"))
        {
            // Collect!
            m_Collected = true;
            valueText.SetText(scoreValue.ToString());
            valueText.color = good ? Color.green : Color.red; 
            audioData.Play(0);
            // Particle System Instatiae and rescale it
            ParticleSystem ps = Instantiate(explosionParticle, transform.position, transform.rotation); 
            ParticleSystem.MainModule mainps = ps.main; 
            mainps.scalingMode = ParticleSystemScalingMode.Local; 
            ps.transform.localScale = Vector3.one * particulesScaleFactor; 
        }
    }

    public void setGoodorBad(bool goodCoin)
    {
        good = goodCoin; 
    }

}
