# Andrei Lucaci's Minesweeper

Implementation of the original game written in C#, WPF, Prism 7.1

## Download latest version (installer - packed) 
- will install under "C:\Program Files (x86)\Andrei Lucaci\Minesweeper":
### [Download](https://1drv.ms/u/s!AuLwjdUez7WhikBrYMQ0J9mydkKw)

# Features:
1. Three original custom boards:

    ![newGameImg](https://i.imgur.com/K8V6F1l.png)
    1. Beginner: 8x8 with 10 mines
    2. Advanced: 16x16 with 40 mines
    3. Expert: 24x24 with 99 mines

2. Information:

    ![infoImg](https://i.imgur.com/1Timp2M.png)
    1. About us
    2. Rules    

3. Header information:
    1. Number of mines (either valid or flagged) - left

    ![flagMineImg](https://i.imgur.com/j8MHWHk.gif)

    2. Restart game - displays also game states - middle
        1. Smile - new / ongoing game
        2. Glasses (cool) - win
        3. x.x face (dead) - loose

    3. Timer - right

    ![timerImg](https://i.imgur.com/OKWVzmv.gif)

4. Stats - after each game you get the stats:
   
    
# Screenshots

![img1](https://i.imgur.com/sm0Hodz.png)

# Advanced ongoing game:

![gameImg](https://i.imgur.com/VPxQMlt.gif)

# Code:
The code is written in *C#* with *WPF* and *Prism 7.1*

The project structure is as follows:
- Minesweeper.Engine:
    - Holds the logic of the application, all the computations and calculations
- Minesweeper.Infrastructure:
    - Holds DTO's and configuration information globally available to all the application
- Minesweeper.Models:
    - Holds the DTO's of the application

- Minesweeper.Ui:
    - Handles all interaction of the ui


The dependency diagram shows something like this:
![dependencyImg](https://i.imgur.com/mmzhCkZ.png)

The code metrics shows information about the code. Currently the code's maintainability index is green, that is, it's not that of a spagetti code :)
![codeMetricsImg](https://i.imgur.com/6vpLcRy.png)


# Known issues:
- ~~a known issue is the slow loading time for the Expert games. The algorithm that generates the board still needs improvement.~~

- ~~code issues: there are still code issues, which will be solved in time:~~ under implementation
![codeIssueImg](https://i.imgur.com/lfKxVHV.png)

# Pllaned features:
- add save / open games
- add records / time / configuration
- add amazed face to the left / right click
- ~~stability improvements~~
- ~~add skins~~
- ~~fix code issues~~
- redesign stats
- redesign pop-ups
