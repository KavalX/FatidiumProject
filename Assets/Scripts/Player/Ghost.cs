using UnityEngine;

public class Ghost : PlayerBase
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float cooldown;
    private float _actualCooldown;
    [SerializeField] private AudioSource _shootSound;
    
    private new void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_actualCooldown <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                _actualCooldown = cooldown;
                _shootSound.Play();
            }
        }
        else if (_actualCooldown > 0)
        {
            _actualCooldown -= Time.deltaTime;
        }
    }
}
