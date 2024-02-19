namespace NetWareLoader.SharpMonoInjector
{
    public struct ExportedFunction
    {
        public string Name;

        public nint Address;

        public ExportedFunction(string name, nint address)
        {
            Name = name;
            Address = address;
        }
    }
}
