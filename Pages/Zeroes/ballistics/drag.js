// import atmospherics from './atmospherics';
// import conversions from './conversions';
// import INGALS from './ingals.data';

const drag = {
	gravity: 32.176,     // Feet Per Second Per Second
	clicksToReachMaximumPointBlankRangeZero: (ballisticCoefficient, scopeHeightInches, scopeElevationClicksPerMOA, maximumOrdinate, muzzleVelocityFPS, muzzleAngleDegrees) => {
		// Calculates how many up or down clicks to adjust the scope to change from the current zero to the maximum point blank range zero.  Maximum point blank range is the maximum range at
		//     which the user can shoot, without holdover or scope adjustment, while not exceeding a pre-determined maximum ordinate (target radius).
		// Calculate the range you need to zero the rifle to obtain a maximum point blank range
		const maxPointBlankRangeZeroYards = drag.maximumPointBlankRangeZero(ballisticCoefficient, muzzleVelocityFPS, maximumOrdinate);
		// Calculate the velocity (feet per second) of the bullet at the new zero
		const velocityAtMaxPointBlankRangeZero = drag.velocityFromRange(ballisticCoefficient, muzzleVelocityFPS, maxPointBlankRangeZeroYards);
		// Calculate the time (seconds) of flight of the bullet at the new velocity
		const timeAtMaxPointBlankRangeZero = drag.time(ballisticCoefficient, muzzleVelocityFPS, velocityAtMaxPointBlankRangeZero);
		// Calculate the drop (inches) of the bullet at the new time and velocity
		const dropAtMaxPointBlankRangeZero = drag.drop(muzzleVelocityFPS, velocityAtMaxPointBlankRangeZero, timeAtMaxPointBlankRangeZero);
		// Calculate the vertical position (inches) of the bullet at the given drop
		const verticalPositionAtMaxPointBlankRangeZero = (-scopeHeightInches / 12) + ((dropAtMaxPointBlankRangeZero / 12) + (maxPointBlankRangeZeroYards * 3) * Math.tan(conversions.degreesToRadians(muzzleAngleDegrees))) * 12;
		// Calculate the number of scope clicks needed to correct the above calculated vertical position making the new vertical position zero.
		return -(conversions.inchesToMinutesOfAngle(verticalPositionAtMaxPointBlankRangeZero, maxPointBlankRangeZeroYards) * scopeElevationClicksPerMOA);
	},
	crossWindDrift: (currentRangeYards, currentTimeSeconds, crossWindAngleDegrees, crossWindVelocityMPH, muzzleAngleDegrees, muzzleVelocityFPS) => {
		// Calculates how far the bullet drifts (inches) due to wind.
		return (Math.sin(conversions.degreesToRadians(crossWindAngleDegrees)) * conversions.milesPerHourToInchesPerSecond(crossWindVelocityMPH) / 12 * (currentTimeSeconds - (currentRangeYards * 3) / (muzzleVelocityFPS * Math.cos(conversions.degreesToRadians(muzzleAngleDegrees))))) * 12;
	},
	drop: (muzzleVelocityFPS, currentVelocityFPS, currentTimeSeconds) => {
		// Calculates how far the bullet falls (inches) due to gravity, if their were no angle at the muzzle.
		const falls = atmospherics.dropTable[Math.floor((currentVelocityFPS / muzzleVelocityFPS) * 100 + 0.5)];
		return -(falls * Math.pow(currentTimeSeconds, 2));
	},
	energy: (bulletWeightGrains, currentVelocityFPS) => {
		// Calculates the kinetic energy (foot pounds) retained in the bullet.
		return bulletWeightGrains * Math.pow(currentVelocityFPS, 2) / (drag.gravity * 7000 * 2);
	},
	ingalsSpaceFromVelocity: (currentVelocity) => {
		// Returns the space value from the Ingals table at the given velocity.
		let counter = 0;
		while(INGALS.v[counter] > currentVelocity) {
			counter++;
		}
		let spaceFromVelocity;
		if(INGALS.v[counter] === currentVelocity) {
			spaceFromVelocity = INGALS.s[counter];
		} else {
			// Interoperlate Array
			const differenceBetweenVelocityIndexes = INGALS.v[counter - 1] - INGALS.v[counter];
			const distanceFromVelocityIndex = currentVelocity - INGALS.v[counter];
			const differenceBetweenSpaceIndexes = INGALS.s[counter] - INGALS.s[counter - 1];
			const percentage = distanceFromVelocityIndex / differenceBetweenVelocityIndexes;
			spaceFromVelocity = INGALS.s[counter] - (differenceBetweenSpaceIndexes * percentage);
		}
		return spaceFromVelocity;
	},
	ingalsTimeFromVelocity: (currentVelocity) => {
		// Returns the Time value from the Ingals table at the given Velocity.
		let counter = 0;
		while(INGALS.v[counter] > currentVelocity) {
			counter++;
		}
		let timeFromVelocity;
		if(INGALS.v[counter] === currentVelocity) {
			timeFromVelocity = INGALS.t[counter];
		} else {
			// Interoperlate Array
			const differenceBetweenVelocityIndexes = INGALS.v[counter - 1] - INGALS.v[counter];
			const distanceFromVelocityIndex = currentVelocity - INGALS.v[counter];
			const differenceBetweenTimeIndexes = INGALS.t[counter] - INGALS.t[counter - 1];
			const percentage = distanceFromVelocityIndex / differenceBetweenVelocityIndexes;
			timeFromVelocity = INGALS.t[counter] - (differenceBetweenTimeIndexes * percentage);
		}
		return timeFromVelocity;
	},
	ingalsVelocityFromSpace: (currentSpace) => {
		// Returns the Velocity value from the Ingals table at the given Space.
		let counter = 0;
		while(INGALS.s[counter] < currentSpace) {
			counter++;
		}
		let velocityFromSpace;
		if(INGALS.s[counter] === currentSpace) {
			velocityFromSpace = INGALS.v[counter];
		} else {
			// Interoperlate Array
			const differenceBetweenSpaceIndexes = INGALS.s[counter] - INGALS.s[counter - 1];
			const distanceFromSpaceIndex = INGALS.s[counter] - currentSpace;
			const differenceBetweenVelocityIndexes = INGALS.v[counter - 1] - INGALS.v[counter];
			const percentage = distanceFromSpaceIndex / differenceBetweenSpaceIndexes;
			velocityFromSpace = INGALS.v[counter] + (differenceBetweenVelocityIndexes * percentage);
		}
		return velocityFromSpace;
	},
	ingalsVelocityFromTime: (currentTime) => {
		// Returns the Velocity value from the Ingals table at the given Time.
		let counter = 0;
		while(INGALS.t[counter] < currentTime) {
			counter++;
		}
		let velocityFromTime;
		if(INGALS.t[counter] === currentTime) {
			velocityFromTime = INGALS.v[counter];
		} else {
			// Interoperlate Array
			const differenceBetweenTimeIndexes = INGALS.t[counter] - INGALS.t[counter - 1];
			const distanceFromTimeIndex = INGALS.t[counter] - currentTime;
			const differenceBetweenVelocityIndexes = INGALS.v[counter - 1] - INGALS.v[counter];
			const percentage = distanceFromTimeIndex / differenceBetweenTimeIndexes;
			velocityFromTime = INGALS.v[counter] + (differenceBetweenVelocityIndexes * percentage);
		}
		return velocityFromTime;
	},
	lead: (targetSpeedMPH, currentTimeSeconds) => {
		// Calculates how far the user needs to lead (inches) a moving target.
		return conversions.milesPerHourToInchesPerSecond(targetSpeedMPH) * currentTimeSeconds;
	},
	maximumPointBlankRange: (ballisticCoefficient, muzzleVelocityFPS, maximumOrdinate) => {
		// console.log('maximumOrdinate :>> ', maximumOrdinate);
		// Calculate the maximum range at which the user can shoot, without holdover or scope adjustment, while not exceeding a pre-determined maximum ordinate (target radius).
		// Time (seconds)it takes to reach the range having a maximum ordinate supplied above
		const timeToMaximumOrdinate = 0.25 * Math.pow(maximumOrdinate / 3, 0.5);
		// Velocity (feet per second) of the bullet at the above calculated time
		const velocityAtTimeToMaximumOrdinate = drag.velocityFromTime(ballisticCoefficient, muzzleVelocityFPS, timeToMaximumOrdinate);
		// Drop (inches) of the bullet at the above given time and velocity***
		const dropAtMaximumPointBlankRangeZero = drag.drop(muzzleVelocityFPS, velocityAtTimeToMaximumOrdinate, timeToMaximumOrdinate);

		// console.log('dropAtMaximumPointBlankRangeZero :>> ', dropAtMaximumPointBlankRangeZero);
		// The bullet may drop the radius of the target below zero at the true maximum point blank range
		const dropAtMaximumPointBlankRange = dropAtMaximumPointBlankRangeZero - maximumOrdinate;
		// console.log('dropAtMaximumPointBlankRange :>> ', dropAtMaximumPointBlankRange);
		// Loop through dropping velocity until Drop = DropAtMaximumPointBlankRange to find the velocity at the true point blank range
		let velocityAtMaximumPointBlankRange = velocityAtTimeToMaximumOrdinate;
		while(drag.drop(muzzleVelocityFPS, velocityAtMaximumPointBlankRange, drag.time(ballisticCoefficient, muzzleVelocityFPS, velocityAtMaximumPointBlankRange)) > dropAtMaximumPointBlankRange) {
			// console.log('velocityAtMaximumPointBlankRange :>> ', velocityAtMaximumPointBlankRange);
			velocityAtMaximumPointBlankRange -= 0.1;
		}
		// Given the velocity at the point blank range, calculate the actual range
		return drag.range(ballisticCoefficient, muzzleVelocityFPS, velocityAtMaximumPointBlankRange);
	},
	maximumPointBlankRangeZero: (ballisticCoefficient, muzzleVelocityFPS, maximumOrdinate) => {
		// Maximum Point Blank Range Zero (yards) is the range that the user should zero his/her rifle to obtain their maximum point blank range.
		// This range allows a user to shoot, without holdover or scope adjustment, while not exceeding a pre-determined maximum ordinate (target radius).
		// Time (seconds)it takes to reach the range having a maximum ordinate supplied above
		const timeToMaximumOrdinate = 0.25 * Math.pow(maximumOrdinate / 3, 0.5);
		// Velocity (feet per second) of the bullet at the above calculated time
		const velocityAtTimeToMaximumOrdinate = drag.velocityFromTime(ballisticCoefficient, muzzleVelocityFPS, timeToMaximumOrdinate);
		// Given the velocity at the point blank range zero, calculate the actual range to zero the rifle
		return drag.range(ballisticCoefficient, muzzleVelocityFPS, velocityAtTimeToMaximumOrdinate);
	},
	modifiedBallisticCoefficient: (ballisticCoefficient, altitudeFeet, temperatureFahrenheit, barometricPressureInchesHg, relativeHumidityPercent) => {
		// Takes the bullets ballistic coefficient at standard atmospheric conditions (sea level), and converts it to a new ballistic coefficient at the current altitudeFeet and conditions.
		const altitudeAdjustmentFactor = atmospherics.altitudeAdjustmentFactor(altitudeFeet);
		const temperatureAdjustmentFactor = atmospherics.temperatureAdjustmentFactor(altitudeFeet, temperatureFahrenheit);
		const barometricPressureAdjustmentFactor = atmospherics.barometricPressureAdjustmentFactor(altitudeFeet, barometricPressureInchesHg);
		const relativeHumidityAdjustmentFactor = atmospherics.relativeHumidityAdjustmentFactor(temperatureFahrenheit, barometricPressureInchesHg, relativeHumidityPercent / 100);
		return ballisticCoefficient * (altitudeAdjustmentFactor * (1 + temperatureAdjustmentFactor - barometricPressureAdjustmentFactor) * relativeHumidityAdjustmentFactor);
	},
	muzzleAngleDegreesForZeroRange: (muzzleVelocityFPS, zeroRangeYards, scopeHeightInches, ballisticCoefficient) => {
		// Calculates the neccessary angle (degrees) of the muzzle to obtain the requested zero range.
		// This is done by looping through vertical position with different muzzle angles at the given range until a muzzle angle is found that produces a vertical position of 0.
		const velocityAtZeroRange = drag.velocityFromRange(ballisticCoefficient, muzzleVelocityFPS, zeroRangeYards);
		const timeAtZeroRange = drag.time(ballisticCoefficient, muzzleVelocityFPS, velocityAtZeroRange);
		const dropAtZeroRange = drag.drop(muzzleVelocityFPS, velocityAtZeroRange, timeAtZeroRange);
		let muzzleAngleDegreesForZeroRange = 0;
		while(drag.verticalPosition(scopeHeightInches, muzzleAngleDegreesForZeroRange, zeroRangeYards, dropAtZeroRange) < 0) {
		muzzleAngleDegreesForZeroRange += 0.00001;
		}
		return muzzleAngleDegreesForZeroRange;
	},
	optimalRiflingTwist: (bulletDiameterInches, bulletLengthInches) => {
		// Calculates the best rifling twist rate (inches per twist) to stabalize the length of bullet being used.
		return bulletDiameterInches * 150 / (bulletLengthInches / bulletDiameterInches);
	},
	range: (ballisticCoefficient, muzzleVelocityFPS, currentVelocityFPS) => {
		// Calculates the range (yards) of the bullet at a given velocity.
		return ballisticCoefficient * (drag.ingalsSpaceFromVelocity(currentVelocityFPS) - drag.ingalsSpaceFromVelocity(muzzleVelocityFPS)) / 3;
	},
	rifleRecoilVelocity: (bulletWeightGrains, muzzleVelocityFPS, powderWeightGrains, rifleWeightPounds) => {
		// Calculates the amount of rearward velocity (feet per second) of the rifle upon firing.
		return (bulletWeightGrains * muzzleVelocityFPS + powderWeightGrains * 4000) / (rifleWeightPounds * 7000);
	},
	rifleRecoilEnergy: (bulletWeightGrains, muzzleVelocityFPS, powderWeightGrains, rifleWeightPounds) => {
		// Calculates the amount of rearward force (foot pounds) of the rifle upon firing.
		return rifleWeightPounds * Math.pow(drag.rifleRecoilVelocity(bulletWeightGrains, muzzleVelocityFPS, powderWeightGrains, rifleWeightPounds), 2) / (drag.gravity * 2);
	},
	sectionalDensity: (bulletWeightGrains, bulletDiameterInches) => {
		// Calculates the mass per given diameter of the bullet.  Used in determining form factor.
		return bulletWeightGrains / (7000 * Math.pow(bulletDiameterInches, 2));
	},
	time: (ballisticCoefficient, muzzleVelocityFPS, currentVelocityFPS) => {
		// Calculates the amount of time (seconds) it takes the bullet to slow from the initial velocity to a specific lower velocity.
		return ballisticCoefficient * (drag.ingalsTimeFromVelocity(currentVelocityFPS) - drag.ingalsTimeFromVelocity(muzzleVelocityFPS));
	},
	velocityFromRange: (ballisticCoefficient, muzzleVelocityFPS, currentRangeYards) => {
		// Calculates the velocity (feet per second) remaining in the bullet at a given range (yards).
		const currentSpace = drag.ingalsSpaceFromVelocity(muzzleVelocityFPS) + ((currentRangeYards * 3) / ballisticCoefficient);
		return drag.ingalsVelocityFromSpace(currentSpace);
	},
	velocityFromTime: (ballisticCoefficient, muzzleVelocityFPS, currentTimeSeconds) => {
		// Calculates the velocity (feet per second) remaining in the bullet at a given time (seconds).
		return drag.ingalsVelocityFromTime(currentTimeSeconds / ballisticCoefficient + drag.ingalsTimeFromVelocity(muzzleVelocityFPS));
	},
	verticalPosition: (scopeHeightInches, muzzleAngleDegrees, currentRangeYards, currentDropInches) => {
		// Calculates how far the bullet falls (inches) due to gravity, taking into account the angle of the muzzle.
		return (currentDropInches+(currentRangeYards*36)*Math.tan(conversions.degreesToRadians(muzzleAngleDegrees)))-scopeHeightInches;
	}
}

// export default drag;


// // export 
// function test( ) {
// 	alert("ping!")
// }