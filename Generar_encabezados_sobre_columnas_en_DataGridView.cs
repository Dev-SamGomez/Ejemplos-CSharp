public void GenerateColumns()
{
    this.DataGridView1.Columns.Add("Title 1", "Work Order");
    this.DataGridView1.Columns.Add("Title 1", "Order");
    this.DataGridView1.Columns.Add("Title 2", "Work Order");
    this.DataGridView1.Columns.Add("Title 2", "Order");
    for (int j = 0; j <= this.DataGridView1.ColumnCount - 1; j++)
    {
        this.DataGridView1.Columns(j).Width = 80;
        this.DataGridView1.Columns(j).SortMode = DataGridViewColumnSortMode.NotSortable;
    }
    this.DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
    this.DataGridView1.ColumnHeadersHeight = this.DataGridView1.ColumnHeadersHeight * 2;
    this.DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
}
private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
{
    Rectangle rtHeader = this.DataGridView1.DisplayRectangle;
    rtHeader.Height = this.DataGridView1.ColumnHeadersHeight / (double)2;
    this.DataGridView1.Invalidate(rtHeader);
}
private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
{
    Rectangle rtHeader = this.DataGridView1.DisplayRectangle;
    rtHeader.Height = this.DataGridView1.ColumnHeadersHeight / (double)2;
    this.DataGridView1.Invalidate(rtHeader);
}
private void dataGridView1_Paint(object sender, PaintEventArgs e)
{
    if (this.DataGridView1.ColumnCount > 0)
    {
        string[] Maq = new[] { "Title 1", "Title 2" };
        int j = 0;
        while (j < 4)
        {
            Rectangle r1 = this.DataGridView1.GetCellDisplayRectangle(j, -1, true);
            int w2 = this.DataGridView1.GetCellDisplayRectangle(j, -1, true).Width;
            r1.X += 1;
            r1.Y += 1;
            r1.Width = r1.Width + w2 - 2;
            r1.Height = r1.Height / (double)2 - 2;
            e.Graphics.FillRectangle(new SolidBrush(this.DataGridView1.ColumnHeadersDefaultCellStyle.BackColor), r1);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(Maq[j / (double)2], this.DataGridView1.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(this.DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor), r1, format);
            j += 2;
        }
    }
}
public void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
{
    if (e.RowIndex == -1 && e.ColumnIndex > -1)
    {
        Rectangle r2 = e.CellBounds;
        r2.Y += e.CellBounds.Height / (double)2;
        r2.Height = e.CellBounds.Height / (double)2;
        e.PaintBackground(r2, true);
        e.PaintContent(r2);
        e.Handled = true;
    }
}