using UnityEngine;

namespace Mechanics
{
    public class BigCandyRotation : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 50f;

        void Update()
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }
}