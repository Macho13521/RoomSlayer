using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyQueen : Enemy
{

    [SerializeField]
    private float runAwayDistance = 3;

    private Vector3[] rayCastsPos;
    private List<Vector3> hitPos;

    [SerializeField]
    private float spawnRate = 2;
    private float nextSpawn = 2;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private GameObject fly;

    private void Start()
    {
        player = GameManager.Instance.player;

        rigidbody = gameObject.GetComponent<Rigidbody>();

        health *= GameManager.Instance.GetFlyHpGain();

        damagePoints *= GameManager.Instance.GetFlyDmgGain();

        rayCastsPos = new Vector3[9];
        hitPos = new List<Vector3>();


        States();
    }

    void Update()
    {
        Vector3 destPos = new Vector3(transform.position.x, navMeshAgent.destination.y, transform.position.z);
        if (navMeshAgent.destination == destPos)
        {
            if(!frozen)
                States();
        }   
    }


    private void States()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > runAwayDistance)
        {
            transform.LookAt(player.transform);
            SpawnFly();
            
        }
        else
            RunAway();
    }    

    private void RunAway()
    {

        ShootRayCasts();
        if (navMeshAgent.enabled)
            if (!isDying)
                navMeshAgent.destination = FindGoVector();

    }

    private void ShootRayCasts()
    {
        rayCastsPos[0] = -transform.forward;
        rayCastsPos[1] = transform.right;
        rayCastsPos[2] = -transform.right;
        rayCastsPos[3] = new Vector3(1, 0, 1);
        rayCastsPos[4] = new Vector3(-1, 0, 1);
        rayCastsPos[5] = new Vector3(0.5f, 0, 1);
        rayCastsPos[6] = new Vector3(-0.5f, 0, 1);
        rayCastsPos[7] = new Vector3(1, 0, 0.5f);
        rayCastsPos[8] = new Vector3(-1, 0, 0.5f);

        for (int i = 0; i < rayCastsPos.Length; i++)
        {

            RaycastHit hit;
            Ray ray = new Ray(transform.position, rayCastsPos[i]);
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject != player)
                    hitPos.Add(hit.transform.position);
            }
        }
    }

    private Vector3 FindGoVector()
    {
        float fgmax = 0;
        int index = 0;
        for(int i=0; i< hitPos.Count; i++)
        {
            if (hitPos[i] == null)
                continue;
            float fdist = Vector3.Distance(hitPos[i], transform.position);
            float gdist = Vector3.Distance(hitPos[i], player.transform.position) * 1.5f;
            if (fgmax < fdist+ gdist)
            {
                fgmax = fdist + gdist;
                index = i;
            }
        }
        
        return hitPos[index];
    }

    private void SpawnFly()
    {
        if (Time.time > nextSpawn)
        {
            Instantiate(fly, spawnPoint.position, transform.rotation);
            nextSpawn= Time.time + spawnRate;

        }
    }

    public override void Freeze(bool freeze, float freezTime)
    {
        base.Freeze(freeze, freezTime);
        animator?.SetBool(AnimationNames.flyQueenFreeze, true);
    }

    protected override void UnFreeze()
    {
        base.UnFreeze();
        animator?.SetBool(AnimationNames.flyFreeze, false);
    }

    protected override void Dying()
    {
        base.Dying();
        animator?.SetTrigger(AnimationNames.flyQueenDying);
    }
}
