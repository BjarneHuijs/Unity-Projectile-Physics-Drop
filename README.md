# Unity Projectile Physics drop simulation.

## Introduction:
<br/>
My first idea was to implement a simple machine learning AI built using the ML-Agents library which is available on the unity framework. This is a framework that will train the AI according to certain parameters you pass as positive or negative influences.
I wanted to do this as machine learning to me is a very interesting subject where once you create the base for the AI to build on, you can see it grow generation by generation.



In games where projectiles are fired, launched or thrown by an AI. Having that AI be able to calculate or predict where the projectile will fall can be essential to have the AI function in a manner that is wanted by the developer.
For this implementation I Focused around bullets as they are the simplest and easiest to calculate and visualize.

This project was built using the unity engine to speed up and simplify some of the steps required to calculate and simulate the steps of firing a bullet.

To calculate a trajectory that is influenced by multiple forces like gravity, wind and air friction is hard. Because of this I opted to ignore air friction and wind to once more simplify the calculations and simulations.
In this project you are able to control the camera using the _W_, _A_, _S_ and _D_ keys to move. The _Spacebar_ to move up and the _Left_ and _Right_ _Shift_ keys to move down.
Your mouse movement controls the looking direction and a bullet is fired using the _Left Mouse Button_.

I visualized the trajectory using a drawn dotted line and an opaque sphere will signify the impact point if any is present.

## The Math explained:
<br/>
Before I can go into how I simulated the bullet drop I will explain in short the basic math required for this implementation.
<br/>
To start off, we need a formula that can tell us how far the bullet will travel before hitting the ground.<br/> This means our forward velocity _v0x_ and our total travel time _t_ will determine this value, resulting in following formula<br/>
![Core travel distance formula](/Images/BasicTravelDistance.png)<br/>
But this formula has an issue, we do not at the start know our travelled time _(t)_. This means that even if we know our starting height_(h)_ and velocity_(v0x)_, we will first have to calculate the time the bullet travels.

1. To calculate the travelled time we start by calculating the y-component displacement, or the distance it will drop down before it hits a wall or the ground.<br/>
For this we use the Y-displacement formula:<br/>
![Y Displacement formula](/Images/yDisplacement.png)<br/>
	* The _(v0y)_ value stands for initial y velocity
		* This value is important in case the bullet is not fired straight forward,<br/> as said bullet would possibly first travel upwards or downwards which would greatly impact the shape of our trajectory arc. 
		* Luckily the Unity framework makes this a lot easier as many of the vectors we would have to calculate are kept as standard variables inside of an object.
	* For this example I will use a bullet fired straight forward from here onwards.

2. We can reform the formula to a new form incorporating our starting height since when we shoot straight forward, its a standard forward vector at height _h_.<br/>
![Reformed Y Displacement formula](/Images/NewYDisplacement.png)<br/>

3. From here we can rearrange to formula to solve for our total time the bullet is underway: <br/>
![Bullet travel time formula](/Images/TimeTravelled.png)<br/>

