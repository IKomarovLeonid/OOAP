using System.Collections.Generic;
using FluentAssertions;
using Queues;
using Queues.Enums;
using Xbehave;

namespace ModelsTests
{
    public class DequeModelScenarios
    {
        [Scenario]
        public void When_DequeIsNotInitializedAnyOperation_MustSetNotInitializedError()
        {
            DequeModel<int> deque = null;
            OperationStatus status1 = OperationStatus.Ok;
            OperationStatus status2 = OperationStatus.Ok;
            OperationStatus status3 = OperationStatus.Ok;
            OperationStatus status4 = OperationStatus.Ok;
            OperationStatus status5 = OperationStatus.Ok;
            OperationStatus status6 = OperationStatus.Ok;
            OperationStatus status7 = OperationStatus.Ok;

            "Given deque which is not initialized"
                .x(() =>
                {
                    deque = new DequeModel<int>();
                });

            "When user tries to use deque operations"
                .x(() =>
                {
                    deque.Enqueue(10);
                    status1 = deque.LastEnqueueStatus();
                    deque.Dequeue();
                    status2 = deque.LastDequeueStatus();
                    deque.PeekHeadItem();
                    status3 = deque.LastPeekStatus();
                    deque.Size();
                    status4 = deque.LastGetSizeStatus();
                    deque.DequeueLast();
                    status5 = deque.DequeueLastStatus();
                    deque.EnqueueFirst(12);
                    status6 = deque.LastEnqueueFirstStatus();
                    deque.PeekLastItem();
                    status7 = deque.LastPeekStatus();
                });

            "Then all statuses should return 'Not initialized'"
                .x(() =>
                {
                    status1.Should().Be(OperationStatus.NotInitialized);
                    status2.Should().Be(OperationStatus.NotInitialized);
                    status3.Should().Be(OperationStatus.NotInitialized);
                    status4.Should().Be(OperationStatus.NotInitialized);
                    status5.Should().Be(OperationStatus.NotInitialized);
                    status6.Should().Be(OperationStatus.NotInitialized);
                    status7.Should().Be(OperationStatus.NotInitialized);
                    
                });
        }

        [Scenario]
        public void When_DequeHasCountZero_Peek_ShouldReturnError()
        {
            DequeModel<int> deque = null;
            OperationStatus status1 = OperationStatus.Ok;
            OperationStatus status2 = OperationStatus.Ok;

            "Given there is an deque model with zero items count"
                .x(() =>
                {
                    deque = new DequeModel<int>();
                    deque.Initialize();
                });

            "When user tries to peek items from empty deque"
                .x(() =>
                {
                    deque.PeekLastItem();
                    status1 = deque.LastPeekStatus();
                    deque.PeekHeadItem();
                    status2 = deque.LastPeekStatus();
                });

            "Then last peek status should be error"
                .x(() =>
                {
                    status1.Should().Be(OperationStatus.Error);
                    status2.Should().Be(OperationStatus.Error);
                });
        }

        [Scenario]
        public void When_DequeIsInitializedEnqueueFirst_Must_AddItem()
        {

            DequeModel<int> deque = null;

            "Given there is an deque model with items 3,4 and 5"
                .x(() =>
                {
                    deque = new DequeModel<int>();
                    deque.Initialize();
                    // items 
                    deque.Enqueue(3);
                    deque.Enqueue(4);
                    deque.Enqueue(5);
                });

            "When user uses enqueue first operation and adds item 10"
                .x(() =>
                {
                    deque.EnqueueFirst(10);
                });

            "Then deque items should be 10,3,4,5"
                .x(() =>
                {
                    var items = deque.GetItems();

                    items.Should().BeEquivalentTo(new List<int>() {10, 3, 4, 5});
                });

            "And enqueue first operation should have status 'Ok'"
                .x(() =>
                {
                    var status = deque.LastEnqueueFirstStatus();

                    status.Should().Be(OperationStatus.Ok);
                });
        }

        [Scenario]
        public void When_DequeIsNotEmptyPeekLastItemShouldReturnItemFromTail()
        {
            DequeModel<int> deque = null;
            var item = 0;

            "Given there is an deque model with items 3,4 and 5"
                .x(() =>
                {
                    deque = new DequeModel<int>();
                    deque.Initialize();
                    // items 
                    deque.Enqueue(3);
                    deque.Enqueue(4);
                    deque.Enqueue(5);
                });

            "When user peeks item from tail of deque"
                .x(() =>
                {
                    item = deque.PeekLastItem();
                });

            "Then item should be equal to 5"
                .x(() =>
                {
                    item.Should().Be(5);
                });

            "And peek operation should have status 'Ok'"
                .x(() =>
                {
                    var status = deque.LastPeekStatus();

                    status.Should().Be(OperationStatus.Ok);
                });
        }

        [Scenario]
        public void When_DequeIsNotEmptyDequeueLastItemShouldRemoveItemFromTail()
        {
            DequeModel<int> deque = null;

            "Given there is an deque model with items 3,4 and 5"
                .x(() =>
                {
                    deque = new DequeModel<int>();
                    deque.Initialize();
                    // items 
                    deque.Enqueue(3);
                    deque.Enqueue(4);
                    deque.Enqueue(5);
                });

            "When user dequeue item from tail"
                .x(() =>
                {
                    deque.DequeueLast();
                });

            "Then deque should have items 3 and 4"
                .x(() =>
                {
                    var items = deque.GetItems();

                    items.Should().BeEquivalentTo(new List<int>() {3, 4});
                });

            "And peek operation should have status 'Ok'"
                .x(() =>
                {
                    var status = deque.DequeueLastStatus();

                    status.Should().Be(OperationStatus.Ok);
                });
        }
    }
}
