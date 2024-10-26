using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int trackIndex = 0;
    List<Transform> tracks;
    float speed = 15f;
    Rigidbody rb;
    float jumpCooldown = 0.0f;
    float jumpCooldownMax = 1.0f;
    float jumpStopCooldown = 0.0f;
    float jumpStopCooldownMax = 0.75f;
    public Animator animator;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        tracks = Game.singleton.tracks;
        trackIndex = 1;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            animator.SetBool("jumping", true);
            return;
        }

        FollowTrack(tracks[trackIndex].position);

        if (Input.GetButtonDown("Up"))
        {
            trackIndex--;
        } else if (Input.GetButtonDown("Down"))
        {
            trackIndex++;
        } else if (Input.GetButtonDown("Jump"))
        {
            if (jumpCooldown <= 0f)
            {
                rb.AddForce(Vector3.up * 250f);
                jumpCooldown = jumpCooldownMax;
                Debug.Log("jump!");
                animator.SetBool("jumping", true);
                jumpStopCooldown = jumpStopCooldownMax;
            }
        }
        if (trackIndex >= tracks.Count)
            trackIndex = tracks.Count;
        if (trackIndex < 0)
            trackIndex = 0;

        if (jumpCooldown >= 0f)
            jumpCooldown -= Time.deltaTime;

        if (jumpStopCooldown < 0.0f)
        {
            animator.SetBool("jumping", false);
        } else
        {
            jumpStopCooldown -= Time.deltaTime;
        }
        if (dead)
        {
            jumpStopCooldown = 10f;
            animator.SetBool("jumping", true);
        }
    }

    void FollowTrack(Vector3 v)
    {
        Vector3 target = new Vector3(transform.position.x, transform.position.y, v.z);
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Pumpkin")
        {
            Game.singleton.score += 1;
            Game.singleton.DestroyMovingObject(other.transform);
        } else if (other.transform.tag == "Deadly")
        {
            Game.singleton.GameOver();
            dead = true;
        }
    }
}
