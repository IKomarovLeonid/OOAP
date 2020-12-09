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



