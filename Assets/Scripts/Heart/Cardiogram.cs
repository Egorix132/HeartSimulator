using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cardiogram : MonoBehaviour, IHeartbeatListener
{
    float startY;

    [SerializeField]
    private GameObject particlePrefab;

    private bool isBeat = false;

    void OnEnable()
    {
        startY = transform.position.y;
        Heartbeat.OnBeat += AddBeat;
    }

    void FixedUpdate()
    {
        if (!isBeat)
        {
            GameObject newParticle = Instantiate(particlePrefab, transform.position, new Quaternion());
            newParticle.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        }
    }

    public IEnumerator Beat()
    {
        isBeat = true;
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        GameObject newParticle = Instantiate(particlePrefab, transform.position, Quaternion.Euler(0, 0, 0));
        newParticle.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        newParticle.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        for (int i = 1; i < 4; i++)
        {
            newParticle = Instantiate(
                particlePrefab,
                transform.position + new Vector3(0.01f * (1 + (i / 2)), 0.01f * i, 0),
                Quaternion.Euler(0, 0, 0));
            newParticle.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            newParticle.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        }      
        yield return new WaitForSeconds(0.03f);

        isBeat = true;
        transform.position = new Vector3(transform.position.x, startY + 0.04f, transform.position.z);
        for (int i = 0; i < 9; i++)
        {
            newParticle = Instantiate(
                particlePrefab,
                transform.position + new Vector3(0.01f * (1 + (i / 3)), -0.01f * i, 0),
                Quaternion.Euler(0, 0, 0));
            newParticle.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            newParticle.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        }
        yield return new WaitForSeconds(0.03f);

        isBeat = true;
        transform.position = new Vector3(transform.position.x, startY - 0.04f, transform.position.z);
        for (int i = 0; i < 17; i++)
        {
            newParticle = Instantiate(
                particlePrefab,
                transform.position + new Vector3(0.01f * (1 + (i / 5)), 0.01f * i, 0),
                Quaternion.Euler(0, 0, 0));
            newParticle.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            newParticle.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        }
        yield return new WaitForSeconds(0.04f);

        isBeat = true;
        transform.position = new Vector3(transform.position.x, startY + 0.08f, transform.position.z);
        for (int i = 0; i < 21; i++)
        {
            newParticle = Instantiate(
                particlePrefab,
                transform.position + new Vector3(0.01f * (1 + (i / 8)), -0.01f * i, 0),
                Quaternion.Euler(0, 0, 0));
            newParticle.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            newParticle.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        }
        yield return new WaitForSeconds(0.03f);

        isBeat = true;
        transform.position = new Vector3(transform.position.x, startY - 0.12f, transform.position.z);
        for (int i = 0; i < 17; i++)
        {
            newParticle = Instantiate(
                particlePrefab,
                transform.position + new Vector3(0.01f * (1 + (i / 5)), 0.01f * i, 0),
                Quaternion.Euler(0, 0, 0));
            newParticle.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            newParticle.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        }
        yield return new WaitForSeconds(0.04f);

        isBeat = true;
        transform.position = new Vector3(transform.position.x, startY + 0.04f, transform.position.z);
        for (int i = 0; i < 5; i++)
        {
            newParticle = Instantiate(
                particlePrefab,
                transform.position + new Vector3(0.01f * (1 + (i / 2)), -0.01f * i, 0),
                Quaternion.Euler(0, 0, 0));
            newParticle.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            newParticle.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        }
        yield return new WaitForSeconds(0.04f);
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        isBeat = false;
    }

    public void AddBeat()
    {
        StartCoroutine(Beat());
    }

    void OnDisable()
    {
        Heartbeat.OnBeat -= AddBeat;
    }
}
