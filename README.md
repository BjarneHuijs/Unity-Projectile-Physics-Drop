# Unity Projectile Physics drop simulation.

**Introduction:**

In games where projectiles are fired, launched or thrown by an AI. Having that AI be able to calculate or predict where the projectile will fall can be essential to have the AI function in a manner that is wanted by the developer.
For this implementation I Focused around bullets as they are the simplest and easiest to calculate and visualize.

This project was built using the unity engine to speed up and simplify some of the steps required to calculate and simulate the steps of firing a bullet.

To calculate a trejectory that is influenced by multiple forces like gravity, wind and air friction is hard. Because of this I opted to ignore air friction and wind to once more simplify the calculations and simulations.
In this project you are able to control the camera using the _W_, _A_, _S_ and _D_ keys to move. The _Spacebar_ to move up and the _Left_ and _Right_ _Shift_ keys to move down.
Your mouse movement controls the looking direction and a bullet is fired using the _Left Mouse Button_.

I visualized the trajectory using a drawn dotted line and an opaque sphere will signify the impact point if any is present.

Now lets get into the interesting stuff:

**The Math explained**
First off, The distance traveled by the bullet can be calculated using this formula: 
![Core travel distance formula](/Images/BasicTravelDistance.png)
But this formula has an issue, we do not at the start know our travelled time _(t)_. This means that even if we know our starting height_(h)_ and velocity_(v0x)_, we will first have to calculate the time the bullet travels.

1. To calculate the travelled time we start by calculating the y-component displacement, or the distance it will drop down before it hits a wall or the ground.
For this we use the Y-displacement formula:
![Bullet travel distance formula](/Images/yDisplacement.png)
	* The _(v0y)_ value stands for initial y velocity, this is important in case the bullet is not fired straight forward, as this bullet would possibly first travel upwards or downwards which would greatly impact the shape of our trajectory arc.
Luckily the Unity framework makes this a lot easier as many of the vectors we would have to calculate are kept as standard variables inside of an object.

![Bullet travel distance formula](/Images/BulletTravelDistance.png)
	* (v0x) is its starting speed
	* (h) is the height it’s fired from
	* (g) is the acceleration due to gravity.(generally gravity acceleration has a constant value of -9.81 m/s^2. This represents the downward pull of 1G. This standard is one I followed as well)



Sources:
Formulas bullet trajectory: https://sciencing.com/calculate-bullet-trajectory-5185428.html
Extras Bullet trajectory: https://www.omnicalculator.com/physics/trajectory-projectile-motion
