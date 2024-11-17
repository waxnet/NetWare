namespace Loader;

public partial class Window : Form
{
    // values
    private bool dragging = false;
    private Point dragCursorPoint;
    private Point dragFormPoint;

    private readonly List<Brush> brushes = [];

    // constructor
    public Window()
    {
        InitializeComponent();

        // bind load button
        loadButton.MouseClick += new MouseEventHandler(LoadButton_Click);

        // bind spoof button
        spoofButton.MouseClick += new MouseEventHandler(SpoofButton_Click);

        // bind close button
        closeButton.MouseClick += new MouseEventHandler(CloseButton_Click);

        // bind console draw logic
        consoleBox.DrawItem += new DrawItemEventHandler(ConsoleBox_DrawItem);

        // make draggable
        titleBar.MouseDown += new MouseEventHandler(TitleBar_MouseDown);
        titleBar.MouseUp += new MouseEventHandler(TitleBar_MouseUp);
        titleBar.MouseMove += new MouseEventHandler(TitleBar_MouseMove);
    }

    // console logging logic
    public void AddConsoleLog(string text = "", Brush color = null)
    {
        if (color is null)
            brushes.Add(Brushes.White);
        else
            brushes.Add(color);
        consoleBox.Items.Add(text);
    }
    public void ClearConsole()
    {
        brushes.Clear();
        consoleBox.Items.Clear();
    }

    // load button logic
    private void LoadButton_Click(object _, MouseEventArgs __)
    {
        if (Program.runningFunction is null || Program.runningFunction?.Status == TaskStatus.RanToCompletion)
            Program.runningFunction = Task.Run(
                () => { Functions.Load.Normal(Program.cancellationToken.Token); }
            );
    }

    // spoof button logic
    private void SpoofButton_Click(object _, MouseEventArgs __)
    {
        if (Program.runningFunction is null || Program.runningFunction?.Status == TaskStatus.RanToCompletion)
            Program.runningFunction = Task.Run(
                () => { Functions.Spoof.Run(Program.cancellationToken.Token); }
            );
    }

    // close button logic
    private void CloseButton_Click(object _, MouseEventArgs __)
    {
        Program.cancellationToken.Cancel();
        Program.runningFunction?.Wait();
        Close();
    }

    // console box logic
    private void ConsoleBox_DrawItem(object _, DrawItemEventArgs args)
    {
        // check for items
        if (args.Index < 0) return;

        // get item text
        string itemText = consoleBox.Items[args.Index].ToString();

        // draw
        args.DrawBackground();
        args.Graphics.DrawString(
            itemText,
            args.Font,
            brushes[args.Index],
            args.Bounds
        );
    }

    // title bar logic
    private void TitleBar_MouseDown(object _, MouseEventArgs __)
    {
        dragging = true;
        dragCursorPoint = Cursor.Position;
        dragFormPoint = Location;
    }
    private void TitleBar_MouseUp(object _, MouseEventArgs __)
    {
        dragging = false;
    }
    private void TitleBar_MouseMove(object _, MouseEventArgs __)
    {
        if (dragging)
        {
            Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
            Location = Point.Add(dragFormPoint, new Size(diff));
        }
    }
}
