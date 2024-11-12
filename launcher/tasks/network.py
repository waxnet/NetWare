# libraries
import socket

from . import Tasks

# task
@Tasks(0, "Checking network . . .", "Network connection is absent")
def _check_network() -> bool:
    # check if user is connected to network
    try:
        connection = socket.create_connection(("www.google.com", 80))
        
        if connection is not None:
            connection.close()
        return True
    except:
        return False
