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
            firstNameTextBox.Location = new Point(325, 123);
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
            addButton.Text = "Přidat";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // updateButton
            // 
            updateButton.Location = new Point(480, 413);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(75, 23);
            updateButton.TabIndex = 4;
            updateButton.Text = "Upravit";
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += updateButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(628, 413);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(75, 23);
            deleteButton.TabIndex = 5;
            deleteButton.Text = "Smazat";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(715, 450);
            Controls.Add(deleteButton);
            Controls.Add(updateButton);
            Controls.Add(addButton);
            Controls.Add(lastNameTextBox);
            Controls.Add(firstNameTextBox);
            Controls.Add(peopleListBox);
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
    }
}
