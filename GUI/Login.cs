using DevExpress.Mvvm.Native;
using DTO;
using BUS;

namespace GUI
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login() => InitializeComponent();

        void btnLogin_Click(object sender, EventArgs e)
        {
            var bAccount = new BUS_Account();
            var emp = new Employee();
            var acc = new Account();

            if (!String.IsNullOrEmpty(txtUsername.Text) && !String.IsNullOrEmpty(txtPassword.Text))
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();
                if (bAccount.isAutheticated(username, password) != null)
                {
                    acc = bAccount.isAutheticated(username, password);
                    emp = acc.Employee;

                    var frmMain = new Main(acc, emp);
                    Hide();
                    frmMain.ShowDialog();
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Username or password is wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Enter username and password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}