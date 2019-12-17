using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        //Claculate the velocity as a 3d vector
        float _xMov = Input.GetAxisRaw("Horizontal");       //Between -1 and 1
        float _zMov = Input.GetAxisRaw("Vertical");         //Between -1 and 1

        //Vector math
        Vector3 _movHorizontal = transform.right * _xMov;   //If moving forward (1,0,0) if not moving (0,0,0) if moving backward (-1,0,0)
        Vector3 _movVertical = transform.forward * _zMov;   //If moving forward (0,0,1) if not moving (0,0,0) if moving backward (0,0,-1)

        //Final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed; //Calculating the velocity

        //Apply movement
        motor.Move(_velocity);

    }
}
