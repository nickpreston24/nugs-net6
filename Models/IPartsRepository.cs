namespace nugsnet6.Models;

public interface IPartRepository
{
	void InsertPart(Part Part);
	IList<Part> GetPartByType(string type);
	void UpdatePartList(IList<Part> PartList);
}
