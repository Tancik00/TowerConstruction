using UnityEngine;

public class BreakCubes : MonoBehaviour
{
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

            Destroy(other.gameObject);
            _isCollisionSet = true;
        }
    }
}
