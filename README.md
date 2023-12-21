# Shopping-List-CLI
 A task for learning programming, involving creating a shopping list.

Shopping list in the CLI
This task is built in stages. Please complete the tasks in the order they are given.
### Task 1
Create a basic program that reads in articles until you type “stop”, and then just outputs them
again, sorted in alphabetical order.
Example:
```
ShoppingList> Tomatoes
ShoppingList> Apples
ShoppingList> Potatoes
ShoppingList> stop
Apples
Potatoes
Tomatoes
```
### Task 2
Instead of using strings to store the arƟcles, now store the amount of them alongside the
name. This can be realized with classes.
Note that the input should now include the number of items. Should the item already be in
the list, it should be added to the exisƟng items:
```
ShoppingList> Tomatoes 2
ShoppingList> Tomatoes 3
ShoppingList> Apples 3
ShoppingList> Potatoes 4
ShoppingList> stop
3 Apples
4 Potatoes
5 Tomatoes
```
### Task 3
Now, instead of using “stop” to just output it once, we can improve that a lot.
Implement the following commands:
- add \<article> \<amount>
(Will add the specified arƟcle and amount to the shopping list.)
- remove \<article> \<amount>
(Will remove the specified amount of the item from the list. Give a message if it doesn’t
exist. To remove all, allow the user to put a * instead of a number.)
- list
(Will list out the elements in the list.)
- clear
(Will clear all articles from the list.)
- undo
(Will undo the last action.)
- exit
(Will exit the program.)
### Task 4
Now let the program save the list to the disk when it exits and load it back when it starts up.
How you store those doesn’t really matter, but I would recommend either CSV or JSON.
### Task 5
Now how about allowing the user to have multiple lists? When the program starts up, and
sees multiple lists saved, it should prompt the user which one to load.
On exit, the program should ask for a filename for the new list. If it exists, also make sure to
ask whether to overwrite the existing list.
