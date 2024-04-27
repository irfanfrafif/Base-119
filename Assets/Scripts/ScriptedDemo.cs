using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedDemo : MonoBehaviour
{
    float time = 0;
    bool isRunning;

    bool event1;
    bool event2;
    bool event3;
    bool event4;
    bool event5;
    bool event6;
    bool event7;
    bool event8;
    bool event9;

    public void DayStart()
    {
        isRunning = true;
    }

    void EventAddCustomer(int id, float secondFromStart, ref bool eventDone)
    {
        if (time > secondFromStart && !eventDone)
        {
            Debug.Log("Event played");
            eventDone = true;

            //DoSomething

            ServiceLocator.Instance.customerManager.AddCustomer(id);
        }
    }

    void EventLoadModifier(int id, float secondFromStart, ref bool eventDone)
    {
        if (time > secondFromStart && !eventDone)
        {
            Debug.Log("Event played");
            eventDone = true;

            //DoSomething

            ServiceLocator.Instance.dayManager.LoadModifier(id);
        }
    }

    void Update()
    {
        if (isRunning) time += Time.deltaTime;

        EventAddCustomer(0, 3, ref event1);

        EventAddCustomer(1, 40, ref event2);
        EventLoadModifier(1, 40, ref event3);

        EventAddCustomer(2, 95, ref event4);
        EventLoadModifier(3, 95, ref event5);

        EventAddCustomer(3, 110, ref event6);
        EventAddCustomer(0, 115, ref event7);

        EventAddCustomer(1, 150, ref event8);

        EventAddCustomer(2, 180, ref event9);
    }
}
