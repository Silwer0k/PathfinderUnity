using UnityEngine;
using System.Collections;

public class SpriteSorter : MonoBehaviour {
    // Скрипт автоматически сортирует спрайты в зависимости от Y-позиции.
    // Сортировка требует, чтобы объект находился на сортировочном лэере отдельно от фона, иначе некоторые объекты (может быть) могут отображаться за пейзажем.
    public float sortOrderOffset; // Значение, с помощью которого можно смещать автоматический порядок в лэере. Положительное число означает, что объект визуализируется раньше и наоборот.
    private float zSort; // Это значение будет использоваться в Start для установки Z-позиции 2D объекта (для сортировки спрайтов.)
    private Vector3 newPos = new Vector3(0.0f, 0.0f, 0.0f); // Новый вектор положения, который мы передаем фактическому положению объекта.

    void Start() {
		zSort = (transform.position.y / 10) - sortOrderOffset;
		newPos = new Vector3(transform.position.x, transform.position.y, zSort);
		transform.position = newPos;
		Destroy(this);
	}
}
