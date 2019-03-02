# Aquarium
MACHINE LEARNING CANNIBALISTIC-FISH SIMULATOR


Well if that doesn't get your attention, I don't know what will.

So this project began when I saw an article showing Unity's machine learning toolkit and thought to myself, "Wow, that looks really cool! I wanna try it out for myself!" That was really my whole thought process. This is the article for anyone who's interested: https://blogs.unity3d.com/2017/12/11/using-machine-learning-agents-in-a-real-game-a-beginners-guide/.

And thus began my journey into creating the Aquarium. What started off as one A.I. searching for a little green pellet soon became a version of Agar.IO where the player is pit against several highly intelligence (a bit of an alliteration) fighting to see who can become the largest. How on earth did it come to that? Well keep reading and you'll find out. (Go to the "Omega" section to read about the final product) 

# Fish v1
I began by creating a very basic machine learning brain that is rewarded whenever it finds a food. This in itself was a long learning process as I prefer jumping straight into a project before I learn what each of the buttons and switches do. But after several YouTube series and skimming through the documentation, I created my first Fish. Fish v1 was simple minded. It spawned in a random location in a small "tank" (bounded area) and was given one values: the distance between it and the closest piece of food. These pieces of food were scattered around the tank in random locations as well. When the fish touched the food, it was rewarded. As time went on, the fish was punished for taking longer to locate the food. The fish then took the input, and returned two values: the movement in the x-axis and the movement in the y-axis. Ideally the fish should have quickly swam from food to food, until there were no more. But of course, what code works on the first try? The fish swam randomly, most of the time moving in one direction. I then did what any good programmer does, and sleep until I have a revelation while trying to fall asleep. And right on que, the revelation came. I realized two solutions. One, give the fish a more accurate description of the location of the food. That is, give it two values: the coordinate difference between its location and the food's location. Two, give it a small reward whenever it moves closer to the food. Kind of like a kick-start to the learning process. These changes produced a speedy fish who quickly zoomed from food to food. Nice. 

# Fish v2
