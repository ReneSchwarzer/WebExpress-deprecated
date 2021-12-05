﻿using System;

namespace WebExpress.WebTask
{
    /// <summary>
    /// Verwaltung von Ad-hoc-Aufgaben
    /// </summary>
    public static class TaskManager
    {
        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Jobs gelistet sind
        /// </summary>
        private static TaskDictionary Dictionary { get; } = new TaskDictionary();

        /// <summary>
        /// Prüft ob eine Aufgebe bereits erstellt wurde
        /// </summary>
        /// <param name="id">Die ID der Aufgabe</param>
        /// <returns>True wenn es diese Aufgabe schon gibt, false sonst</returns>
        public static bool ContainsTask(string id)
        {
            return Dictionary.ContainsKey(id?.ToLower());
        }

        /// <summary>
        /// Liefert eine bestehende Aufgabe
        /// </summary>
        /// <param name="id">Die ID der Aufgabe</param>
        /// <returns>Die Aufgabe oder null</returns>
        public static ITask GetTask(string id)
        {
            if (Dictionary.ContainsKey(id?.ToLower()))
            {
                return Dictionary[id?.ToLower()];
            }

            return null;
        }

        /// <summary>
        /// Erstellt eine neue Aufgabe oder leifert eine bestehende Aufgabe zurück
        /// </summary>
        /// <param name="id">Die ID der Aufgabe</param>
        /// <param name="handler">Der Eventhandler</param>
        /// <param name="args">Eventargumente</param>
        /// <returns>Die Aufgabe</returns>
        public static ITask CreateTask(string id, EventHandler<TaskEventArgs> handler, params object[] args)
        {
            return CreateTask<Task>(id, handler, args);
        }

        /// <summary>
        /// Erstellt eine neue Aufgabe oder leifert eine bestehende Aufgabe zurück
        /// </summary>
        /// <param name="id">Die ID der Aufgabe</param>
        /// <param name="handler">Der Eventhandler</param>
        /// <param name="args">Eventargumente</param>
        /// <returns>Die Aufgabe</returns>
        public static ITask CreateTask<T>(string id, EventHandler<TaskEventArgs> handler, params object[] args) where T : Task, new()
        {
            var key = id?.ToLower();

            if (!Dictionary.ContainsKey(id))
            {
                var task = new Task() { ID = id, State = TaskState.Created, Arguments = args };
                Dictionary.Add(key, task);

                task.Initialization();

                task.Process += handler;

                return task;
            }

            return Dictionary[id];
        }
    }
}