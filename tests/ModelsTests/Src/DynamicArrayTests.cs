using System;
using DynamicArray;
using DynamicArray.Enums;
using DynamicArray.Src;
using DynamicArray.Src.Enums;
using FluentAssertions;
using Xbehave;

namespace ModelsTests
{
    public class DynamicArrayTests
    {
        [Scenario]
        public void User_CanInitializeArrayWithDefaultCapacity()
        {
            DynamicArray<int> array = null;

            "When user tries to create new dynamic array model with default capacity 16 items"
                .x(() =>
                {
                    array = new DynamicArray<int>();
                    array.SetCapacity();
                });

            "Then system must initialize array with default capacity"
                .x(() =>
                {
                    var capacity = array.GetCapacity();
                    var isInitialized = array.IsInitialized();

                    capacity.Should().Be(16);
                    isInitialized.Should().BeTrue();
                });
        }

        [Example(0)]
        [Example(2000000)]
        [Scenario]
        public void User_CanInitializeArrayWithCustomCapacity(int capacity)
        {
            DynamicArray<int> array = null;

            $"When user tries to create new dynamic array model with custom capacity '{capacity}' items"
                .x(() =>
                {
                    array = new DynamicArray<int>();
                    array.SetCapacity(capacity: capacity);
                });

            $"Then system must initialize array with '{capacity}' capacity"
                .x(() =>
                {
                    var arrayCap = array.GetCapacity();
                    var isInitialized = array.IsInitialized();

                    arrayCap.Should().Be(capacity);
                    isInitialized.Should().BeTrue();
                });
        }

        [Scenario]
        public void When_UserNotInitializeArrayAnyOperationMustSetStatusToArrayNotInitialized()
        {
            DynamicArray<int> array = null;
            Action setAction = null, getAction = null, removeAction = null, moveAction = null;

            $"Given dynamic array model with default capacity '16' items"
                .x(() =>
                {
                    array = new DynamicArray<int>();
                });

            "When user tries to produce any array action"
                .x(() =>
                {
                    setAction = () => array.SetCursor(1);
                    getAction = () => array.GetItem();
                    removeAction = () => array.Remove();
                    moveAction = () => array.MoveCursorNext();
                });

            "Then such array's last operations status must return 'array not initialized'"
                .x(() =>
                {
                    getAction.Invoke();
                    var getStatus = array.LastGetItemStatus();
                    getStatus.Should().Be(OperationStatus.ArrayNotInitialized);
                    // remove
                    removeAction.Invoke();
                    var removeStatus = array.LastRemoveStatus();
                    removeStatus.Should().Be(OperationStatus.ArrayNotInitialized);
                    // move action 
                    moveAction.Invoke();
                    var moveStatus = array.LastMoveCursorOperation();
                    moveStatus.Should().Be(OperationStatus.ArrayNotInitialized);
                    // set operation
                    setAction.Invoke();
                    var setStatus = array.LastSetCursorStatus();
                    setStatus.Should().Be(OperationStatus.ArrayNotInitialized);
                });
        }

        [Scenario]
        public void User_CanRequestCountOfInitializedArray()
        {
            DynamicArray<int> array = null;
            var count = 0;

            $"Given dynamic array model with default capacity '16' and two items"
                .x(() =>
                {
                    array = new DynamicArray<int>();
                    array.SetCapacity(16);
                    array.SetCursor(0);
                    array.SetItem(5);
                    array.SetCursor(10);
                    array.SetItem(-4);
                });

            "When user tries to request array count of items"
                .x(() =>
                {
                    count = array.GetItemsCount();
                });

            "Then array must represent count equal to '2'"
                .x(() =>
                {
                    count.Should().Be(2);
                });

        }

