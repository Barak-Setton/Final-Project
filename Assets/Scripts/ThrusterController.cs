using UnityEngine;
using System.Collections;

internal enum SpeedType
{
	MPH,
	KPH
}


public class ThrusterController : MonoBehaviour {

    public bool toHeigh;

    private float rotationX = 0f;

    public float drag;

    public float acceleration;
    public float rotationRate;

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
	}
	
	// Update is called once per frame
	public void Move (float steering, float accel) {

        // check if we are touching the ground:
        if (Physics.Raycast (transform.position, transform.up*-1, 3f))
        {
            toHeigh = false;

            // we are on the ground; enable the accelerator and increase drag:
            carRigidbody.drag = drag;

            // clamp if foward vecter to height
           // rotationX = Mathf.Clamp(rotationX, -45, 45);
            transform.localEulerAngles = new Vector3(-rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z );

            // calculate forward force:
			Vector3 forwardForce = transform.forward * acceleration * accel;
            
      
            // correct force for deltatime and vehicle mass:
            forwardForce = forwardForce * Time.deltaTime * carRigidbody.mass;
            carRigidbody.AddForce(forwardForce);
        }
        else
        {
            toHeigh = true;
            // we aren't on the ground and dont want to just halt in mid-air: reduce drag:
            carRigidbody.drag = 0f; // NEED TO FIGURE THIS OUT SO IT DOESNT DRIFT FOR REALLY LONG
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


		CapSpeed ();
	}




//	public void Turn (float steering){
//		// You can turn in the air or no the ground:
//		Vector3 turnTorque = Vector3.up * rotationRate * steering ;
//
//		// Correct force for deltatime and vehicle mass:
//		turnTorque = turnTorque * Time.deltaTime * carRigidbody.mass;
//		carRigidbody.AddTorque(turnTorque);
//
//		// "FAke" rotate the car when you are turning:
//		Vector3 newRotation = transform.eulerAngles;
//		newRotation.z = Mathf.SmoothDampAngle(newRotation.z, steering * -turnRotationAngle, ref rotationVelocity, turnRotationSeekSpeed);
//		transform.eulerAngles = newRotation;
//	}
//
//	public void Move(float accel, float footbrake, float handbrake){
//		rotationX = Mathf.Clamp(rotationX, -45, 45);
//		transform.localEulerAngles = new Vector3(-rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z );
//
//
//		// clamp if  foward vecter to height
//		/*
//            if (transform.localEulerAngles.x < 350 && transform.localEulerAngles.x > 10) {
//                transform.rotation = Quaternion.AngleAxis(10, Vector3.right);
//
//            }
//            */
//
//
//		// calculate forward force:
//		Vector3 forwardForce = transform.forward * acceleration * accel ;
//
//
//		// correct force for deltatime and vehicle mass:
//		forwardForce = forwardForce * Time.deltaTime * carRigidbody.mass;
//		carRigidbody.AddForce(forwardForce);
//	
//	}
//	public void Move(float steering, float accel, float footbrake, float handbrake)
//	{
//		for (int i = 0; i < 4; i++)
//		{
//			Quaternion quat;
//			Vector3 position;
//			m_WheelColliders[i].GetWorldPose(out position, out quat);
//			m_WheelMeshes[i].transform.position = position;
//			m_WheelMeshes[i].transform.rotation = quat;
//		}
//
//		//clamp input values
//		steering = Mathf.Clamp(steering, -1, 1);
//		AccelInput = accel = Mathf.Clamp(accel, 0, 1);
//		BrakeInput = footbrake = -1*Mathf.Clamp(footbrake, -1, 0);
//		handbrake = Mathf.Clamp(handbrake, 0, 1);
//
//		//Set the steer on the front wheels.
//		//Assuming that wheels 0 and 1 are the front wheels.
//		m_SteerAngle = steering*m_MaximumSteerAngle;
//		m_WheelColliders[0].steerAngle = m_SteerAngle;
//		m_WheelColliders[1].steerAngle = m_SteerAngle;
//
//		SteerHelper();
//		ApplyDrive(accel, footbrake);
//		CapSpeed();
//
//		//Set the handbrake.
//		//Assuming that wheels 2 and 3 are the rear wheels.
//		if (handbrake > 0f)
//		{
//			var hbTorque = handbrake*m_MaxHandbrakeTorque;
//			m_WheelColliders[2].brakeTorque = hbTorque;
//			m_WheelColliders[3].brakeTorque = hbTorque;
//		}
//
//
//		CalculateRevs();
//		GearChanging();
//
//		AddDownForce();
//		CheckForWheelSpin();
//		TractionControl();
//	}
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
}
