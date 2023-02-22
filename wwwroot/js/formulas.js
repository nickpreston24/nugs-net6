export function calcKineticEnergy(m,v) {
	return calcKineticEnergy(m,v,false);
}

export function calcKineticEnergy(m,v,inJoules) {
	// m = mass in grains
	// v = velocity in feet per second
	// e = energy in ft lbs
	if(!m)
		return "N/A";
	if(!v)
		return "N/A";
	var e = (m*(v*v))/(450282);
	if(!inJoules)
		return Math.round(e);
	else 
		return Math.round(e * 1.35581795);
}

export function calculate_MPBR(v0, bc, bc_type = "G1") {
	return -9999
}