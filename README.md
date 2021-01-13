# Unity Projectile Physics drop simulation.

**Introduction:**

In games where projectiles are fired, launched or thrown by an AI. Having that AI be able to calculate or predict where the projectile will fall can be essential to have the AI function in a manner that is wanted by the developer.
For this implementation I Focused around bullets as they are the simplest and easiest to calculate and visualize.

This project was built using the unity engine to speed up and simplify some of the steps required to calculate and simulate the steps of firing a bullet.

To calculate a trajectory that is influenced by multiple forces like gravity, wind and air friction is hard. Because of this I opted to ignore air friction and wind to once more simplify the calculations and simulations.
In this project you are able to control the camera using the _W_, _A_, _S_ and _D_ keys to move. The _Spacebar_ to move up and the _Left_ and _Right_ _Shift_ keys to move down.
Your mouse movement controls the looking direction and a bullet is fired using the _Left Mouse Button_.

I visualized the trajectory using a drawn dotted line and an opaque sphere will signify the impact point if any is present.

Now lets get into the interesting stuff:

**The Math explained:**

To start off, we need a formula that can tell us how far the bullet will travel before hitting the ground.<br/> This means our forward velocity _v0x_ and our total travel time _t_ will determine this value, resulting in following formula<br/>
![Core travel distance formula](/Images/BasicTravelDistance.png)<br/>
But this formula has an issue, we do not at the start know our travelled time _(t)_. This means that even if we know our starting height_(h)_ and velocity_(v0x)_, we will first have to calculate the time the bullet travels.

1. To calculate the travelled time we start by calculating the y-component displacement, or the distance it will drop down before it hits a wall or the ground.<br/>
For this we use the Y-displacement formula:<br/>
![Y Displacement formula](/Images/yDisplacement.png)<br/>
	* The _(v0y)_ value stands for initial y velocity, this is important in case the bullet is not fired straight forward, as this bullet would possibly first travel upwards or downwards which would greatly impact the shape of our trajectory arc. <br/>Luckily the Unity framework makes this a lot easier as many of the vectors we would have to calculate are kept as standard variables inside of an object.
	* For this example I will use a bullet fired straight forward from here onwards.

2. We can reform the formula to a form incorporating our starting height since when we shoot straight forward, its a standard forward vector at height _h_.<br/>
![Reformed Y Displacement formula](/Images/NewYDisplacement.png)<br/>

3. From here we can rearrange to formula to solve for our total time the bullet is underway: <br/>
![Bullet travel time formula](/Images/TimeTravelled.png)<br/>

4. Now that we know our formula for travelled time, we can plug this into our basic distance function and remove the _t_ from it.<br/>
![Bullet travel distance formula](/Images/BulletTravelDistance.png)<br/>
	* The _(v0x)_ represents the start velocity (In this formula we are mainly interested in the X-value since the Y-value was incorporated into the time travelled _t_)
	* The starting height is represented by _(h)_
	* Gravitational acceleration is represented as _(g)_ (Generally gravity acceleration has a constant value of _-9.81 m/s^2_ or _9.81 m/s^2_ (depends on interpretation, some use the negative value some don't). This represents the downward pull of 1G. This standard is one I followed as well)

5. An example taken from the reference site shows how this formula would represent our distance. <br/>
![Bullet travel distance formula Example](/Images/Example.png)<br/>


Sources:
Formulas and basic explanation for calculating a bullet trajectory: https://sciencing.com/calculate-bullet-trajectory-5185428.html
Reference for parts of implementation of Bullet trajectory: https://www.omnicalculator.com/physics/trajectory-projectile-motion
