# libraries
from threading import Thread
import tkinter as tk
import math
import time

from ui import Window
from tasks import (
    Tasks,

    network,
    dotnet,
    loader,
    end
)

# methods
def main() -> None:
    # make root
    root = tk.Tk()

    # init app
    app = Window(root)

    # launch
    def launch():
        tasks = Tasks.get()
        percentage_per_task = math.ceil((1 / len(tasks)) * 100)

        time.sleep(1)
        app.update_progress(1)
        for task in tasks:
            task_status = task[0]
            task_function = task[1]
            task_error = task[2]

            # set status
            app.update_status(task_status)

            # run function
            status = task_function()
            if not status:
                app.update_status(task_error)
                app.error()
                return
            time.sleep(.1)

            # update progress bar
            app.update_progress(percentage_per_task)
        time.sleep(.25)
        root.quit()
    Thread(target=launch).start()
    
    # root mainloop
    root.mainloop()

# entry
if __name__ == "__main__":
    main()
