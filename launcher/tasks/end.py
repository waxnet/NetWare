# libraries
from . import Tasks

# task
@Tasks(100, "Done!", "")
def _done():
    return True
