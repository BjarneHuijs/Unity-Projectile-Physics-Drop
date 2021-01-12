using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject _bulletTemplate = null;
    [SerializeField] private float _fireRate = 5.0f;
    [SerializeField] private List<Transform> _fireSockets = new List<Transform>();
    private bool _triggerPulled;
    private float _fireTimer = 0.0f;

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

}
