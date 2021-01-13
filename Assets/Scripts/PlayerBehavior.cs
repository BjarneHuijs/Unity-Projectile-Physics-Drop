using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    protected FreeCam _movementBehaviour;
    protected ShootingBehavior _shootingBehavior;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _movementBehaviour = GetComponent<FreeCam>();
        _shootingBehavior = GetComponent<ShootingBehavior>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        HandleFireInput();
        PredictImpactPoint();
    }

    void HandleFireInput()
    {
        if (_shootingBehavior == null) 
            return;

        if (Input.GetAxis("PrimaryFire") > 0.0f)
            _shootingBehavior.PrimaryFire();

    }

    void PredictImpactPoint() 
    {
        print("A bullet fired from this position will impact at position: " + _shootingBehavior.GetPredictedImpactPoint().x + ", " + _shootingBehavior.GetPredictedImpactPoint().y + ", " + _shootingBehavior.GetPredictedImpactPoint().z);
    }
}
