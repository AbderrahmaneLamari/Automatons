
namespace GraphicalAutomaton
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.automatonFrom = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.setTransitionsBtn = new System.Windows.Forms.Button();
            this.alphabetTxt = new System.Windows.Forms.TextBox();
            this.finalStatesTxt = new System.Windows.Forms.TextBox();
            this.initalStateTxt = new System.Windows.Forms.TextBox();
            this.statesNumberTxt = new System.Windows.Forms.TextBox();
            this.transitionTable = new System.Windows.Forms.DataGridView();
            this.start_state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transition_char = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.end_state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enterTransitions = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.usagePage = new System.Windows.Forms.TabPage();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.validateWordBtn = new System.Windows.Forms.Button();
            this.inputWordTxt = new System.Windows.Forms.TextBox();
            this.automatonFrom.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transitionTable)).BeginInit();
            this.usagePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // automatonFrom
            // 
            this.automatonFrom.Controls.Add(this.tabPage1);
            this.automatonFrom.Controls.Add(this.usagePage);
            this.automatonFrom.Location = new System.Drawing.Point(4, 1);
            this.automatonFrom.Name = "automatonFrom";
            this.automatonFrom.SelectedIndex = 0;
            this.automatonFrom.Size = new System.Drawing.Size(846, 476);
            this.automatonFrom.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.setTransitionsBtn);
            this.tabPage1.Controls.Add(this.alphabetTxt);
            this.tabPage1.Controls.Add(this.finalStatesTxt);
            this.tabPage1.Controls.Add(this.initalStateTxt);
            this.tabPage1.Controls.Add(this.statesNumberTxt);
            this.tabPage1.Controls.Add(this.transitionTable);
            this.tabPage1.Controls.Add(this.enterTransitions);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(838, 450);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Form - Create Automaton";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // setTransitionsBtn
            // 
            this.setTransitionsBtn.Enabled = false;
            this.setTransitionsBtn.Location = new System.Drawing.Point(192, 191);
            this.setTransitionsBtn.Name = "setTransitionsBtn";
            this.setTransitionsBtn.Size = new System.Drawing.Size(114, 23);
            this.setTransitionsBtn.TabIndex = 18;
            this.setTransitionsBtn.Text = "Set Transitions";
            this.setTransitionsBtn.UseVisualStyleBackColor = true;
            this.setTransitionsBtn.Click += new System.EventHandler(this.setTransitions_Click);
            // 
            // alphabetTxt
            // 
            this.alphabetTxt.Location = new System.Drawing.Point(153, 125);
            this.alphabetTxt.Name = "alphabetTxt";
            this.alphabetTxt.Size = new System.Drawing.Size(153, 20);
            this.alphabetTxt.TabIndex = 17;
            // 
            // finalStatesTxt
            // 
            this.finalStatesTxt.Location = new System.Drawing.Point(153, 99);
            this.finalStatesTxt.Name = "finalStatesTxt";
            this.finalStatesTxt.Size = new System.Drawing.Size(153, 20);
            this.finalStatesTxt.TabIndex = 16;
            // 
            // initalStateTxt
            // 
            this.initalStateTxt.Location = new System.Drawing.Point(153, 73);
            this.initalStateTxt.Name = "initalStateTxt";
            this.initalStateTxt.Size = new System.Drawing.Size(153, 20);
            this.initalStateTxt.TabIndex = 15;
            // 
            // statesNumberTxt
            // 
            this.statesNumberTxt.Location = new System.Drawing.Point(153, 47);
            this.statesNumberTxt.Name = "statesNumberTxt";
            this.statesNumberTxt.Size = new System.Drawing.Size(153, 20);
            this.statesNumberTxt.TabIndex = 14;
            // 
            // transitionTable
            // 
            this.transitionTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.transitionTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.start_state,
            this.transition_char,
            this.end_state});
            this.transitionTable.Enabled = false;
            this.transitionTable.Location = new System.Drawing.Point(485, 47);
            this.transitionTable.Name = "transitionTable";
            this.transitionTable.Size = new System.Drawing.Size(347, 368);
            this.transitionTable.TabIndex = 13;
            // 
            // start_state
            // 
            this.start_state.HeaderText = "Start-State";
            this.start_state.Name = "start_state";
            // 
            // transition_char
            // 
            this.transition_char.HeaderText = "Transition-Character";
            this.transition_char.Name = "transition_char";
            // 
            // end_state
            // 
            this.end_state.HeaderText = "End-State";
            this.end_state.Name = "end_state";
            // 
            // enterTransitions
            // 
            this.enterTransitions.Location = new System.Drawing.Point(192, 162);
            this.enterTransitions.Name = "enterTransitions";
            this.enterTransitions.Size = new System.Drawing.Size(114, 23);
            this.enterTransitions.TabIndex = 12;
            this.enterTransitions.Text = "Set States";
            this.enterTransitions.UseVisualStyleBackColor = true;
            this.enterTransitions.Click += new System.EventHandler(this.setStates_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Alphabet:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Final States:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Initial State:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Number Of States:";
            // 
            // usagePage
            // 
            this.usagePage.Controls.Add(this.canvas);
            this.usagePage.Controls.Add(this.panel1);
            this.usagePage.Location = new System.Drawing.Point(4, 22);
            this.usagePage.Name = "usagePage";
            this.usagePage.Padding = new System.Windows.Forms.Padding(3);
            this.usagePage.Size = new System.Drawing.Size(838, 450);
            this.usagePage.TabIndex = 1;
            this.usagePage.Text = "Validate Words";
            this.usagePage.UseVisualStyleBackColor = true;
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(212, 6);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(620, 438);
            this.canvas.TabIndex = 3;
            this.canvas.TabStop = false;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.validateWordBtn);
            this.panel1.Controls.Add(this.inputWordTxt);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 270);
            this.panel1.TabIndex = 2;
            // 
            // validateWordBtn
            // 
            this.validateWordBtn.Location = new System.Drawing.Point(68, 97);
            this.validateWordBtn.Name = "validateWordBtn";
            this.validateWordBtn.Size = new System.Drawing.Size(107, 23);
            this.validateWordBtn.TabIndex = 1;
            this.validateWordBtn.Text = "Validate Word";
            this.validateWordBtn.UseVisualStyleBackColor = true;
            this.validateWordBtn.Click += new System.EventHandler(this.validateWordBtn_Click);
            // 
            // inputWordTxt
            // 
            this.inputWordTxt.Location = new System.Drawing.Point(21, 12);
            this.inputWordTxt.Multiline = true;
            this.inputWordTxt.Name = "inputWordTxt";
            this.inputWordTxt.Size = new System.Drawing.Size(154, 67);
            this.inputWordTxt.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 479);
            this.Controls.Add(this.automatonFrom);
            this.Name = "MainWindow";
            this.Text = "Graphical Automaton";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.automatonFrom.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transitionTable)).EndInit();
            this.usagePage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl automatonFrom;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage usagePage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button enterTransitions;
        private System.Windows.Forms.TextBox alphabet;
        private System.Windows.Forms.TextBox finalStatesTxt;
        private System.Windows.Forms.TextBox initalStateTxt;
        private System.Windows.Forms.TextBox statesNumberTxt;
        private System.Windows.Forms.DataGridView transitionTable;
        private System.Windows.Forms.Button setTransitionsBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn start_state;
        private System.Windows.Forms.DataGridViewTextBoxColumn transition_char;
        private System.Windows.Forms.DataGridViewTextBoxColumn end_state;
        private System.Windows.Forms.TextBox alphabetTxt;
        private System.Windows.Forms.Button validateWordBtn;
        private System.Windows.Forms.TextBox inputWordTxt;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Panel panel1;
    }
}

