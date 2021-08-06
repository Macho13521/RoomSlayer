using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    float speed = 150f;
    [SerializeField]
    List<Sword> PrefabSwords;

    private Sword randomSword;

    private GameObject swordGO;

    private void Start()
    {
        if (PrefabSwords.Count > 0)
        {
            int random = Random.Range(0, PrefabSwords.Count);
            randomSword = PrefabSwords[random];
            swordGO = Instantiate(randomSword.gameObject, transform.GetChild(0));
            swordGO.transform.localScale /= 3f;
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Atack playerAtack = other.GetComponent<Atack>();
        if (playerAtack != null)
        {
            Tutorial.FirstSword();
            Destroy(swordGO);
            playerAtack.CreateSword(randomSword.gameObject);
        }
    }
}
