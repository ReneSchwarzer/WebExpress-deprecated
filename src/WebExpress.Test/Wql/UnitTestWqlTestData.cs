using System;
using System.Collections.Generic;

namespace WebExpress.Test.Wql
{
    internal class UnitTestWqlTestData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Salutation { get; set; }
        public string Address { get; set; }

        public static List<UnitTestWqlTestData> GenerateTestData()
        {
            var testDataList = new List<UnitTestWqlTestData>
            {
                new UnitTestWqlTestData
                {
                    FirstName = "Emma",
                    LastName = "Smith",
                    PhoneNumber = "123-456-7890",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Salutation = "Ms.",
                    Address = "123 Main St."
                },
                new UnitTestWqlTestData
                {
                    FirstName = "Liam",
                    LastName = "Johnson",
                    PhoneNumber = "234-567-8901",
                    DateOfBirth = new DateTime(1985, 2, 2),
                    Salutation = "Mr.",
                    Address = "456 Elm St."
                },
                new UnitTestWqlTestData
                {
                    FirstName = "Olivia",
                    LastName = "Williams",
                    PhoneNumber = "523-446-9890",
                    DateOfBirth = new DateTime(1991, 1, 16),
                    Salutation = "Ms.",
                    Address = "923 Main St."
                },
                new UnitTestWqlTestData
                {
                    FirstName = "Noah",
                    LastName = "Jones",
                    PhoneNumber = "834-517-8933",
                    DateOfBirth = new DateTime(1975, 8, 2),
                    Salutation = "Mr.",
                    Address = "756 Elm St."
                },
                new UnitTestWqlTestData
                {
                    FirstName = "Isabella",
                    LastName = "Miller",
                    PhoneNumber = "342-983-9256",
                    DateOfBirth = new DateTime(1961, 8, 6),
                    Salutation = "Ms.",
                    Address = "154 Main St."
                },
                new UnitTestWqlTestData
                {
                    FirstName = "Sophia",
                    LastName = "Brown",
                    PhoneNumber = "823-406-7821",
                    DateOfBirth = new DateTime(1979, 7, 13),
                    Salutation = "Ms.",
                    Address = "456 Elm St."
                },
                new UnitTestWqlTestData
                {
                    FirstName = "Xantia",
                    LastName = "Garcia",
                    PhoneNumber = "345-093-9352",
                    DateOfBirth = new DateTime(1981, 6, 30),
                    Salutation = "Mr.",
                    Address = "14 Maple Ave."
                },
                new UnitTestWqlTestData
                {
                    FirstName = "Ava",
                    LastName = "Martinez",
                    PhoneNumber = "345-093-9352",
                    DateOfBirth = new DateTime(1961, 9, 20),
                    Salutation = "Ms.",
                    Address = "154 Oak Ave."
                },
                new UnitTestWqlTestData
                {
                    FirstName = "James",
                    LastName = "Davis",
                    PhoneNumber = "937-482-8352",
                    DateOfBirth = new DateTime(1952, 3, 15),
                    Salutation = "Mr.",
                    Address = "536 Pine St."
                },
                new UnitTestWqlTestData
                {
                    FirstName = "Noah",
                    LastName = "Smith",
                    PhoneNumber = "631-378-2956",
                    DateOfBirth = new DateTime(1952, 3, 15),
                    Salutation = "Mr.",
                    Address = "756 Elm St."
                },
                new UnitTestWqlTestData
                {
                    FirstName = "Isabella",
                    LastName = "Johnson",
                    PhoneNumber = "245-5239-3325",
                    DateOfBirth = new DateTime(1955, 6, 13),
                    Salutation = "Ms.",
                    Address = "154 Main St."
                }
            };

            // Add more test data here
            for (int i = 0; i < 100; i++)
            {
                testDataList.Add(new UnitTestWqlTestData
                {
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}",
                    PhoneNumber = $"{i:000}-000-0000",
                    DateOfBirth = new DateTime(2000, 1, 1).AddDays(i),
                    Salutation = i % 2 == 0 ? "Mr." : "Ms.",
                    Address = $"{i} Main St."
                });
            }

            return testDataList;
        }
    }
}
