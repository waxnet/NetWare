# libraries
from win32api import GetSystemMetrics
import tkinter as tk

# window class
class Window:
    def __init__(self, root):
        # initialize variables
        self._progress_value = 0

        self._dragging = False
        self._mouse_x = 0
        self._mouse_y = 0

        # setup window
        self.root = root
        root.attributes("-topmost", True)
        self.root.overrideredirect(True)
        self.root.config(bg="#000000")

        # window position and size
        self.width = 300
        self.height = 90

        position_x = round((GetSystemMetrics(0) / 2) - (self.width / 2))
        position_y = round((GetSystemMetrics(1) / 2) - (self.height / 2))

        self.root.geometry(f"{self.width}x{self.height}+{position_x}+{position_y}")

        # build window
        self._build()

    # menu
    def _build(self):
        # outline
        outline_frame = tk.Frame(
            self.root,
            bg="#222222",
            bd=0,
            width=(self.width - 2),
            height=(self.height - 2)
        )
        outline_frame.place(x=1, y=1)

        # title bar
        title_bar = tk.Frame(
            outline_frame,
            bg="#181818",
            bd=0,
            width=self.width,
            height=20
        )
        title_bar.place(x=0, y=0)

        # title
        title_label = tk.Label(
            title_bar,
            text="NetWare Launcher",
            font=("Arial", 8, "bold", "italic"),
            bg="#181818",
            fg="white"
        )
        title_label.place(x=0, y=0)

        # close
        close_button = tk.Button(
            title_bar,
            command=self.root.destroy,
            relief="sunken",
            bd=0,
            borderwidth=0,
            text="✖",
            font=("Arial", 8),
            bg="#181818",
            fg="#ffffff",
            activebackground="#181818",
            activeforeground="#ffffff"
        )
        close_button.place(x=(self.width - 20), y=0)

        # status
        self._status_label = tk.Label(
            outline_frame,
            text="Starting . . .",
            font=("Arial", 8, "bold"),
            bg="#222222",
            fg="white"
        )
        self._status_label.place(x=6, y=32)

        # progress bar
        self._progress_bar = tk.Canvas(
            outline_frame,
            highlightthickness=0,
            bg="#191919",
            width=(self.width - 20),
            height=20
        )
        self._progress_bar.place(x=8, y=50)

        # bind drag events
        title_bar.bind("<B1-Motion>", self._drag)
        title_bar.bind("<Button-1>", self._start_drag)
        title_bar.bind("<ButtonRelease-1>", self._stop_drag)

    # drag logic
    def _drag(self, event):
        if self._dragging:
            x = self.root.winfo_x() + (event.x - self._mouse_x)
            y = self.root.winfo_y() + (event.y - self._mouse_y)
            self.root.geometry(f"+{x}+{y}")

    def _start_drag(self, event):
        self._dragging = True
        self._mouse_x = event.x
        self._mouse_y = event.y

    def _stop_drag(self, _):
        self._dragging = False

    # update values
    def update_progress(self, value):
        self._progress_value += value
        
        self._progress_bar.delete("all")
        self._progress_bar.create_rectangle(
            0,
            0,
            (self._progress_value * 2.8),
            20,
            outline="#222222",
            fill="#00ff00"
        )

    def update_status(self, text):
        self._status_label.config(text=text)

    # error
    def error(self):
        self._progress_bar.delete("all")
        self._progress_bar.create_rectangle(
            0,
            0,
            (self._progress_value * 2.8),
            20,
            outline="#222222",
            fill="#ff0000"
        )
