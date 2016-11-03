using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

internal enum SpeedType
{
	MPH,
	KPH
}


public class ThrusterController : MonoBehaviour {


    private AudioSource audioMotor;
    public AudioClip motor;

    private float rotationX = 0f;

    public float drag;
    public float brakePower = 0.01f;

    public float acceleration;
    public float thrust;
    public float spring;
    public float downForce;
    public float rotationRate;
    public Transform centerOfMass;

    public float turnRotationAngle;
    public float turnRotationSeekSpeed;

    private float rotationVelocity;
    private float groundAngleVelocity;

    private Rigidbody carRigidbody;

	[SerializeField]private SpeedType m_SpeedType;
	[SerializeField]private float m_Topspeed = 200;

	public float MaxSpeed{get { return m_Topspeed; }}
	public float CurrentSpeed{ get { return carRigidbody.velocity.magnitude*2.23693629f; }}

	// Use this for initialization
	void Start () {
        carRigidbody = GetComponent<Rigidbody>();
        carRigidbody.centerOfMass = centerOfMass.localPosition;
        audioMotor = AddAudio(motor, true, true, 0.5F);
        audioMotor.Play();
    }
	
	// Update is called once per frame
	public void Move (float steering, float accel, float breaks, float boost, float jump) {

        // apply boost (1 or 0)
        carRigidbody.AddForce(transform.forward * thrust*boost);

        // apply the jump
        carRigidbody.AddForce(transform.up * spring * jump);

        // check if we are touching the ground:
        if (Physics.Raycast (transform.position, transform.up*-1, 3f))
        {
            // we are on the ground; enable the accelerator and increase drag:
            carRigidbody.drag = drag;

            // clamp if foward vecter to height
           // rotationX = Mathf.Clamp(rotationX, -45, 45);
            //transform.localEulerAngles = new Vector3(-rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z );
            
            if (breaks == 1)
            {
                carRigidbody.velocity -= carRigidbody.velocity * brakePower;
            }

            // calculate forward force:
            Vector3 forwardForce = transform.forward * acceleration * accel;

  
            // correct force for deltatime and vehicle mass:
            forwardForce = forwardForce * Time.deltaTime * carRigidbody.mass;
            carRigidbody.AddForce(forwardForce);
        }
        else
        {
            // we aren't on the ground and dont want to just halt in mid-air: reduce drag:
            carRigidbody.drag = 0f;
            carRigidbody.AddForce(transform.up*(-downForce)); // so it does float for ever
        }

        


        // You can turn in the air or no the ground:
		Vector3 turnTorque = Vector3.up * rotationRate * steering ;

        // Correct force for deltatime and vehicle mass:
        turnTorque = turnTorque * Time.deltaTime * carRigidbody.mass;
        carRigidbody.AddTorque(turnTorque);

        // "FAke" rotate the car when you are turning:
        Vector3 newRotation = transform.eulerAngles;
		newRotation.z = Mathf.SmoothDampAngle(newRotation.z, steering * -turnRotationAngle, ref rotationVelocity, turnRotationSeekSpeed);
        transform.eulerAngles = newRotation;

        MotorAudio();

        CapSpeed ();
	}

	private void CapSpeed()
	{
		float speed = carRigidbody.velocity.magnitude;
		switch (m_SpeedType)
		{
		case SpeedType.MPH:

			speed *= 2.23693629f;
			if (speed > m_Topspeed)
				carRigidbody.velocity = (m_Topspeed/2.23693629f) * carRigidbody.velocity.normalized;
			break;

		case SpeedType.KPH:
			speed *= 3.6f;
			if (speed > m_Topspeed)
				carRigidbody.velocity = (m_Topspeed/3.6f) * carRigidbody.velocity.normalized;
			break;
		}
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

    private void MotorAudio()
    {
        // motor audio relative to cars mag of its velocity of one of the wheels
        
        audioMotor.pitch = carRigidbody.velocity.magnitude * 0.008f + 0.7f;
    }




}
