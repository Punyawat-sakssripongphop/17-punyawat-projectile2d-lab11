using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject target;
    [SerializeField] Rigidbody2D bulletPrefab;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // shoot raycast to detect mouse clicked position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.green, 5f);
            //get click point
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            //if hit object with collider
            if (hit.collider != null)
            {
                //show target at click point
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log("hit" + hit.collider.name);
                //calculate projecttile velocity
                Vector2 projectileVelocity = calculateProjectileVelocity(shootPoint.position, hit.point, 1f);
                //shoot bullet prefab using  rigidbody2d
                Rigidbody2D shootBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                //add projecttlie velocity vector to the bullet rigidbody
                shootBullet.linearVelocity = projectileVelocity;
            }
        }
    }

    Vector2 calculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;
        //find velocity of x and y axis
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y);
        //get projectile vector
        Vector2 projectileVelocity = new Vector2(velocityX, velocityY);
        return projectileVelocity;
    }
}
