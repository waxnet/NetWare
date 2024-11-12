# libraries
import subprocess

from . import Tasks

# task
@Tasks(1, "Installing .NET . . .", "Could not install .NET")
def _install_dotnet() -> bool:
    is_installed = False

    # check for dotnet
    try:
        result = subprocess.run(
            ["dotnet", "--version"],
            creationflags=subprocess.CREATE_NO_WINDOW,
            capture_output=True, text=True
        )
        is_installed = (result.returncode == 0)
    except: pass

    # install if not installed
    if not is_installed:
        try:
            # install dotnet 8 using winget
            result = subprocess.run(
                [
                    "winget", "install", "Microsoft.DotNet.SDK.8",
                    "--accept-source-agreements", "--accept-package-agreements"
                ],
                creationflags=subprocess.CREATE_NO_WINDOW,
                capture_output=True, text=True
            )

            # return command status
            return (result.returncode == 0)
        except:
            return False
    return True
