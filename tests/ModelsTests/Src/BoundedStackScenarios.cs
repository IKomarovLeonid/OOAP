﻿using System;
using System.Reflection.Metadata;
using FluentAssertions;
using Stack;
using Stack.Enums;
using Xbehave;

namespace ModelsTests
{
    public class BoundedStackScenarios
    {
        [Scenario]
        public void DefaultBoundedStackCanContainsOnly32Items()
        {
            BoundedStack<int> stack = null;

            "Given there is a default bounded stack model with 32 capacity"
                .x(() =>
                {
                    stack = new BoundedStack<int>();
                    stack.Initialize();
                });

            "When user pushes 33 items into bounded stack"
                .x(() =>
                {
                    for (int index = 0; index < 33; index++) 
                    {
                        stack.Push(index);
                    }
                });

            "Then bounded stack model should add only 32 items"
                .x(() =>
                {
                    stack.Size().Should().Be(32);
                });
        }

        [Example(10)]
        [Example(100)]
        [Scenario]
        public void BoundedStackWithUserCapacityCanHaveNotGreaterItemsCount(int capacity)
        {
            BoundedStack<string> stack = null;

            $"Given there is a user's capacity bounded stack model with '{capacity}' capacity"
                .x(() =>
                {
                    stack = new BoundedStack<string>();
                    stack.Initialize(capacity);
                });

            $"When user pushes '{2*capacity}' items into such bounded stack"
                .x(() =>
                {
                    for (int index = 0; index < 2 * capacity; index++)
                    {
                        stack.Push(index.ToString());
                    }
                });

            $"Then bounded stack model should add only '{capacity}' items"
                .x(() =>
                {
                    stack.Size().Should().Be(capacity);
                });
        }

        [Scenario]
        public void WhenPushHappensWithFullOfItemsStackPushResultShouldBeError()
        {
            BoundedStack<string> stack = null;

            "Given there is an stack with item's count equal to capacity"
                .x(() =>
                {
                    stack = new BoundedStack<string>();
                    stack.Initialize(2);
                    // items
                    stack.Push("I");
                    stack.Push("Processing");
                });

            "When push operation happens"
                .x(() =>
                {
                    stack.Push("New string");
                });

            "Then get last push operation result should be 'Error'"
                .x(() =>
                {
                    var status = stack.GetPushStatus();

                    status.Should().Be(OpResult.ERROR);
                });
        }

        [Scenario]
        public void WhenPushHappensWithNotFullOfItemsStackPushResultShouldBeError()
        {
            BoundedStack<string> stack = null;

            "Given there is an stack with item's count less than capacity"
                .x(() =>
                {
                    stack = new BoundedStack<string>();
                    stack.Initialize(10);
                    // items
                    stack.Push("I");
                    stack.Push("Processing");
                });

            "When push operation happens"
                .x(() =>
                {
                    stack.Push("New string");
                });

            "Then get last push operation result should be 'Ok'"
                .x(() =>
                {
                    var status = stack.GetPushStatus();

                    status.Should().Be(OpResult.OK);
                });
        }

        [Scenario]
        public void PopCanRemoveItemFromNotEmptyStack()
        {
            BoundedStack<int> stack = null;

            "Given there is an default bounded stack model with 32 capacity"
                .x(() =>
                {
                    stack = new BoundedStack<int>();
                    stack.Initialize();
                });

            "And stack has items 5 4 3 2 8"
                .x(() =>
                {
                    stack.Push(5);
                    stack.Push(4);
                    stack.Push(3);
                    stack.Push(2);
                    stack.Push(8);
                });

            "When pop operation happens"
                .x(() =>
                {
                    stack.Pop();
                });

            "Then peek operation should return 4"
                .x(() =>
                {
                    stack.Peek().Should().Be(4);
                });

            "And last pop operation status should be 'OK'"
                .x(() =>
                {
                    stack.GetPopStatus().Should().Be(OpResult.OK);
                });
        }

        [Scenario]
        public void PopCanNotRemoveItemFromEmptyStack()
        {
            BoundedStack<int> stack = null;

            "Given there is a default bounded stack with no items"
                .x(() =>
                {
                    stack = new BoundedStack<int>();
                    stack.Initialize();
                });

            "When pop operation happens"
                .x(() =>
                {
                    stack.Pop();
                });

            "Then system should set last pop operation status to 'Error'"
                .x(() =>
                {
                    stack.GetPopStatus().Should().Be(OpResult.ERROR);
                });
        }

        [Scenario]
        public void PeekCanGetItemWhenStackIsNotEmpty()
        {
            BoundedStack<int> stack = null;
            // to assert
            var peekItem = 0;

            "Given there is a bounded stack model"
                .x(() =>
                {
                    stack = new BoundedStack<int>();
                    stack.Initialize();
                });

            "And this stack has 7 items: -2 3 5 1 0 -2 3 with first added item equal to -2"
                .x(() =>
                {
                    stack.Push(-2);
                    stack.Push(3);
                    stack.Push(5);
                    stack.Push(1);
                    stack.Push(0);
                    stack.Push(-2);
                    stack.Push(3);
                });

            "When peek operation is requested"
                .x(() =>
                {
                    peekItem = stack.Peek();
                });

            "Then stack should represent -2"
                .x(() =>
                {
                    peekItem.Should().Be(-2);
                });

        }

