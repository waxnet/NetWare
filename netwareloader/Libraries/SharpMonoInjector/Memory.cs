using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace NetWareLoader.SharpMonoInjector
{
    public class Memory : IDisposable
    {
        private readonly nint _handle;

        private readonly Dictionary<nint, int> _allocations = new Dictionary<nint, int>();

        public Memory(nint processHandle)
        {
            _handle = processHandle;
        }

        public string ReadString(nint address, int length, Encoding encoding)
        {
            List<byte> bytes = [];

            for (int i = 0; i < length; i++)
            {
                byte read = ReadBytes(address + bytes.Count, 1)[0];

                if (read == 0x00)
                    break;

                bytes.Add(read);
            }

            return encoding.GetString(bytes.ToArray());
        }

        public string ReadUnicodeString(nint address, int length)
        {
            return Encoding.Unicode.GetString(ReadBytes(address, length));
        }

        public short ReadShort(nint address)
        {
            return BitConverter.ToInt16(ReadBytes(address, 2), 0);
        }

        public int ReadInt(nint address)
        {
            return BitConverter.ToInt32(ReadBytes(address, 4), 0);
        }

        public long ReadLong(nint address)
        {
            return BitConverter.ToInt64(ReadBytes(address, 8), 0);
        }

        public byte[] ReadBytes(nint address, int size)
        {
            byte[] bytes = new byte[size];

            if (!Native.ReadProcessMemory(_handle, address, bytes, size))
                throw new InjectorException("Failed to read process memory", new Win32Exception(Marshal.GetLastWin32Error()));

            return bytes;
        }

        public nint AllocateAndWrite(byte[] data)
        {
            nint addr = Allocate(data.Length);
            Write(addr, data);
            return addr;
        }

        public nint AllocateAndWrite(string data) => AllocateAndWrite(Encoding.UTF8.GetBytes(data));

        public nint AllocateAndWrite(int data) => AllocateAndWrite(BitConverter.GetBytes(data));

        public nint AllocateAndWrite(long data) => AllocateAndWrite(BitConverter.GetBytes(data));

        public nint Allocate(int size)
        {
            nint addr =
                Native.VirtualAllocEx(_handle, nint.Zero, size,
                    AllocationType.MEM_COMMIT, MemoryProtection.PAGE_EXECUTE_READWRITE);

            if (addr == nint.Zero)
                throw new InjectorException("Failed to allocate process memory", new Win32Exception(Marshal.GetLastWin32Error()));

            _allocations.Add(addr, size);
            return addr;
        }

        public void Write(nint addr, byte[] data)
        {
            if (!Native.WriteProcessMemory(_handle, addr, data, data.Length))
                throw new InjectorException("Failed to write process memory", new Win32Exception(Marshal.GetLastWin32Error()));
        }

        public void Dispose()
        {
            foreach (var kvp in _allocations)
                Native.VirtualFreeEx(_handle, kvp.Key, kvp.Value, MemoryFreeType.MEM_DECOMMIT);
        }
    }
}
