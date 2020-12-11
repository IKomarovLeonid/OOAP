using System;
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
    }
}
