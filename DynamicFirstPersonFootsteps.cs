using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFirstPersonFootsteps : MonoBehaviour
    

{
    public float footstepTiming, sprintFootstepTiming, sideStepTiming; 
    public float footstepsStartDelay;
    public float sprintStartDelay;
    public AudioClip[] footsteps;
    public AudioClip[] backwardsFootsteps;
    public AudioClip[] sprintForwards;
    public AudioClip[] sprintBackwards;
    public AudioClip[] sideStepLeft;
    public AudioClip[] sideStepRight;
    private AudioSource audiosource;
    bool isWPressed, isSPressed, isShiftWPressed, isShiftSPressed, sprintCheck, isAPressed, isDPressed;
    int counter_01 = 0, counter_02 = 0, counter_03 = 0, counter_04 = 0;


    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Shuffle Audioclip Declarations


    public static int shuffleForwardSteps;
    public static int shuffleForwardSprintSteps;
    public static int shuffleBackwardSteps;
    public static int shuffleBackwardSprintSteps;
    public static int shuffleSideStepLeft;
    public static int shuffleSideStepRight;
    public int setShuffleForwardSteps;
    public int setShuffleForwardSprintSteps;
    public int setShuffleBackwardSteps;
    public int setShuffleBackwardSprintSteps;
    public int setShuffleSideStepLeft;
    public int setShuffleSideStepRight;
    public int[] indexForwardSteps;
    public int[] indexForwardSprintSteps;
    public int[] indexBackwardSteps;
    public int[] indexBackwardSprintSteps;
    public int[] indexSideStepLeft;
    public int[] indexSideStepRight;
    private int currentIndexForwardSteps = 0;
    private int currentIndexForwardSprintSteps = 0;
    private int currentIndexBackwardSteps = 0;
    private int currentIndexBackwardSprintSteps = 0;
    private int currentIndexSideStepLeft = 0;
    private int currentIndexSideStepRight = 0;



    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    void Start()
    {

        shuffleForwardSteps = setShuffleForwardSteps;
        shuffleForwardSprintSteps = setShuffleForwardSprintSteps;
        shuffleBackwardSteps = setShuffleBackwardSteps;
        shuffleBackwardSprintSteps = setShuffleBackwardSprintSteps;
        shuffleSideStepLeft = setShuffleSideStepLeft;
        shuffleSideStepRight = setShuffleSideStepRight;

        indexForwardSteps = new int[shuffleForwardSteps];
        indexForwardSprintSteps = new int[shuffleForwardSprintSteps];
        indexBackwardSteps = new int[shuffleBackwardSteps];
        indexBackwardSprintSteps = new int[shuffleBackwardSprintSteps];
        indexSideStepLeft = new int[shuffleSideStepLeft];
        indexSideStepRight = new int[shuffleSideStepRight];

        for (int i = 0; i < shuffleForwardSteps; i++) { indexForwardSteps[i] = -1; }
        for (int i = 0; i < shuffleForwardSprintSteps; i++) { indexForwardSprintSteps[i] = -1; }
        for (int i = 0; i < shuffleBackwardSteps; i++) { indexBackwardSteps[i] = -1; }
        for (int i = 0; i < shuffleBackwardSprintSteps; i++) { indexBackwardSprintSteps[i] = -1; }
        for (int i = 0; i < shuffleSideStepLeft; i++) { indexSideStepLeft[i] = -1; }
        for (int i = 0; i < shuffleSideStepRight; i++) { indexSideStepRight[i] = -1; }

        audiosource = GetComponent<AudioSource>(); 
        
    }

    void Update()
    {     
        if (Input.GetKeyDown(KeyCode.W))
        {
            print("W pressed");
            StopAllCoroutines(); 
            isWPressed = true;
            StartCoroutine("WaitForStart"); 
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            print("S pressed");
            StopAllCoroutines();
            isSPressed = true;
            StartCoroutine("WaitForStart"); 

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            StopAllCoroutines();
            isAPressed = true;
            StartCoroutine("WaitForStart");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            StopAllCoroutines();
            isDPressed = true;
            StartCoroutine("WaitForStart");

        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {

            if (counter_01 == 0)
            {
                StopAllCoroutines();
            }

            if (counter_02 == 0)
            {
                isShiftWPressed = true;
                StartCoroutine("WaitForSprint");
            }

            counter_01++;
            counter_02++; 

        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            if (counter_03 == 0)
            {
                StopAllCoroutines();
            }

            if (counter_04 == 0)
            {
                isShiftSPressed = true;
                StartCoroutine("WaitForSprint"); 
            }
            counter_03++;
            counter_04++; 
        }


        if (Input.GetKeyUp(KeyCode.W))
        {
            StopAllCoroutines(); 
            isWPressed = false; 
            counter_01 = 0;
            counter_02 = 0;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            StopAllCoroutines();
            isSPressed = false;
            counter_03 = 0;
            counter_04 = 0; 
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            StopAllCoroutines();
            isAPressed = false;

        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            StopAllCoroutines();
            isDPressed = false;
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            counter_01 = 0; 
            counter_02 = 0;
            isShiftWPressed = false;
            isShiftSPressed = false;
            if (checkWPressed())
                {
                    print("W still pressed");
                    StartCoroutine("PlayFootsteps");
                }
            if (checkSPressed())
            {
                StartCoroutine("PlayBackwardsFootsteps"); 
            }
        }
        
    }

    int randomGeneratorForward() { return Random.Range(0, footsteps.Length); }
    int randomGeneratorForwardSprint() { return Random.Range(0, sprintForwards.Length); }
    int randomGeneratorBackward() { return Random.Range(0, backwardsFootsteps.Length); }
    int randomGeneratorBackwardSprint() { return Random.Range(0, sprintBackwards.Length); }
    int randomGeneratorSideLeft() { return Random.Range(0, sideStepLeft.Length); }
    int randomGeneratorSideRight() { return Random.Range(0, sideStepRight.Length); }

    IEnumerator PlayFootsteps()
    {
        while (true)
        {
            CheckandPlaySoundForward();
            yield return new WaitForSeconds(footstepTiming);
        }
         
    }

    IEnumerator PlayBackwardsFootsteps()
    {
        while (true)
        {
            CheckandPlaySoundBackward();
            yield return new WaitForSeconds(footstepTiming);
        }
    }

    IEnumerator PlaySprintForwards()
    {
        while (true)
        {
            CheckandPlaySoundForwardSprint();
            yield return new WaitForSeconds(sprintFootstepTiming);
        }
    }

    IEnumerator PlaySprintBackwards()
    {
        while (true)
        {
            CheckandPlaySoundBackwardSprint(); 
            yield return new WaitForSeconds(sprintFootstepTiming);
        }
    }

    IEnumerator PlayLeftSideStep()
    {
        while (true)
        {
            CheckandPlaySoundSideLeft(); 
            yield return new WaitForSeconds(sideStepTiming);
        }
            
    }

    IEnumerator PlayRightSideStep()
    {
        while (true)
        {
            CheckandPlaySoundSideRight(); 
            yield return new WaitForSeconds(sideStepTiming); 
        }

    }

    IEnumerator WaitForStart()
    {

        yield return new WaitForSeconds(footstepsStartDelay);

        if (isWPressed)
        {
            StartCoroutine("PlayFootsteps");

        }
        if (isSPressed)
        {
            StartCoroutine("PlayBackwardsFootsteps");
        }
        if (isAPressed)
        {
            StartCoroutine("PlayLeftSideStep");
        }
        if (isDPressed)
        {
            StartCoroutine("PlayRightSideStep");
        }
    }

    IEnumerator WaitForSprint()
    {

        yield return new WaitForSeconds(sprintStartDelay);

        if (isShiftWPressed)
        {
           
            StartCoroutine("PlaySprintForwards");
        }
        if (isShiftSPressed)
        {  
            StartCoroutine("PlaySprintBackwards");
        }

    }

    bool checkWPressed()
    {
        if (Input.GetKey(KeyCode.W))
        {
            StopAllCoroutines(); 
            return true; 
        }

        return false; 
    }

    bool checkSPressed()
    {
        if (Input.GetKey(KeyCode.S))
        {
            StopAllCoroutines();
            return true;
        }
        return false; 
    }

    void CheckandPlaySoundForward()
    {
        int soundPlaying;

        //The while loop cycles through the index checking for a previously played sound. If it finds one while loop starts again picking another random value. If not breaks from loop. 

        while (true)
        {
            soundPlaying = randomGeneratorForward();
            bool foundSoundPlaying = false;
            for (int i = 0; i < shuffleForwardSteps; i++)
            {
                if (indexForwardSteps[i] == soundPlaying) { foundSoundPlaying = true; }
            }
            if (!foundSoundPlaying) { break; }
        }

        indexForwardSteps[currentIndexForwardSteps] = soundPlaying;
        currentIndexForwardSteps++;
        if (currentIndexForwardSteps == shuffleForwardSteps)
        {
            currentIndexForwardSteps = 0; 
        }

        //Plays sound. 
        audiosource.clip = footsteps[soundPlaying];
        audiosource.Play();
    }

    void CheckandPlaySoundForwardSprint()
    {
        int soundPlaying;

        //The while loop cycles through the index checking for a previously played sound. If it finds one while loop starts again picking another random value. If not breaks from loop. 

        while (true)
        {
            soundPlaying = randomGeneratorForwardSprint();
            bool foundSoundPlaying = false;
            for (int i = 0; i < shuffleForwardSprintSteps; i++)
            {
                if (indexForwardSprintSteps[i] == soundPlaying) { foundSoundPlaying = true; }
            }
            if (!foundSoundPlaying) { break; }
        }

        indexForwardSprintSteps[currentIndexForwardSprintSteps] = soundPlaying;
        currentIndexForwardSprintSteps++;
        if (currentIndexForwardSprintSteps == shuffleForwardSprintSteps)
        {
            currentIndexForwardSprintSteps = 0;
        }

        //Plays sound. 
        audiosource.clip = sprintForwards[soundPlaying];
        audiosource.Play();
    }

    void CheckandPlaySoundBackward()
    {
        int soundPlaying;

        //The while loop cycles through the index checking for a previously played sound. If it finds one while loop starts again picking another random value. If not breaks from loop. 

        while (true)
        {
            soundPlaying = randomGeneratorBackward();
            bool foundSoundPlaying = false;
            for (int i = 0; i < shuffleBackwardSteps; i++)
            {
                if (indexBackwardSteps[i] == soundPlaying) { foundSoundPlaying = true; }
            }
            if (!foundSoundPlaying) { break; }
        }

        indexBackwardSteps[currentIndexBackwardSteps] = soundPlaying;
        currentIndexBackwardSteps++;
        if (currentIndexBackwardSteps == shuffleBackwardSteps)
        {
            currentIndexBackwardSteps = 0;
        }

        //Plays sound. 
        audiosource.clip = backwardsFootsteps[soundPlaying];
        audiosource.Play();

    }

    void CheckandPlaySoundBackwardSprint()
    {
        int soundPlaying;

        //The while loop cycles through the index checking for a previously played sound. If it finds one while loop starts again picking another random value. If not breaks from loop. 

        while (true)
        {
            soundPlaying = randomGeneratorBackwardSprint();
            bool foundSoundPlaying = false;
            for (int i = 0; i < shuffleBackwardSprintSteps; i++)
            {
                if (indexBackwardSprintSteps[i] == soundPlaying) { foundSoundPlaying = true; }
            }
            if (!foundSoundPlaying) { break; }
        }

        indexBackwardSprintSteps[currentIndexBackwardSprintSteps] = soundPlaying;
        currentIndexBackwardSprintSteps++;
        if (currentIndexBackwardSprintSteps == shuffleBackwardSprintSteps)
        {
            currentIndexBackwardSprintSteps = 0;
        }

        //Plays sound. 
        audiosource.clip = sprintBackwards[soundPlaying];
        audiosource.Play();
    }

    void CheckandPlaySoundSideLeft()
    {
        int soundPlaying;

        //The while loop cycles through the index checking for a previously played sound. If it finds one while loop starts again picking another random value. If not breaks from loop. 

        while (true)
        {
            soundPlaying = randomGeneratorSideLeft();
            bool foundSoundPlaying = false;
            for (int i = 0; i < shuffleSideStepLeft; i++)
            {
                if (indexSideStepLeft[i] == soundPlaying) { foundSoundPlaying = true; }
            }
            if (!foundSoundPlaying) { break; }
        }

        indexSideStepLeft[currentIndexSideStepLeft] = soundPlaying;
        currentIndexSideStepLeft++;
        if (currentIndexSideStepLeft == shuffleSideStepLeft)
        {
            currentIndexSideStepLeft = 0;
        }

        //Plays sound. 
        audiosource.clip = sideStepLeft[soundPlaying];
        audiosource.Play();
    }

    void CheckandPlaySoundSideRight()
    {
        int soundPlaying;

        //The while loop cycles through the index checking for a previously played sound. If it finds one while loop starts again picking another random value. If not breaks from loop. 

        while (true)
        {
            soundPlaying = randomGeneratorSideRight();
            bool foundSoundPlaying = false;
            for (int i = 0; i < shuffleSideStepRight; i++)
            {
                if (indexSideStepRight[i] == soundPlaying) { foundSoundPlaying = true; }
            }
            if (!foundSoundPlaying) { break; }
        }

        indexSideStepRight[currentIndexSideStepRight] = soundPlaying;
        currentIndexSideStepRight++;
        if (currentIndexSideStepRight == shuffleSideStepRight)
        {
            currentIndexSideStepRight = 0;
        }

        //Plays sound. 
        audiosource.clip = sideStepRight[soundPlaying];
        audiosource.Play();
    }

}
