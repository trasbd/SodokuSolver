using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2;

namespace SodokuSolver
{
    public partial class Form2 : Form
    {
        PencilCell penCell;
        bool loading;
        public Form2(PencilCell pCell)
        {
            loading = true;
            InitializeComponent();
            penCell = pCell;




            checkBox1.Checked = penCell.canBe[0];
            checkBox2.Checked = penCell.canBe[1];
            checkBox3.Checked = penCell.canBe[2];
            checkBox4.Checked = penCell.canBe[3];
            checkBox5.Checked = penCell.canBe[4];
            checkBox6.Checked = penCell.canBe[5];
            checkBox7.Checked = penCell.canBe[6];
            checkBox8.Checked = penCell.canBe[7];
            checkBox9.Checked = penCell.canBe[8];
            loading = false;

        }

        private void markChange()
        {
            penCell.canBe[0] = checkBox1.Checked;
            penCell.canBe[1] = checkBox2.Checked;
            penCell.canBe[2] = checkBox3.Checked;
            penCell.canBe[3] = checkBox4.Checked;
            penCell.canBe[4] = checkBox5.Checked;
            penCell.canBe[5] = checkBox6.Checked;
            penCell.canBe[6] = checkBox7.Checked;
            penCell.canBe[7] = checkBox8.Checked;
            penCell.canBe[8] = checkBox9.Checked;

        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(!loading)
            markChange();
        }
    }
}
