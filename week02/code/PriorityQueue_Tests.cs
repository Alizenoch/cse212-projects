using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Three items with different priorities are added to the queue.
    // Expected Result: The item with the highest priority is dequeued first.
    // Defect(s) Found: The original code did not check the last item in the queue when searching for the highest priority.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Bob", 1);
        priorityQueue.Enqueue("Tim", 2);
        priorityQueue.Enqueue("Sue", 5);

        Assert.AreEqual("Sue", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Two items are added and the highest-priority item is dequeued.
    // Expected Result: The dequeued item is removed from the queue, so the next dequeue returns the remaining item.
    // Defect(s) Found: The original code returned the highest-priority item but did not remove it from the queue.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Bob", 3);
        priorityQueue.Enqueue("Tim", 2);

        Assert.AreEqual("Bob", priorityQueue.Dequeue());
        Assert.AreEqual("Tim", priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.
}