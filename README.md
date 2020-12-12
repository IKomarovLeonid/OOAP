# OOAP
Object oriented analysis programming.

You can see models in /src/Models.

You can see models tests in /tests.

You can use models in app project (console app).

All projects uses net 5.0

# First task: Bounded stack (Models/Stack) 6.12.2020

What i have missed in compare with right solution: 
1) int maxSize() - when stack has concrete capacity value it is good for user to know can he push another item into bounded stack or it will be error operation.
2) Initialization is differ. I do initialization in two steps. Maybe it is better to use single operation as in solution - constructor in abstract stack 

What i do in different way:
Set error statuses to enums. Right solution uses 'int' statuses. 

# Second task: ATD LinkedList 6.12.2020

Task: implement ATD Linkedlist with additional feature: cursor.

Done task 9.12.2020 (Models/ListModel) + tests.

What i have missed in compare with right solution: 
I use single status to cover all possible results of operations (OK, Not set, Error). Right solution has several methods to return specific operation result. I'm not sure that we need to split result of get find status or remove status for example

2.2 Why tail operation is not contains any other operations? Because if internal structure has no pointer to tail, we need to travel by each node to tail (O(n))

2.3 Why we do not need operation to find all nodes with same value: Because we can user move to next item (item) and no need to iterate to each node.

# Third task : split one way list and two way linkedlists abstractions 11.12.2020

11.12.2020: Done

Split single list and linked list models. 

# Task four: dynamic array with OOAP model 11.12.2020

12.12.2020: Done 

Difference between realization and correct answer: 

Cursor was not required. Dynamic Array not requires cursor for optimization

# Task five: queue model 12.12.2020

12.12.2020: Done

Difference between realization and correct answer: no difference. I produce correct split of classic dequeue method: dequeue item and get head item becomes separated operations

# Task six: deque model 12.12.2020

Also need to review abstractions in same way as list and linkedList 
