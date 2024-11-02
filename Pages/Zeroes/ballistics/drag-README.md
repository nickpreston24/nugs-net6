# Ballistics Drag Functions NPM Library

## Constants

* `gravity` - Feet per second per second

## Functions

* `clicksToReachMaximumPointBlankRangeZero(ballisticCoefficient, scopeHeightInches, scopeElevationClicksPerMOA, maximumOrdinate, muzzleVelocityFPS, muzzleAngleDegrees)` - Calculates how many up or down clicks to adjust the scope to change from the current zero to the maximum point blank range zero.  Maximum point blank range is the maximum range at which the user can shoot, without holdover or scope adjustment, while not exceeding a pre-determined maximum ordinate (target radius).
* `crossWindDrift(currentRangeYards, currentTimeSeconds, crossWindAngleDegrees, crossWindVelocityMPH, muzzleAngleDegrees, muzzleVelocityFPS)` - Calculates how far the bullet drifts (inches) due to wind.
* `drop(muzzleVelocityFPS, currentVelocityFPS, currentTimeSeconds)` - Calculates how far the bullet falls (inches) due to gravity, if their were no angle at the muzzle.
* `energy(bulletWeightGrains, currentVelocityFPS)` - Calculates the kinetic energy (foot pounds) retained in the bullet.
* `ingalsSpaceFromVelocity(currentVelocity)` - Returns the space value from the Ingals table at the given velocity.
* `ingalsTimeFromVelocity(currentVelocity)` - Returns the Time value from the Ingals table at the given Velocity.
* `ingalsVelocityFromSpace(currentSpace)` - Returns the Velocity value from the Ingals table at the given Space.
* `ingalsVelocityFromTime(currentTime)` - Returns the Velocity value from the Ingals table at the given Time.
* `lead(targetSpeedMPH, currentTimeSeconds)` - Calculates how far the user needs to lead (inches) a moving target.
* `maximumPointBlankRange(ballisticCoefficient, muzzleVelocityFPS, maximumOrdinate)` - Calculate the maximum range at which the user can shoot, without holdover or scope adjustment, while not exceeding a pre-determined maximum ordinate (target radius).
* `maximumPointBlankRangeZero(ballisticCoefficient, muzzleVelocityFPS, maximumOrdinate)` - Maximum Point Blank Range Zero (yards) is the range that the user should zero his/her rifle to obtain their maximum point blank range. This range allows a user to shoot, without holdover or scope adjustment, while not exceeding a pre-determined maximum ordinate (target radius).
* `modifiedBallisticCoefficient(ballisticCoefficient, altitudeFeet, temperatureFahrenheit, barometricPressureInchesHg, relativeHumidityPercent)` - Takes the bullets ballistic coefficient at standard atmospheric conditions (sea level), and converts it to a new ballistic coefficient at the current altitudeFeet and conditions.
* `muzzleAngleDegreesForZeroRange(muzzleVelocityFPS, zeroRangeYards, scopeHeightInches, ballisticCoefficient)` - Calculates the neccessary angle (degrees) of the muzzle to obtain the requested zero range.  This is done by looping through vertical position with different muzzle angles at the given range until a muzzle angle is found that produces a vertical position of 0.
* `optimalRiflingTwist(bulletDiameterInches, bulletLengthInches)` - Calculates the best rifling twist rate (inches per twist) to stabalize the length of bullet being used.
* `range(ballisticCoefficient, muzzleVelocityFPS, currentVelocityFPS)` - Calculates the range (yards) of the bullet at a given velocity.
* `rifleRecoilVelocity(bulletWeightGrains, muzzleVelocityFPS, powderWeightGrains, rifleWeightPounds)` - Calculates the amount of rearward velocity (feet per second) of the rifle upon firing.
* `rifleRecoilEnergy(bulletWeightGrains, muzzleVelocityFPS, powderWeightGrains, rifleWeightPounds)` - Calculates the amount of rearward force (foot pounds) of the rifle upon firing.
* `sectionalDensity(bulletWeightGrains, bulletDiameterInches)` - Calculates the mass per given diameter of the bullet.  Used in determining form factor.
* `time(ballisticCoefficient, muzzleVelocityFPS, currentVelocityFPS)` - Calculates the amount of time (seconds) it takes the bullet to slow from the initial velocity to a specific lower velocity.
* `velocityFromRange(ballisticCoefficient, muzzleVelocityFPS, currentRangeYards)` - Calculates the velocity (feet per second) remaining in the bullet at a given range (yards).
* `velocityFromTime(ballisticCoefficient, muzzleVelocityFPS, currentTimeSeconds)` - Calculates the velocity (feet per second) remaining in the bullet at a given time (seconds).
* `verticalPosition(scopeHeightInches, muzzleAngleDegrees, currentRangeYards, currentDropInches)` - Calculates how far the bullet falls (inches) due to gravity, taking into account the angle of the muzzle.
}
