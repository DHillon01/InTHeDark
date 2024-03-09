    using System.Collections;
    using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Gun : MonoBehaviour
{

   [SerializeField] private float _speed;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private GameObject _bulletTrail;
    [SerializeField] private float  _weaponRange ;
    [SerializeField] private Animator _muzzleAnimator; 
    // Reference to the game object containing the muzzle shot animation
    public GameObject muzzelShot;

    void Update()
    {
        Shoot();
    
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0)&&ScoreManager.Instance.isPLaying ) // Check for left mouse click and fire rate
        {
            _muzzleAnimator.Play("GunShot");
            ScoreManager.Instance.PlayGunShot();
                var hit = Physics2D.Raycast(_gunPoint.position,transform.up,_weaponRange);
            var trail = Instantiate(_bulletTrail,_gunPoint.position,transform.rotation);
            var trailScript = trail.GetComponent<BulletTrail>();

            if(hit.collider != null)
            {
                trailScript.SetTargetPosition(hit.point); 
               
            }
            else
            {
                var endPosition = _gunPoint.position + transform.up *_weaponRange;

                trailScript.SetTargetPosition(endPosition);
            }
        }
        else
        {
            _muzzleAnimator.StopPlayback();
        }
    }
}
