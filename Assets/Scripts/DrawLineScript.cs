using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject LinesObj; //Объект в котором храним линии
    private bool lineStarted = false; //Проверка, что начали рисовать линию
    private List<GameObject> lineRenderers = new List<GameObject>(); //Список всех линий
    private int lastRenderer=0; //Текущий индекс линии
    private Material material; //Материал линии
    void Start()
    {
        //Задаем базовый материал для линии
        material=new Material(Shader.Find("Sprites/Default"));
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//Пускаем луч из камеры в сторону курсора
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)) // Если нажата левая клавиша мыши
        {
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.collider.gameObject.name == "Plane"&&lineStarted==false) //Если мы не начали рисовать линию и нажали по plane
                {
                    StartLine(hit);
                    lineStarted = true;
                }
                else if(hit.collider.gameObject.name == "Plane"&&lineStarted == true)//Если мы начали рисовать линию и нажали по plane
                {
                    lineRenderers[lastRenderer].GetComponent<LineRenderer>().SetPosition(1, hit.point);
                    lineStarted = false;
                    lastRenderer++;
                }
            }
        }
        if (lineStarted)//Двигаем линию за мышкой каждый кадр
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Plane")
                {
                    lineRenderers[lastRenderer].GetComponent<LineRenderer>().SetPosition(1, hit.point);
                }
            }
            else//Если мышка выходит за край plane, удаляем линию
            {
                RemoveLine();
            }
        }
    }
    /// <summary>
    /// Функция удаляет последнюю нарисованую линию
    /// </summary>
    public void RemoveLine()
    {
        if (lineRenderers.Count>0)
        {
            int index = lineRenderers.Count - 1;
            GameObject temp = lineRenderers[index];
            lineRenderers.RemoveAt(index);
            Destroy(temp);
            lineStarted = false;
            lastRenderer--;
        }
    }
    /// <summary>
    /// Функция создает новую линию
    /// </summary>
    /// <param name="hit">Начальная точка</param>
    private void StartLine(RaycastHit hit)
    {
        GameObject obj = new GameObject();//Создаем пустой объет
        obj.name = "Line" + (lastRenderer + 1).ToString(); //Переименовываем его
        obj.transform.SetParent(LinesObj.transform); //Делаем его ребенком объекта в котором мы будем хранить все линии
        lineRenderers.Add(obj); //Добавляем наш объект в список
        lineRenderers[lastRenderer].AddComponent<LineRenderer>(); // Добавляем к нему компонент
        LineRenderer lineRenderer = lineRenderers[lastRenderer].GetComponent<LineRenderer>(); // Находим этот компонент
        //Первоначальная настройка линии
        lineRenderer.material = material;
        lineRenderer.widthMultiplier = 0.3f;
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.SetPosition(0, hit.point);
    }
}
