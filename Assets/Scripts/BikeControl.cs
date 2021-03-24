using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeControl : MonoBehaviour
{
    [SerializeField] private GameObject _rearWheelGO;
    [SerializeField] private GameObject _frontWheelGO;
    private Rigidbody _frontWheelRB;
    private Rigidbody _rearWheelRB;
    private Rigidbody _bodyRB;
    
    private const float _radius = 1.1f;
    private LayerMask _graundMask;

    private const float _reversalDirection = 1;
    private bool _isReversed;

    private float _origFrontWheelAngularDrag;
    private float _origRearWheelAngularDrag;

    private float _jumpForse;
    private const float _jumpForseSkale = 20000;
    private const float _maxJumpForse = 25000f;
    private const float _speedTorque = 850f;
    private const float _speed = 200f;
    private const float _maxSpeed = 50f;
    private const float _minSpeed = -15f;

    void Start()
    {
        _graundMask = LayerMask.GetMask("Graund");

        _bodyRB = GetComponent<Rigidbody>();
        _rearWheelRB = _rearWheelGO.GetComponent<Rigidbody>();
        _frontWheelRB = _frontWheelGO.GetComponent<Rigidbody>();

        _origFrontWheelAngularDrag = _frontWheelRB.angularDrag;
        _origRearWheelAngularDrag = _rearWheelRB.angularDrag;

        Reversal.OnReversal.AddListener(ReversalDirectionChenged);
    }

    private void ReversalDirectionChenged() => _isReversed = !_isReversed;

    private void Update()
    {
        if (Input.GetButtonUp("Jump"))
            _bodyRB.AddForce(transform.forward * _jumpForse);

        if (GraundCheck(_rearWheelGO.transform.position) & GraundCheck(_frontWheelGO.transform.position))
            _jumpForse = IncreseJumpForse(_jumpForse, _maxJumpForse, Input.GetAxis("Jump"));
    }

    private void FixedUpdate()
    {
        MotionControl(GraundCheck(_rearWheelGO.transform.position), Input.GetAxis("Vertical"), _reversalDirection, _isReversed);
        Brake(GraundCheck(_rearWheelGO.transform.position), GraundCheck(_frontWheelGO.transform.position), Input.GetAxis("Brake"));

        if (_bodyRB.angularVelocity.y != 0)
            _bodyRB.angularVelocity = Vector2.Lerp(_bodyRB.angularVelocity, new Vector2(_bodyRB.angularVelocity.x, 0), 1f);

        //Body rotation
        _bodyRB.AddTorque(new Vector3(0, 0, _speedTorque * Input.GetAxis("Horizontal")));
    }

    private void MotionControl(bool rearWheelIsGraunded, float GasInput, float reversalDirection, bool isReversed) 
    {
        float bikeSpeed = ReverseSingValue(_bodyRB.velocity.x, isReversed);
        reversalDirection = ReverseSingValue(reversalDirection, isReversed);

        if (rearWheelIsGraunded)
        {
            if (bikeSpeed < _maxSpeed && GasInput > 0)
                _rearWheelRB.velocity += -transform.right * _speed * Time.deltaTime;

            if (bikeSpeed > _minSpeed && GasInput < 0)
                _rearWheelRB.velocity += transform.right * _speed / 3 * Time.deltaTime;
        }

        _rearWheelRB.AddTorque(new Vector3(0, 0, -_speedTorque * GasInput * reversalDirection));
    }

    private void Brake(bool rearWheelIsGraunded, bool frontWheelIsGraunded, float BrakeInput)
    {
        _frontWheelRB.angularDrag = EnlargeAngularDrag(_origFrontWheelAngularDrag, BrakeInput);
        _rearWheelRB.angularDrag = EnlargeAngularDrag(_origRearWheelAngularDrag, BrakeInput);

        if (BrakeInput != 0)
            if (rearWheelIsGraunded ^ frontWheelIsGraunded)
                _rearWheelRB.velocity = WheelBrake(.2f);
                else if (rearWheelIsGraunded & frontWheelIsGraunded)
                _rearWheelRB.velocity = WheelBrake(.4f);
    }

    private float IncreseJumpForse(float jumpForse, float maxJumpForse, float jumpIntup)
    {
        if (jumpForse < maxJumpForse)
            jumpForse += _jumpForseSkale * Time.deltaTime;

        //_bodyRB.AddForce(-Vector3.up * jumpForse / 350);

        return jumpForse * jumpIntup;
    }

    private bool GraundCheck(Vector3 center)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, _radius, _graundMask);
        if (hitColliders.Length > 0)
            return true;
        else
            return false;
    }

    private float ReverseSingValue(float value, bool isReversed) 
    {
        if(isReversed)
            return value *= -1;

        return value;
    }

    private float EnlargeAngularDrag(float origAngularDrag, float BrakeInput) 
    {
        return origAngularDrag + 8 * BrakeInput;
    }

    private Vector2 WheelBrake(float step) 
    {
        return Vector2.Lerp(_rearWheelRB.velocity, new Vector2(0, _rearWheelRB.velocity.y), step);
    }
}
