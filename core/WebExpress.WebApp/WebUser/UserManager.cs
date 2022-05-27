﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebApp.WebUser
{
    public static class UserManager
    {
        /// <summary>
        /// Liefert oder setzt den Verweis auf Kontext des Hostes
        /// </summary>
        private static IHttpServerContext Context { get; set; }

        /// <summary>
        /// Liefert oder setzt das Wurzelverzeichnis, indem sich die Benutzerdatendaten befinden
        /// </summary>
        private static string UserRootPath { get; set; }

        /// <summary>
        /// Liefert die registrierten Users
        /// </summary>
        public static ICollection<User> Users { get; } = new List<User>();

        /// <summary>
        /// Liefert die registrierten Gruppen
        /// </summary>
        public static ICollection<Group> Groups { get; } = new List<Group>();


        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Verweis auf den Kontext des Hostes</param>
        internal static void Initialization(IHttpServerContext context)
        {
            Context = context;

            UserRootPath = Path.Combine(context.DataPath, "usermanager");

            LoadGroup();
            LoadUser();

            Context.Log.Info(message: I18N("webexpress.webapp:usermanager.initialization"));
        }

        /// <summary>
        /// Lade alle Users
        /// </summary>
        private static void LoadUser()
        {
            var path = Path.Combine(UserRootPath, "users");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var files = Directory.GetFiles(path, "*.xml");
            var serializer = new XmlSerializer(typeof(User));
            foreach (var file in files)
            {
                try
                {
                    using var reader = File.OpenText(file);
                    Users.Add(serializer.Deserialize(reader) as User);
                }
                catch (Exception ex)
                {
                    Context.Log.Exception(ex);
                }
            }
        }

        /// <summary>
        /// Nimmt einen neuen User auf
        /// </summary>
        /// <param name="user">Der neue User</param>
        public static void AddUser(User user)
        {
            UpdateUser(user);

            Users.Add(user);
        }

        /// <summary>
        /// Aktualisiert ein bestehenden User 
        /// </summary>
        /// <param name="user">Der bestehende User</param>
        public static void UpdateUser(User user)
        {
            var serializer = new XmlSerializer(typeof(User));
            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);

            user.Updated = DateTime.Now;

            using (var memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, user, xmlns);

                var utf = new UTF8Encoding();
                var fileName = Path.Combine(UserRootPath, "users", string.Format("{0}.xml", user.ID));

                if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                }

                File.WriteAllText
                (
                    fileName,
                    utf.GetString(memoryStream.ToArray())
                );

                Context.Log.Info(message: I18N("webexpress.webapp:usermanager.user.save"), args: $"{user.Lastname}, {user.Firstname}");
            }
        }

        /// <summary>
        /// Löscht einen Benutzer 
        /// </summary>
        /// <param name="user">Der zu löschende Benutzer</param>
        public static void RemoveUser(User user)
        {
            var fileName = Path.Combine(UserRootPath, "users", string.Format("{0}.xml", user.ID));

            Users.Remove(user);
            File.Delete(fileName);

            Context.Log.Info(message: I18N("webexpress.webapp:usermanager.user.delete"), args: user.Login);
        }

        /// <summary>
        /// Lade alle Gruppen 
        /// </summary>
        private static void LoadGroup()
        {
            var path = Path.Combine(UserRootPath, "groups");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var files = Directory.GetFiles(path, "*.xml");
            var serializer = new XmlSerializer(typeof(Group));
            foreach (var file in files)
            {
                try
                {
                    using var reader = File.OpenText(file);
                    Groups.Add(serializer.Deserialize(reader) as Group);
                }
                catch (Exception ex)
                {
                    Context.Log.Exception(ex);
                }
            }
        }

        /// <summary>
        /// Nimmt einen neuen Gruppe auf
        /// </summary>
        /// <param name="group">Die neue Gruppe</param>
        public static void AddGroup(Group group)
        {
            UpdateGroup(group);

            Groups.Add(group);
        }

        /// <summary>
        /// Aktualisiert ein bestehenden Gruppe 
        /// </summary>
        /// <param name="group">Die bestehende Gruppe</param>
        public static void UpdateGroup(Group group)
        {
            var serializer = new XmlSerializer(typeof(Group));
            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);

            group.Updated = DateTime.Now;

            using (var memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, group, xmlns);

                var utf = new UTF8Encoding();
                var fileName = Path.Combine(UserRootPath, "groups", string.Format("{0}.xml", group.ID));

                if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                }

                File.WriteAllText
                (
                    fileName,
                    utf.GetString(memoryStream.ToArray())
                );

                Context.Log.Info(message: I18N("webexpress.webapp:usermanager.group.save"), args: group.Name);
            }
        }

        /// <summary>
        /// Löscht eine Gruppe 
        /// </summary>
        /// <param name="group">Die zu löschende Gruppe</param>
        public static void RemoveGroup(Group group)
        {
            var fileName = Path.Combine(UserRootPath, "groups", string.Format("{0}.xml", group.ID));

            Parallel.ForEach(Users, i =>
            {
                if (i.GroupIDs.Contains(group?.ID))
                {
                    i.GroupIDs.Remove(group?.ID);
                    UpdateUser(i);
                }
            });

            Groups.Remove(group);
            File.Delete(fileName);

            Context.Log.Info(message: I18N("webexpress.webapp:usermanager.group.delete"), args: group.Name);
        }
    }
}
