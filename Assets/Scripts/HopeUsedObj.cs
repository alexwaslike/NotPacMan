using UnityEngine;
using System.Collections;

public class HopeUsedObj : MonoBehaviour {

    private float lifetime = 5;

	void Update () {
        if (lifetime <= 0)
            Destroy(gameObject);
        lifetime -= 1 * Time.deltaTime;
	}
}
