namespace TicTacToe
{
	partial class TicTacToeForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cell1 = new System.Windows.Forms.Button();
			this.cell2 = new System.Windows.Forms.Button();
			this.cell3 = new System.Windows.Forms.Button();
			this.cell4 = new System.Windows.Forms.Button();
			this.cell5 = new System.Windows.Forms.Button();
			this.cell6 = new System.Windows.Forms.Button();
			this.cell7 = new System.Windows.Forms.Button();
			this.cell8 = new System.Windows.Forms.Button();
			this.cell9 = new System.Windows.Forms.Button();
			this.gameState = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanel1.Controls.Add(this.cell1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.gameState, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.cell2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.cell3, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.cell4, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.cell5, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.cell6, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.cell7, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.cell8, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.cell9, 2, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33335F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(458, 275);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// cell1
			// 
			this.cell1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cell1.Location = new System.Drawing.Point(20, 20);
			this.cell1.Margin = new System.Windows.Forms.Padding(20);
			this.cell1.Name = "cell1";
			this.cell1.Size = new System.Drawing.Size(112, 43);
			this.cell1.TabIndex = 0;
			this.cell1.Tag = "0";
			this.cell1.UseVisualStyleBackColor = true;
			this.cell1.Click += new System.EventHandler(this.Cell_Click);
			// 
			// cell2
			// 
			this.cell2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cell2.Location = new System.Drawing.Point(172, 20);
			this.cell2.Margin = new System.Windows.Forms.Padding(20);
			this.cell2.Name = "cell2";
			this.cell2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cell2.Size = new System.Drawing.Size(112, 43);
			this.cell2.TabIndex = 1;
			this.cell2.Tag = "1";
			this.cell2.UseVisualStyleBackColor = true;
			this.cell2.Click += new System.EventHandler(this.Cell_Click);
			// 
			// cell3
			// 
			this.cell3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cell3.Location = new System.Drawing.Point(324, 20);
			this.cell3.Margin = new System.Windows.Forms.Padding(20);
			this.cell3.Name = "cell3";
			this.cell3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cell3.Size = new System.Drawing.Size(114, 43);
			this.cell3.TabIndex = 2;
			this.cell3.Tag = "2";
			this.cell3.UseVisualStyleBackColor = true;
			this.cell3.Click += new System.EventHandler(this.Cell_Click);
			// 
			// cell4
			// 
			this.cell4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cell4.Location = new System.Drawing.Point(20, 103);
			this.cell4.Margin = new System.Windows.Forms.Padding(20);
			this.cell4.Name = "cell4";
			this.cell4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cell4.Size = new System.Drawing.Size(112, 43);
			this.cell4.TabIndex = 3;
			this.cell4.Tag = "3";
			this.cell4.UseVisualStyleBackColor = true;
			this.cell4.Click += new System.EventHandler(this.Cell_Click);
			// 
			// cell5
			// 
			this.cell5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cell5.Location = new System.Drawing.Point(172, 103);
			this.cell5.Margin = new System.Windows.Forms.Padding(20);
			this.cell5.Name = "cell5";
			this.cell5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cell5.Size = new System.Drawing.Size(112, 43);
			this.cell5.TabIndex = 4;
			this.cell5.Tag = "4";
			this.cell5.UseVisualStyleBackColor = true;
			this.cell5.Click += new System.EventHandler(this.Cell_Click);
			// 
			// cell6
			// 
			this.cell6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cell6.Location = new System.Drawing.Point(324, 103);
			this.cell6.Margin = new System.Windows.Forms.Padding(20);
			this.cell6.Name = "cell6";
			this.cell6.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cell6.Size = new System.Drawing.Size(114, 43);
			this.cell6.TabIndex = 5;
			this.cell6.Tag = "5";
			this.cell6.UseVisualStyleBackColor = true;
			this.cell6.Click += new System.EventHandler(this.Cell_Click);
			// 
			// cell7
			// 
			this.cell7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cell7.Location = new System.Drawing.Point(20, 186);
			this.cell7.Margin = new System.Windows.Forms.Padding(20);
			this.cell7.Name = "cell7";
			this.cell7.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cell7.Size = new System.Drawing.Size(112, 43);
			this.cell7.TabIndex = 6;
			this.cell7.Tag = "6";
			this.cell7.UseVisualStyleBackColor = true;
			this.cell7.Click += new System.EventHandler(this.Cell_Click);
			// 
			// cell8
			// 
			this.cell8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cell8.Location = new System.Drawing.Point(172, 186);
			this.cell8.Margin = new System.Windows.Forms.Padding(20);
			this.cell8.Name = "cell8";
			this.cell8.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cell8.Size = new System.Drawing.Size(112, 43);
			this.cell8.TabIndex = 7;
			this.cell8.Tag = "7";
			this.cell8.UseVisualStyleBackColor = true;
			this.cell8.Click += new System.EventHandler(this.Cell_Click);
			// 
			// cell9
			// 
			this.cell9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cell9.Location = new System.Drawing.Point(324, 186);
			this.cell9.Margin = new System.Windows.Forms.Padding(20);
			this.cell9.Name = "cell9";
			this.cell9.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cell9.Size = new System.Drawing.Size(114, 43);
			this.cell9.TabIndex = 8;
			this.cell9.Tag = "8";
			this.cell9.UseVisualStyleBackColor = true;
			this.cell9.Click += new System.EventHandler(this.Cell_Click);
			// 
			// gameState
			// 
			this.gameState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gameState.AutoSize = true;
			this.gameState.Location = new System.Drawing.Point(157, 254);
			this.gameState.Margin = new System.Windows.Forms.Padding(5);
			this.gameState.Name = "gameState";
			this.gameState.Size = new System.Drawing.Size(142, 16);
			this.gameState.TabIndex = 9;
			this.gameState.Text = "player O";
			this.gameState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TicTacToeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(482, 345);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "TicTacToeForm";
			this.Text = "Tic Tac Toe";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button cell1;
		private System.Windows.Forms.Button cell2;
		private System.Windows.Forms.Button cell3;
		private System.Windows.Forms.Button cell4;
		private System.Windows.Forms.Button cell5;
		private System.Windows.Forms.Button cell6;
		private System.Windows.Forms.Button cell7;
		private System.Windows.Forms.Button cell8;
		private System.Windows.Forms.Button cell9;
		private System.Windows.Forms.Label gameState;
	}
}

