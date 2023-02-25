# Atmospherics NPM library

## Constants

* `altitudeAdjustmentFactorTable` - Altitude adjustment factor at altitude (feet / 1000)
* `barometricPressureTable` - Standard barometric pressure (inches Hg) at altitude (feet / 1000)
* `dropTable` - currentVelocity/muzzleVelocity (feet per second / 1000)
* `speedOfSoundAtSeaLevel` - (feet per second)
* `temperatureTable` - Standard temperature (degrees F) at altitude (feet / 1000)
* `vaporPressureOfWaterTable` - Vapor pressure of water (inches Hg) at temperature (degrees F / 2)
* `weightDensityOfAirAtSeaLevel` - (pounds per cubic foot)

## Functions

* `altitudeAdjustmentFactor(altitude)` - Adjusts from the standard altitude (sea level) to the current altitude.
* `barometricPressureAdjustmentFactor(altitude, barometricPressure)` - Compares the barometric pressure (inches Hg) at a given altitude (feet) to the standard barometric pressure at that altitude.
* `interpolateArray(array, arrayIndex)` - Takes the nearest 2 numbers in a lookup table and returns a number between them.
* `relativeHumidityAdjustmentFactor(temperature, barometricPressure, relativeHumidity)` - Compares the relative humidity (percentage) at a given altitude (feet) to the standard relative humidity at that altitude.
* `speedOfSound(altitude)` - Speed of sound (feet per second) at a given altitude (feet).
* `speedOfSoundFactor(altitude)` - The speed of sound factor compares the speed of sound (feet per second) at a given altitude (feet) to the standard speed of sound at sea level.
* `standardRelativeHumidity(altitude)` - Compares the relative humidity (percentage) at a given altitude (feet) to the standard relative humidity at that altitude.
* `temperatureAdjustmentFactor(altitude, temperature)` - Compares the temperature (degrees F) at a given altitude (feet) to the standard temperature at that altitude.
* `weightDensityOfAir(altitude)` - Barometric pressure (inches Hg) at a given altitude (feet)
