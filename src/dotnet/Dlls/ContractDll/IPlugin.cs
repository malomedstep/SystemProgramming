namespace ContractDll {
    public interface IPlugin {
        string Encode(string text, byte param);
        string Name { get; }
    }
}