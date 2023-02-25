const atmospherics = {
    // Altitude adjustment factor at altitude (feet / 1000)
    altitudeAdjustmentFactorTable: [1, 1.031, 1.062, 1.095, 1.128, 1.163, 1.199, 1.236, 1.273, 1.313, 1.353, 1.394, 1.437, 1.481, 1.527, 1.573],
    // Standard barometric pressure (inches Hg) at altitude (feet / 1000)
    barometricPressureTable: [29.53, 28.45, 27.41, 26.41, 25.45, 24.52, 23.62, 22.75, 21.91, 21.11, 20.33, 19.58, 18.85, 18.16, 17.48, 16.83],
    // currentVelocity/muzzleVelocity (Feet Per Second / 1000)
    dropTable: [113, 114, 115, 115.5, 116, 117, 118, 118.5, 119, 120, 121, 121.5, 122, 123, 124, 124.5, 125, 126, 127, 127.5, 128, 129, 130, 130.5, 131, 131, 133, 133.5, 134, 135, 136, 136.5, 137, 138, 139, 140.5, 142, 143, 144, 144.5, 145, 146.5, 148, 149, 150, 151, 152, 153, 154, 155, 156, 156.5, 157, 158.5, 160, 160.5, 161, 161.5, 162, 163, 164, 165, 166, 166.5, 167, 168, 169, 169.5, 170, 171, 172, 173, 174, 174.5, 175, 175.5, 176, 177, 178, 179, 180, 180.5, 181, 181.5, 182, 183, 184, 184.5, 185, 185.5, 186, 196.5, 187, 188.5, 190, 190.5, 191, 191.5, 192, 192.5, 193],
    speedOfSoundAtSeaLevel: 1120.27, // Feet Per Second
    // Standard temperature (degrees F) at altitude (feet / 1000)
    temperatureTable: [59, 55.4, 51.9, 48.3, 44.7, 41.2, 37.6, 34.1, 30.5, 26.9, 23.4, 19.8, 16.2, 12.7, 9.1, 5.5],
    // Vapor pressure of water (inches Hg) at temperature (degrees F / 2)
    vaporPressureOfWaterTable: [0.04, 0.04, 0.05, 0.05, 0.06, 0.06, 0.07, 0.08, 0.08, 0.09, 0.1, 0.11, 0.12, 0.14, 0.15, 0.16, 0.18, 0.2, 0.21, 0.23, 0.25, 0.27, 0.29, 0.31, 0.34, 0.36, 0.39, 0.42, 0.45, 0.49, 0.52, 0.56, 0.6, 0.64, 0.69, 0.74, 0.79, 0.85, 0.9, 0.97, 1.03, 1.1, 1.18, 1.25, 1.34, 1.42, 1.51, 1.61, 1.71, 1.82, 1.93, 2.05, 2.18, 2.31, 2.45, 2.6, 2.75, 2.91, 3.08, 3.26, 3.45, 3.64],
    weightDensityOfAirAtSeaLevel: 0.0751, // Pounds Per Cubic Foot
    altitudeAdjustmentFactor: (altitude) => {
        // Adjusts from the standard altitude (sea level) to the current altitude.
        return atmospherics.interpolateArray(atmospherics.altitudeAdjustmentFactorTable, altitude / 1000);
    },
    barometricPressureAdjustmentFactor: (altitude, barometricPressure) => {
        // Compares the barometric pressure (inches Hg) at a given altitude (feet) to the standard barometric pressure at that altitude.
        const standardBarometricPressure = atmospherics.interpolateArray(atmospherics.barometricPressureTable, altitude / 1000);
        return (barometricPressure - standardBarometricPressure) / standardBarometricPressure;
    },
    interpolateArray: (array, arrayIndex) => {
        // Takes the nearest 2 numbers in a lookup table and returns a number between them.
        const maxIndex = array.length;
        const minIndex = 0;
        let result = 0;
        if (arrayIndex <= minIndex) {
            result = array[minIndex];
        } else if (arrayIndex >= maxIndex) {
            result = array[maxIndex];
        } else {
            const integerPartOfArrayIndex = Math.floor(arrayIndex);
            const decimalPartOfArrayIndex = arrayIndex - integerPartOfArrayIndex;
            const rangeSpread = array[integerPartOfArrayIndex + 1] - array[integerPartOfArrayIndex];
            result = array[integerPartOfArrayIndex] + (rangeSpread * decimalPartOfArrayIndex);
        }
        return result;
    },
    relativeHumidityAdjustmentFactor: (temperature, barometricPressure, relativeHumidity) => {
        // Compares the relative humidity (percentage) at a given altitude (feet) to the standard relative humidity at that altitude.
        const standardVaporPressureOfWater = atmospherics.interpolateArray(atmospherics.vaporPressureOfWaterTable, temperature / 2);
        return 0.995 * (barometricPressure / (barometricPressure - 0.3783 * relativeHumidity * standardVaporPressureOfWater));
    },
    speedOfSound: (altitude) => {
        // Speed of sound (feet per second) at a given altitude (feet).
        return atmospherics.speedOfSoundFactor(altitude) * atmospherics.speedOfSoundAtSeaLevel;
    },
    speedOfSoundFactor: (altitude) => {
        // The speed of sound factor compares the speed of sound (feet per second) at a given altitude (feet) to the standard speed of sound at sea level.
        return 1 - 0.00001126666 * altitude - 0.00000000006753074 * Math.pow(altitude, 2);
    },
    standardRelativeHumidity: (altitude) => {
        // Compares the relative humidity (percentage) at a given altitude (feet) to the standard relative humidity at that altitude.
        const standardTemperature = atmospherics.interpolateArray(atmospherics.temperatureTable, altitude / 1000);
        const standardVaporPressureOfWater = atmospherics.interpolateArray(atmospherics.vaporPressureOfWaterTable, standardTemperature / 2);
        const standardBarometricPressure = atmospherics.interpolateArray(atmospherics.barometricPressureTable, altitude / 1000);
        return (standardBarometricPressure - (0.995 * standardBarometricPressure)) / (0.3783 * standardVaporPressureOfWater);
    },
    temperatureAdjustmentFactor: (altitude, temperature) => {
        // Compares the temperature (degrees F) at a given altitude (feet) to the standard temperature at that altitude.
        const standardTemperature = atmospherics.interpolateArray(atmospherics.temperatureTable, altitude / 1000);
        return (temperature - standardTemperature) / (459.6 + standardTemperature);
    },
    weightDensityOfAir: (altitude) => {
        // Barometric pressure (inches Hg) at a given altitude (feet)
        return atmospherics.weightDensityOfAirAtSeaLevel * Math.exp(-0.0000302149 * altitude);
    }
}

export default atmospherics;
