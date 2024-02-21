using dnlib.DotNet.Emit;
using dnlib.DotNet;

namespace NetWareLoader
{
    public static class AntiCheat
    {
        public static bool Patch()
        {
            // get and check paths
            string originalAssembly = Path.Combine(Data.gamePath, "1v1_LOL_Data\\Managed\\ACTk.Runtime.dll");
            string patchedAssembly = Path.Combine(Data.gamePath, "1v1_LOL_Data\\Managed\\ACTk.Runtime.dll_");

            if (!File.Exists(originalAssembly))
                return false;

            // load anticheat assembly
            ModuleDefMD module = ModuleDefMD.Load(originalAssembly);

            // patch anticheat assembly
            foreach (TypeDef type in module.GetTypes())
            {
                // check if type is valid
                if (!type.FullName.StartsWith("CodeStage.AntiCheat.Detectors"))
                    continue;

                // patch all methods called "StartDetection" inside the type
                foreach (MethodDef method in type.FindMethods("StartDetection"))
                {
                    method.Body.Instructions.Clear();
                    method.Body.Instructions.Add(OpCodes.Ldnull.ToInstruction());
                    method.Body.Instructions.Add(OpCodes.Ret.ToInstruction());
                }
            }

            // write patched assembly
            module.Write(patchedAssembly);

            // cleanup
            module.Dispose();
            File.Delete(originalAssembly);
            File.Move(patchedAssembly, originalAssembly);

            return true;
        }
    }
}
