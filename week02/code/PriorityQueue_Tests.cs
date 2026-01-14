using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue a single item and dequeue it
    // Expected Result: Returns the item that was added
    // Defect(s) Found: None yet
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 5);
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("first", result);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with different priorities and dequeue the highest priority
    // Expected Result: Returns the item with the highest priority (priority 10)
    // Defect(s) Found: None
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 2);
        priorityQueue.Enqueue("high", 10);
        priorityQueue.Enqueue("medium", 5);
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("high", result);
    }

    [TestMethod]
    // Scenario: Enqueue items and dequeue multiple times to verify correct priority ordering
    // Expected Result: Dequeues return items in order of priority: 10, 5, 2
    // Defect(s) Found: Loop condition skips last element (index < _queue.Count - 1 should be index < _queue.Count); Item with highest priority was not being removed from queue
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 2);
        priorityQueue.Enqueue("high", 10);
        priorityQueue.Enqueue("medium", 5);
        
        Assert.AreEqual("high", priorityQueue.Dequeue());
        Assert.AreEqual("medium", priorityQueue.Dequeue());
        Assert.AreEqual("low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the same highest priority
    // Expected Result: Items with the same priority are dequeued in FIFO order (first added, first removed)
    // Defect(s) Found: FIFO logic broken - uses >= instead of > causing incorrect item selection; Item not removed from queue
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 5);
        priorityQueue.Enqueue("second", 5);
        priorityQueue.Enqueue("third", 5);
        
        Assert.AreEqual("first", priorityQueue.Dequeue());
        Assert.AreEqual("second", priorityQueue.Dequeue());
        Assert.AreEqual("third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items with same priority mixed with different priorities
    // Expected Result: When multiple items have the same highest priority, the one closest to front is removed first
    // Defect(s) Found: Uses >= comparison which always updates to latest index; should use > to keep first occurrence; Item not removed from queue
    public void TestPriorityQueue_5()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first_priority_5", 5);
        priorityQueue.Enqueue("second_priority_5", 5);
        priorityQueue.Enqueue("low", 2);
        
        Assert.AreEqual("first_priority_5", priorityQueue.Dequeue());
        Assert.AreEqual("second_priority_5", priorityQueue.Dequeue());
        Assert.AreEqual("low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Test that the last element in the queue is properly checked for priority
    // Expected Result: The item at the end of the queue can be dequeued if it has the highest priority
    // Defect(s) Found: Loop bounds error - index < _queue.Count - 1 prevents checking the last element of the queue; Item not removed
    public void TestPriorityQueue_6()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 2);
        priorityQueue.Enqueue("medium", 5);
        priorityQueue.Enqueue("highest", 10);  // Added at the end
        
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("highest", result);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    // Scenario: Attempt to dequeue from an empty queue
    // Expected Result: Throws InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None
    public void TestPriorityQueue_7()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Dequeue();
    }

    [TestMethod]
    // Scenario: Verify the exact exception message when queue is empty
    // Expected Result: Exception message is exactly "The queue is empty."
    // Defect(s) Found: None
    public void TestPriorityQueue_8()
    {
        var priorityQueue = new PriorityQueue();
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
        }
    }
}