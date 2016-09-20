using UnityEngine;
using System.Collections;

public class groundCarScript : MonoBehaviour {

    private float m_OldRotation;
    [Range(0, 1)]
    [SerializeField]
    private float m_SteerHelper; // 0 is raw physics , 1 the car will grip in the direction it is facing
    [SerializeField]
    private float m_SlipLimit =0;
    public float m_groundDownforce = 5f;
    public float brakePower = 0.01f;
    private float RPM;
   
    public AudioClip skid;
    public AudioClip motor;

    private AudioSource audioSkid;
    private AudioSource audioMotor;


    public Transform centerOfMass;
    private Rigidbody m_rigidBody;

    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public Transform[] tireMeshes = new Transform[4];

    public float maxTorque = 50f;
    
   
    void UpdateModeMeshesPosition()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);
            tireMeshes[i].position = pos;
            tireMeshes[i].rotation = quat;
        }
    }
	

    void Start()
    {

        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.centerOfMass = centerOfMass.localPosition;
        audioSkid = AddAudio(skid, true, true, 0.05F);
        audioMotor = AddAudio(motor, true, true, 0.05F);
        audioMotor.Play();
    }

	// Update is called once per frame
	void Update () {
        UpdateModeMeshesPosition();
        // check for car movment
    }

    public void Move(float h, float v, float brake)
    {
        // steering of the front wheels
        wheelColliders[2].steerAngle = h * 20f;
        wheelColliders[3].steerAngle = h * 20f;
        
        // steer helper
        SteerHelper();
        AddDownForce(m_groundDownforce);


        if (brake == 1)
        {
            m_rigidBody.velocity -= m_rigidBody.velocity * brakePower;
        }

        // acceleration
        float accelerate = v;
        for (int i = 0; i < 4; i++)
        {
            wheelColliders[i].motorTorque = accelerate * maxTorque;
        }

        MotorAudio();
        CheckForWheelSpin();
    }

    // this is used to add more grip in relation to speed
    private void AddDownForce(float downForce)
    {
        GetComponent<Rigidbody>().AddForce(-transform.up * downForce * GetComponent<Rigidbody>().velocity.magnitude);
    }

    // sterring helper
    private void SteerHelper()
    {
        for (int i = 0; i < 4; i++)
        {
            WheelHit wheelhit;
            wheelColliders[i].GetGroundHit(out wheelhit);
            if (wheelhit.normal == Vector3.zero)
                return; // wheels arent on the ground so dont realign the rigidbody velocity
        }
        

        // this if is needed to avoid gimbal lock problems that will make the car suddenly shift direction
        if (Mathf.Abs(m_OldRotation - transform.eulerAngles.y) < 10f)
        {
            var turnadjust = (transform.eulerAngles.y - m_OldRotation) * m_SteerHelper;
            Quaternion velRotation = Quaternion.AngleAxis(turnadjust, Vector3.up);
            m_rigidBody.velocity = velRotation * m_rigidBody.velocity;
        }
        m_OldRotation = transform.eulerAngles.y;
    }

    private AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }

    private void CheckForWheelSpin()
    {
        int wheelskidcounter = 0;
        // loop through all wheels
        for (int i = 0; i < 4; i++)
        {
            WheelHit wheelHit;
            wheelColliders[i].GetGroundHit(out wheelHit);

            // is the tire slipping above the given threshhold
            if ( Mathf.Abs(wheelHit.sidewaysSlip) >= 0.5)
            {
                wheelskidcounter++;
                continue;
            }
        }

        if (wheelskidcounter >0)
        {
            // if sound not playing then play the skid sound
            if (!audioSkid.isPlaying)
            {
                audioSkid.Play();
            }
        }
        else
        {
            // if sound if playing then stop it
            if (audioSkid.isPlaying)
            {
                audioSkid.Stop();
            }
        }
    }

    private void MotorAudio()
    {
        // motor audio relative to cars rmp of one of the wheels
        RPM = wheelColliders[0].rpm;
        audioMotor.pitch = RPM * 0.0008f + 0.7f;
    }

    private bool IsCarMoving()
    {
        return (m_rigidBody.velocity.magnitude > 0);
    }

}
