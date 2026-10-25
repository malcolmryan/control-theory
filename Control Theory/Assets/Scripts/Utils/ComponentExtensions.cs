/**
 * Extension methods for the Object class
 *
 * Author: Malcolm Ryan
 * Version: 1.0
 * For Unity Version: 6.3
 */
 
using UnityEngine;
using System;

namespace WordsOnPlay.Utils {
	public static class ComponentExtensions
    {
        public static T GetComponent<T>(this Component o, bool throwIfNull) {

            T component = o.GetComponent<T>();

            if (throwIfNull && component == null)
            {
                throw new NullReferenceException($"{o.gameObject.name} missing expected component of type {typeof(T).Name}");
            }

            return component;
        }
    }
}