using System.Collections.Generic;
using DynamicArray.Src.Enums;
using FluentAssertions;
using Queue.Src;
using Xbehave;
using OperationStatus = Queue.Src.Enums.OperationStatus;

namespace ModelsTests
{
    public class QueueModelScenarios
    {
        [Scenario]
        public void When_QueueIsNotInitializedAnyOperationsMustSetInitializationStatusToNotInit()
        {
            QueueModel<string> queue = new QueueModel<string>();
            OperationStatus status1 = OperationStatus.Ok;
            OperationStatus status2 = OperationStatus.Ok; 
            OperationStatus status3 = OperationStatus.Ok;
            OperationStatus status4 = OperationStatus.Ok;

            "When user tries to perform different actions with that queue"
                .x(() =>
                {
                    queue.Enqueue("10");
                    status1 = queue.LastEnqueueStatus();
                    queue.Dequeue();
                    status2 = queue.LastDequeueStatus();
                    queue.PeekHeadItem();
                    status3 = queue.LastPeekStatus();
                    queue.Size();
                    status4 = queue.LastGetSizeStatus();
                });

            "Then statuses of that operations should be 'Not initialized'"
                .x(() =>
                {
                    status1.Should().Be(OperationStatus.NotInitialized);
                    status2.Should().Be(OperationStatus.NotInitialized);
                    status3.Should().Be(OperationStatus.NotInitialized);
                    status4.Should().Be(OperationStatus.NotInitialized);
                });
        }

        [Scenario]
        public void User_CanAddItemToInitializedQueue()
        {
            QueueModel<string> queue = null;

            "Given there is an initialized queue with 3 items"
                .x(() =>
                {
                    queue = new QueueModel<string>();
                    queue.Initialize();
                    // items
                    queue.Enqueue("Item new");
                    queue.Enqueue("Item new two");
                    queue.Enqueue("Item new three");
                });

            "When user adds new item to queue"
                .x(() =>
                {
                    queue.Enqueue("Add item again");
                });

            "Then count of such queue should be 4"
                .x(() =>
                {
                    var count = queue.Size();

                    count.Should().Be(4);
                });
            "And last enqueue status should be 'Ok'"
                .x(() =>
                {
                    var status = queue.LastEnqueueStatus();

                    status.Should().Be(OperationStatus.Ok);
                });
        }

        [Scenario]
        public void User_CanDequeueFromNotEmptyQueue()
        {
            QueueModel<string> queue = null;

            "Given there is an initialized queue with 3 items"
                .x(() =>
                {
                    queue = new QueueModel<string>();
                    queue.Initialize();
                    // items
                    queue.Enqueue("Item new");
                    queue.Enqueue("Item new two");
                    queue.Enqueue("Item new three");
                });

            "When user requests dequeue operation"
                .x(() =>
                {
                    queue.Dequeue();
                });

            "Then count of such queue should be 2"
                .x(() =>
                {
                    var count = queue.Size();

                    count.Should().Be(2);
                });
            "And last dequeue status should be 'Ok'"
                .x(() =>
                {
                    var status = queue.LastDequeueStatus();

                    status.Should().Be(OperationStatus.Ok);
                });
        }

        [Scenario]
        public void User_CanNotDequeueFromEmptyQueue()
        {
            QueueModel<string> queue = null;

            "Given there is an initialized queue with 0 items"
                .x(() =>
                {
                    queue = new QueueModel<string>();
                    queue.Initialize();
                });

            "When user requests dequeue operation"
                .x(() =>
                {
                    queue.Dequeue();
                });

            "Then last dequeue status should be 'Error'"
                .x(() =>
                {
                    var status = queue.LastDequeueStatus();

                    status.Should().Be(OperationStatus.Error);
                });
        }

        [Scenario]
        public void User_CanNotPeekItemFromEmptyQueue()
        {
            QueueModel<string> queue = null;

            "Given there is an initialized queue with 0 items"
                .x(() =>
                {
                    queue = new QueueModel<string>();
                    queue.Initialize();
                });

            "When user requests peek operation"
                .x(() =>
                {
                    queue.PeekHeadItem();
                });

            "Then last peek status should be 'Error'"
                .x(() =>
                {
                    var status = queue.LastPeekStatus();

                    status.Should().Be(OperationStatus.Error);
                });
        }

        [Scenario]
        public void User_CanPeekItemWhichWillBeDeletedFirst()
        {
            QueueModel<int> queue = null;
            var peekItem = 0;

            "Given there is an initialized queue with 3 items: 1000, 500, -100"
                .x(() =>
                {
                    queue = new QueueModel<int>();
                    queue.Initialize();
                    // items
                    queue.Enqueue(1000);
                    queue.Enqueue(500);
                    queue.Enqueue(-100);

                });

            "When user requests peek operation"
                .x(() =>
                {
                    peekItem = queue.PeekHeadItem();
                });

            "Then received peek item should be equal to 1000"
                .x(() =>
                {
                    peekItem.Should().Be(1000);
                });
            "And last peek status should be 'Ok'"
                .x(() =>
                {
                    var status = queue.LastPeekStatus();

                    status.Should().Be(OperationStatus.Ok);
                });
        }

        [Scenario]
        public void When_UserMakeFewOperationsQueueItemsMustBeReceived()
        {
            QueueModel<int> queue = null;

            "Given there is an initialized queue with 5 items: 1, 2, 3, 4, 5"
                .x(() =>
                {
                    queue = new QueueModel<int>();
                    queue.Initialize();
                    // items
                    queue.Enqueue(1);
                    queue.Enqueue(2);
                    queue.Enqueue(3);
                    queue.Enqueue(4);
                    queue.Enqueue(5);

                });

            "When user produce two deque operations"
                .x(() =>
                {
                    queue.Dequeue();
                    queue.Dequeue();
                });

            "Then queue should contain only 3 4 5 items"
                .x(() =>
                {
                    var items = queue.GetItems();

                    items.Should().BeEquivalentTo(new List<int>() {3, 4, 5});
                });
         
        }
    }
}
