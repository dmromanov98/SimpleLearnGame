using UnityEngine;

public class CollisionListener : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Obstacles"))
        {
            Destroy(collision.gameObject);
        }
    }
}