        [Scenario]
        public void User_CanAddItemInTailOfArrayModel()
        {
            DynamicArray<int> array = null;

            "Given there is an array model with first items 3,4"
                .x(() =>
                {
                    array = new DynamicArray<int>();
                    array.SetCapacity(16);
                    array.SetCursor(0);
                    array.SetItem(3);
                    array.MoveCursorNext();
                    array.SetItem(4);
                });

            "When user invoke operation add in tail with new item 10"
                .x(() =>
                {
                    array.AddLast(10);
                    // set cursor to index 2
                    array.SetCursor(2);
                });

            "Then new item '10' must be placed to index '2'"
                .x(() =>
                {
                    var item = array.GetItem();

                    item.Should().Be(10);
                });
        }

        [Scenario]
        public void ArrayDoNotResizeIfNewCountEqualsCapacity()
        {
            DynamicArray<int> array = null;

            "Given there is an array model with 16 capacity and 15 items"
                .x(() =>
                {
                    array = new DynamicArray<int>();
                    array.SetCapacity(16);
                    // generate 
                    for(int index = 0; index < 15; index++) array.AddLast(index);
                });

            "When user adds 16's item in array's tail"
                .x(() =>
                {
                    array.AddLast(1000);
                });

            "Then array's count should be equal to array's capacity and no resize happens"
                .x(() =>
                {
                    var count = array.GetItemsCount();
                    var capacity = array.GetCapacity();
                    var resizeStatus = array.ResizeStatus();

                    count.Should().Be(capacity);
                    resizeStatus.Should().Be(ResizeStatus.NoChanges);
                });
        }

        [Example(16, 32)]
        [Example(7, 14)]
        [Scenario]
        public void When_UserTriesToAddLastItemAndCountEqualsCapacityArray_Must_Resize(int capacity, int newCapacity)
        {
            DynamicArray<int> array = null;

            $"Given there is an array model with '{capacity}' capacity and '{capacity}' items"
                .x(() =>
                {
                    array = new DynamicArray<int>();
                    array.SetCapacity(capacity);
                    // generate 
                    for (int index = 1; index < capacity+1; index++) array.AddLast(index);
                });

            $"When tries to adds '{capacity + 1}' item in array's tail"
                .x(() =>
                {
                    array.AddLast(1000);
                });

            $"Then array's capacity should resize from '{capacity}' to '{newCapacity}'"
                .x(() =>
                {
                    var cap = array.GetCapacity();
                    var resizeStatus = array.ResizeStatus();

                    cap.Should().Be(newCapacity);
                    resizeStatus.Should().Be(ResizeStatus.Increase);
                });
        }

        [Scenario]
        public void User_CanAddItemInArrayWhenCountEqualsCapacity()
        {
            DynamicArray<int> array = null;

            $"Given there is an array model with '16' capacity and '16' items"
                .x(() =>
                {
                    array = new DynamicArray<int>();
                    array.SetCapacity(16);
                    // generate 
                    for (int index = 1; index < 17; index++) array.AddLast(index);
                });

            $"When tries to adds '17' item in array's tail"
                .x(() =>
                {
                    array.AddLast(1000);
                    // set cursor
                    array.SetCursor(16);
                });

            $"Then item should be added to 17's position"
                .x(() =>
                {
                    var item = array.GetItem();
                    var count = array.GetItemsCount();

                    item.Should().Be(1000);
                    count.Should().Be(17);
                });
        }

        [Scenario]
        public void User_CanRemoveSelectedItemFromArrayModel()
        {
            DynamicArray<int> array = null;

            $"Given there is an array model with capacity 16 and items 2,3,4 at index 0,10,13"
                .x(() =>
                {
                    array = new DynamicArray<int>();
                    array.SetCapacity(16);
                    // items
                    array.SetCursor(0);
                    array.SetItem(2);
                    array.SetCursor(10);
                    array.SetItem(3);
                    array.SetCursor(13);
                    array.SetItem(4);
                    array.SetCursor(14);
                    array.SetItem(4);
                    array.SetCursor(15);
                    array.SetItem(4);
                    array.SetCursor(1);
                    array.SetItem(4);
                    array.SetCursor(2);
                    array.SetItem(4);
                    array.SetCursor(3);
                    array.SetItem(4);
                    array.SetCursor(4);
                    array.SetItem(4);
                    array.SetCursor(5);
                    array.SetItem(4);
                });

            $"When user tries to remove item at position 10"
                .x(() =>
                {
                    array.SetCursor(10);
                    array.Remove();
                });

            $"Then array model must contain default value at index 10"
                .x(() =>
                {
                    var item = array.GetItem();
                    var count = array.GetItemsCount();

                    item.Should().Be(default);
                    count.Should().Be(9);
                });
        }

