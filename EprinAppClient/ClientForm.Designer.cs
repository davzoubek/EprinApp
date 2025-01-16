namespace EprinAppClient
{
    partial class ClientForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            peopleListBox = new ListBox();
            firstNameTextBox = new TextBox();
            lastNameTextBox = new TextBox();
            addButton = new Button();
            updateButton = new Button();
            deleteButton = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // peopleListBox
            // 
            peopleListBox.FormattingEnabled = true;
            peopleListBox.ItemHeight = 15;
            peopleListBox.Location = new Point(12, 12);
            peopleListBox.Name = "peopleListBox";
            peopleListBox.Size = new Size(256, 424);
            peopleListBox.TabIndex = 0;
            // 
            // firstNameTextBox
            // 
            firstNameTextBox.Location = new Point(325, 95);
            firstNameTextBox.Name = "firstNameTextBox";
            firstNameTextBox.Size = new Size(378, 23);
            firstNameTextBox.TabIndex = 1;
            // 
            // lastNameTextBox
            // 
            lastNameTextBox.Location = new Point(325, 152);
            lastNameTextBox.Name = "lastNameTextBox";
            lastNameTextBox.Size = new Size(378, 23);
            lastNameTextBox.TabIndex = 2;
            // 
            // addButton
            // 
            addButton.Location = new Point(325, 413);
            addButton.Name = "addButton";
            addButton.Size = new Size(75, 23);
            addButton.TabIndex = 3;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // updateButton
            // 
            updateButton.Location = new Point(475, 413);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(75, 23);
            updateButton.TabIndex = 4;
            updateButton.Text = "Edit";
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += updateButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(628, 413);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(75, 23);
            deleteButton.TabIndex = 5;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(325, 77);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 6;
            label1.Text = "First name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(325, 134);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 7;
            label2.Text = "Second name";
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(715, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(deleteButton);
            Controls.Add(updateButton);
            Controls.Add(addButton);
            Controls.Add(lastNameTextBox);
            Controls.Add(firstNameTextBox);
            Controls.Add(peopleListBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ClientForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox peopleListBox;
        private TextBox firstNameTextBox;
        private TextBox lastNameTextBox;
        private Button addButton;
        private Button updateButton;
        private Button deleteButton;
        private Label label1;
        private Label label2;
    }
}
