using dnlib.DotNet.Emit;
using dnlib.DotNet;

namespace NetWareLoader
{
    public static class AntiCheat
    {
        public static bool Patch()
        {
            // classes to patch
            string[] targetClasses = {
                "ObscuredCheatingDetector",
                "SpeedHackDetector",
                "InjectionDetector"
            };

            // get and check paths
            string originalAssembly = Path.Combine(Data.gamePath, "1v1_LOL_Data\\Managed\\ACTk.Runtime.dll");
            string patchedAssembly = Path.Combine(Data.gamePath, "1v1_LOL_Data\\Managed\\ACTk.Runtime.dll_");

            if (!File.Exists(originalAssembly))
                return false;

            // load original anticheat assembly
            ModuleDefMD module = ModuleDefMD.Load(originalAssembly);

            // patch classes
            foreach (string targetClass in targetClasses)
            {
                // find class
                TypeDef type = module.Find("CodeStage.AntiCheat.Detectors." + targetClass, true);

                // patch all methods called "StartDetection"
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