4. Now that we know our formula for travelled time, we replace the _t_ with it.<br/>
![Bullet travel distance formula](/Images/BulletTravelDistance.png)<br/>
	* The _(v0x)_ represents the start velocity (In this formula we are mainly interested in the X-value since the Y-value was incorporated into the time travelled _t_)
	* The starting height is represented by _(h)_
	* Gravitational acceleration is represented as _(g)_ (Generally gravity acceleration has a constant value of _-9.81 m/s^2_ or _9.81 m/s^2_ (depends on interpretation, some use the negative value some don't). This represents the downward pull of 1G. This standard is one I followed as well)

5. An example taken from the reference site shows how this formula would represent our distance. <br/>
![Bullet travel distance formula Example](/Images/Example.png)<br/>


## My implementation of this math into a 3d simulated space.
<br/>
I begun with a basic 1st person FreeCam of which I found the basis online (See sources). From there on I built a small level that would properly be able to show the prediction from multiple looking angles and heights.<br/>
![Level Image](/Images/Level.png)<br/>

After this I built The basic necessities for a simple shooter.<br/>
1. A projectile which is only influenced by it's starting velocity and gravity
2. A projectile manager which spawns and fires the bullets
	2.1 In here the prediction calculations are done as well
3. A Player object combining the camera with the projectile manager inputs, forming the bridge between UI and play.
<br/>
From here on I spent most of my time implementing the formulas for bullet drops and visualizing it.<br/>
To start I worked on how I could implement the formulas I explained above. Here I noticed that the basic mathemathical implementation of these formulas does not match well with how Unity was built.
<br/> 
<br/> 
So I set out to find an application of this principle using more Unity based concepts.<br/>
My resulting implementation for how the point is calculated uses the elapsed time since last frame, the constant gravity downwards velocity, the position of the object, and it's forward vector.
<br/>
<br/>
Applying them into follo code, sets a point on the line according to the predicted path, creating a nice curve along which the bullet will travel.<br/>
``` 
	void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
	{
		int numSteps = 50;
		float timeDelta = 1.0f / initialVelocity.magnitude;

		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.positionCount = numSteps;

		Vector3 position = initialPosition;
		Vector3 velocity = initialVelocity;

		for (int i = 0; i < numSteps; ++i)
		{
			if (WallDetection(position, velocity))
			{
				//print(transform.Find("Target").gameObject.name);
				//print(transform.Find("Target").position);
				transform.Find("Target").transform.position = _impactPoint;
				print("found impact point at " + _impactPoint.x + ", " + _impactPoint.y + _impactPoint.z + ", ");
				lineRenderer.positionCount = i;
				break;
			} else
			{
				// Set point to void if no hit found
				transform.Find("Target").position = new Vector3(-999f, -999f, -999f);
			}

			lineRenderer.SetPosition(i, position);

			// Calculate next position using the the current velocity, position, and time since last calculation
			position += velocity * timeDelta + 0.5f * gravity * timeDelta * timeDelta; 
			//Gives the trail it's downward arc influenced by gravity, higher speed == longer and flatter arc
			velocity += gravity * timeDelta; 
		}
	}
 ```

<br/>
This will result in a line element with a dotted texture to be drawn along the theoretical archway of the bullet path. as shown on following image.<br/>
![Impact Visualisation](/Images/LineExample.png)<br/>

The small sphere shows the impact point which can be retrieved for any needed behavior calculations later on in the loop.

## Uses and examples:
<br/>
A path prediction for hpysics objects is useful for both players and AI alike, it allows them to make better decisions by visualising approximately where the object will land/impact.<br/>
For AI, this means their accuracy can rise by a solid degree without having too much impact on resources.
<br/>
<br/>
Continuing on the visualisation point of view, it is already regularly used in games where you are supposed to know where the projectile will go.<br/>
It has many uses, both in arcade games like _Bubble Shooter_, as well as in simulation games with realistic bullet drops like _World of Tanks_, the _Battlefield_ series and more...

**Example Images:**
Chart of bullet drop over distance on _Battlefield Bad Company 2_:<br/>
![Bullet drop Graph](/Images/BulletDropChartBF.png)<br/><br/>
Prediction line of orb path in _Bubble Shooter_:<br/>
![Bubble Shooter img](/Images/BubbleShooterPrediciton.png)<br/><br/>

![Bullet drop Graph](/Images/BulletDropChartBF.png)<br/><br/>
## Sources:
<br/>
Formulas and basic explanation for calculating a bullet trajectory: https://sciencing.com/calculate-bullet-trajectory-5185428.html<br/>
Reference for parts of implementation of Bullet trajectory: https://www.omnicalculator.com/physics/trajectory-projectile-motion<br/>
FreeCam source reference: https://gist.github.com/ashleydavis/f025c03a9221bc840a2b<br/>