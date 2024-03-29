﻿namespace HyperXMuteTaskbar
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.taskbarIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.taskbarContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deviceCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerDevicesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepHeadsetAwakeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskbarContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // taskbarIcon
            // 
            this.taskbarIcon.ContextMenuStrip = this.taskbarContextMenuStrip;
            this.taskbarIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("taskbarIcon.Icon")));
            this.taskbarIcon.Text = "HyperX mute on taskbar";
            this.taskbarIcon.Visible = true;
            // 
            // taskbarContextMenuStrip
            // 
            this.taskbarContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deviceCountToolStripMenuItem,
            this.registerDevicesMenuItem,
            this.keepHeadsetAwakeMenuItem,
            this.exitToolStripMenuItem});
            this.taskbarContextMenuStrip.Name = "taskbarContextMenuStrip";
            this.taskbarContextMenuStrip.ShowCheckMargin = true;
            this.taskbarContextMenuStrip.ShowImageMargin = false;
            this.taskbarContextMenuStrip.Size = new System.Drawing.Size(186, 114);
            // 
            // deviceCountToolStripMenuItem
            // 
            this.deviceCountToolStripMenuItem.Enabled = false;
            this.deviceCountToolStripMenuItem.Name = "deviceCountToolStripMenuItem";
            this.deviceCountToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.deviceCountToolStripMenuItem.Text = ".";
            // 
            // registerDevicesMenuItem
            // 
            this.registerDevicesMenuItem.Name = "registerDevicesMenuItem";
            this.registerDevicesMenuItem.Size = new System.Drawing.Size(185, 22);
            this.registerDevicesMenuItem.Text = "Look for new devices";
            this.registerDevicesMenuItem.Click += new System.EventHandler(this.RegisterDevicesMenuItem_Click);
            // 
            // keepHeadsetAwakeMenuItem
            // 
            this.keepHeadsetAwakeMenuItem.Checked = true;
            this.keepHeadsetAwakeMenuItem.CheckOnClick = true;
            this.keepHeadsetAwakeMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.keepHeadsetAwakeMenuItem.Name = "keepHeadsetAwakeMenuItem";
            this.keepHeadsetAwakeMenuItem.Size = new System.Drawing.Size(185, 22);
            this.keepHeadsetAwakeMenuItem.Text = "Keep headset awake";
            this.keepHeadsetAwakeMenuItem.CheckedChanged += new System.EventHandler(this.KeepHeadsetAwakeMenuItem_CheckedChanged);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.taskbarContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon taskbarIcon;
        private System.Windows.Forms.ContextMenuStrip taskbarContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerDevicesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deviceCountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keepHeadsetAwakeMenuItem;
    }
}

