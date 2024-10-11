# libraries
import subprocess
import os

from . import Tasks

# task
@Tasks(2, "Starting loader . . .", "Could not start the loader")
def _start_loader():
    # get loader path
    current_path = os.getcwd()
    loader_path = os.path.join(
        current_path, "bin", "Loader.exe"
    )
    
    # check path
    if not os.path.exists(loader_path):
        return False
    
    # start loader
    try:
        subprocess.Popen(
            ["cmd.exe", "/c", "start", "", loader_path],
            creationflags=(
                subprocess.CREATE_NEW_CONSOLE |
                subprocess.CREATE_NO_WINDOW
            )
        )
        return True
    except:
        return False
