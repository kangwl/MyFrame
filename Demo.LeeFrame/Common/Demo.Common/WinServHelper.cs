using System;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Demo.Common {
    public class WinServHelper {
        /// <summary>
        /// 使用Windows Service对应的exe文件 安装Service
        /// 和 installutil xxx.exe 效果相同
        /// </summary>
        /// <param name="installFile">exe文件（包含路径）</param>
        /// <returns>是否安装成功</returns>
        public static bool InstallServie(string installFile) {
            string[] args = {installFile};
            try {
                ManagedInstallerClass.InstallHelper(args);
                return true;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// 使用Windows Service对应的exe文件 卸载Service
        /// 和 installutil /u xxx.exe 效果相同
        /// </summary>
        /// <param name="installFile">exe文件（包含路径）</param>
        /// <param name="svcName">服务名</param>
        /// <returns>是否卸载成功</returns>
        public static bool UninstallService(string installFile, string svcName) {
            string[] args = {"/u", installFile};
            try {
 
                // 在卸载服务之前 要先停止windows服务
                StopService(svcName);

                ManagedInstallerClass.InstallHelper(args);
                return true;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <returns>是否启动成功</returns>
        public static bool StartService(string serviceName) {
            ServiceController sc = GetService(serviceName);

            if (sc.Status != ServiceControllerStatus.Running) {
                try {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running); // 等待服务达到指定状态
                }
                catch {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <returns>是否停止服务成功，如果服务启动后不可以停止，则抛异常</returns>
        public static bool StopService(string serviceName) {
            ServiceController sc = GetService(serviceName);
            if (sc.Status == ServiceControllerStatus.Stopped) {
                return true;
            }
            if (!sc.CanStop) {
                throw new Exception(string.Format("服务{0}启动后不可以停止.", serviceName));
            }

            if (sc.Status != ServiceControllerStatus.Stopped) {
                try {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped); // 等待服务达到指定状态
                }
                catch {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 获得service对应的ServiceController对象
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <returns>ServiceController对象，若没有该服务，则返回null</returns>
        public static ServiceController GetService(string serviceName) {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services) {
                if (s.ServiceName == serviceName) {
                    return s;
                }
            }
            return null;
        }
    }
}