        [Scenario]
        public void ArrayModelProduceResizeWhenAfterRemoveCountLessThanHalfOfCapacity()
        {
            DynamicArray<int> array = null;

            $"Given there is an array model with capacity 6 and items 2,3,4 at index 0,1,3"
                .x(() =>
                {
                    array = new DynamicArray<int>();
                    array.SetCapacity(6);
                    // items
                    array.SetCursor(0);
                    array.SetItem(2);
                    array.SetCursor(1);
                    array.SetItem(3);
                    array.SetCursor(3);
                    array.SetItem(4);
                });

            $"When count of this array has been reduced after remove operation and new count less than half of capacity"
                .x(() =>
                {
                    array.SetCursor(1);
                    array.Remove();
                });

            $"Then array model must be resized"
                .x(() =>
                {
                    var resizeStatus = array.ResizeStatus();
                    var count = array.GetItemsCount();
                    var capacity = array.GetCapacity();

                    resizeStatus.Should().Be(ResizeStatus.Decrease);
                    count.Should().Be(2);
                    capacity.Should().Be(4);
                });
        }

        [Scenario]
        public void WhenResizeHappensToLessCapacityItemsAreMovesByIndexesDown()
        {
            DynamicArray<int> array = null;

            $"Given there is an array model with capacity 6 and items 2,3,4 at index 0,1,3"
                .x(() =>
                {
                    array = new DynamicArray<int>();
                    array.SetCapacity(6);
                    // items
                    array.SetCursor(0);
                    array.SetItem(2);
                    array.SetCursor(1);
                    array.SetItem(3);
                    array.SetCursor(3);
                    array.SetItem(4);
                });

            $"When count of this array has been reduced after remove operation and new count less than half of capacity"
                .x(() =>
                {
                    array.SetCursor(1);
                    array.Remove();
                    array.SetCursor(0);
                });

            $"Then array model must be resized and items should be 0 -> 2 and 1 -> 4"
                .x(() =>
                {
                    var capacity = array.GetCapacity();
                    var firstItem = array.GetItem();
                    array.MoveCursorNext();
                    var secondItem = array.GetItem();

                    capacity.Should().Be(4);
                    firstItem.Should().Be(2);
                    secondItem.Should().Be(4);
                });
        }

        [Scenario]
        public void When_NewCountAfterRemoveGreaterThanHalfOfCapacityArray_Must_NotResizeAndChangeStructure()
        {
            DynamicArray<int> array = null;

            $"Given there is an array model with capacity 16 and 10 items"
                .x(() =>
                {
                    array = new DynamicArray<int>();
                    array.SetCapacity(16);
                    // items
                    array.AddLast(1);
                    array.AddLast(2);
                    array.AddLast(3);
                    array.AddLast(4);
                    array.AddLast(5);
                    array.AddLast(6);
                    array.AddLast(7);
                    array.AddLast(8);
                    array.AddLast(9);
                    array.AddLast(10);
                });

            $"When user removes existed item at position 6"
                .x(() =>
                {
                    array.SetCursor(6);
                    array.Remove();
                });

            $"Then array model must not be resized to straight items"
                .x(() =>
                {
                    var capacity = array.GetCapacity();
                    var item = array.GetItem();
                    var status = array.ResizeStatus();

                    capacity.Should().Be(16);
                    item.Should().Be(default);
                    status.Should().Be(ResizeStatus.NoChanges);
                });
        }
    }
}
