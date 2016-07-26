using UnityEngine;
using System.Collections.Generic;
using System;

public class gvmBehaviourContainer {

    private static gvmBehaviourContainer _instance;

    private gvmBehaviourContainer() { }

    public static gvmBehaviourContainer GetInstance() {
        if(_instance == null) {
            _instance = new gvmBehaviourContainer();
        }
        return _instance;
    }

    public object GetBehaviourById(int behaviourId) {
        return new gvmWaveBehaviour();
        /*
        switch (behaviourId) {
            case 0:
                return new gvmWaveBehaviour();
        }
        return null;*/
    }     
}
