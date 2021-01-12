using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _shotManagerTemplate = null;

    private ProjectileManager _Gun = null;
    private void Awake()
    {
        if (_shotManagerTemplate != null)
        {
            var gunObject = Instantiate(_shotManagerTemplate, transform, true);
            gunObject.transform.localPosition = Vector3.zero;
            gunObject.transform.localRotation = Quaternion.identity;
            _Gun = gunObject.GetComponent<ProjectileManager>();
        }
    }

    public void PrimaryFire()
    {
        if (_Gun != null)
            _Gun.Fire();
    }

}
