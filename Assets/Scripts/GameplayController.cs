using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance;

    public ItemSO[] items;
    private int goalItemsCount;
    private int curItemsCount;
    private ItemSO goalItem;

    public Animator playerAnimator;

    private bool taking;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        goalItemsCount = Random.Range(1, 6);
        goalItem = items[Random.Range(0, items.Length)];
        UIController.Instance.SetStartingDisplay(goalItem.name, goalItem.itemIcon, goalItemsCount);

        StartCoroutine(SpawnCoroutine());
    }

    public void Check(ItemType type, Vector3 value, GameObject itemObject) 
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle Male"))
        {
            playerAnimator.SetBool("Take", true);
            if (type == goalItem.type)
            {
                playerAnimator.SetBool("Good item", true);
                GoodItem(value);
                if (curItemsCount == goalItemsCount)
                {
                    playerAnimator.SetTrigger("Win");
                    StartCoroutine(EndGame());
                }
            }
            else
            {
                Handheld.Vibrate();
            }

            StartCoroutine(EndAnim());
            Destroy(itemObject);
        }
    }

    IEnumerator EndGame() 
    {
        yield return new WaitForSeconds(2f);
        UIController.Instance.Show(UIController.Instance.endDisplay);
    }

    public void GoodItem(Vector3 value) 
    {
        UIController.Instance.AddingObject(value);
        curItemsCount++;
        UIController.Instance.UpdateGoalText(curItemsCount, goalItemsCount);
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(1.1f);
        Instantiate(items[Random.Range(0, items.Length)].prefab, Vector3.up + Vector3.forward * -7.5f, Quaternion.identity);
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator EndAnim() 
    {
        yield return new WaitForSeconds(0.2f);
        playerAnimator.SetBool("Take", false);
        
    }

    private void Update()
    {
        //Debug.Log(.ToString());
    }

    public void ReloadLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}