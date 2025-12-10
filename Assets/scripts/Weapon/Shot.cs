using UnityEngine;

public class Shot : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject bullet;
    public float shotForce = 1500f;
    public float shotRate = 0.5f;

    private float shotRateTime = 0; 


    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        {


            //if (Time.time > shotRateTime)
            //{
            //    GameObject newBullet;

            //    newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

            //    newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

            //    shotRateTime = Time.time + shotRate;

            //    Destroy(newBullet,5 );
            //}

            // Usamos Time.unscaledTime para que funcione durante ZaWardo
            if (Time.unscaledTime > shotRateTime)
            {
                GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

                // Para la bala que siga moviéndose incluso con Time.timeScale = 0
                Bullet bulletScript = newBullet.GetComponent<Bullet>();
                if (bulletScript != null)
                    bulletScript.speed = shotForce * 0.01f; // ajusta según tu escala

                shotRateTime = Time.unscaledTime + shotRate;
                Destroy(newBullet, 5f);
            }
        }



    }
}
