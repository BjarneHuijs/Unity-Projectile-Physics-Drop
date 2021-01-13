# Unity Projectile Physics drop simulation.

**Introduction:**
In games where projectiles are fired, launched or thrown by an AI. Having that AI be able to calculate or predict where the projectile will fall can be essential to have the AI function in a manner that is wanted by the developer.

In this simulation I ignored any type of friction caused by wind, this means the only applied force on the bullet is gravity.
To calculate this trajectory we need multiple pieces.

First off, The distance traveled by the bullet can be calculated using this formula: ![Bullet travel distance formula](/Images/BulletTravelDistance.png)
	* (v0x) is its starting speed
	* (h) is the height it’s fired from
	* (g) is the acceleration due to gravity.(generally gravity acceleration has a constant value of -9.81. This represents the downward pull of 1G. This standard is one I followed as well)



Sources:
Formulas bullet trajectory: https://sciencing.com/calculate-bullet-trajectory-5185428.html
Extras Bullet trajectory: https://www.omnicalculator.com/physics/trajectory-projectile-motion
