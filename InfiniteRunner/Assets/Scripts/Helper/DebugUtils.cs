#define DEBUG

using UnityEngine;

/// <summary>
/// A utility class to handle all debug calls
/// Easier to remove all debug calls from code by removing debug defination
/// </summary>

namespace Utils
{
    public static class DebugUtils
    {

#if DEBUG

        /// <summary>
        /// Logs state machine related messages
        /// </summary>
        /// <param name="message"></param>
        public static void LogState(string message)
        {
            Debug.Log(message);
        }


        /// <summary>
        /// Normal log messages
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            Debug.Log(message);
        }


        /// <summary>
        /// Logs error messages
        /// </summary>
        /// <param name="error"></param>
        public static void LogError(string error)
        {
            Debug.LogError(error);
        }


        /// <summary>
        /// Logs warning messages
        /// </summary>
        /// <param name="warning"></param>
        public static void LogWarning(string warning)
        {
            Debug.LogWarning(warning);
        }

#endif
    }

}
