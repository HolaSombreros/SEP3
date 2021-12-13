using System;
using System.Collections.Generic;

namespace SEP3UI.Data {
    public static class NavBarEvent {

        public static List<Action> Actions = new();
        
        public static void AddAction(Action action) {
            Actions.Add(action);
        }

        public static void Invoke() {
            Actions.ForEach(a => a.Invoke());
        }
    }
}