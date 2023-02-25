/* eslint-disable no-unused-vars */
const conversions = {
    minuteOfAngle: 1.04719755119, // Inches at 100 yards
    mil: 3.6, // Inches at 100 yards
    pi: 3.14159265358979,
    degreesToRadians: (degrees) => {
        // Converts from a degree to a radian angle.
        return degrees * conversions.pi / 180;
    },
    inchesToIPHY: (inches, currentRange) => {
        // Converts from inches to inches per 100 yards
        return (inches * 100 / currentRange);
    },
    inchesToMil: (inches, currentRange) => {
        // Converts from inches to milliradians.
        return (inches * 100 / conversions.mil / currentRange);
    },
    inchesToMinutesOfAngle: (inches, currentRange) => {
        // Converts from inches to minutes of angle (MoA).
        return (inches * 100 / conversions.minuteOfAngle / currentRange);
    },
    isEven: (input) => {
        // Returns true if the inputed integer is an even number.
        if (input / 2 * 2 === input) {
            return true;
        } else {
            return false;
        }
    },
    metersToYards: (meters) => {
        return (meters / 0.9144);
    },
    milesPerHourToInchesPerSecond: (inputVelocityMPH) => {
        // Converts from a miles per hour (MPH) to inches per second.
        return inputVelocityMPH * 17.6004;
    },
    radiansToDegrees: (radians) => {
        // Converts from a radian ro a degree angle.
        return radians * 180 / conversions.pi;
    },
    sec: (angle) => {
        // Secant
        return 1 / Math.cos(angle);
    },
    sizeToDistance: (actualTargetSizeInches, reticleViewedTargetSizeMils) => {
        return Math.round((actualTargetSizeInches / 36) * 1000 / reticleViewedTargetSizeMils);
    },
    yardsToMeters: (yards) => {
        return (yards * 0.9144);
    }
};

export default conversions;
