namespace visualizadorTGA
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirTgaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ferramentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centralizarImagemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelImagem = new System.Windows.Forms.Panel();
            this.pictureBoxImagem = new System.Windows.Forms.PictureBox();
            this.labelZoom = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.panelImagem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagem)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.ferramentasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(960, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirTgaToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // abrirTgaToolStripMenuItem
            // 
            this.abrirTgaToolStripMenuItem.Name = "abrirTgaToolStripMenuItem";
            this.abrirTgaToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.abrirTgaToolStripMenuItem.Text = "Abrir TGA...";
            this.abrirTgaToolStripMenuItem.Click += new System.EventHandler(this.abrirTgaToolStripMenuItem_Click);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // ferramentasToolStripMenuItem
            // 
            this.ferramentasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.centralizarImagemToolStripMenuItem});
            this.ferramentasToolStripMenuItem.Name = "ferramentasToolStripMenuItem";
            this.ferramentasToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.ferramentasToolStripMenuItem.Text = "Ferramentas";
            // 
            // centralizarImagemToolStripMenuItem
            // 
            this.centralizarImagemToolStripMenuItem.CheckOnClick = true;
            this.centralizarImagemToolStripMenuItem.Name = "centralizarImagemToolStripMenuItem";
            this.centralizarImagemToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.centralizarImagemToolStripMenuItem.Text = "Centralizar imagem";
            this.centralizarImagemToolStripMenuItem.CheckedChanged += new System.EventHandler(this.centralizarImagemToolStripMenuItem_CheckedChanged);
            // 
            // panelImagem
            // 
            this.panelImagem.AutoScroll = true;
            this.panelImagem.BackColor = System.Drawing.Color.White;
            this.panelImagem.Controls.Add(this.pictureBoxImagem);
            this.panelImagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImagem.Location = new System.Drawing.Point(0, 24);
            this.panelImagem.Name = "panelImagem";
            this.panelImagem.Size = new System.Drawing.Size(960, 576);
            this.panelImagem.TabIndex = 1;
            this.panelImagem.Resize += new System.EventHandler(this.panelImagem_Resize);
            // 
            // pictureBoxImagem
            // 
            this.pictureBoxImagem.BackColor = System.Drawing.Color.White;
            this.pictureBoxImagem.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxImagem.Name = "pictureBoxImagem";
            this.pictureBoxImagem.Size = new System.Drawing.Size(1, 1);
            this.pictureBoxImagem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImagem.TabIndex = 0;
            this.pictureBoxImagem.TabStop = true;
            // 
            // labelZoom
            // 
            this.labelZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelZoom.AutoSize = true;
            this.labelZoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.labelZoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelZoom.ForeColor = System.Drawing.Color.White;
            this.labelZoom.Location = new System.Drawing.Point(899, 572);
            this.labelZoom.Name = "labelZoom";
            this.labelZoom.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.labelZoom.Size = new System.Drawing.Size(49, 23);
            this.labelZoom.TabIndex = 2;
            this.labelZoom.Text = "100%";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 600);
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelZoom);
            this.Controls.Add(this.panelImagem);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualizador TGA";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelImagem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirTgaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ferramentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centralizarImagemToolStripMenuItem;
        private System.Windows.Forms.Panel panelImagem;
        private System.Windows.Forms.PictureBox pictureBoxImagem;
        private System.Windows.Forms.Label labelZoom;
    }
}
