using System;
using System.Collections.Generic;

namespace SEP3UI.Data {
    public static class NavBarEvent {
        private static readonly IList<Action> actions = new List<Action>();
        
        public static void AddAction(Action action) {
            actions.Add(action);
        }

        public static void Invoke() {
            foreach (Action action in actions) {
                action.Invoke();
            }
        }
    }
}