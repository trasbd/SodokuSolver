namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        sRow[] grid = new sRow[9];
        PencilRow[] PencilGrid = new PencilRow[9];
        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new sRow();
                PencilGrid[i] = new PencilRow();
            }

            dataGridView1.DataSource = grid;
            dataGridView2.DataSource = PencilGrid;

            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            dataGridView1.DefaultCellStyle.DataSourceNullValue = 0;

            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                col.Width = 44;
            }

        }



        void sizeDGV(DataGridView dgv)
        {

            DataGridViewElementStates states = DataGridViewElementStates.None;
            dgv.ScrollBars = ScrollBars.None;
            var totalHeight = dgv.Rows.GetRowsHeight(states) + 3;
            //totalHeight += (dgv.Rows.Count) * 4;  // specific correction for OP
            var totalWidth = dgv.Columns.GetColumnsWidth(states) + 3;
            dgv.ClientSize = new Size(totalWidth, totalHeight);


        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {


            if (e.RowIndex % 3 == 0)
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
            }

            if (e.ColumnIndex % 3 == 0)
            {
                e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
            }

            sizeDGV(sender as DataGridView);

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            int value = (int)e.Value;
            if (value == 0)
            {
                e.Value = string.Empty;
                e.FormattingApplied = true;
            }

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    cell.Value = 0;
                }
            }
        }

        public void updatePencil()
        {
            foreach (var row in grid)
            {

            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int eValue = (int)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            if ((eValue > 0))
            {



                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    (row.Cells[e.ColumnIndex].Value as PencilCell).removeMark(eValue);

                }
                foreach (DataGridViewCell cell in dataGridView2.Rows[e.RowIndex].Cells)
                {
                    (cell.Value as PencilCell).removeMark(eValue);
                }

           (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as PencilCell).removeMark(-1);
            }

            dataGridView2.Refresh();

        }
    }

    public class sRow
    {
        public int cell1 { get; set; }
        public int cell2 { get; set; }
        public int cell3 { get; set; }
        public int cell4 { get; set; }
        public int cell5 { get; set; }
        public int cell6 { get; set; }
        public int cell7 { get; set; }
        public int cell8 { get; set; }
        public int cell9 { get; set; }

    }



public class PencilRow
{
    public PencilCell cell1 { get; set; } = new PencilCell();
    public PencilCell cell2 { get; set; } = new PencilCell();
    public PencilCell cell3 { get; set; } = new PencilCell();
    public PencilCell cell4 { get; set; } = new PencilCell();
    public PencilCell cell5 { get; set; } = new PencilCell();
    public PencilCell cell6 { get; set; } = new PencilCell();
    public PencilCell cell7 { get; set; } = new PencilCell();
    public PencilCell cell8 { get; set; } = new PencilCell();
    public PencilCell cell9 { get; set; } = new PencilCell();

}

public class PencilCell
{
    public bool[] canBe = new bool[9];

    public PencilCell()
    {
        for (int i = 0; i < 9; i++)
        {
            canBe[i] = true;
        }
    }

    public PencilCell(int val)
    {
        for (int i = 0; i < 9; i++)
        {
            canBe[i] = false;
        }
        canBe[val - 1] = true;
    }

    public void removeMark (int val)
        {
            if(val == 0)
            {
                for(int i = 0;i < 9;i++)
                    canBe[i] = false;
            }
            else
            canBe[val-1] = false;
        }

    public override string ToString()
    {
        var displayString = "";
        for (int i = 1; i <= 9; i++)
        {
            if (canBe[i - 1])
            {
                displayString += i.ToString();
            }
            else
            {
                displayString += " ";
            }
            if (i == 3 || i == 6)
            {
                displayString += Environment.NewLine;
            }
            else if (i != 9)
            {
                displayString += " ";
            }
        }
        return displayString;
    }

}
}
