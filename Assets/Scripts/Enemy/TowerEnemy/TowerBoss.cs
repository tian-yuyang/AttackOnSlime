/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBoss : MonoBehaviour
{
    public int dead = 0;
    [Tooltip("塔间距")]
    public float interval = 10f;
    static public UIManager uiManager;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 v = new Vector3(interval, 0f, 0f);
        GameObject newEnemy = Instantiate(Resources.Load("Prefabs/Enemy/TowerEnemy") as GameObject);
        newEnemy.transform.position = transform.position + v;
        newEnemy.GetComponent<TowerEnemy>().SetElite(this);
        
        GameObject newEnemy1 = Instantiate(Resources.Load("Prefabs/Enemy/TowerEnemy") as GameObject);
        newEnemy1.transform.position = transform.position - v;
        newEnemy1.GetComponent<TowerEnemy>().SetElite(this);
        
        v = new Vector3(0f, interval, 0f);
        GameObject newEnemy2 = Instantiate(Resources.Load("Prefabs/Enemy/TowerEnemy") as GameObject);
        newEnemy2.transform.position = transform.position + v;
        newEnemy2.GetComponent<TowerEnemy>().SetElite(this);
        
        GameObject newEnemy3 = Instantiate(Resources.Load("Prefabs/Enemy/TowerEnemy") as GameObject);
        newEnemy3.transform.position = transform.position - v;
        newEnemy3.GetComponent<TowerEnemy>().SetElite(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == 4)
        {
            //win
            Destroy(transform.gameObject);
        }
    }

    static public void SetUI(UIManager newUI)
    {
        uiManager = newUI;
    }

    public void DeadOne()
    {
        dead += 1;
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBoss : MonoBehaviour
{
    public int dead = 0;
    [Tooltip("Ëþ¼ä¾à")]
    public float interval = 10f;
    static public UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 v = new Vector3(interval, 0f, 0f);
        GameObject newEnemy = Instantiate(Resources.Load("Prefabs/Enemy/TowerEnemy") as GameObject);
        newEnemy.transform.position = transform.position + v;
        newEnemy.GetComponent<TowerEnemy>().SetElite(this);

        GameObject newEnemy1 = Instantiate(Resources.Load("Prefabs/Enemy/TowerEnemy") as GameObject);
        newEnemy1.transform.position = transform.position - v;
        newEnemy1.GetComponent<TowerEnemy>().SetElite(this);

        v = new Vector3(0f, interval, 0f);
        GameObject newEnemy2 = Instantiate(Resources.Load("Prefabs/Enemy/TowerEnemy") as GameObject);
        newEnemy2.transform.position = transform.position + v;
        newEnemy2.GetComponent<TowerEnemy>().SetElite(this);

        GameObject newEnemy3 = Instantiate(Resources.Load("Prefabs/Enemy/TowerEnemy") as GameObject);
        newEnemy3.transform.position = transform.position - v;
        newEnemy3.GetComponent<TowerEnemy>().SetElite(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == 4)
        {
            //win
            Destroy(transform.gameObject);
            uiManager.Win();
        }
    }

    static public void SetUI(UIManager newUI)
    {
        uiManager = newUI;
    }

    public void DeadOne()
    {
        dead += 1;
    }
}
