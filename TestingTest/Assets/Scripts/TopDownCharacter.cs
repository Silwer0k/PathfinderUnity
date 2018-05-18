using UnityEngine;
using System.Collections;

public class TopDownCharacter : MonoBehaviour
{
    // Скрипт для перспективы сверху вниз, который включает в себя направленное / диагональное движение и автоматическую сортировку спрайтов.
    // Перс должен находиться на том же лэере, что и объекты.
    // Изменение очереди сортировки следует использовать только в том случае, если объекты декораций отображаются поверх символа неправильно.
    // Для этого скрипта требуется компонент rigidbody и collider, так как он использует физическую силу для перемещения.См.префаб виспа для идеальных значений rigidbody.Гравитационная шкала должна быть установлена на 0!

    public float minMoveValue = 0.1f; // Минимальное значение, которое должно быть для перемещения персонажа.
    public float moveSpeed = 6.0f; // Значение скорости перемещения во всех направлениях. Значение позже умножается на pixelsPerUnit; пикселей на единицу.
    public int pixelsPerUnit = 32; // Количество пикселей на единицу в Unity.
    private float speed; // Конечное значение скорости; moveSpeed, умноженное на pixelsPerUnit.

    public float sortOrderOffset = 0.0f; // Значение для смещения автоматической очереди в лэеерах. Положительное число означает, что объект визуализируется раньше и наоборот.
    private float zSort; // Это значение будет использоваться в обновлении для установки Z-позиции 2D объекта (для сортировки спрайтов.)
    private Vector3 newPos = new Vector3(0.0f, 0.0f, 0.0f); // Новый вектор положения, который передается фактическому положению объекта.

    void FixedUpdate() {
		speed = moveSpeed * pixelsPerUnit; // Умножает скорость на единицу измерения. Но это не значит, что скорость = 1 блок в секунду, еще есть масса и сопротивлению rigidbody, влияющие на скорость.

        // Горизонтальное движение.
        if (Input.GetAxisRaw("Horizontal") > minMoveValue) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(speed,0) * Time.deltaTime);
		}
		else if (Input.GetAxisRaw("Horizontal") < -minMoveValue) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed,0) * Time.deltaTime);
		}

        // Вертикальное перемещение.
        if (Input.GetAxisRaw("Vertical") > minMoveValue) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0,speed) * Time.deltaTime);
		}
		else if (Input.GetAxisRaw("Vertical") < -minMoveValue) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-speed) * Time.deltaTime);
		}
	}
	
	void Update () {
		zSort = (transform.position.y / 10) - sortOrderOffset;
		newPos = new Vector3(transform.position.x, transform.position.y, zSort);
		transform.position = newPos;
	}
}
