using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    private float _lifeTime = 10.0f;

    private void Awake()
    {
        Invoke("Kill", _lifeTime);
    }

    void FixedUpdate()
    {
        if (!WallDetection())
            transform.position += transform.forward * Time.deltaTime * _speed;
    }

    static string[] RAYCAST_MASK = new string[] { "StaticLevel", "DynamicLevel" };
    bool WallDetection()
    {
        Ray collisionRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(collisionRay, out RaycastHit hit, 
            Time.deltaTime * _speed, LayerMask.GetMask(RAYCAST_MASK)))
        {
            Kill();
            return true;
        }
        return false;
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision hit)
    {
        
        if (hit.gameObject.tag != "Map" || hit.gameObject.tag == tag)
            return;

        Kill();
    }
}
