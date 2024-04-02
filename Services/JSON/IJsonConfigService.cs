namespace CodeMechanic.RazorHAT.Services;

public interface IJsonConfigService
{
    public string ReadConfig(string filename);
    public T GetSetting<T>(string key, string json);
    T ReadConfigSettings<T>(string filename);
}