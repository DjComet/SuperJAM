﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Private
    private int _currentLife = 4;
    // todo (Gabi): This should be START.
    private GameManagerState _currentState = GameManagerState.PLAYING;
    private bool _assignedCoroutineBoxSpawn = false;
    private bool _assignedCoroutineRobotSpawn = false;
    #endregion

    #region Public
    public int lifeStart = 4;
    public int waitSecondsForBoxSpawn = 10;
    public int waitSecondsForRobotSpawn = 10;
    public GameObject boxPrefab = null;
    public GameObject robotPrefab = null;
    #endregion

    #region MonoBehaviour
    void Start()
    {
        _currentLife = lifeStart;
    }

    void Update()
    {
        Control();
    }
    #endregion

    #region Methods

    /// <summary>
    /// Control this instance.
    /// </summary>
    void Control()
    {
        switch(_currentState)
        {
            case GameManagerState.START:
                break;
            case GameManagerState.PLAYING:
                if (_currentLife <= 0)
                {
                    _currentState = GameManagerState.FINISH;
                    return;
                }
                StartCoroutinesForSpawn();
                break;
            case GameManagerState.FINISH:

                Debug.Log("Game has finished, todo the end screen");
                break;
            default:
                Debug.LogWarning("State not defined!");
                _currentState = GameManagerState.START;
                break;
        }
    }

    /// <summary>
    /// Instantiates a random box.
    /// </summary>
    IEnumerator InstantiateRandomBox()
    {
        //GameObject box = (GameObject)Instantiate(boxPrefab);
        //Vector2 randomPosition = ReturnRandomAvailablePosition(box.transform);
        //box.transform.position = new Vector3(randomPosition.x, 0, randomPosition.y);       
        while (true)
        {
            Debug.Log("WORK");
            yield return new WaitForSeconds(waitSecondsForBoxSpawn);
        }
    }

    /// <summary>
    /// Instantiates a random robot.
    /// </summary>
    IEnumerator InstantiateRandomRobot()
    {
        GameObject robot = (GameObject)Instantiate(robotPrefab);
        Vector2 randomPosition = ReturnRandomAvailablePosition(robot.transform);
        robot.transform.position = new Vector3(randomPosition.x, 0, randomPosition.y);
        yield return new WaitForSeconds(waitSecondsForBoxSpawn);
    }

    /// <summary>
    /// Returns the random available position depending on the scale of the object.
    /// </summary>
    /// <returns>Vector2: The random available position.</returns>
    public Vector2 ReturnRandomAvailablePosition(Transform tr)
    {
        return Vector2.one;
    }

    /// <summary>
    /// Starts the coroutines for spawning if they're not set to null.
    /// </summary>
    /// 
    void StartCoroutinesForSpawn()
    {
        if (!_assignedCoroutineBoxSpawn)
        {
            StartCoroutine("InstantiateRandomBox");
            _assignedCoroutineBoxSpawn = true;
        }
    }

    /// <summary>
    /// Sets the coroutines to null.
    /// </summary>
    void SetCoroutinesToNull()
    {
        _assignedCoroutineBoxSpawn = false;
        _assignedCoroutineRobotSpawn = false;
    }
    #endregion 
}