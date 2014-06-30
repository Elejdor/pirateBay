using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private float rotationSpeed = 50;
    public float velocity = 0;
    private float maxSpeed = 8;

    private float Leap = 0;
    private float RotationRate = 0;
    public List<ParticleSystem> frontParticles;
    private bool frontParticlesPlayed = false;

    float frontWaveFactor = 8;
    float sideWaveFactor = 5;

    public GameObject ShipActor;
    public Transform Board;
    private bool negateSideWave = false;
    private bool changeWave = true;

    private bool useDicepl = false;
    // Use this for initialization
    void Start()
    {
        velocity = maxSpeed;
        Leap = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (!useDicepl)
        {
            if (Input.GetKey(KeyCode.A))
            {
                Leap = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Leap = 1;
            }
            else
            {
                Leap = 0;
            }
        }

        if (changeWave)
        {
            negateSideWave = !negateSideWave;
            changeWave = false;
            StartCoroutine(wavesRandomizer());
        }

        RotationRate = Mathf.Lerp(RotationRate, Leap, 0.01f);

        ShipActor.transform.Rotate(new Vector3(0.0f, RotationRate * rotationSpeed * Time.deltaTime, 0));

        if (negateSideWave)
        {
            
            sideWaveFactor = Mathf.Lerp(sideWaveFactor, -10.0f*Random.value, 0.01f);
        }
        else
        {
            sideWaveFactor = Mathf.Lerp(sideWaveFactor, 10.0f * Random.value, 0.01f);
        }

        float sinOfTime = Mathf.Sin(Time.fixedTime);

        //I want to play particles once
        if (sinOfTime > 0.5 && !frontParticlesPlayed)
        {
            foreach (ParticleSystem item in frontParticles)
            {
                item.Play();
            }
            frontParticlesPlayed = true;
        }
        else if (sinOfTime < 0)
        {
            frontParticlesPlayed = false;
        }

        Board.rotation = Quaternion.Euler(new Vector3(sinOfTime * frontWaveFactor, Board.eulerAngles.y, -RotationRate * 30.0f - sinOfTime * sideWaveFactor));

        transform.Translate(ShipActor.transform.forward * velocity * Time.deltaTime);
    }

    IEnumerator wavesRandomizer()
    {
        yield return new WaitForSeconds(5);
        changeWave = true;
    }

}
