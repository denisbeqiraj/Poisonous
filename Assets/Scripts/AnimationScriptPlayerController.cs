using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScriptPlayerController : MonoBehaviour
{
    [SerializeField] private AudioSource runningSoundSource;
    [SerializeField] private AudioClip runningAudioClip;

    [SerializeField] private AudioSource walkingSoundSource;
    [SerializeField] private AudioClip walkingAudioClip;

    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isWalkingRightHash;
    int isWalkingLeftHash;
    int isWalkingBackHash;
    int isJumpHash;
    int isAimHash;
    int isAimWalkHash;
    public Transform hand;
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingRightHash = Animator.StringToHash("isWalkingRight");
        isWalkingLeftHash = Animator.StringToHash("isWalkingLeft");
        isWalkingBackHash = Animator.StringToHash("isWalkingBack");
        isJumpHash = Animator.StringToHash("isJump");
        isAimHash = Animator.StringToHash("isAim");
        isAimWalkHash = Animator.StringToHash("isAimWalk");

    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalkingRight = animator.GetBool(isWalkingRightHash);
        bool isWalkingLeft = animator.GetBool(isWalkingLeftHash);
        bool isWalkingBack = animator.GetBool(isWalkingBackHash);
        bool isJump = animator.GetBool(isJumpHash);
        bool isAim = animator.GetBool(isAimHash);
        bool isAimWalk = animator.GetBool(isAimWalkHash);


        bool forwardPress = Input.GetKey("w");
        bool rightPress = Input.GetKey("d");
        bool leftPress = Input.GetKey("a");
        bool backPress = Input.GetKey("s");
        bool jumpPress = Input.GetKey("space");
        bool aimPress = Input.GetMouseButton(1) || Input.GetMouseButton(0);
        bool aimWalkPress = Input.GetMouseButton(1) || Input.GetMouseButton(0);


        bool leftShiftPress = Input.GetKey("left shift");

        if (forwardPress)
        {
            if (isRunning)
            {
                if (!runningSoundSource.isPlaying)
                {
                    runningSoundSource.PlayOneShot(runningAudioClip);
                }
            }
            else
            {
                if (!walkingSoundSource.isPlaying)
                {
                    walkingSoundSource.PlayOneShot(walkingAudioClip);
                }
            }
            
        }

        if (!isWalking && forwardPress)
        {
            animator.SetBool(isAimHash, false);
            animator.SetBool(isAimWalkHash, false);
            animator.SetBool(isWalkingHash, true);

        }
        if (isWalking && !forwardPress)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isRunning && (leftShiftPress && forwardPress))
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && (!leftShiftPress || !forwardPress))
        {
            animator.SetBool(isRunningHash, false);
        }

        if (!isWalkingRight && rightPress)
        {
            animator.SetBool(isAimHash, false);
            animator.SetBool(isAimWalkHash, false);
            animator.SetBool(isWalkingRightHash, true);

        }
        if (isWalkingRight && !rightPress)
        {
            animator.SetBool(isWalkingRightHash, false);
        }

        if (!isWalkingLeft && leftPress)
        {
            animator.SetBool(isAimHash, false);
            animator.SetBool(isAimWalkHash, false);
            animator.SetBool(isWalkingLeftHash, true);

        }
        if (isWalkingLeft && !leftPress)
        {
            animator.SetBool(isWalkingLeftHash, false);
        }

        if (!isWalkingBack && backPress)
        {
            animator.SetBool(isAimHash, false);
            animator.SetBool(isAimWalkHash, false);
            animator.SetBool(isWalkingBackHash, true);
        }
        if (isWalkingBack && !backPress)
        {
            animator.SetBool(isWalkingBackHash, false);
        }

        if (!isJump && jumpPress)
        {
            animator.SetBool(isAimHash, false);
            animator.SetBool(isAimWalkHash, false);
            animator.SetBool(isJumpHash, true);
        }
        if (isJump && !jumpPress)
        {
            animator.SetBool(isJumpHash, false);
        }

        if (!isAim && aimPress)
        {
            animator.SetBool(isAimHash, true);
        }
        if (isAim && !aimPress)
        {
            animator.SetBool(isAimHash, false);
        }

        if (!isAimWalk && aimWalkPress)
        {
            animator.SetBool(isAimWalkHash, true);
        }
        if ((isAimWalk && !aimWalkPress) || !forwardPress)
        {
            animator.SetBool(isAimWalkHash, false);
        }
    }
}
