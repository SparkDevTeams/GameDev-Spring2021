/**
 * Event System
 * ==============
 * The following code if for the explicit purpose of managing events in the game.
 * 
 * IMPORTANT: There should only be 1 Event System object present at any point in
 * a game scene
 * 
 * Event System runs on int IDs so remember to have a parameter for the ID in your
 * event methods and general events. Events contained within a general event that
 * have the same ID will occur at the same time
 * 
 * Please keep new events general to make full use of the ID system (i.e. if 2 events 
 * are very similar like opening a door then put them under the same general event
 * instead of making a new general event for both)
 * 
 * Instructions of Use
 * ========================
 *  IN EVENT SYSTEM FILE
 *  
 * -A general event and a event triggermust be created in this EventSystem file before 
 *  events can be set up
 * -Format for setting up genral events is as follows (you can also look at test events
 *  for examples)
 * 
 *      public event Action<int(for the ID), [any other parameters]> generalEventName;
 *      
 *      public void GeneralEventTrigger(int id, [other parameters])
 *      {
 *          if(generalEventName != null)
 *          {
 *              generalEventName(id, [other parameters]);
 *          }
 *      }
 * 
 * IN YOUR SCRIPT FILE
 * 
 * -After a general event is created you can start creating your own events but you must
 *  make sure you have and variable for an ID in your file and that the parameter types
 *  in your event method matchup with the general event parameter types
 *  
 *  For Example:
 *      private const int ID = 69;
 *      
 *      public void MyEvent(int id, [other parameters])
 *      {
 *          if(id == ID)
 *          {
 *              //DO SHIT
 *          }
 *      }
 *      
 *  -Then you have to add your event to the general event list so it can be called by an 
 *   event trigger and that must be done in your script file's Start() method by referencing 
 *   the EventSystem object like below
 *      
 *      void Start()
 *      {
 *          EventSystem.eventController.generalEventName += MyEvent;
 *      }
 *  
 *  TRIGGERING AN EVENT
 *  
 *  -After the general event is created anf your events are added you can trigger an event
 *   by referencing the EventSystem object and calling the event trigger method you created
 *   in that file with your ID and parameters like below
 *      
 *      EventSystem.eventController.GeneralEventTrigger(triggerId, [other parameters]);
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem eventController;

    private void Awake()
    {
        eventController = this;
    }

    public event Action<int> onCrystalTrigger;

    public void CrystalTrigger(int id)
    {
        if (onCrystalTrigger != null)
        {
            onCrystalTrigger(id);
        }
    }
}