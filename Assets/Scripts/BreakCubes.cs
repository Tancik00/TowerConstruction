using UnityEngine;

public class BreakCubes : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject explosion;
    private bool _isCollisionSet;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cube") && !_isCollisionSet)
        {
            for (int i = other.transform.childCount-1; i >= 0; i--)
            {
                Transform child = other.transform.GetChild(i);
                child.gameObject.AddComponent<Rigidbody>();
                child.GetComponent<Rigidbody>().AddExplosionForce(70f, Vector3.up, 5f);
                child.SetParent(null);
            }

            restartButton.SetActive(true);
            Camera.main.transform.localPosition -= new Vector3(0f, 0f, 3f);
            Camera.main.gameObject.AddComponent<CameraShake>();
            
            if (PlayerPrefs.GetInt("musicOn")==1)
                GetComponent<AudioSource>().Play();

            GameObject explosionEffect = Instantiate(explosion,
                new Vector3(other.contacts[0].point.x, other.contacts[0].point.y, other.contacts[0].point.z),
                Quaternion.identity);
            Destroy(explosionEffect, 2f);

            Destroy(other.gameObject);
            _isCollisionSet = true;
        }
    }
}
