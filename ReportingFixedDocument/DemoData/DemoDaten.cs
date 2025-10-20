//-----------------------------------------------------------------------
// <copyright file="DemoDaten.cs" company="Lifeprojects.de">
//     Class: DemoDaten
//     Copyright © Lifeprojects.de 2025
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>17.10.2025 09:01:37</date>
//
// <summary>
// Klasse für 
// </summary>
//-----------------------------------------------------------------------

namespace ReportingFixedDocument
{
    using System.Collections.Generic;

    public class DemoDaten
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DemoDaten"/> class.
        /// </summary>
        public DemoDaten()
        {
        }

        public static List<MyItem> Create()
        {
            List<MyItem> lvSource = new List<MyItem>();
            lvSource.Add(new MyItem { Id = 1, Vorname = "Gerhard", Nachname = "Ahrens" });
            lvSource.Add(new MyItem { Id = 2, Vorname = "PTA", Nachname = "GmbH" });
            lvSource.Add(new MyItem { Id = 41, Vorname = "Charlie", Nachname = "Hund" });
            lvSource.Add(new MyItem { Id = 42, Vorname = "Charlie", Nachname = "Brown" });
            lvSource.Add(new MyItem { Id = 43, Vorname = "Snoopy", Nachname = string.Empty });
            lvSource.Add(new MyItem { Id = 44, Vorname = "Woodstock", Nachname = string.Empty });
            lvSource.Add(new MyItem { Id = 45, Vorname = "Donald", Nachname = "Duck" });
            lvSource.Add(new MyItem { Id = 46, Vorname = "Dagobert", Nachname = "Duck" });
            lvSource.Add(new MyItem { Id = 47, Vorname = "Micky", Nachname = "Maus" });
            lvSource.Add(new MyItem { Id = 48, Vorname = "Minnie", Nachname = "Maus" });
            lvSource.Add(new MyItem { Id = 49, Vorname = "Daisy", Nachname = "Duck" });
            lvSource.Add(new MyItem { Id = 50, Vorname = "Daniel", Nachname = "Düsentrieb" });
            lvSource.Add(new MyItem { Id = 51, Vorname = "Goofy", Nachname = string.Empty });
            lvSource.Add(new MyItem { Id = 52, Vorname = "Kater", Nachname = "Karlo", Aktiv = false });
            lvSource.Add(new MyItem { Id = 53, Vorname = "Gustav", Nachname = "Gans" });
            lvSource.Add(new MyItem { Id = 54, Vorname = "Dorette", Nachname = "Duck" });
            lvSource.Add(new MyItem { Id = 55, Vorname = "Primus von", Nachname = "Quack" });
            lvSource.Add(new MyItem { Id = 56, Vorname = "Dussel", Nachname = "Duck" });
            lvSource.Add(new MyItem { Id = 57, Vorname = "Klaas", Nachname = "Klever" });
            lvSource.Add(new MyItem { Id = 58, Vorname = "Gundel", Nachname = "Gaukeley" });
            lvSource.Add(new MyItem { Id = 59, Vorname = "Die Panzerknacker", Nachname = string.Empty });

            return lvSource;
        }
    }

    public class MyItem
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public bool Aktiv { get; set; } = true;
    }
}
