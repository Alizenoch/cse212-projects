using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TakingTurnsQueueTests
{
    [TestMethod]
    // Scenario: Three players with finite turn counts are added to the queue.
    // Expected Result: Players take turns in round-robin order until each player's turns are exhausted and the queue becomes empty.
    // Defect(s) Found: None.
    public void TestTakingTurnsQueue_FiniteRepetition()
    {
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = { bob, tim, sue, bob, tim, sue, tim, sue, tim, tim };

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        int i = 0;
        while (players.Length > 0)
        {
            if (i >= expectedResult.Length)
                Assert.Fail("Queue should have ran out of items by now.");

            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
            i++;
        }
    }

    [TestMethod]
    // Scenario: A new player is added after the queue has already started processing turns.
    // Expected Result: The new player joins the queue correctly and receives the expected number of turns in the proper order.
    // Defect(s) Found: None.
    public void TestTakingTurnsQueue_AddPlayerMidway()
    {
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);
        var george = new Person("George", 3);

        Person[] expectedResult =
        {
            bob, tim, sue, bob, tim, sue, tim, george, sue, tim, george, tim, george
        };

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        int i = 0;
        for (; i < 5; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        players.AddPerson("George", 3);

        while (players.Length > 0)
        {
            if (i >= expectedResult.Length)
                Assert.Fail("Queue should have ran out of items by now.");

            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
            i++;
        }
    }

    [TestMethod]
    // Scenario: A player with zero turns (representing infinite turns) is added to the queue.
    // Expected Result: The player remains in the queue after all finite-turn players have finished.
    // Defect(s) Found: None.
    public void TestTakingTurnsQueue_ForeverZero()
    {
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 0);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = { bob, tim, sue, bob, tim, sue, tim, sue, tim, tim };

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        for (int i = 0; i < expectedResult.Length; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        var infinitePerson = players.GetNextPerson();
        Assert.AreEqual(0, infinitePerson.Turns);
    }

    [TestMethod]
    // Scenario: A player with a negative turn count (representing infinite turns) is added to the queue.
    // Expected Result: The player continues receiving turns after all finite-turn players have completed theirs.
    // Defect(s) Found: None.
    public void TestTakingTurnsQueue_ForeverNegative()
    {
        var tim = new Person("Tim", -3);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = { tim, sue, tim, sue, tim, sue, tim, tim, tim, tim };

        var players = new TakingTurnsQueue();
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        for (int i = 0; i < expectedResult.Length; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        var infinitePerson = players.GetNextPerson();
        Assert.AreEqual(-3, infinitePerson.Turns);
    }

    [TestMethod]
    // Scenario: Attempt to retrieve a player from an empty queue.
    // Expected Result: An InvalidOperationException is thrown.
    // Defect(s) Found: None.
    public void TestTakingTurnsQueue_Empty()
    {
        var players = new TakingTurnsQueue();
        Assert.ThrowsException<InvalidOperationException>(() => players.GetNextPerson());
    }
}