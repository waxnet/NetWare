namespace NetWareLoader.SharpMonoInjector
{
    public class Assembler
    {
        private readonly List<byte> _asm = new List<byte>();

        public void MovRax(nint arg)
        {
            _asm.AddRange(new byte[] { 0x48, 0xB8 });
            _asm.AddRange(BitConverter.GetBytes(arg));
        }

        public void MovRcx(nint arg)
        {
            _asm.AddRange(new byte[] { 0x48, 0xB9 });
            _asm.AddRange(BitConverter.GetBytes(arg));
        }

        public void MovRdx(nint arg)
        {
            _asm.AddRange(new byte[] { 0x48, 0xBA });
            _asm.AddRange(BitConverter.GetBytes(arg));
        }

        public void MovR8(nint arg)
        {
            _asm.AddRange(new byte[] { 0x49, 0xB8 });
            _asm.AddRange(BitConverter.GetBytes(arg));
        }

        public void MovR9(nint arg)
        {
            _asm.AddRange(new byte[] { 0x49, 0xB9 });
            _asm.AddRange(BitConverter.GetBytes(arg));
        }

        public void SubRsp(byte arg)
        {
            _asm.AddRange(new byte[] { 0x48, 0x83, 0xEC });
            _asm.Add(arg);
        }

        public void CallRax()
        {
            _asm.AddRange(new byte[] { 0xFF, 0xD0 });
        }

        public void AddRsp(byte arg)
        {
            _asm.AddRange(new byte[] { 0x48, 0x83, 0xC4 });
            _asm.Add(arg);
        }

        public void MovRaxTo(nint dest)
        {
            _asm.AddRange(new byte[] { 0x48, 0xA3 });
            _asm.AddRange(BitConverter.GetBytes(dest));
        }

        public void Push(nint arg)
        {
            _asm.Add((int)arg < 128 ? (byte)0x6A : (byte)0x68);
            _asm.AddRange((int)arg <= 255 ? new[] { (byte)arg } : BitConverter.GetBytes((int)arg));
        }

        public void MovEax(nint arg)
        {
            _asm.Add(0xB8);
            _asm.AddRange(BitConverter.GetBytes((int)arg));
        }

        public void CallEax()
        {
            _asm.AddRange(new byte[] { 0xFF, 0xD0 });
        }

        public void AddEsp(byte arg)
        {
            _asm.AddRange(new byte[] { 0x83, 0xC4 });
            _asm.Add(arg);
        }

        public void MovEaxTo(nint dest)
        {
            _asm.Add(0xA3);
            _asm.AddRange(BitConverter.GetBytes((int)dest));
        }

        public void Return()
        {
            _asm.Add(0xC3);
        }

        public byte[] ToByteArray() => _asm.ToArray();
    }
}
