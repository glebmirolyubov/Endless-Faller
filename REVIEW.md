# Review of Endless Faller project

*completed by Gleb Mirolyubov, 2019* 

## What I learned
While developing Endless Faller, I have learned how powerful and handy some programming pattern may be. For example, On many classes in the game I use Singleton programming pattern, which essentially allows for the creation of instance variable, which can be accessed from anywhere in the game (if public). This is very handy as it allows to not create a reference to the script. Newly instantiated objects, such as MovingPlatform, can access the instance during runtime.

## Where I struggled
When I was almost finishing the projects, I noticed that my GameOver method in LevelManager class became too cluttered. I figured that implementing a State programming pattern would solve this issue as I would have distinct states, i.e. StartState, PlayState, GameOverState, which would allow for more clear transitions. However, I decided to stay within the 8-hour working window. As I've learned, the game is never finished and it's better to have a working prototype in time, rather than spend a lot of time achieving the same thing.

## What I liked
I really liked implementing fun particle system for high score. I also liked implementing Scriptable Object as I find this solution very clean and clever. Non-programmer members of the team can easily change game mechanics without ever touching the code with Scriptable Objects!

