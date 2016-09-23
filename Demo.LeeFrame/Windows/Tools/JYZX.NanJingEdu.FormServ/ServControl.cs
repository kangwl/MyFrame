using System;
using System.ServiceProcess;
using System.Windows.Forms;
using Demo.Common;

namespace Tools.FormService {
    public partial class ServControl : Form {

        readonly string iniFile = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
        /// <summary>
        /// 读取配置文件
        /// </summary>
        public string ServExeFileConfig {
            get {
                string exe_serv_filename = IniHelper.iniFile_GetVal(iniFile, "ServiceFile", "serv_file");
                if (string.IsNullOrEmpty(exe_serv_filename)) {
                    return AppDomain.CurrentDomain.BaseDirectory + "WinJSEduServ\\JYZX.NanJingEdu.WinService.exe";
                }
                return exe_serv_filename.Trim();
            }
        }

        public string ServNameConfig {
            get {
                string service_name = IniHelper.iniFile_GetVal(iniFile, "ServiceName", "serv_name");
                if (string.IsNullOrEmpty(service_name)) {
                    return "JSEDUService";
                }
                return service_name.Trim();
            }
        }

        public ServControl() {
            InitializeComponent();
            //init
            Init();
        }

        private void Init() {
            textBox1.Text = ServExeFileConfig;
            textBox_ServName.Text = ServNameConfig;
        }

        public string ServExeFile {
            get { return textBox1.Text.Trim(); }
        }

        public string ServName {
            get { return textBox_ServName.Text.Trim(); }
        }

        private void button1_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(ServExeFile)) {
                MessageBox.Show(this, "请输入服务文件路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //开启服务
            System.Windows.Forms.DialogResult resbtn = MessageBox.Show(this, "是否开启该服务？", "提示",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button2);
            if (resbtn == DialogResult.OK) {
                bool success = WinServHelper.StartService(ServName);
                if (success) {
                    button1.Enabled = false;
                    button2.Enabled = true;
                    MessageBox.Show("服务已开启");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(ServExeFile)) {
                MessageBox.Show(this, "请输入服务文件路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                System.Windows.Forms.DialogResult resbtn = MessageBox.Show(this, "是否停止该服务？", "提示", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Warning,
               MessageBoxDefaultButton.Button2);
            if (resbtn == DialogResult.OK) {
                //停止服务
                bool success = WinServHelper.StopService(ServName);
                if (success) {
                    button1.Enabled = true;
                    button2.Enabled = false;
                    MessageBox.Show("服务已停止");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(ServExeFile)) {
                MessageBox.Show(this, "请输入服务文件路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //安装服务
            System.Windows.Forms.DialogResult resbtn = MessageBox.Show(this, "是否安装该服务？", "提示", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Information,
               MessageBoxDefaultButton.Button2);
            if (resbtn == DialogResult.OK) {
                bool success = WinServHelper.InstallServie(ServExeFile);
                if (success) {
                    button3.Enabled = false;
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button_Uninstall.Enabled = true;
                    MessageBox.Show("安装成功");
                }
            }
        }

        private void CheckServExist() {
            ServiceController serviceController = WinServHelper.GetService(ServName);
            if (serviceController == null) {
                button3.Enabled = true;
                button1.Enabled = false;
                button2.Enabled = false;
                button_Uninstall.Enabled = false;
            }
            else {
                if (serviceController.Status == ServiceControllerStatus.Running) {
                    button1.Enabled = false;
                    button2.Enabled = true;
                    button_Uninstall.Enabled = true;
                }
                else {
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button_Uninstall.Enabled = true;
                }
                button3.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            CheckServExist();
        }

        private void button_Uninstall_Click(object sender, EventArgs e) {
            System.Windows.Forms.DialogResult resbtn = MessageBox.Show(this, "是否卸载该服务？", "提示", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);
            if (resbtn == DialogResult.OK) {
                bool sucess = WinServHelper.UninstallService(ServExeFile, ServName);
                if (sucess) {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    button_Uninstall.Enabled = false;
                    MessageBox.Show("已卸载！");
                }
                else {
                    MessageBox.Show("卸载失败");
                }
            }
        }

    }
}
