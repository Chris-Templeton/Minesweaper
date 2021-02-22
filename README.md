# Minesweaper Project

This project uses the command line to allow users to play minsweaper. The board is presented as rows (labeled with letters) and columns (numbers) and the user guesses a space by entering the letter & number combination (ie. A1). User can either play a standard game (10x10 board) on Easy, Medium, or Hard difficulties or they can create a custom game and choose the size & difficulty.

## Main classes used:
### Minesweaper
This class creates as board with given size & difficulty and does the work of letting the user play Minesweaper. The Play method continues to prompt the user for a guess until the user hits a mine, or completes the board.

The helper method 'Guess' returns the square value the user guessed and guesses surrounding squares if the guess is an empty square.


### Board
This class holds a dictionary used to represent a board. Keys are the square names (ie. A1), and the value is an enumerator created to represent the space's value (Empty, one adjacent mine ... Mine).

This class has public methods to get the value of a square (GetValue), tell whether the board contains a square (Contains), and a static method to convert an integer into a letter value (for converting a list index into a letter for the user - 0 is A, 1 is B and so on).


### Menu
This abstract class defines the default behavour of menus in a command-line interface. It contains one Open method that runs through a dictionary of options and presents them to the user.

All classes that inherit this functionality must define the options dicitonary, which correlates a string prompt (ie. Play) with an Action. This way classes that inherit from Menu only need to define the options dicitonary and have methods for every option defining what that specific menu does.


### Helpers
IUIHelper is an interface that defines what UI must be able to do. Instances of classes that implement IUIHelper are declared in the Main method of Program.cs and then passed where needed in the application.

CLIBoardWriter handles writing a Board object to the command line.