using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Allocation
{
    public struct block
    {
        public int startingaddress;
        public int endingaddress;
        public int type;
        public int blocksize;
        public string name;
        public block(int st, int ed, int ty, int t = 0, string nm = "BUSY")
        {
            startingaddress = st;
            endingaddress = ed;
            type = ty;
            blocksize = t;
            name = nm;
        }
    }
    public struct startlist
    {
        public int space;
        public int startingaddress;
        public startlist(int sa, int sp)
        {
            space = sp;
            startingaddress = sa;
        }
    }
    public struct pro
    {
        public string name;
        public int size;
        public pro(string n, int s)
        {
            name = n;
            size = s;
        }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            AutoScroll = true;
            button1.Enabled = false;
            button2.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            listBox1.Enabled = false;
        }
        
        int memorySize = 0;
        
        
        List<startlist> listOfHoles=new List<startlist>();
        alogrthism memory = new alogrthism();

        /*-----------------------------------Memory Drawing Fn-----------------------------------------*/
        int X_Coordinate=350;
        int Y_Coordinate=32;
        int drawingWidth=200;
        int numOfDraws = 0;
        //initialize list of blocks
        List<block> listOfBlocks = new List<block>(0);
        CheckBox[] checkBoxArray;
        void initializeList()
        {
            listOfBlocks.Add(new block(0, 100, 1, 100, "p1"));
            listOfBlocks.Add(new block(100, 500, 1, 400, "p2"));

        }
        public void drawMemory(List<block> listOfBlocks)
        {
            //initializeList();
            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Location = new System.Drawing.Point(X_Coordinate+drawingWidth*numOfDraws, Y_Coordinate);
            tableLayoutPanel1.Name = "TableLayoutPanel1";
            tableLayoutPanel1.BackColor = Color.White;
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            Controls.Add(tableLayoutPanel1);
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.RowCount = listOfBlocks.Count();
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            Label[] blocksNameLabels = new Label[listOfBlocks.Count()];
            Label[] startingAddressLabels= new Label[listOfBlocks.Count()];
            Label[] blockSizeLabels= new Label[listOfBlocks.Count()];
            Panel[] panels = new Panel[listOfBlocks.Count()];
            //CheckBox[] checkBoxArray = new CheckBox[listOfBlocks.Count()];
            checkBoxArray = new CheckBox[listOfBlocks.Count()];
            for (int i = 0; i < listOfBlocks.Count(); i++)
            {
                checkBoxArray[i] = new CheckBox();
                
                checkBoxArray[i].CheckedChanged += (s, e) => 
                {
                    if(((CheckBox)s).Checked==true)
                    {
                        button5.Enabled = true;
                        for (int j = 0; j < listOfBlocks.Count(); j++)
                        {
                            if(!(checkBoxArray[j].Equals((CheckBox)s)))
                                checkBoxArray[j].Enabled = false;
                        }
                    }
                    else
                    {
                        button5.Enabled = false;
                        for (int j = 0; j < listOfBlocks.Count(); j++)
                        {
                            
                            checkBoxArray[j].Enabled = true;
                        }
                    }
                    
                    
                };
                startingAddressLabels[i] = new Label();
                blockSizeLabels[i] = new Label();
                checkBoxArray[i].Dock = DockStyle.Top;
                tableLayoutPanel1.Controls.Add(checkBoxArray[i], 0, i);
                blocksNameLabels[i] = new Label();
                blocksNameLabels[i].Text = listOfBlocks[i].name+"\n"+"size:\n"+listOfBlocks[i].blocksize;
                blocksNameLabels[i].Dock = DockStyle.Fill;
                blocksNameLabels[i].TextAlign = ContentAlignment.MiddleCenter;
                tableLayoutPanel1.Controls.Add(blocksNameLabels[i], 1, i);
                float sizePercentage=(listOfBlocks[i].blocksize/(float)memorySize)*100;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, sizePercentage));
                panels[i] = new Panel();
                panels[i].Dock = DockStyle.Fill;
                startingAddressLabels[i].Text = listOfBlocks[i].startingaddress.ToString();
                blockSizeLabels[i].Text = listOfBlocks[i].endingaddress.ToString();
                startingAddressLabels[i].Dock = DockStyle.Top;
                blockSizeLabels[i].Dock = DockStyle.Bottom;
                startingAddressLabels[i].AutoSize = true;
                blockSizeLabels[i].AutoSize = true;
                panels[i].Controls.Add(startingAddressLabels[i]);
                panels[i].Controls.Add(blockSizeLabels[i]);
                tableLayoutPanel1.Controls.Add(panels[i], 2, i);
                tableLayoutPanel1.MinimumSize = new Size(0, 500);
                tableLayoutPanel1.MaximumSize = new Size(0, 700);
                tableLayoutPanel1.AutoSize = true;
                tableLayoutPanel1.AutoScroll = true;
            }
        }
            //--------------------------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                memorySize = int.Parse(textBox1.Text);
                memory = new alogrthism(memorySize);
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox2.Select();
            }
            catch
            {
                MessageBox.Show("Enter Valid memory size","Error");
                textBox1.Select();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            
            textBox2.Select();
            try
            {
                int holeSize = int.Parse(textBox2.Text);
                int startingAddress = int.Parse(textBox3.Text);
                if(startingAddress+holeSize>memorySize)
                {
                    MessageBox.Show("the hole information is not valid with respect to the memory size", "Error");
                }
                else
                {
                    listOfHoles.Add(new startlist(holeSize, startingAddress));
                    button3.Enabled = true;
                    textBox2.Text = "";
                    textBox3.Text = "";
                }

                
            }
            catch
            {
                MessageBox.Show("Enter valid hole information","Error");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text!=""&&textBox3.Text!="")
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false; 
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "")
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //send memorySize and listofholes to rizk fns
            listOfBlocks= memory.Enter_the_init_list(listOfHoles);
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            listBox1.Enabled = true;
            //draw memory
            drawMemory(listOfBlocks);
            numOfDraws++;
            textBox4.Select();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "" && listBox1.SelectedIndex != -1)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != ""&&listBox1.SelectedIndex!=-1)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int processSize;
            try
            {
                processSize = int.Parse(textBox5.Text);
                if(processSize> memorySize)
                {
                    MessageBox.Show("The Process size is greater than the memory size", "Error");
                }
                else
                {
                    pro process = new pro(textBox4.Text, processSize);

                    if (numOfDraws > 0)
                    {
                        for (int i = 0; i < listOfBlocks.Count(); i++)
                        {
                            checkBoxArray[i].Enabled = false;
                        }
                    }
                    //send the process to rizk and algorithm 
                    switch (listBox1.SelectedIndex)
                    {
                        case 0:
                            if (memory.first_fit(process, ref listOfBlocks))
                            {
                                drawMemory(listOfBlocks);
                                numOfDraws++;
                            }
                            else
                            {
                                MessageBox.Show("Cannot allocate process try to deallocate memory first");
                                for (int i = 0; i < listOfBlocks.Count(); i++)
                                {
                                    checkBoxArray[i].Enabled = true;
                                }
                            }
                            break;
                        case 1:
                            if (memory.best_fit(process, ref listOfBlocks))
                            {
                                drawMemory(listOfBlocks);
                                numOfDraws++;
                            }
                            else
                            {
                                MessageBox.Show("Cannot allocate process try to deallocate memory first");
                                for (int i = 0; i < listOfBlocks.Count(); i++)
                                {
                                    checkBoxArray[i].Enabled = true;
                                }
                            }

                            break;
                        case 2:
                            if (memory.worst_fit(process, ref listOfBlocks))
                            {
                                drawMemory(listOfBlocks);
                                numOfDraws++;
                            }
                            else
                            {
                                MessageBox.Show("Cannot allocate process try to deallocate memory first");
                                for (int i = 0; i < listOfBlocks.Count(); i++)
                                {
                                    checkBoxArray[i].Enabled = true;
                                }
                            }

                            break;
                    }
                }
                
                
                
               
            }
            catch
            {
          
                MessageBox.Show("Enter valid process size", "Error");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int startingAddressOfDeallocatedBlock=0;
            for (int i = 0; i < listOfBlocks.Count(); i++)
            {
                if(checkBoxArray[i].Checked==true)
                {
                    startingAddressOfDeallocatedBlock= listOfBlocks[i].startingaddress;
                }
            }
            for (int i = 0; i < listOfBlocks.Count(); i++)
            {
                checkBoxArray[i].Enabled = false;
            }
            button5.Enabled = false;
            //send the starting address to rizk
            memory.remove_by_starting_address(startingAddressOfDeallocatedBlock, ref listOfBlocks);
            drawMemory(listOfBlocks);
            numOfDraws++;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button4.PerformClick();
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button4.PerformClick();
            }
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button4.PerformClick();
            }
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "" && listBox1.SelectedIndex != -1)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            memory.compact(ref listOfBlocks);
            drawMemory(listOfBlocks);
            numOfDraws++;
        }
    }
}
