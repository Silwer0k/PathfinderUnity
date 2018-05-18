using UnityEngine;
using System.Collections;

public class DemoStairs : MonoBehaviour {
	public GameObject teleportPoint; // Точка, в которую мы телепортируемся (GameObject).

    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") // Если объект с тегом "Player" входит в триггер, перемещаем его в точку телепорта.
        {
			other.transform.position = teleportPoint.transform.position;
		}
	}
}
