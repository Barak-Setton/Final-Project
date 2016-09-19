using UnityEngine;
using System.Collections;

public class groundCarScript : MonoBehaviour {

    private float m_OldRotation;
    [Range(0, 1)]
    [SerializeField]
    private float m_SteerHelper; // 0 is raw physics , 1 the car will grip in the direction it is facing
    public float m_groundDownforce = 5f;
    public float brakePower = 0.01f;


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

    }

	// Update is called once per frame
	void Update () {
        UpdateModeMeshesPosition();
    }

    public void Move(float h, float v, float brake)
    {
        // steering of the front wheels
        wheelColliders[2].steerAngle = h * 45f;
        wheelColliders[3].steerAngle = h * 45f;
        
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


}
