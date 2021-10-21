using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Scriptable_Objs;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    private int currentWorld = 1;
    private int currentLevel = 1;

    [SerializeField] private Transform doorSpawn;
    [SerializeField] private WorldData[] worlds;
    [SerializeField] private TMP_Text worldNumberText;
    [SerializeField] private TMP_Text worldNameText;
    [SerializeField] private TMP_Text levelNumberText;

    private GameObject currentDoor;

    private void Start()
    {
        var doorObj = Instantiate(worlds[0].door, doorSpawn.position, quaternion.identity);
        doorObj.transform.parent = doorSpawn;
        doorObj.transform.localScale = Vector3.one*0.7f;
        doorObj.transform.DOLocalMove(Vector3.zero - Vector3.up * 0.2f,1).SetEase(Ease.OutBack);
        currentDoor = doorObj;
        UpdateText();
        MoveDoorDown();
    }

    void SpawnDoor()
    {
        var doorObj = Instantiate(worlds[currentWorld-1].door, doorSpawn.position - Vector3.up * 5f, quaternion.identity);
        doorObj.transform.parent = doorSpawn;
        doorObj.transform.localScale = Vector3.one*0.7f;
        doorObj.transform.DOLocalMove(Vector3.zero - Vector3.up * 0.2f, 1).SetEase(Ease.OutBack);
        currentDoor = doorObj;
    }

    IEnumerator DestroyDoor()
    {
        var door = currentDoor;
        door.transform.DOLocalMove(Vector3.up * - 5, 1).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1);
        Destroy(door);
    }

    public void MoveDoorUp()
    {
        doorSpawn.DOLocalMove(Vector3.zero, 1).SetEase(Ease.OutBack);
    }

    public void MoveDoorDown()
    {
        doorSpawn.DOLocalMove(Vector3.up * -5, 1).SetEase(Ease.OutBack);
    }

    public void IncrementWorld()
    {
        if(currentWorld < worlds.Length)
        {
            currentWorld++;
            currentLevel = 1;
        }
        else
        {
            currentWorld = 1;
            currentLevel = 1;
        }

        StartCoroutine(DestroyDoor());
        SpawnDoor();
        UpdateText();
    }
    
    public void DecrementWorld()
    {
        if(currentWorld > 1)
        {
            currentWorld--;
            currentLevel = 1;
        }
        else
        {
            currentWorld = worlds.Length;
            currentLevel = 1;
        }
        StartCoroutine(DestroyDoor());
        SpawnDoor();
        UpdateText();
    }
    
    public void IncrementLevel()
    {
        if (currentLevel < worlds[currentWorld - 1].levelAmount)
        {
            currentLevel++;
        }
        else
        {
            currentLevel = 1;
        }
        UpdateText();
    }
    
    public void DecrementLevel()
    {
        if (currentLevel > 1)
        {
            currentLevel--;
        }
        else
        {
            currentLevel = worlds[currentWorld - 1].levelAmount;
        }

        UpdateText();
    }

    void UpdateText()
    {
        worldNameText.text = worlds[currentWorld-1].name;
        worldNumberText.text = currentWorld.ToString();
        levelNumberText.text = currentLevel.ToString();
    }
    
}
