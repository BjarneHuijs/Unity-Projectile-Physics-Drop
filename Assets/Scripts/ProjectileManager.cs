using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject _bulletTemplate = null;
    [SerializeField] private float _fireRate = 5.0f;
    [SerializeField] private List<Transform> _fireSockets = new List<Transform>();
    [SerializeField] private float _speed = 30.0f;
    private bool _triggerPulled;
    private float _fireTimer = 0.0f;
    private Vector3 _gravity = new Vector3(0.0f, -9.81f, 0.0f);

    private Vector3 _impactPoint = new Vector3();
    private bool _impacted = false;

    private void Awake()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (_fireTimer > 0.0f)
        {
            _fireTimer -= Time.deltaTime;
        }

        if (_fireTimer <= 0.0f && _triggerPulled)
            FireProjectile();

        _triggerPulled = false;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < _fireSockets.Count; i++)
        {
            UpdateTrajectory(_fireSockets[i].position, _fireSockets[i].forward * _speed, _gravity);
        }
    }

    private void FireProjectile()
    {
        if (_bulletTemplate == null)
            return;

        for (int i = 0; i < _fireSockets.Count; i++)
        {
            Instantiate(_bulletTemplate, _fireSockets[i].position, _fireSockets[i].rotation);
        }

        _fireTimer += 1.0f / _fireRate;

    }

    public void Fire()
    {
        _triggerPulled = true;
    }


    void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
    {
        int numSteps = 50; // for example
        float timeDelta = 1.0f / initialVelocity.magnitude; // for example

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numSteps;

        Vector3 position = initialPosition;
        Vector3 velocity = initialVelocity;

        for (int i = 0; i < numSteps; ++i)
        {
            if (WallDetection(position, velocity))
            {
                //print(transform.Find("Target").gameObject.name);
                //print(transform.Find("Target").position);
                transform.Find("Target").transform.position = _impactPoint;
                print("found impact point at " + _impactPoint.x + ", " + _impactPoint.y + _impactPoint.z + ", ");
                lineRenderer.positionCount = i;
                break;
            }
            else
            {
                // Set point to void if no hit found
                transform.Find("Target").position = new Vector3(-999f, -999f, -999f);
            }

            lineRenderer.SetPosition(i, position);

            // Calculate next position using the the current velocity, position, and time since last calculation
            position += velocity * timeDelta + 0.5f * gravity * timeDelta * timeDelta; 
            //Gives the trail it's downward arc influenced by gravity, higher speed == longer and flatter arc
            velocity += gravity * timeDelta; 

        }
    }

    static string[] RAYCAST_MASK = new string[] { "StaticLevel", "DynamicLevel" };
    bool WallDetection(Vector3 newPosition, Vector3 newForward)
    {
        Ray collisionRay = new Ray(newPosition, newForward);
        if (Physics.Raycast(collisionRay, out RaycastHit hit,
            Time.deltaTime * _speed, LayerMask.GetMask(RAYCAST_MASK)))
        {
            _impactPoint = hit.point;
            return true;
        }
        return false;
    }

    public Vector3 GetImpactPoint() 
    {
        return _impactPoint;
    }
}
