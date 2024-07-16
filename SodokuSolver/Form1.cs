using System.Data;
using System.Text;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        sRow[] grid = new sRow[9];
        int oldVal;
        bool selectionLock = false;
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

            int value = Convert.ToInt32(e.Value);
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

                DataGridViewCell cell = dataGridView1.SelectedCells[dataGridView1.SelectedCells.Count-1];
                oldVal = Convert.ToInt32(cell.Value);
                cell.Value = 0;
                updatePencil(cell.RowIndex, cell.ColumnIndex);

            }
        }

        public void updatePencil(int rowEdit, int colEdit)
        {
            var again = true;
            while (again)
            {
                again = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {


                        foreach (DataGridViewCell pencell in dataGridView2.Rows[row.Index].Cells)
                        {
                            if (oldVal != 0 && colEdit == cell.ColumnIndex && rowEdit == cell.RowIndex)
                                (pencell.Value as PencilCell).removeMark(oldVal * -1);
                            if (Convert.ToInt32(cell.Value) != 0)
                                (pencell.Value as PencilCell).removeMark(Convert.ToInt32(cell.Value));

                        }

                        foreach (DataGridViewRow penrow in dataGridView2.Rows)
                        {
                            if (oldVal != 0 && colEdit == cell.ColumnIndex && rowEdit == cell.RowIndex)
                                (penrow.Cells[cell.ColumnIndex].Value as PencilCell).removeMark(oldVal * -1);
                            if (Convert.ToInt32(cell.Value) != 0)
                                (penrow.Cells[cell.ColumnIndex].Value as PencilCell).removeMark(Convert.ToInt32((cell.Value)));
                        }

                        for (int i = cell.ColumnIndex - (cell.ColumnIndex % 3); i <= cell.ColumnIndex + 2 - (cell.ColumnIndex % 3); i++)
                        {
                            for (int j = cell.RowIndex - (cell.RowIndex % 3); j <= cell.RowIndex + 2 - (cell.RowIndex % 3); j++)
                            {
                                if (oldVal != 0 && colEdit == cell.ColumnIndex && rowEdit == cell.RowIndex)
                                    (dataGridView2.Rows[j].Cells[i].Value as PencilCell).removeMark(oldVal * -1);
                                if (Convert.ToInt32(cell.Value) != 0)
                                    (dataGridView2.Rows[j].Cells[i].Value as PencilCell).removeMark((Convert.ToInt32(cell.Value)));
                            }
                        }

                        if (Convert.ToInt32(cell.Value) != 0)
                        {
                            (dataGridView2.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value as PencilCell).removeMark(0);
                        }
                        else if (Convert.ToInt32(cell.Value) == 0 && oldVal != 0 && colEdit == cell.ColumnIndex && rowEdit == cell.RowIndex)
                        {
                            dataGridView2.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = new PencilCell();
                            again = true;
                            oldVal = 0;
                        }


                    }
                }
            }
            dataGridView2.Refresh();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            updatePencil(e.RowIndex, e.ColumnIndex);
            /*
            int eValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

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

            */

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            oldVal = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!selectionLock)
            {
                selectionLock = true;
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    var selectedCell = dataGridView1.SelectedCells[0];
                    int selectedValue = Convert.ToInt32(dataGridView1.SelectedCells[0].Value);

                    if (selectedValue != 0)
                    {
                        //dataGridView1.MultiSelect = true;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {

                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (Convert.ToInt32(cell.Value) == selectedValue)
                                {
                                    cell.Selected = true;

                                }
                            }
                        }
                        //dataGridView1.MultiSelect = false;
                        selectedCell.Selected = true;
                    }

                }
                selectionLock = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckSingleNumberCell();
        }

        private void CheckSingleNumberCell()
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    PencilCell pencell = cell.Value as PencilCell;
                    if (pencell.canBe.Count(x => x) == 1)
                    {
                        oldVal = Convert.ToInt32(dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value);
                        dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = Array.IndexOf(pencell.canBe, true) + 1;
                        updatePencil(cell.RowIndex, cell.ColumnIndex);
                        dataGridView2.Refresh();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveGridCSV(".\\grid.csv");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadGridCSV(".\\grid.csv");
        }

        private void SaveGridCSV(string filename)
        {
            var sb = new StringBuilder();

            var headers = dataGridView1.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>();
                sb.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));
            }

            File.WriteAllText(".\\grid.csv", sb.ToString());
        }

        private void LoadGridCSV(string filePath)
        {
            //var filePath = ".\\grid.csv";
            var dt = new DataTable();
            // Creating the columns
            foreach (var headerLine in File.ReadLines(filePath).Take(1))
            {
                foreach (var headerItem in headerLine.Replace("\"", "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    dt.Columns.Add(headerItem.Trim());
                }
            }

            // Adding the rows
            foreach (var line in File.ReadLines(filePath).Skip(1))
            {
                dt.Rows.Add(line.Replace("\"", "").Split(','));
            }

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = new PencilCell();
                }
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            oldVal = 0;
            updatePencil(0, 0);
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

        public void removeMark(int val)
        {
            if (val == 0)
            {
                for (int i = 0; i < 9; i++)
                    canBe[i] = false;
            }
            else if (val < 0)
            {
                canBe[val * -1 - 1] = true;
            }
            else
                canBe[val - 1] = false;
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
