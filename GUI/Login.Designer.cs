namespace GUI
{
    partial class Login
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
            txtUsername = new DevExpress.XtraEditors.TextEdit();
            txtPassword = new DevExpress.XtraEditors.TextEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            labelControl4 = new DevExpress.XtraEditors.LabelControl();
            labelControl5 = new DevExpress.XtraEditors.LabelControl();
            btnLogin = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)txtUsername.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).BeginInit();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.EditValue = "";
            txtUsername.Location = new Point(41, 162);
            txtUsername.Margin = new Padding(3, 2, 3, 2);
            txtUsername.Name = "txtUsername";
            txtUsername.Properties.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtUsername.Properties.Appearance.Options.UseFont = true;
            txtUsername.Size = new Size(298, 26);
            txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.EditValue = "";
            txtPassword.Location = new Point(41, 219);
            txtPassword.Margin = new Padding(3, 2, 3, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.Properties.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtPassword.Properties.Appearance.Options.UseFont = true;
            txtPassword.Properties.Name = "txtPassword";
            txtPassword.Properties.PasswordChar = '*';
            txtPassword.Size = new Size(298, 26);
            txtPassword.TabIndex = 2;
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new Font("Tahoma", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            labelControl1.Appearance.ForeColor = Color.Black;
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Appearance.Options.UseForeColor = true;
            labelControl1.Location = new Point(42, 10);
            labelControl1.Margin = new Padding(3, 2, 3, 2);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(112, 22);
            labelControl1.TabIndex = 3;
            labelControl1.Text = "Welcome to ";
            // 
            // labelControl2
            // 
            labelControl2.Appearance.Font = new Font("Tahoma", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            labelControl2.Appearance.ForeColor = Color.Black;
            labelControl2.Appearance.Options.UseFont = true;
            labelControl2.Appearance.Options.UseForeColor = true;
            labelControl2.Location = new Point(41, 101);
            labelControl2.Margin = new Padding(3, 2, 3, 2);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new Size(175, 22);
            labelControl2.TabIndex = 4;
            labelControl2.Text = "Sign In to Continue";
            // 
            // labelControl3
            // 
            labelControl3.Appearance.Font = new Font("Tahoma", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            labelControl3.Appearance.ForeColor = Color.Black;
            labelControl3.Appearance.Options.UseFont = true;
            labelControl3.Appearance.Options.UseForeColor = true;
            labelControl3.Location = new Point(42, 140);
            labelControl3.Margin = new Padding(3, 2, 3, 2);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new Size(61, 17);
            labelControl3.TabIndex = 5;
            labelControl3.Text = "Username";
            // 
            // labelControl4
            // 
            labelControl4.Appearance.Font = new Font("Tahoma", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            labelControl4.Appearance.ForeColor = Color.Black;
            labelControl4.Appearance.Options.UseFont = true;
            labelControl4.Appearance.Options.UseForeColor = true;
            labelControl4.Location = new Point(42, 37);
            labelControl4.Margin = new Padding(3, 2, 3, 2);
            labelControl4.Name = "labelControl4";
            labelControl4.Size = new Size(186, 22);
            labelControl4.TabIndex = 6;
            labelControl4.Text = "Airport Management";
            // 
            // labelControl5
            // 
            labelControl5.Appearance.Font = new Font("Tahoma", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            labelControl5.Appearance.ForeColor = Color.Black;
            labelControl5.Appearance.Options.UseFont = true;
            labelControl5.Appearance.Options.UseForeColor = true;
            labelControl5.Location = new Point(42, 197);
            labelControl5.Margin = new Padding(3, 2, 3, 2);
            labelControl5.Name = "labelControl5";
            labelControl5.Size = new Size(58, 17);
            labelControl5.TabIndex = 7;
            labelControl5.Text = "Password";
            // 
            // btnLogin
            // 
            btnLogin.Appearance.BackColor = Color.FromArgb(42, 93, 196);
            btnLogin.Appearance.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnLogin.Appearance.ForeColor = Color.White;
            btnLogin.Appearance.Options.UseBackColor = true;
            btnLogin.Appearance.Options.UseFont = true;
            btnLogin.Appearance.Options.UseForeColor = true;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Location = new Point(41, 275);
            btnLogin.Margin = new Padding(3, 2, 3, 2);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(298, 36);
            btnLogin.TabIndex = 9;
            btnLogin.Text = "Sign in";
            btnLogin.Click += btnLogin_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(375, 368);
            Controls.Add(btnLogin);
            Controls.Add(labelControl5);
            Controls.Add(labelControl4);
            Controls.Add(labelControl3);
            Controls.Add(labelControl2);
            Controls.Add(labelControl1);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Login";
            ((System.ComponentModel.ISupportInitialize)txtUsername.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
    }
}