using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(CircleCollider2D))]
public class Food : MonoBehaviour
{
    [SerializeField] private GameObject WinPrefab;
    Light2D survivor_light;
    public bool isAdded;
   [SerializeField]private  GameObject lightObject;
    [SerializeField]GameObject smokeParticlePrefab;
    public bool Iskillable;
    private void Start()
    {
       
        survivor_light = lightObject.GetComponent<Light2D>();
        survivor_light.lightType = Light2D.LightType.Freeform;
        survivor_light.intensity = 0.03f;
        survivor_light.falloffIntensity = 1f;
        isAdded = false;
        Iskillable = true;
    }


    public void LightChange()
    {
        survivor_light.lightType = Light2D.LightType.Point;
        survivor_light.intensity = 0.18f;
        survivor_light.falloffIntensity =0.51f;

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("kaboom"); 
          
        }
    }
    public void WIN()
    {
        StartCoroutine(WIneffect());
    }
    private IEnumerator WIneffect()
    {
        for (float i = 1f; i <= 1.2f; i += 0.05f)
        {
            transform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        for (float i = 1.2f; i >= 1f; i -= 0.05f)
        {
            transform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(1f, 1f, 1f);
        Instantiate(WinPrefab, transform.position, Quaternion.identity);
    }

}