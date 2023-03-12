namespace SystemWatcher
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FileWatcherInstaller = new System.ServiceProcess.ServiceInstaller();
            this.FileWatcherProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            // 
            // FileWatcherInstaller
            // 
            this.FileWatcherInstaller.ServiceName = "FileWatcherInstaller";
            // 
            // FileWatcherProcessInstaller
            // 
            this.FileWatcherProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.FileWatcherProcessInstaller.Password = null;
            this.FileWatcherProcessInstaller.Username = null;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.FileWatcherInstaller,
            this.FileWatcherProcessInstaller});

        }

        #endregion
        private System.ServiceProcess.ServiceInstaller FileWatcherInstaller;
        private System.ServiceProcess.ServiceProcessInstaller FileWatcherProcessInstaller;
    }
}