        [Scenario]
        public void PeekOperationDoNotRemoveItemFromHeadPosition()
        {
            BoundedStack<double> stack = null;
            const double firstItem = 0.12;

            $"Given there is a bounded stack model with items {firstItem} 3.45 -23.02 11"
                .x(() =>
                {
                    stack = new BoundedStack<double>();
                    stack.Initialize();

                    stack.Push(firstItem);
                    stack.Push(3.45);
                    stack.Push(-23.02);
                    stack.Push(11);
                });

            "When peek operation happens"
                .x(() =>
                {
                    stack.Peek();
                });

            $"Then another stack's peek operation should return same item '{firstItem}'"
                .x(() =>
                {
                    var item = stack.Peek();

                    item.Should().Be(firstItem);
                });
        }

        [Scenario]
        public void PeekOperationForEmptyStackSetPeekOperationResultToError()
        {
            BoundedStack<double> stack = null;

            $"Given there is a bounded stack model with no elements"
                .x(() =>
                {
                    stack = new BoundedStack<double>();
                    stack.Initialize();
                });

            "When peek operation happens"
                .x(() =>
                {
                    stack.Peek();
                });

            $"Then last peek operation result should be 'Error'"
                .x(() =>
                {
                    stack.GetPeekStatus().Should().Be(OpResult.ERROR);
                });
        }

        [Scenario]
        public void WhenAfterErrorPeekOperationHappensSuccessfulPeekOperationSystemSetSuccessLastStatus()
        {
            BoundedStack<double> stack = null;

            "Given there is a empty bounded stack with last peek error status"
                .x(() =>
                {
                    stack = new BoundedStack<double>();
                    stack.Initialize();
                    // peek operation for empty stack set error status
                    stack.Peek();
                });

            "When some item has been pushed"
                .x(() => { stack.Push(2); });

            "And peek operation happens again"
                .x(() => { stack.Peek(); });

            "Then last peek operation status should be 'OK'"
                .x(() =>
                {
                    var status = stack.GetPeekStatus();

                    status.Should().Be(OpResult.OK);
                });
        }

        [Scenario]
        public void WhenAfterErrorPopOperationHappensSuccessfulPopOperationSystemSetSuccessLastStatus()
        {
            BoundedStack<double> stack = null;

            "Given there is a empty bounded stack with last pop error status"
                .x(() =>
                {
                    stack = new BoundedStack<double>();
                    stack.Initialize();
                    // peek operation for empty stack set error status
                    stack.Pop();
                });

            "When some item has been pushed"
                .x(() => { stack.Push(2); });

            "And pop operation happens again"
                .x(() => { stack.Pop(); });

            "Then last pop operation status should be 'OK'"
                .x(() =>
                {
                    var status = stack.GetPopStatus();

                    status.Should().Be(OpResult.OK);
                });
        }

        [Scenario]
        public void When_PeekAndPopOperationsHasNotBeenProcessedSystemGetStatusMustReturnNil()
        {
            BoundedStack<double> stack = null;

            "When new bounded stack has been created"
                .x(() =>
                {
                    stack = new BoundedStack<double>();
                    stack.Initialize();
                });

            "Then get peek and pop statuses must be 'Nil'"
                .x(() =>
                {
                    var popStatus = stack.GetPopStatus();
                    var peekStatus = stack.GetPeekStatus();

                    popStatus.Should().Be(OpResult.NIL);
                    peekStatus.Should().Be(OpResult.NIL);
                });
        }

        [Scenario]
        public void ClearNotEmptyStackCanRemoveAllItems()
        {
            BoundedStack<double> stack = null;

            "Given there is a bounded stack with items"
                .x(() =>
                {
                    stack = new BoundedStack<double>();
                    stack.Initialize();
                    // elements
                    stack.Push(1);
                    stack.Push(2);
                    stack.Push(3);
                    stack.Push(4);
                    stack.Push(5);
                });

            "When clear operation on this stack happens"
                .x(() =>
                {
                    stack.Clear();
                });

            "Then such stack must not have any elements"
                .x(() =>
                {
                    var count = stack.Size();

                    count.Should().Be(0);
                });
        }

        [Scenario]
        public void UserCanNotInitializeStackWithNegativeCapacity()
        {
            Action func = null;

            "When user tries to initialize stack with negative capacity"
                .x(() =>
                {
                    var stack = new BoundedStack<int>();
                    func = () => stack.Initialize(-4);
                });

            "Then system must generate exception"
                .x(() =>
                {
                    func.Should().Throw<ArgumentException>();
                });
        }

        [Scenario]
        public void StackWithNoInitializationThrowsException()
        {
            BoundedStack<double> stack = null;

            "When stack is not initialized"
                .x(() =>
                {
                    stack = new BoundedStack<double>();
                });

            "Then system must generate exception's on every stack's methods: push pop peek clear and size"
                .x(() =>
                {
                    Action actionPush = () => stack.Push(234);
                    Action actionPop = () => stack.Pop();
                    Action actionPeek = () => stack.Peek();
                    Action actionClear = () => stack.Clear();
                    Action actionSize = () => stack.Size();

                    actionPush.Should().Throw<Exception>();
                    actionPop.Should().Throw<Exception>();
                    actionPeek.Should().Throw<Exception>();
                    actionClear.Should().Throw<Exception>();
                    actionSize.Should().Throw<Exception>();
                });
        }

    }
}
