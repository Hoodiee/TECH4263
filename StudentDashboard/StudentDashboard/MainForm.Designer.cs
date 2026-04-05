namespace StudentDashboard;

partial class MainForm
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
        // Containers
        splitMain = new SplitContainer();   // horizontal: top | bottom
        splitTop = new SplitContainer();   // vertical inside top: left | right

        // Left panel
        lblStudentsHeader = new Label();
        lstStudents = new ListBox();

        // Right panel
        lblDetailHeader = new Label();
        lblNoSelection = new Label();
        pnlDetailFields = new Panel();
        lblIdKey = new Label();
        lblIdValue = new Label();
        lblNameKey = new Label();
        lblNameValue = new Label();
        lblMajorKey = new Label();
        lblMajorValue = new Label();

        // Bottom panel
        lblCreateHeader = new Label();
        lblName = new Label();
        txtName = new TextBox();
        lblAge = new Label();
        txtAge = new TextBox();
        lblMajor = new Label();
        txtMajor = new TextBox();
        btnCreate = new Button();
        lblStatus = new Label();

        ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
        splitMain.Panel1.SuspendLayout();
        splitMain.Panel2.SuspendLayout();
        splitMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitTop).BeginInit();
        splitTop.Panel1.SuspendLayout();
        splitTop.Panel2.SuspendLayout();
        splitTop.SuspendLayout();
        pnlDetailFields.SuspendLayout();
        SuspendLayout();

        // splitMain: outer horizontal split — top area | bottom create panel
        splitMain.Dock = DockStyle.Fill;
        splitMain.Orientation = Orientation.Horizontal;
        splitMain.FixedPanel = FixedPanel.Panel2;
        splitMain.SplitterDistance = 340;
        splitMain.SplitterWidth = 6;
        splitMain.Panel2MinSize = 130;
        splitMain.IsSplitterFixed = true;
        splitMain.Name = "splitMain";
        splitMain.TabIndex = 0;
        splitMain.Panel1.Controls.Add(splitTop);
        splitMain.Panel2.Padding = new Padding(14, 8, 14, 8);
        splitMain.Panel2.Controls.Add(lblStatus);
        splitMain.Panel2.Controls.Add(btnCreate);
        splitMain.Panel2.Controls.Add(txtMajor);
        splitMain.Panel2.Controls.Add(lblMajor);
        splitMain.Panel2.Controls.Add(txtAge);
        splitMain.Panel2.Controls.Add(lblAge);
        splitMain.Panel2.Controls.Add(txtName);
        splitMain.Panel2.Controls.Add(lblName);
        splitMain.Panel2.Controls.Add(lblCreateHeader);

        // splitTop: inner vertical split — left list | right detail
        splitTop.Dock = DockStyle.Fill;
        splitTop.Orientation = Orientation.Vertical;
        splitTop.FixedPanel = FixedPanel.Panel1;
        splitTop.SplitterDistance = 220;
        splitTop.SplitterWidth = 6;
        splitTop.Panel1MinSize = 160;
        splitTop.Name = "splitTop";
        splitTop.TabIndex = 0;
        splitTop.Panel1.Padding = new Padding(10);
        splitTop.Panel1.Controls.Add(lstStudents);
        splitTop.Panel1.Controls.Add(lblStudentsHeader);
        splitTop.Panel2.Padding = new Padding(14);
        splitTop.Panel2.Controls.Add(pnlDetailFields);
        splitTop.Panel2.Controls.Add(lblNoSelection);
        splitTop.Panel2.Controls.Add(lblDetailHeader);

        // Left — student list
        lblStudentsHeader.AutoSize = false;
        lblStudentsHeader.Dock = DockStyle.Top;
        lblStudentsHeader.Height = 30;
        lblStudentsHeader.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblStudentsHeader.ForeColor = Color.FromArgb(30, 70, 160);
        lblStudentsHeader.Name = "lblStudentsHeader";
        lblStudentsHeader.Text = "Students";
        lblStudentsHeader.TextAlign = ContentAlignment.MiddleLeft;

        lstStudents.Dock = DockStyle.Fill;
        lstStudents.BorderStyle = BorderStyle.FixedSingle;
        lstStudents.Font = new Font("Segoe UI", 9.5F);
        lstStudents.ItemHeight = 22;
        lstStudents.Name = "lstStudents";
        lstStudents.TabIndex = 0;
        lstStudents.SelectedIndexChanged += lstStudents_SelectedIndexChanged;

        // Right — student detail
        lblDetailHeader.AutoSize = false;
        lblDetailHeader.Dock = DockStyle.Top;
        lblDetailHeader.Height = 30;
        lblDetailHeader.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblDetailHeader.ForeColor = Color.FromArgb(30, 70, 160);
        lblDetailHeader.Name = "lblDetailHeader";
        lblDetailHeader.Text = "Student Details";
        lblDetailHeader.TextAlign = ContentAlignment.MiddleLeft;

        lblNoSelection.Dock = DockStyle.Fill;
        lblNoSelection.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
        lblNoSelection.ForeColor = Color.Gray;
        lblNoSelection.Name = "lblNoSelection";
        lblNoSelection.Text = "Select a student from the list to view details.";
        lblNoSelection.TextAlign = ContentAlignment.MiddleCenter;
        lblNoSelection.Visible = true;

        pnlDetailFields.Dock = DockStyle.Fill;
        pnlDetailFields.Name = "pnlDetailFields";
        pnlDetailFields.Visible = false;
        pnlDetailFields.Controls.AddRange(new Control[]
        {
            lblIdKey, lblIdValue, lblNameKey, lblNameValue, lblMajorKey, lblMajorValue
        });

        int rowH = 38;

        lblIdKey.AutoSize = false;
        lblIdKey.Location = new Point(0, 4);
        lblIdKey.Size = new Size(70, rowH);
        lblIdKey.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblIdKey.ForeColor = Color.FromArgb(100, 100, 120);
        lblIdKey.Name = "lblIdKey";
        lblIdKey.Text = "ID";
        lblIdKey.TextAlign = ContentAlignment.MiddleLeft;

        lblIdValue.AutoSize = false;
        lblIdValue.Location = new Point(80, 4);
        lblIdValue.Size = new Size(300, rowH);
        lblIdValue.Font = new Font("Segoe UI", 9.5F);
        lblIdValue.Name = "lblIdValue";
        lblIdValue.Text = "";
        lblIdValue.TextAlign = ContentAlignment.MiddleLeft;

        lblNameKey.AutoSize = false;
        lblNameKey.Location = new Point(0, 4 + rowH);
        lblNameKey.Size = new Size(70, rowH);
        lblNameKey.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNameKey.ForeColor = Color.FromArgb(100, 100, 120);
        lblNameKey.Name = "lblNameKey";
        lblNameKey.Text = "Name";
        lblNameKey.TextAlign = ContentAlignment.MiddleLeft;

        lblNameValue.AutoSize = false;
        lblNameValue.Location = new Point(80, 4 + rowH);
        lblNameValue.Size = new Size(300, rowH);
        lblNameValue.Font = new Font("Segoe UI", 9.5F);
        lblNameValue.Name = "lblNameValue";
        lblNameValue.Text = "";
        lblNameValue.TextAlign = ContentAlignment.MiddleLeft;

        lblMajorKey.AutoSize = false;
        lblMajorKey.Location = new Point(0, 4 + rowH * 2);
        lblMajorKey.Size = new Size(70, rowH);
        lblMajorKey.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblMajorKey.ForeColor = Color.FromArgb(100, 100, 120);
        lblMajorKey.Name = "lblMajorKey";
        lblMajorKey.Text = "Major";
        lblMajorKey.TextAlign = ContentAlignment.MiddleLeft;

        lblMajorValue.AutoSize = false;
        lblMajorValue.Location = new Point(80, 4 + rowH * 2);
        lblMajorValue.Size = new Size(300, rowH);
        lblMajorValue.Font = new Font("Segoe UI", 9.5F);
        lblMajorValue.Name = "lblMajorValue";
        lblMajorValue.Text = "";
        lblMajorValue.TextAlign = ContentAlignment.MiddleLeft;

        // Bottom — add new student
        lblCreateHeader.AutoSize = false;
        lblCreateHeader.Dock = DockStyle.Top;
        lblCreateHeader.Height = 28;
        lblCreateHeader.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblCreateHeader.ForeColor = Color.FromArgb(30, 70, 160);
        lblCreateHeader.Name = "lblCreateHeader";
        lblCreateHeader.Text = "Add New Student";
        lblCreateHeader.TextAlign = ContentAlignment.MiddleLeft;

        int iy = 38;

        lblName.AutoSize = false;
        lblName.Location = new Point(0, iy + 4);
        lblName.Size = new Size(44, 24);
        lblName.Font = new Font("Segoe UI", 9.5F);
        lblName.Name = "lblName";
        lblName.Text = "Name";
        lblName.TextAlign = ContentAlignment.MiddleRight;

        txtName.Location = new Point(50, iy);
        txtName.Size = new Size(160, 24);
        txtName.Font = new Font("Segoe UI", 9.5F);
        txtName.Name = "txtName";
        txtName.TabIndex = 1;

        lblAge.AutoSize = false;
        lblAge.Location = new Point(224, iy + 4);
        lblAge.Size = new Size(30, 24);
        lblAge.Font = new Font("Segoe UI", 9.5F);
        lblAge.Name = "lblAge";
        lblAge.Text = "Age";
        lblAge.TextAlign = ContentAlignment.MiddleRight;

        txtAge.Location = new Point(260, iy);
        txtAge.Size = new Size(60, 24);
        txtAge.Font = new Font("Segoe UI", 9.5F);
        txtAge.Name = "txtAge";
        txtAge.TabIndex = 2;

        lblMajor.AutoSize = false;
        lblMajor.Location = new Point(334, iy + 4);
        lblMajor.Size = new Size(44, 24);
        lblMajor.Font = new Font("Segoe UI", 9.5F);
        lblMajor.Name = "lblMajor";
        lblMajor.Text = "Major";
        lblMajor.TextAlign = ContentAlignment.MiddleRight;

        txtMajor.Location = new Point(384, iy);
        txtMajor.Size = new Size(160, 24);
        txtMajor.Font = new Font("Segoe UI", 9.5F);
        txtMajor.Name = "txtMajor";
        txtMajor.TabIndex = 3;

        btnCreate.Location = new Point(560, iy - 1);
        btnCreate.Size = new Size(90, 28);
        btnCreate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnCreate.Name = "btnCreate";
        btnCreate.TabIndex = 4;
        btnCreate.Text = "Create";
        btnCreate.UseVisualStyleBackColor = true;
        btnCreate.Click += btnCreate_Click;

        lblStatus.AutoSize = false;
        lblStatus.Location = new Point(0, iy + 36);
        lblStatus.Size = new Size(560, 22);
        lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
        lblStatus.ForeColor = Color.Gray;
        lblStatus.Name = "lblStatus";
        lblStatus.Text = "";

        // MainForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(860, 500);
        Controls.Add(splitMain);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(700, 460);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Student Dashboard";

        pnlDetailFields.ResumeLayout(false);
        splitTop.Panel1.ResumeLayout(false);
        splitTop.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitTop).EndInit();
        splitTop.ResumeLayout(false);
        splitMain.Panel1.ResumeLayout(false);
        splitMain.Panel2.ResumeLayout(false);
        splitMain.Panel2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
        splitMain.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private SplitContainer splitMain;
    private SplitContainer splitTop;
    private Label lblStudentsHeader;
    private ListBox lstStudents;
    private Label lblDetailHeader;
    private Label lblNoSelection;
    private Panel pnlDetailFields;
    private Label lblIdKey;
    private Label lblIdValue;
    private Label lblNameKey;
    private Label lblNameValue;
    private Label lblMajorKey;
    private Label lblMajorValue;
    private Label lblCreateHeader;
    private Label lblName;
    private TextBox txtName;
    private Label lblAge;
    private TextBox txtAge;
    private Label lblMajor;
    private TextBox txtMajor;
    private Button btnCreate;
    private Label lblStatus;
}