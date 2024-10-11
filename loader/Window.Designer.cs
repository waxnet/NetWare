namespace Loader
{
    partial class Window
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            mainPanel = new Panel();
            consoleTitle = new Label();
            spoofButton = new Button();
            loadButton = new Button();
            consoleBox = new ListBox();
            titleBar = new Panel();
            closeButton = new Button();
            title = new Label();
            mainPanel.SuspendLayout();
            titleBar.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.BackColor = Color.FromArgb(33, 33, 33);
            mainPanel.Controls.Add(consoleTitle);
            mainPanel.Controls.Add(spoofButton);
            mainPanel.Controls.Add(loadButton);
            mainPanel.Controls.Add(consoleBox);
            mainPanel.Controls.Add(titleBar);
            mainPanel.Location = new Point(1, 1);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(398, 298);
            mainPanel.TabIndex = 0;
            // 
            // consoleTitle
            // 
            consoleTitle.AutoSize = true;
            consoleTitle.Font = new Font("Arial", 8F, FontStyle.Bold);
            consoleTitle.ForeColor = Color.White;
            consoleTitle.Location = new Point(3, 83);
            consoleTitle.Name = "consoleTitle";
            consoleTitle.Size = new Size(65, 16);
            consoleTitle.TabIndex = 6;
            consoleTitle.Text = "Console";
            // 
            // spoofButton
            // 
            spoofButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            spoofButton.BackColor = Color.FromArgb(24, 24, 24);
            spoofButton.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 255);
            spoofButton.FlatAppearance.BorderSize = 0;
            spoofButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 24, 24);
            spoofButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 24, 24);
            spoofButton.FlatStyle = FlatStyle.Flat;
            spoofButton.Font = new Font("Arial", 10F, FontStyle.Bold);
            spoofButton.ForeColor = Color.White;
            spoofButton.Location = new Point(204, 46);
            spoofButton.Name = "spoofButton";
            spoofButton.Size = new Size(187, 30);
            spoofButton.TabIndex = 2;
            spoofButton.TabStop = false;
            spoofButton.Text = "Spoof";
            spoofButton.UseVisualStyleBackColor = false;
            // 
            // loadButton
            // 
            loadButton.BackColor = Color.FromArgb(24, 24, 24);
            loadButton.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 255);
            loadButton.FlatAppearance.BorderSize = 0;
            loadButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 24, 24);
            loadButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 24, 24);
            loadButton.FlatStyle = FlatStyle.Flat;
            loadButton.Font = new Font("Arial", 10F, FontStyle.Bold);
            loadButton.ForeColor = Color.White;
            loadButton.Location = new Point(6, 46);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(187, 30);
            loadButton.TabIndex = 1;
            loadButton.TabStop = false;
            loadButton.Text = "Load";
            loadButton.UseVisualStyleBackColor = false;
            // 
            // consoleBox
            // 
            consoleBox.BackColor = Color.FromArgb(24, 24, 24);
            consoleBox.BorderStyle = BorderStyle.None;
            consoleBox.DrawMode = DrawMode.OwnerDrawFixed;
            consoleBox.Font = new Font("Arial", 8F, FontStyle.Bold);
            consoleBox.ForeColor = Color.White;
            consoleBox.FormattingEnabled = true;
            consoleBox.ItemHeight = 16;
            consoleBox.Location = new Point(6, 100);
            consoleBox.Name = "consoleBox";
            consoleBox.Size = new Size(386, 192);
            consoleBox.TabIndex = 5;
            consoleBox.TabStop = false;
            consoleBox.Enabled = false;
            // 
            // titleBar
            // 
            titleBar.BackColor = Color.FromArgb(24, 24, 24);
            titleBar.Controls.Add(closeButton);
            titleBar.Controls.Add(title);
            titleBar.Location = new Point(0, 0);
            titleBar.Name = "titleBar";
            titleBar.Size = new Size(398, 30);
            titleBar.TabIndex = 0;
            // 
            // closeButton
            // 
            closeButton.BackColor = Color.FromArgb(24, 24, 24);
            closeButton.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 255);
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 24, 24);
            closeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 24, 24);
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.Font = new Font("Arial", 8F);
            closeButton.ForeColor = Color.White;
            closeButton.Location = new Point(368, 0);
            closeButton.Margin = new Padding(0);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(30, 30);
            closeButton.TabIndex = 1;
            closeButton.TabStop = false;
            closeButton.Text = "✖";
            closeButton.UseVisualStyleBackColor = false;
            // 
            // title
            // 
            title.BackColor = Color.FromArgb(24, 24, 24);
            title.Font = new Font("Arial", 8F, FontStyle.Bold | FontStyle.Italic);
            title.ForeColor = Color.White;
            title.ImageAlign = ContentAlignment.BottomRight;
            title.Location = new Point(1, 1);
            title.Name = "title";
            title.Size = new Size(123, 29);
            title.TabIndex = 1;
            title.Text = " NetWare Loader";
            title.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Window
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(400, 300);
            ControlBox = false;
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Window";
            Text = "NetWare Loader";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            titleBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel mainPanel;
        private Panel titleBar;
        private Label title;
        private Button closeButton;
        private Button spoofButton;
        private ListBox consoleBox;
        private Button loadButton;
        private Label consoleTitle;
    }
}
